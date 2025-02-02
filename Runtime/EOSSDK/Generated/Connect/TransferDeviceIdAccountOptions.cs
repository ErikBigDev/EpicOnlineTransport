// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Connect
{
	/// <summary>
	/// Input parameters for the <see cref="ConnectInterface.TransferDeviceIdAccount" /> Function.
	/// </summary>
	public class TransferDeviceIdAccountOptions
	{
		/// <summary>
		/// The primary product user id, currently logged in, that is already associated with a real external user account (such as Epic Games, PlayStation(TM)Network, Xbox Live and other).
		/// 
		/// The account linking keychain that owns this product user will be preserved and receive
		/// the Device ID login credentials under it.
		/// </summary>
		public ProductUserId PrimaryLocalUserId { get; set; }

		/// <summary>
		/// The product user id, currently logged in, that has been originally created using the anonymous local Device ID login type,
		/// and whose Device ID login will be transferred to the keychain of the PrimaryLocalUserId.
		/// </summary>
		public ProductUserId LocalDeviceUserId { get; set; }

		/// <summary>
		/// Specifies which <see cref="ProductUserId" /> (i.e. game progression) will be preserved in the operation.
		/// 
		/// After a successful transfer operation, subsequent logins using the same external account or
		/// the same local Device ID login will return user session for the ProductUserIdToPreserve.
		/// 
		/// Set to either PrimaryLocalUserId or LocalDeviceUserId.
		/// </summary>
		public ProductUserId ProductUserIdToPreserve { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct TransferDeviceIdAccountOptionsInternal : ISettable, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_PrimaryLocalUserId;
		private System.IntPtr m_LocalDeviceUserId;
		private System.IntPtr m_ProductUserIdToPreserve;

		public ProductUserId PrimaryLocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref m_PrimaryLocalUserId, value);
			}
		}

		public ProductUserId LocalDeviceUserId
		{
			set
			{
				Helper.TryMarshalSet(ref m_LocalDeviceUserId, value);
			}
		}

		public ProductUserId ProductUserIdToPreserve
		{
			set
			{
				Helper.TryMarshalSet(ref m_ProductUserIdToPreserve, value);
			}
		}

		public void Set(TransferDeviceIdAccountOptions other)
		{
			if (other != null)
			{
				m_ApiVersion = ConnectInterface.TransferdeviceidaccountApiLatest;
				PrimaryLocalUserId = other.PrimaryLocalUserId;
				LocalDeviceUserId = other.LocalDeviceUserId;
				ProductUserIdToPreserve = other.ProductUserIdToPreserve;
			}
		}

		public void Set(object other)
		{
			Set(other as TransferDeviceIdAccountOptions);
		}

		public void Dispose()
		{
		}
	}
}