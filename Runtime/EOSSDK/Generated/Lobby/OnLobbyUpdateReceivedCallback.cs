// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Lobby
{
	/// <summary>
	/// Function prototype definition for notifications that comes from <see cref="LobbyInterface.AddNotifyLobbyUpdateReceived" />
	/// </summary>
	/// <param name="data">A <see cref="LobbyUpdateReceivedCallbackInfo" /> containing the output information and result</param>
	public delegate void OnLobbyUpdateReceivedCallback(LobbyUpdateReceivedCallbackInfo data);

	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
	internal delegate void OnLobbyUpdateReceivedCallbackInternal(System.IntPtr data);
}