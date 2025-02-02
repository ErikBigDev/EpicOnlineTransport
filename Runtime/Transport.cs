﻿using System;
using UnityEngine;

//https://github.com/vis2k/Mirror/blob/52561c2d7f12ab2b87ac2303b844a306aa89f3de/Assets/Mirror/Runtime/Transport/Transport.cs
namespace Mirror
{
	//https://github.com/vis2k/Mirror/blob/52561c2d7f12ab2b87ac2303b844a306aa89f3de/Assets/Mirror/Runtime/Utils.cs#L39-L48
	public static class Channels
	{
		public const int Reliable = 0;      // ordered
		public const int Unreliable = 1;    // unordered

		[Obsolete("Use Channels.Reliable instead")]
		public const int DefaultReliable = Reliable;
		[Obsolete("Use Channels.Unreliable instead")]
		public const int DefaultUnreliable = Unreliable;
	}


	/// <summary>
	/// Abstract transport layer component
	/// </summary>
	/// <remarks>
	/// <h2>
	///   Transport Rules
	/// </h2>
	/// <list type="bullet">
	///   <listheader><description>
	///     All transports should follow these rules so that they work correctly with mirror
	///   </description></listheader>
	///   <item><description>
	///     When Monobehaviour is disabled the Transport should not invoke callbacks
	///   </description></item>
	///   <item><description>
	///     Callbacks should be invoked on main thread. It is best to do this from LateUpdate
	///   </description></item>
	///   <item><description>
	///     Callbacks can be invoked after <see cref="ServerStop"/> or <see cref="ClientDisconnect"/> as been called
	///   </description></item>
	///   <item><description>
	///     <see cref="ServerStop"/> or <see cref="ClientDisconnect"/> can be called by mirror multiple times
	///   </description></item>
	///   <item><description>
	///     <see cref="Available"/> should check the platform and 32 vs 64 bit if the transport only works on some of them
	///   </description></item>
	///   <item><description>
	///     <see cref="GetMaxPacketSize"/> should return size even if transport is not running
	///   </description></item>
	///   <item><description>
	///     Default channel should be reliable <see cref="Channels.Reliable"/>
	///   </description></item>
	/// </list>
	/// </remarks>
	public abstract class Transport : MonoBehaviour
	{
		/// <summary>
		/// The current transport used by Mirror.
		/// </summary>
		public static Transport activeTransport;

		/// <summary>
		/// Is this transport available in the current platform?
		/// <para>Some transports might only be available in mobile</para>
		/// <para>Many will not work in webgl</para>
		/// <para>Example usage: return Application.platform == RuntimePlatform.WebGLPlayer</para>
		/// </summary>
		/// <returns>True if this transport works in the current platform</returns>
		public abstract bool Available();

		#region Client
		/// <summary>
		/// Notify subscribers when this client establish a successful connection to the server
		/// <para>callback()</para>
		/// </summary>
		public Action OnClientConnected = () => Debug.LogWarning("OnClientConnected called with no handler");

		/// <summary>
		/// Notify subscribers when this client receive data from the server
		/// <para>callback(ArraySegment&lt;byte&gt; data, int channel)</para>
		/// </summary>
		public Action<ArraySegment<byte>, int> OnClientDataReceived = (data, channel) => Debug.LogWarning("OnClientDataReceived called with no handler");

		/// <summary>
		/// Notify subscribers when this client encounters an error communicating with the server
		/// <para>callback(Exception e)</para>
		/// </summary>
		public Action<Exception> OnClientError = (error) => Debug.LogWarning("OnClientError called with no handler");

		/// <summary>
		/// Notify subscribers when this client disconnects from the server
		/// <para>callback()</para>
		/// </summary>
		public Action OnClientDisconnected = () => Debug.LogWarning("OnClientDisconnected called with no handler");

		/// <summary>
		/// Determines if we are currently connected to the server
		/// </summary>
		/// <returns>True if a connection has been established to the server</returns>
		public abstract bool ClientConnected();

		/// <summary>
		/// Establish a connection to a server
		/// </summary>
		/// <param name="address">The IP address or FQDN of the server we are trying to connect to</param>
		public abstract void ClientConnect(string address);

		/// <summary>
		/// Establish a connection to a server
		/// </summary>
		/// <param name="uri">The address of the server we are trying to connect to</param>
		public virtual void ClientConnect(Uri uri)
		{
			// By default, to keep backwards compatibility, just connect to the host
			// in the uri
			ClientConnect(uri.Host);
		}

		/// <summary>
		/// Send data to the server
		/// </summary>
		/// <param name="channelId">The channel to use.  0 is the default channel,
		/// but some transports might want to provide unreliable, encrypted, compressed, or any other feature
		/// as new channels</param>
		/// <param name="segment">The data to send to the server. Will be recycled after returning, so either use it directly or copy it internally. This allows for allocation-free sends!</param>
		public abstract void ClientSend(int channelId, ArraySegment<byte> segment);

		/// <summary>
		/// Disconnect this client from the server
		/// </summary>
		public abstract void ClientDisconnect();

		#endregion

		#region Server


		/// <summary>
		/// Retrieves the address of this server.
		/// Useful for network discovery
		/// </summary>
		/// <returns>the url at which this server can be reached</returns>
		public abstract Uri ServerUri();

		/// <summary>
		/// Notify subscribers when a client connects to this server
		/// <para>callback(int connId)</para>
		/// </summary>
		public Action<int> OnServerConnected = (connId) => Debug.LogWarning("OnServerConnected called with no handler");

		/// <summary>
		/// Notify subscribers when this server receives data from the client
		/// <para>callback(int connId, ArraySegment&lt;byte&gt; data, int channel)</para>
		/// </summary>
		public Action<int, ArraySegment<byte>, int> OnServerDataReceived = (connId, data, channel) => Debug.LogWarning("OnServerDataReceived called with no handler");

		/// <summary>
		/// Notify subscribers when this server has some problem communicating with the client
		/// <para>callback(int connId, Exception e)</para>
		/// </summary>
		public Action<int, Exception> OnServerError = (connId, error) => Debug.LogWarning("OnServerError called with no handler");

		/// <summary>
		/// Notify subscribers when a client disconnects from this server
		/// <para>callback(int connId)</para>
		/// </summary>
		public Action<int> OnServerDisconnected = (connId) => Debug.LogWarning("OnServerDisconnected called with no handler");

		/// <summary>
		/// Determines if the server is up and running
		/// </summary>
		/// <returns>true if the transport is ready for connections from clients</returns>
		public abstract bool ServerActive();

		/// <summary>
		/// Start listening for clients
		/// </summary>
		public abstract void ServerStart();

		/// <summary>
		/// Send data to a client.
		/// </summary>
		/// <param name="connectionId">The client connection id to send the data to</param>
		/// <param name="channelId">The channel to be used.  Transports can use channels to implement
		/// other features such as unreliable, encryption, compression, etc...</param>
		/// <param name="data"></param>
		public abstract void ServerSend(int connectionId, int channelId, ArraySegment<byte> segment);

		/// <summary>
		/// Disconnect a client from this server.  Useful to kick people out.
		/// </summary>
		/// <param name="connectionId">the id of the client to disconnect</param>
		/// <returns>true if the client was kicked</returns>
		public abstract bool ServerDisconnect(int connectionId);

		/// <summary>
		/// Get the client address
		/// </summary>
		/// <param name="connectionId">id of the client</param>
		/// <returns>address of the client</returns>
		public abstract string ServerGetClientAddress(int connectionId);

		/// <summary>
		/// Stop listening for clients and disconnect all existing clients
		/// </summary>
		public abstract void ServerStop();

		#endregion

		/// <summary>
		/// The maximum packet size for a given channel.  Unreliable transports
		/// usually can only deliver small packets. Reliable fragmented channels
		/// can usually deliver large ones.
		///
		/// GetMaxPacketSize needs to return a value at all times. Even if the
		/// Transport isn't running, or isn't Available(). This is because
		/// Fallback and Multiplex transports need to find the smallest possible
		/// packet size at runtime.
		/// </summary>
		/// <param name="channelId">channel id</param>
		/// <returns>the size in bytes that can be sent via the provided channel</returns>
		public abstract int GetMaxPacketSize(int channelId = Channels.Reliable);
		/// <summary>
		/// The maximum batch(!) size for a given channel.
		/// Uses GetMaxPacketSize by default.
		/// Some transports like kcp support large max packet sizes which should
		/// not be used for batching all the time because they end up being too
		/// slow (head of line blocking etc.).
		/// </summary>
		/// <param name="channelId">channel id</param>
		/// <returns>the size in bytes that should be batched via the provided channel</returns>
		public virtual int GetMaxBatchSize(int channelId) =>
			GetMaxPacketSize(channelId);

		// block Update & LateUpdate to show warnings if Transports still use
		// them instead of using
		//   Client/ServerEarlyUpdate: to process incoming messages
		//   Client/ServerLateUpdate: to process outgoing messages
		// those are called by NetworkClient/Server at the right time.
		//
		// allows transports to implement the proper network update order of:
		//      process_incoming()
		//      update_world()
		//      process_outgoing()
		//
		// => see NetworkLoop.cs for detailed explanations!
#pragma warning disable UNT0001 // Empty Unity message
		public void Update() { }
		public void LateUpdate() { }
#pragma warning restore UNT0001 // Empty Unity message

		/// <summary>
		/// NetworkLoop NetworkEarly/LateUpdate were added for a proper network
		/// update order. the goal is to:
		///    process_incoming()
		///    update_world()
		///    process_outgoing()
		/// in order to avoid unnecessary latency and data races.
		/// </summary>
		// => split into client and server parts so that we can cleanly call
		//    them from NetworkClient/Server
		// => VIRTUAL for now so we can take our time to convert transports
		//    without breaking anything.
		public virtual void ClientEarlyUpdate() { }
		public virtual void ServerEarlyUpdate() { }
		public virtual void ClientLateUpdate() { }
		public virtual void ServerLateUpdate() { }

		/// <summary>
		/// Shut down the transport, both as client and server
		/// </summary>
		public abstract void Shutdown();

		/// <summary>
		/// called when quitting the application by closing the window / pressing stop in the editor
		/// <para>virtual so that inheriting classes' OnApplicationQuit() can call base.OnApplicationQuit() too</para>
		/// </summary>
		//public virtual void OnApplicationQuit()
		//{
		//	// stop transport (e.g. to shut down threads)
		//	// (when pressing Stop in the Editor, Unity keeps threads alive
		//	//  until we press Start again. so if Transports use threads, we
		//	//  really want them to end now and not after next start)
		//	Shutdown();
		//}
	}
}