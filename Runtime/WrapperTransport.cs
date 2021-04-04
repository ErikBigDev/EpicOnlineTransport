using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using MLAPI;
using MLAPI.Transports;
using MLAPI.Transports.Tasks;

using UnityEngine;

using Epic.OnlineServices.P2P;

namespace EpicTransport
{
	public class WrapperTransport : NetworkTransport
	{
		public string ConnectAddress;


		private EosTransport eosTransport;

		private Queue<NetworkEventInfo> networkEvents = new Queue<NetworkEventInfo>();

		private List<int> connectedClients = new List<int>();
		private Dictionary<int, ulong> pings = new Dictionary<int, ulong>();

		public override ulong ServerClientId => int.MaxValue;

		public override bool IsSupported => Application.platform != RuntimePlatform.WebGLPlayer;

		public override void DisconnectLocalClient()
		{
			eosTransport.ClientDisconnect();
		}

		public override void DisconnectRemoteClient(ulong clientId)
		{
			Debug.Assert(clientId < int.MaxValue, "Casting error");

			eosTransport.ServerDisconnect((int)clientId);
		}

		public override ulong GetCurrentRtt(ulong clientId)
		{
			ulong ping;

			if (pings.ContainsKey((int)clientId))
				ping = pings[(int)clientId];
			else
				ping = 0;

			Debug.Log($"Client {clientId} has ping {ping}");

			return ping;
		}

		public override void Init()
		{
			if (eosTransport is null)
				eosTransport = gameObject.AddComponent<EosTransport>();

			Debug.Assert(IsSupported, "This platform is not supported by current transport");

			eosTransport.OnClientConnected += OnClientConnect;
			eosTransport.OnClientDisconnected += OnClientDisconnect;
			eosTransport.OnClientDataReceived += OnClientDataRecv;
			eosTransport.OnClientError += OnClientError;

			eosTransport.OnServerConnected += OnServerConnect;
			eosTransport.OnServerDisconnected += OnServerDisconnect;
			eosTransport.OnServerDataReceived += OnServerDataRecv;
			eosTransport.OnServerError += OnServerError;


			int count = MLAPI_CHANNELS.Length;

			eosTransport.Channels = new PacketReliability[count + 3];
			eosTransport.Channels[0] = PacketReliability.ReliableOrdered;
			eosTransport.Channels[1] = PacketReliability.UnreliableUnordered;
			eosTransport.Channels[2] = PacketReliability.ReliableOrdered;

			for (int i = 0; i < count; i++)
			{
				eosTransport.Channels[i + 3] = ConvertNetworkDelivery(MLAPI_CHANNELS[i].Delivery);
			}
		}

		public override NetworkEvent PollEvent(out ulong clientId, out NetworkChannel channel, out ArraySegment<byte> payload, out float receiveTime)
		{
			NetworkEventInfo info = networkEvents.Count > 0 ? networkEvents.Dequeue() : null;

			if (info != null)
			{
				if (info.error)
				{
					payload = default;
					channel = default;
					clientId = info.clientId;
					receiveTime = Time.realtimeSinceStartup;

					throw info.exception;
				}

				clientId = info.clientId;
				channel = (NetworkChannel)info.channel;
				payload = info.payload;
				receiveTime = info.receiveTime;

				return info.eventType;
			}

			payload = default;
			channel = default;
			clientId = default;
			receiveTime = Time.realtimeSinceStartup;
			return NetworkEvent.Nothing;
		}

		public override void Send(ulong clientId, ArraySegment<byte> data, NetworkChannel networkChannel)
		{
			InternalSend((int)clientId, data, (byte)(networkChannel + 3));
		}

		private void InternalSend(int clientId, ArraySegment<byte> data, byte networkChannel)
		{
			if (clientId == (int)ServerClientId)
				eosTransport.ClientSend(networkChannel, data);
			else
				eosTransport.ServerSend(clientId, networkChannel, data);
		}

		public override void Shutdown()
		{
			eosTransport.Shutdown();
		}

		public override SocketTasks StartClient()
		{
			eosTransport.ClientConnect(ConnectAddress);

			return SocketTask.Done.AsTasks();
		}

		public override SocketTasks StartServer()
		{
			eosTransport.ServerStart();

			return SocketTask.Done.AsTasks();
		}


		float lastPing = 0.0f;
		float pingInterval = 0.200f;
		
		private void Update()
		{
			if(!(eosTransport is null))
			{
				if (eosTransport.ClientActive()) eosTransport.ClientEarlyUpdate();
				if (eosTransport.ServerActive()) eosTransport.ServerEarlyUpdate();
			}

			if(Time.realtimeSinceStartup - lastPing > pingInterval && false)
			{
				lastPing = Time.realtimeSinceStartup;

				byte[] data = new byte[5];

				foreach (var item in connectedClients)
				{
					if (eosTransport.ClientActive() && eosTransport.ServerActive() && item == (int)ServerClientId)
						continue;

					byte[] time = BitConverter.GetBytes(Time.realtimeSinceStartup);

					Buffer.BlockCopy(time, 0, data, 1, 4);
					data[0] = 0;

					InternalSend(item, new ArraySegment<byte>(data), 3);
				}
			}
		}

		public PacketReliability ConvertNetworkDelivery(NetworkDelivery delivery)
		{
			switch (delivery)
			{
				case NetworkDelivery.Unreliable:
					return PacketReliability.UnreliableUnordered;
				case NetworkDelivery.UnreliableSequenced:
					return PacketReliability.ReliableOrdered;
				case NetworkDelivery.Reliable:
					return PacketReliability.ReliableUnordered;
				case NetworkDelivery.ReliableSequenced:
					return PacketReliability.ReliableOrdered;
				case NetworkDelivery.ReliableFragmentedSequenced:
					return PacketReliability.ReliableOrdered;
				default:
					return PacketReliability.ReliableOrdered;
			}
		}


		private void OnClientConnect()
		{
			connectedClients.Add((int)ServerClientId);

			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Connect;
			info.clientId = ServerClientId;
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}
		private void OnClientDisconnect()
		{
			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Disconnect;
			info.clientId = ServerClientId;
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}
		private void OnClientDataRecv(ArraySegment<byte> payload, int channel)
		{
			if(channel == 3 && false)
			{
				if(payload.Array[0 + payload.Offset] == 0)
				{
					payload.Array[0 + payload.Offset] = 0xff;
					InternalSend((int)ServerClientId, payload, (byte)channel);
				}
				else
				{
					float sendTime = BitConverter.ToSingle(payload.Array, payload.Offset + 1);
					pings[(int)ServerClientId] = (ulong)((Time.realtimeSinceStartup - sendTime) / 1000.0f);
				}

				return;
			}

			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Data;
			info.clientId = ServerClientId;
			info.channel = (byte)channel;
			info.payload = payload;
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}
		private void OnClientError(Exception e)
		{
			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Nothing;
			info.clientId = ServerClientId;
			info.error = true;
			info.exception = e;
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}

		private void OnServerConnect(int clientId)
		{
			connectedClients.Add(clientId);

			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Connect;
			info.clientId = (ulong)clientId;
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}
		private void OnServerDisconnect(int clientId)
		{
			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Disconnect;
			info.clientId = (ulong)clientId;
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}
		private void OnServerDataRecv(int clientId, ArraySegment<byte> payload, int channel)
		{
			if (channel == 3 && false)
			{
				if (payload.Array[0 + payload.Offset] == 0)
				{
					payload.Array[0 + payload.Offset] = 0xff;
					InternalSend(clientId, payload, (byte)channel);
				}
				else
				{
					float sendTime = BitConverter.ToSingle(payload.Array, payload.Offset + 1);
					pings[clientId] = (ulong)((Time.realtimeSinceStartup - sendTime) / 1000.0f);
				}

				return;
			}

			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Data;
			info.clientId = (ulong)clientId;
			info.channel = (byte)channel;
			info.payload = payload;
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}
		private void OnServerError(int clientId, Exception e)
		{
			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Nothing;
			info.clientId = (ulong)clientId;
			info.error = true;
			info.exception = e;
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}

	}


	internal class NetworkEventInfo
	{
		public NetworkEvent eventType = NetworkEvent.Nothing;
		public ulong clientId = 0;
		public byte channel = 0;
		public ArraySegment<byte> payload;
		public float receiveTime;

		public bool error = false;
		public Exception exception = null;
	}
}
