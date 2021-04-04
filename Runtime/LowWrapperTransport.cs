using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MLAPI;
using MLAPI.Transports;
using MLAPI.Transports.Tasks;

using Epic.OnlineServices.P2P;

using UnityEngine;

namespace EpicTransport
{
	public class LowWrapperTransport : NetworkTransport
	{
		public string RemoteAddress;

		internal PacketReliability[] Channels;

		public float timeout = 7.0f;

		SocketTask connectTask;

		private Queue<NetworkEventInfo> networkEvents = new Queue<NetworkEventInfo>();

		private List<ulong> connectedClients = new List<ulong>();
		private Dictionary<ulong, ulong> pings = new Dictionary<ulong, ulong>();

		private Client client;
		private Server server;

		//public Action<byte[], int> OnClientDataReceived;
		//public Action OnClientConnected;
		//public Action OnClientDisconnected;
		//
		//public Action<int> OnServerConnected;
		//public Action<int, byte[], int> OnServerDataReceived;
		//public Action<int> OnServerDisconnected;
		//public Action<int, Exception> OnServerError;

		public Action<ulong> OnCommonConnected;
		public Action<ulong, byte[], int> OnCommonDataReceived;
		public Action<ulong> OnCommonDisconnected;
		public Action<ulong, Exception> OnCommonErrored;


		public override ulong ServerClientId => 0;

		public override void DisconnectLocalClient()
		{
			Debug.Log("Disconnecting client");
			if (client != null && client.Connected)
				client?.Disconnect();
		}

		public override void DisconnectRemoteClient(ulong clientId)
		{
			Debug.Log($"Disconnecting client {clientId}");
			server?.Disconnect(clientId);
		}

		public override ulong GetCurrentRtt(ulong clientId)
		{
			ulong ping;

			if (pings.ContainsKey(clientId))
				ping = pings[clientId];
			else
				ping = 0;

			Debug.Log($"Client {clientId} has ping {ping}");

			return ping;
		}

		public override void Init()
		{
			Debug.Assert(IsSupported, "This platform is not supported by current transport");

			//OnClientConnected += OnClientConnect;
			//OnClientDisconnected += OnClientDisconnect;
			//OnClientDataReceived += OnClientDataRecv;
			//OnClientError += OnClientException;

			///OnServerConnected += OnServerConnect;
			///OnServerDisconnected += OnServerDisconnect;
			///OnServerDataReceived += OnServerDataRecv;
			///OnServerError += OnServerException;

			OnCommonConnected += OnCommonConnect;
			OnCommonDisconnected += OnCommonDisconnect;
			OnCommonDataReceived += OnCommonDataRecv;
			OnCommonErrored += OnCommonException;

			int count = MLAPI_CHANNELS.Length;

			Channels = new PacketReliability[count + 3];
			Channels[0] = PacketReliability.ReliableOrdered;
			Channels[1] = PacketReliability.UnreliableUnordered;
			Channels[2] = PacketReliability.ReliableOrdered;

			for (int i = 0; i < count; i++)
			{
				Channels[i + 3] = ConvertNetworkDelivery(MLAPI_CHANNELS[i].Delivery);
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

				if(client != null && client.isConnecting && clientId == ServerClientId &&
					(info.eventType == NetworkEvent.Connect || info.eventType == NetworkEvent.Disconnect))
				{
					if (info.eventType == NetworkEvent.Connect)
						connectTask.Success = true;
					if (info.eventType == NetworkEvent.Disconnect)
						connectTask.Success = false;

					connectTask.IsDone = true;
					connectTask = null;
				}

				return info.eventType;
			}

			payload = default;
			channel = default;
			clientId = default;
			receiveTime = Time.realtimeSinceStartup;
			return NetworkEvent.Nothing;
		}

		public override void Send(ulong clientId, ArraySegment<byte> data, NetworkChannel channel)
		{
			SendInternal(clientId, data, (byte)(channel + 3));
		}

		private void SendInternal(ulong clientId, ArraySegment<byte> data, byte channel)
		{
			byte[] arrayData = new byte[data.Count];
			Array.Copy(data.Array, data.Offset, arrayData, 0, data.Count);

			if (clientId == ServerClientId)
				client.Send(arrayData, channel);
			else
				server.SendAll(clientId, arrayData, channel);
		}

		public override void Shutdown()
		{
			Debug.Log("Transport shutting down.");

			server?.Shutdown();
			client?.Disconnect();

			server = null;
			client = null;
			//activeNode = null;
		}

		public override SocketTasks StartClient()
		{
			Debug.Log("startClient");
			
			if (!EOSSDKComponent.Initialized)
			{
				Debug.LogError("EOS not initialized. Client could not be started.");
				OnCommonDisconnected.Invoke(ServerClientId);
				return SocketTask.Fault.AsTasks();
			}

			//StartCoroutine("FetchEpicAccountId");

			if (server != null)
			{
				Debug.LogError("Transport already running as server!");
				//return SocketTask.Fault.AsTasks();
			}

			if (client == null || (client != null && client.Error))
			{
				Debug.Log($"Starting client, target address {RemoteAddress}.");

				client = Client.CreateClient(this, RemoteAddress);

				client.Connect(RemoteAddress);
				client.isConnecting = true;

				connectTask = SocketTask.Working;
				return connectTask.AsTasks();
			}
			else
			{
				Debug.LogError("Client already running!");
			}

			return SocketTask.Done.AsTasks();
		}

		public override SocketTasks StartServer()
		{
			Debug.Log("startServer");

			if (!EOSSDKComponent.Initialized)
			{
				Debug.LogError("EOS not initialized. Server could not be started.");
				return SocketTask.Fault.AsTasks();
			}
			if (client != null)
			{
				Debug.LogError("Transport already running as client!");
				//return;
			}

			if(server == null)
			{
				server = Server.CreateServer(this, 100);
			}
			else
				Debug.LogError("Server already started!");

			return SocketTask.Done.AsTasks();
		}


		private void Update()
		{
			if(EOSSDKComponent.Initialized)
				EOSSDKComponent.Tick();

			if (client != null && client.isConnecting)
				client.ReceiveData();

			if (server != null)
				server.ReceiveData();
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

		/*
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
		private void OnClientDataRecv(byte[] payload, int channel)
		{
			if (channel == 3 && false)
			{
				if (payload[0] == 0)
				{
					payload[0] = 0xff;
					SendInternal(ServerClientId, new ArraySegment<byte>(payload), (byte)channel);
				}
				else
				{
					float sendTime = BitConverter.ToSingle(payload, 1);
					//pings[(int)ServerClientId] = (ulong)((Time.realtimeSinceStartup - sendTime) / 1000.0f);
				}

				return;
			}

			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Data;
			info.clientId = ServerClientId;
			info.channel = (byte)channel;
			info.payload = new ArraySegment<byte>(payload);
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}
		private void OnClientException(Exception e)
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
		private void OnServerDataRecv(int clientId, byte[] payload, int channel)
		{
			if (channel == 3 && false)
			{
				if (payload[0] == 0)
				{
					payload[0] = 0xff;
					SendInternal((ulong)clientId, new ArraySegment<byte>(payload), (byte)channel);
				}
				else
				{
					float sendTime = BitConverter.ToSingle(payload, 1);
					//pings[clientId] = (ulong)((Time.realtimeSinceStartup - sendTime) / 1000.0f);
				}

				return;
			}

			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Data;
			info.clientId = (ulong)clientId;
			info.channel = (byte)channel;
			info.payload = new ArraySegment<byte>(payload);
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}
		private void OnServerException(int clientId, Exception e)
		{
			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Nothing;
			info.clientId = (ulong)clientId;
			info.error = true;
			info.exception = e;
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}
		*/

		private void OnCommonConnect(ulong clientId)
		{
			connectedClients.Add(clientId);

			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Connect;
			info.clientId = clientId;
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}
		private void OnCommonDisconnect(ulong clientId)
		{
			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Disconnect;
			info.clientId = clientId;
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}
		private void OnCommonDataRecv(ulong clientId, byte[] payload, int channel)
		{
			if (channel == 3 && false)
			{
				if (payload[0] == 0)
				{
					payload[0] = 0xff;
					SendInternal(clientId, new ArraySegment<byte>(payload), (byte)channel);
				}
				else
				{
					float sendTime = BitConverter.ToSingle(payload, 1);
					//pings[clientId] = (ulong)((Time.realtimeSinceStartup - sendTime) / 1000.0f);
				}

				return;
			}

			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Data;
			info.clientId = clientId;
			info.channel = (byte)channel;
			info.payload = new ArraySegment<byte>(payload);
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}
		private void OnCommonException(ulong clientId, Exception e)
		{
			NetworkEventInfo info = new NetworkEventInfo();
			info.eventType = NetworkEvent.Nothing;
			info.clientId = clientId;
			info.error = true;
			info.exception = e;
			info.receiveTime = Time.realtimeSinceStartup;

			networkEvents.Enqueue(info);
		}
	}

	//internal class NetworkEventInfo
	//{
	//	public NetworkEvent eventType = NetworkEvent.Nothing;
	//	public ulong clientId = 0;
	//	public byte channel = 0;
	//	public ArraySegment<byte> payload;
	//	public float receiveTime;
	//
	//	public bool error = false;
	//	public Exception exception = null;
	//}
}
