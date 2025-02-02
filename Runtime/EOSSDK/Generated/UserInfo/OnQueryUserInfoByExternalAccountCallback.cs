// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.UserInfo
{
	/// <summary>
	/// Function prototype definition for callbacks passed to <see cref="UserInfoInterface.QueryUserInfo" />
	/// </summary>
	/// <param name="data">A <see cref="QueryUserInfoByExternalAccountCallbackInfo" /> containing the output information and result</param>
	public delegate void OnQueryUserInfoByExternalAccountCallback(QueryUserInfoByExternalAccountCallbackInfo data);

	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
	internal delegate void OnQueryUserInfoByExternalAccountCallbackInternal(System.IntPtr data);
}