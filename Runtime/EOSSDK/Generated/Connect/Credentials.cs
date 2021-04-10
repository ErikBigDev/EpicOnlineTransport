// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Connect
{
	/// <summary>
	/// A structure that contains external login credentials.
	/// 
	/// This is part of the input structure <see cref="LoginOptions" />.
	/// <seealso cref="ExternalCredentialType" />
	/// <seealso cref="ConnectInterface.Login" />
	/// </summary>
	public class Credentials : ISettable
	{
		/// <summary>
		/// External token associated with the user logging in.
		/// </summary>
		public string Token { get; set; }

		/// <summary>
		/// Type of external login; identifies the auth method to use.
		/// </summary>
		public ExternalCredentialType Type { get; set; }

		internal void Set(CredentialsInternal? other)
		{
			if (other != null)
			{
				Token = other.Value.Token;
				Type = other.Value.Type;
			}
		}

		public void Set(object other)
		{
			Set(other as CredentialsInternal?);
		}
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct CredentialsInternal : ISettable, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_Token;
		private ExternalCredentialType m_Type;

		public string Token
		{
			get
			{
				string value;
				Helper.TryMarshalGet(m_Token, out value);
				return value;
			}

			set
			{
				Helper.TryMarshalSet(ref m_Token, value);
			}
		}

		public ExternalCredentialType Type
		{
			get
			{
				return m_Type;
			}

			set
			{
				m_Type = value;
			}
		}

		public void Set(Credentials other)
		{
			if (other != null)
			{
				m_ApiVersion = ConnectInterface.CredentialsApiLatest;
				Token = other.Token;
				Type = other.Type;
			}
		}

		public void Set(object other)
		{
			Set(other as Credentials);
		}

		public void Dispose()
		{
			Helper.TryMarshalDispose(ref m_Token);
		}
	}
}