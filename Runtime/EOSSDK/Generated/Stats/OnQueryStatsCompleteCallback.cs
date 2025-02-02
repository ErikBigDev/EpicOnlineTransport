// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Stats
{
	/// <summary>
	/// Function prototype definition for callbacks passed to <see cref="StatsInterface.QueryStats" />
	/// <seealso cref="StatsInterface.Release" />
	/// </summary>
	/// <param name="data">A <see cref="OnQueryStatsCompleteCallbackInfo" /> containing the output information and result</param>
	public delegate void OnQueryStatsCompleteCallback(OnQueryStatsCompleteCallbackInfo data);

	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
	internal delegate void OnQueryStatsCompleteCallbackInternal(System.IntPtr data);
}