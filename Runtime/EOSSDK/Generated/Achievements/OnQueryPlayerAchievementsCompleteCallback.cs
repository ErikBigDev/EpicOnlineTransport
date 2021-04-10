// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Achievements
{
	/// <summary>
	/// Function prototype definition for callbacks passed to <see cref="AchievementsInterface.QueryPlayerAchievements" />
	/// <seealso cref="AchievementsInterface.Release" />
	/// </summary>
	/// <param name="data">An <see cref="OnQueryPlayerAchievementsCompleteCallbackInfo" /> containing the output information and result</param>
	public delegate void OnQueryPlayerAchievementsCompleteCallback(OnQueryPlayerAchievementsCompleteCallbackInfo data);

	[System.Runtime.InteropServices.UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.StdCall)]
	internal delegate void OnQueryPlayerAchievementsCompleteCallbackInternal(System.IntPtr data);
}