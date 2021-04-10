// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Leaderboards
{
	/// <summary>
	/// Input parameters for the <see cref="LeaderboardsInterface.QueryLeaderboardDefinitions" /> function.
	/// StartTime and EndTime are optional parameters, they can be used to limit the list of definitions
	/// to overlap the time window specified.
	/// </summary>
	public class QueryLeaderboardDefinitionsOptions
	{
		/// <summary>
		/// An optional POSIX timestamp for the leaderboard's start time, or <see cref="LeaderboardsInterface.TimeUndefined" />
		/// </summary>
		public System.DateTimeOffset? StartTime { get; set; }

		/// <summary>
		/// An optional POSIX timestamp for the leaderboard's end time, or <see cref="LeaderboardsInterface.TimeUndefined" />
		/// </summary>
		public System.DateTimeOffset? EndTime { get; set; }

		/// <summary>
		/// Product User ID for user who is querying definitions.
		/// Must be set when using a client policy that requires a valid logged in user.
		/// Not used for Dedicated Server where no user is available.
		/// </summary>
		public ProductUserId LocalUserId { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct QueryLeaderboardDefinitionsOptionsInternal : ISettable, System.IDisposable
	{
		private int m_ApiVersion;
		private long m_StartTime;
		private long m_EndTime;
		private System.IntPtr m_LocalUserId;

		public System.DateTimeOffset? StartTime
		{
			set
			{
				Helper.TryMarshalSet(ref m_StartTime, value);
			}
		}

		public System.DateTimeOffset? EndTime
		{
			set
			{
				Helper.TryMarshalSet(ref m_EndTime, value);
			}
		}

		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref m_LocalUserId, value);
			}
		}

		public void Set(QueryLeaderboardDefinitionsOptions other)
		{
			if (other != null)
			{
				m_ApiVersion = LeaderboardsInterface.QueryleaderboarddefinitionsApiLatest;
				StartTime = other.StartTime;
				EndTime = other.EndTime;
				LocalUserId = other.LocalUserId;
			}
		}

		public void Set(object other)
		{
			Set(other as QueryLeaderboardDefinitionsOptions);
		}

		public void Dispose()
		{
		}
	}
}