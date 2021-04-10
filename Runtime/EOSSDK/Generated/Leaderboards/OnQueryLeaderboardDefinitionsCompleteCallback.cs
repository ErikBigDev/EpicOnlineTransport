// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Leaderboards
{
	/// <summary>
	/// Function prototype definition for callbacks passed to <see cref="LeaderboardsInterface.QueryLeaderboardDefinitions" />
	/// </summary>
	/// <param name="data">A <see cref="OnQueryLeaderboardDefinitionsCompleteCallbackInfo" /> containing the output information and result</param>
	public delegate void OnQueryLeaderboardDefinitionsCompleteCallback(OnQueryLeaderboardDefinitionsCompleteCallbackInfo data);

	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
	internal delegate void OnQueryLeaderboardDefinitionsCompleteCallbackInternal(System.IntPtr data);
}