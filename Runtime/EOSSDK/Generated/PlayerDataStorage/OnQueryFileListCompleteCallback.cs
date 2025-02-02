// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.PlayerDataStorage
{
	/// <summary>
	/// Callback for when <see cref="PlayerDataStorageInterface.QueryFileList" /> completes
	/// </summary>
	public delegate void OnQueryFileListCompleteCallback(QueryFileListCallbackInfo data);

	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
	internal delegate void OnQueryFileListCompleteCallbackInternal(System.IntPtr data);
}