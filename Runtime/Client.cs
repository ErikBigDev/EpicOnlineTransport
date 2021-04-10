using Epic.OnlineServices;
using Epic.OnlineServices.P2P;
using Mirror;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace EpicTransport {
	public class Client : Common {

		public SocketId socketId;
		public ProductUserId serverId;

		public bool Connected { get; private set; }
		public bool Error { get; private set; }

		//public event Action<byte[], int> OnReceivedData;
		//public event Action OnConnected;
		//public event Action OnDisconnected;

		private TimeSpan ConnectionTimeout;

		private ulong currentPing;

		public bool isConnecting = false;
		public string hostAddress = "";
		private ProductUserId hostProductId = null;
		private TaskCompletionSource<Task> connectedComplete;
		private CancellationTokenSource cancelToken;

		private Client(LowWrapperTransport transport) : base(transport) {
			ConnectionTimeout = TimeSpan.FromSeconds(Math.Max(1, transport.timeout));
		}

		public static Client CreateClient(LowWrapperTransport transport, string host) {
			Client c = new Client(transport);

			c.hostAddress = host;
			c.socketId = new SocketId() { SocketName = RandomString.Generate(20) };

			//c.OnConnected += () => transport.OnClientConnected.Invoke();
			//c.OnDisconnected += () => transport.OnClientDisconnected.Invoke();
			//c.OnReceivedData += (data, channel) => transport.OnClientDataReceived.Invoke(data, channel);
			//c.OnReceivedData += (data, channel) => transport.OnClientDataReceived.Invoke(new ArraySegment<byte>(data), channel);

			return c;
		}

		public async void Connect(string host) {
			Debug.Log("Connecting");
			cancelToken = new CancellationTokenSource();

			try {
				hostProductId = ProductUserId.FromString(host);
				serverId = hostProductId;
				connectedComplete = new TaskCompletionSource<Task>();

				transport.OnCommonConnected += SetConnectedComplete;
				
				SendInternal(hostProductId, socketId, InternalMessages.CONNECT);

				Task connectedCompleteTask = connectedComplete.Task;

				if (await Task.WhenAny(connectedCompleteTask, Task.Delay(ConnectionTimeout/*, cancelToken.Token*/)) != connectedCompleteTask) {
					Debug.LogError($"Connection to {host} timed out.");
					transport.OnCommonConnected -= SetConnectedComplete;
					OnConnectionFailed(hostProductId);
				}

				transport.OnCommonConnected -= SetConnectedComplete;
			} catch (FormatException) {
				Debug.LogError($"Connection string was not in the right format. Did you enter a ProductId?");
				Error = true;
				OnConnectionFailed(hostProductId);
			} catch (Exception ex) {
				Debug.LogError(ex.Message);
				Error = true;
				OnConnectionFailed(hostProductId);
			} finally {
				if (Error) {
					OnConnectionFailed(null);
				}
			}

		}

		public void Disconnect() {
			if (serverId != null) {
				CloseP2PSessionWithUser(serverId, socketId);

				serverId = null;
			} else {
				return;
			}

			SendInternal(hostProductId, socketId, InternalMessages.DISCONNECT);

			Dispose();
			cancelToken?.Cancel();

			WaitForClose(hostProductId, socketId);
		}

		private void SetConnectedComplete(ulong dummy) => connectedComplete.SetResult(connectedComplete.Task);

		protected override void OnReceiveData(byte[] data, ProductUserId clientUserId, int channel) {
			if (ignoreAllMessages) {
				return;
			}

			if (clientUserId != hostProductId) {
				Debug.LogError("Received a message from an unknown");
				return;
			}

			transport.OnCommonDataReceived.Invoke(transport.ServerClientId, data, channel);
			//OnReceivedData.Invoke(data, channel);
		}

		protected override void OnNewConnection(OnIncomingConnectionRequestInfo result) {
			if (ignoreAllMessages) {
				return;
			}

			if (deadSockets.Contains(result.SocketId.SocketName)) {
				Debug.LogError("Received incoming connection request from dead socket");
				return;
			}

			if (hostProductId == result.RemoteUserId) {
				EOSSDKComponent.GetP2PInterface().AcceptConnection(
					new AcceptConnectionOptions() {
						LocalUserId = EOSSDKComponent.LocalUserProductId,
						RemoteUserId = result.RemoteUserId,
						SocketId = result.SocketId
					});
			} else {
				Debug.LogError("P2P Acceptance Request from unknown host ID.");
			}
		}

		protected override void OnReceiveInternalData(InternalMessages type, ProductUserId clientUserId, SocketId socketId, byte[] payload = null) {
			if (ignoreAllMessages) {
				return;
			}

			switch (type) {
				case InternalMessages.ACCEPT_CONNECT:
					Connected = true;
					transport.OnCommonConnected.Invoke(transport.ServerClientId);
					//OnConnected.Invoke();
					Debug.Log("Connection established.");
					break;
				case InternalMessages.DISCONNECT:
					Connected = false;
					Debug.Log("Disconnected.");

					transport.OnCommonDisconnected.Invoke(transport.ServerClientId);
					//OnDisconnected.Invoke();
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
						currentPing = (ulong)((Time.realtimeSinceStartup - sendTime) / 1000.0f);
					}
					break;
				default:
					Debug.Log("Received unknown message type");
					break;
			}
		}

		public void Send(byte[] data, int channelId) => Send(hostProductId, socketId, data, (byte) channelId);

		protected override void OnConnectionFailed(ProductUserId remoteId) => transport.OnCommonDisconnected.Invoke(transport.ServerClientId);
		public void EosNotInitialized() => transport.OnCommonDisconnected.Invoke(transport.ServerClientId);

		public override ulong GetPing(ulong clientId = ulong.MaxValue)
		{
			if (clientId == ulong.MaxValue || clientId == transport.ServerClientId)
				return currentPing;
			else
				throw new Exception("Something went wrong");
		}
		public override void SendPing()
		{
			byte[] data = new byte[6];
			data[0] = (byte)InternalMessages.PING;
			data[1] = 0;

			byte[] time = BitConverter.GetBytes(Time.realtimeSinceStartup);

			Buffer.BlockCopy(time, 0, data, 2, 4);

			SendInternal(hostProductId, socketId, data);
		}
	}
}