// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Sanctions
{
	/// <summary>
	/// Input parameters for the <see cref="SanctionsInterface.QueryActivePlayerSanctions" /> API.
	/// </summary>
	public class QueryActivePlayerSanctionsOptions
	{
		/// <summary>
		/// Product User ID of the user whose active sanctions are to be retrieved.
		/// </summary>
		public ProductUserId TargetUserId { get; set; }

		/// <summary>
		/// The Product User ID of the local user who initiated this request. Dedicated servers should set this to null.
		/// </summary>
		public ProductUserId LocalUserId { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct QueryActivePlayerSanctionsOptionsInternal : ISettable, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_TargetUserId;
		private System.IntPtr m_LocalUserId;

		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref m_TargetUserId, value);
			}
		}

		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref m_LocalUserId, value);
			}
		}

		public void Set(QueryActivePlayerSanctionsOptions other)
		{
			if (other != null)
			{
				m_ApiVersion = SanctionsInterface.QueryactiveplayersanctionsApiLatest;
				TargetUserId = other.TargetUserId;
				LocalUserId = other.LocalUserId;
			}
		}

		public void Set(object other)
		{
			Set(other as QueryActivePlayerSanctionsOptions);
		}

		public void Dispose()
		{
		}
	}
}