// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Sessions
{
	/// <summary>
	/// Function prototype definition for callbacks passed to <see cref="SessionsInterface.RejectInvite" />
	/// </summary>
	/// <param name="data">A <see cref="RejectInviteCallbackInfo" /> containing the output information and result</param>
	public delegate void OnRejectInviteCallback(RejectInviteCallbackInfo data);

	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
	internal delegate void OnRejectInviteCallbackInternal(System.IntPtr data);
}