// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices
{
	public sealed partial class ProductUserId : Handle
	{
		public ProductUserId()
		{
		}

		public ProductUserId(System.IntPtr innerHandle) : base(innerHandle)
		{
		}

		/// <summary>
		/// A character buffer of this size is large enough to fit a successful output of <see cref="ToString" />. This length does not include the null-terminator.
		/// </summary>
		public const int ProductuseridMaxLength = 128;

		/// <summary>
		/// Retrieve an <see cref="ProductUserId" /> from a raw string representing an Epic Online Services Product User ID. The input string must be null-terminated.
		/// </summary>
		/// <param name="productUserIdString">The stringified product user ID for which to retrieve the Epic Online Services Product User ID</param>
		/// <returns>
		/// The <see cref="ProductUserId" /> that corresponds to the ProductUserIdString
		/// </returns>
		public static ProductUserId FromString(string productUserIdString)
		{
			System.IntPtr productUserIdStringAddress = System.IntPtr.Zero;
			Helper.TryMarshalSet(ref productUserIdStringAddress, productUserIdString);

			var funcResult = EOS_ProductUserId_FromString(productUserIdStringAddress);

			Helper.TryMarshalDispose(ref productUserIdStringAddress);

			ProductUserId funcResultReturn;
			Helper.TryMarshalGet(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Check whether or not the given account unique ID is considered valid
		/// </summary>
		/// <param name="accountId">The Product User ID to check for validity</param>
		/// <returns>
		/// true if the <see cref="ProductUserId" /> is valid, otherwise false
		/// </returns>
		public bool IsValid()
		{
			var funcResult = EOS_ProductUserId_IsValid(InnerHandle);

			bool funcResultReturn;
			Helper.TryMarshalGet(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Retrieve a null-terminated stringified Product User ID from an <see cref="ProductUserId" />. This is useful for replication of Product User IDs in multiplayer games.
		/// This string will be no larger than <see cref="ProductuseridMaxLength" /> + 1 and will only contain UTF8-encoded printable characters (excluding the null-terminator).
		/// </summary>
		/// <param name="accountId">The Product User ID for which to retrieve the stringified version.</param>
		/// <param name="outBuffer">The buffer into which the character data should be written</param>
		/// <param name="inOutBufferLength">
		/// The size of the OutBuffer in characters.
		/// The input buffer should include enough space to be null-terminated.
		/// When the function returns, this parameter will be filled with the length of the string copied into OutBuffer including the null termination character.
		/// </param>
		/// <returns>
		/// An <see cref="Result" /> that indicates whether the Product User ID string was copied into the OutBuffer.
		/// <see cref="Result.Success" /> - The OutBuffer was filled, and InOutBufferLength contains the number of characters copied into OutBuffer including the null terminator.
		/// <see cref="Result.InvalidParameters" /> - Either OutBuffer or InOutBufferLength were passed as NULL parameters.
		/// <see cref="Result.InvalidUser" /> - The AccountId is invalid and cannot be stringified.
		/// <see cref="Result.LimitExceeded" /> - The OutBuffer is not large enough to receive the Product User ID string. InOutBufferLength contains the required minimum length to perform the operation successfully.
		/// </returns>
		public Result ToString(out string outBuffer)
		{
			System.IntPtr outBufferAddress = System.IntPtr.Zero;
			int inOutBufferLength = ProductuseridMaxLength + 1;
			Helper.TryMarshalAllocate(ref outBufferAddress, inOutBufferLength);

			var funcResult = EOS_ProductUserId_ToString(InnerHandle, outBufferAddress, ref inOutBufferLength);

			Helper.TryMarshalGet(outBufferAddress, out outBuffer);
			Helper.TryMarshalDispose(ref outBufferAddress);

			return funcResult;
		}

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern System.IntPtr EOS_ProductUserId_FromString(System.IntPtr productUserIdString);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern int EOS_ProductUserId_IsValid(System.IntPtr accountId);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern Result EOS_ProductUserId_ToString(System.IntPtr accountId, System.IntPtr outBuffer, ref int inOutBufferLength);
	}
}