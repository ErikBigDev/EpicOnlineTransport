// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Sanctions
{
	/// <summary>
	/// Input parameters for the <see cref="SanctionsInterface.GetPlayerSanctionCount" /> function.
	/// </summary>
	public class GetPlayerSanctionCountOptions
	{
		/// <summary>
		/// Product User ID of the user whose sanction count should be returned
		/// </summary>
		public ProductUserId TargetUserId { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct GetPlayerSanctionCountOptionsInternal : ISettable, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_TargetUserId;

		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref m_TargetUserId, value);
			}
		}

		public void Set(GetPlayerSanctionCountOptions other)
		{
			if (other != null)
			{
				m_ApiVersion = SanctionsInterface.GetplayersanctioncountApiLatest;
				TargetUserId = other.TargetUserId;
			}
		}

		public void Set(object other)
		{
			Set(other as GetPlayerSanctionCountOptions);
		}

		public void Dispose()
		{
		}
	}
}