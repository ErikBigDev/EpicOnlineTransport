using Epic.OnlineServices;
using Epic.OnlineServices.P2P;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EpicTransport {
	public class Server : Common {
		//private event Action<int> OnConnected;
		//private event Action<int, byte[], int> OnReceivedData;
		//private event Action<int> OnDisconnected;
		//private event Action<int, Exception> OnReceivedError;

		private Dictionary<ulong, ulong> pings = new Dictionary<ulong, ulong>();

		private BidirectionalDictionary<ProductUserId, ulong> epicToMirrorIds;
		private Dictionary<ProductUserId, SocketId> epicToSocketIds;
		private int maxConnections;
		private ulong nextConnectionID;

		public static Server CreateServer(LowWrapperTransport transport, int maxConnections) {
			Server s = new Server(transport, maxConnections);

			//s.OnConnected += (id) => transport.OnServerConnected.Invoke(id);
			//s.OnDisconnected += (id) => transport.OnServerDisconnected.Invoke(id);
			//s.OnReceivedData += (id, data, channel) => transport.OnServerDataReceived.Invoke(id, data, channel);
			//s.OnReceivedData += (id, data, channel) => transport.OnServerDataReceived.Invoke(id, new ArraySegment<byte>(data), channel);
			//s.OnReceivedError += (id, exception) => transport.OnServerError.Invoke(id, exception);

			if (!EOSSDKComponent.Initialized) {
				Debug.LogError("EOS not initialized.");
			}

			return s;
		}

		private Server(LowWrapperTransport transport, int maxConnections) : base(transport) {
			this.maxConnections = maxConnections;
			epicToMirrorIds = new BidirectionalDictionary<ProductUserId, ulong>();
			epicToSocketIds = new Dictionary<ProductUserId, SocketId>();
			nextConnectionID = 1;
		}

		protected override void OnNewConnection(OnIncomingConnectionRequestInfo result) {
			if (ignoreAllMessages) {
				return;
			}

			if (deadSockets.Contains(result.SocketId.SocketName)) {
				Debug.LogError("Received incoming connection request from dead socket");
				return;
			}

			EOSSDKComponent.GetP2PInterface().AcceptConnection(
				new AcceptConnectionOptions() {
				LocalUserId = EOSSDKComponent.LocalUserProductId,
				RemoteUserId = result.RemoteUserId,
				SocketId = result.SocketId
				});
		}

		protected override void OnReceiveInternalData(InternalMessages type, ProductUserId clientUserId, SocketId socketId, byte[] payload = null) {
			if (ignoreAllMessages) {
				return;
			}

			switch (type) {
				case InternalMessages.CONNECT:
					if (epicToMirrorIds.Count >= maxConnections) {
						Debug.LogError("Reached max connections");
						//CloseP2PSessionWithUser(clientUserId, socketId);
						SendInternal(clientUserId, socketId, InternalMessages.DISCONNECT);
						return;
					}

					SendInternal(clientUserId, socketId, InternalMessages.ACCEPT_CONNECT);

					ulong connectionId = nextConnectionID++;
					epicToMirrorIds.Add(clientUserId, connectionId);
					epicToSocketIds.Add(clientUserId, socketId);
					transport.OnCommonConnected.Invoke(connectionId);
					//OnConnected.Invoke(connectionId);

					string clientUserIdString;
					clientUserId.ToString(out clientUserIdString);
					Debug.Log($"Client with Product User ID {clientUserIdString} connected. Assigning connection id {connectionId}");
					break;
				case InternalMessages.DISCONNECT:
					if (epicToMirrorIds.TryGetValue(clientUserId, out ulong connId)) {
						transport.OnCommonDisconnected.Invoke(connId);
						//OnDisconnected.Invoke(connId);
						//CloseP2PSessionWithUser(clientUserId, socketId);
						epicToMirrorIds.Remove(clientUserId);
						epicToSocketIds.Remove(clientUserId);
						clientUserId.ToString(out string clientUserIdstr);
						Debug.Log($"Client with Product User ID {clientUserIdstr} disconnected.");
					} else {
						transport.OnCommonErrored.Invoke(0, new Exception("ERROR Unknown Product User ID"));
					}

					break;
				case InternalMessages.PING:
					if (payload[1] == 0)
					{
						payload[1] = 0xff;
						SendInternal(clientUserId, socketId, payload);
					}
					else
					{
						float sendTime = BitConverter.ToSingle(payload, 2);
						pings[epicToMirrorIds[clientUserId]] = (ulong)((Time.realtimeSinceStartup - sendTime) / 1000.0f);
					}
					break;
				default:
					Debug.Log("Received unknown message type");
					break;
			}
		}

		protected override void OnReceiveData(byte[] data, ProductUserId clientUserId, int channel) {
			if (ignoreAllMessages) {
				return;
			}

			if (epicToMirrorIds.TryGetValue(clientUserId, out ulong connectionId)) {
				transport.OnCommonDataReceived.Invoke(connectionId, data, channel);
				//OnReceivedData.Invoke(connectionId, data, channel);
			} else {
				SocketId socketId;
				epicToSocketIds.TryGetValue(clientUserId, out socketId);
				CloseP2PSessionWithUser(clientUserId, socketId);

				string productId;
				clientUserId.ToString(out productId);

				Debug.LogError("Data received from epic client thats not known " + productId);
				transport.OnCommonErrored.Invoke(0, new Exception("ERROR Unknown product ID"));
			}
		}

		public bool Disconnect(ulong connectionId) {
			if (epicToMirrorIds.TryGetValue(connectionId, out ProductUserId userId)) {
				SocketId socketId;
				epicToSocketIds.TryGetValue(userId, out socketId);
				SendInternal(userId, socketId, InternalMessages.DISCONNECT);
				epicToMirrorIds.Remove(userId);
				epicToSocketIds.Remove(userId);
				return true;
			} else {
				Debug.LogWarning("Trying to disconnect unknown connection id: " + connectionId);
				return false;
			}
		}

		public void Shutdown() {
			foreach (KeyValuePair<ProductUserId, ulong> client in epicToMirrorIds) {
				Disconnect(client.Value);
				SocketId socketId;
				epicToSocketIds.TryGetValue(client.Key, out socketId);
				WaitForClose(client.Key, socketId);
			}

			ignoreAllMessages = true;
			ReceiveData();

			Dispose();
		}

		public void SendAll(ulong connectionId, byte[] data, int channelId) {
			if (epicToMirrorIds.TryGetValue(connectionId, out ProductUserId userId)) {
				SocketId socketId;
				epicToSocketIds.TryGetValue(userId, out socketId);
				Send(userId, socketId, data, (byte)channelId);
			} else {
				Debug.LogError("Trying to send on unknown connection: " + connectionId);
				transport.OnCommonErrored.Invoke(connectionId, new Exception("ERROR Unknown Connection"));
			}

		}

		public string ServerGetClientAddress(ulong connectionId) {
			if (epicToMirrorIds.TryGetValue(connectionId, out ProductUserId userId)) {
				string userIdString;
				userId.ToString(out userIdString);
				return userIdString;
			} else {
				Debug.LogError("Trying to get info on unknown connection: " + connectionId);
				transport.OnCommonErrored.Invoke(connectionId, new Exception("ERROR Unknown Connection"));
				return string.Empty;
			}
		}

		protected override void OnConnectionFailed(ProductUserId remoteId) {
			if (ignoreAllMessages) {
				return;
			}

			ulong connectionId = epicToMirrorIds.TryGetValue(remoteId, out ulong connId) ? connId : nextConnectionID++;
			transport.OnCommonDisconnected.Invoke(connectionId);
			//OnDisconnected.Invoke(connectionId);

			Debug.LogError("Connection Failed, removing user");
			epicToMirrorIds.Remove(remoteId);
			epicToSocketIds.Remove(remoteId);
		}

		public override ulong GetPing(ulong clientId = ulong.MaxValue)
		{
			if (pings.ContainsKey(clientId))
				return pings[clientId];
			else
				throw new Exception("Could not find the client");
		}
		public override void SendPing()
		{
			byte[] data = new byte[6];
			data[0] = (byte)InternalMessages.PING;
			data[1] = 0;

			foreach (KeyValuePair<ProductUserId, SocketId> item in epicToSocketIds)
			{
				if (deadSockets.Contains(item.Value.SocketName))
					continue;

				byte[] time = BitConverter.GetBytes(Time.realtimeSinceStartup);

				Buffer.BlockCopy(time, 0, data, 2, 4);

				SendInternal(item.Key, item.Value, data);
			}
		}
	}
}