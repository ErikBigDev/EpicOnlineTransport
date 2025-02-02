// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Stats
{
	public sealed partial class StatsInterface : Handle
	{
		public StatsInterface()
		{
		}

		public StatsInterface(System.IntPtr innerHandle) : base(innerHandle)
		{
		}

		/// <summary>
		/// The most recent version of the <see cref="CopyStatByIndexOptions" /> struct.
		/// </summary>
		public const int CopystatbyindexApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="CopyStatByNameOptions" /> struct.
		/// </summary>
		public const int CopystatbynameApiLatest = 1;

		/// <summary>
		/// DEPRECATED! Use <see cref="GetstatscountApiLatest" /> instead.
		/// </summary>
		public const int GetstatcountApiLatest = GetstatscountApiLatest;

		/// <summary>
		/// The most recent version of the <see cref="GetStatsCount" /> API.
		/// </summary>
		public const int GetstatscountApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="IngestData" /> struct.
		/// </summary>
		public const int IngestdataApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="IngestStat" /> struct.
		/// </summary>
		public const int IngeststatApiLatest = 3;

		/// <summary>
		/// Maximum number of stats that can be ingested in a single <see cref="IngestStat" /> operation.
		/// </summary>
		public const int MaxIngestStats = 3000;

		/// <summary>
		/// Maximum number of stats that can be queried in a single <see cref="QueryStats" /> operation.
		/// </summary>
		public const int MaxQueryStats = 1000;

		/// <summary>
		/// The most recent version of the <see cref="QueryStats" /> struct.
		/// </summary>
		public const int QuerystatsApiLatest = 3;

		/// <summary>
		/// The most recent version of the <see cref="Stat" /> struct.
		/// </summary>
		public const int StatApiLatest = 1;

		/// <summary>
		/// Timestamp value representing an undefined StartTime or EndTime for <see cref="Stat" />
		/// </summary>
		public const int TimeUndefined = -1;

		/// <summary>
		/// Fetches a stat from a given index. Use <see cref="Release" /> when finished with the data.
		/// <seealso cref="Release" />
		/// </summary>
		/// <param name="options">Structure containing the Epic Online Services Account ID and index being accessed</param>
		/// <param name="outStat">The stat data for the given index, if it exists and is valid</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the information is available and passed out in OutStat
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.NotFound" /> if the stat is not found
		/// </returns>
		public Result CopyStatByIndex(CopyStatByIndexOptions options, out Stat outStat)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<CopyStatByIndexOptionsInternal, CopyStatByIndexOptions>(ref optionsAddress, options);

			var outStatAddress = System.IntPtr.Zero;

			var funcResult = EOS_Stats_CopyStatByIndex(InnerHandle, optionsAddress, ref outStatAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			if (Helper.TryMarshalGet<StatInternal, Stat>(outStatAddress, out outStat))
			{
				EOS_Stats_Stat_Release(outStatAddress);
			}

			return funcResult;
		}

		/// <summary>
		/// Fetches a stat from cached stats by name. Use <see cref="Release" /> when finished with the data.
		/// <seealso cref="Release" />
		/// </summary>
		/// <param name="options">Structure containing the Epic Online Services Account ID and name being accessed</param>
		/// <param name="outStat">The stat data for the given name, if it exists and is valid</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the information is available and passed out in OutStat
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.NotFound" /> if the stat is not found
		/// </returns>
		public Result CopyStatByName(CopyStatByNameOptions options, out Stat outStat)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<CopyStatByNameOptionsInternal, CopyStatByNameOptions>(ref optionsAddress, options);

			var outStatAddress = System.IntPtr.Zero;

			var funcResult = EOS_Stats_CopyStatByName(InnerHandle, optionsAddress, ref outStatAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			if (Helper.TryMarshalGet<StatInternal, Stat>(outStatAddress, out outStat))
			{
				EOS_Stats_Stat_Release(outStatAddress);
			}

			return funcResult;
		}

		/// <summary>
		/// Fetch the number of stats that are cached locally.
		/// <seealso cref="CopyStatByIndex" />
		/// </summary>
		/// <param name="options">The Options associated with retrieving the stat count</param>
		/// <returns>
		/// Number of stats or 0 if there is an error
		/// </returns>
		public uint GetStatsCount(GetStatCountOptions options)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<GetStatCountOptionsInternal, GetStatCountOptions>(ref optionsAddress, options);

			var funcResult = EOS_Stats_GetStatsCount(InnerHandle, optionsAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			return funcResult;
		}

		/// <summary>
		/// Ingest a stat by the amount specified in Options.
		/// When the operation is complete and the delegate is triggered the stat will be uploaded to the backend to be processed.
		/// The stat may not be updated immediately and an achievement using the stat may take a while to be unlocked once the stat has been uploaded.
		/// </summary>
		/// <param name="options">Structure containing information about the stat we're ingesting.</param>
		/// <param name="clientData">Arbitrary data that is passed back to you in the CompletionDelegate.</param>
		/// <param name="completionDelegate">This function is called when the ingest stat operation completes.</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the operation completes successfully
		/// <see cref="Result.InvalidParameters" /> if any of the options are incorrect
		/// <see cref="Result.InvalidUser" /> if target user ID is missing or incorrect
		/// </returns>
		public void IngestStat(IngestStatOptions options, object clientData, OnIngestStatCompleteCallback completionDelegate)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<IngestStatOptionsInternal, IngestStatOptions>(ref optionsAddress, options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnIngestStatCompleteCallbackInternal(OnIngestStatCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			EOS_Stats_IngestStat(InnerHandle, optionsAddress, clientDataAddress, completionDelegateInternal);

			Helper.TryMarshalDispose(ref optionsAddress);
		}

		/// <summary>
		/// Query for a list of stats for a specific player.
		/// </summary>
		/// <param name="options">Structure containing information about the player whose stats we're retrieving.</param>
		/// <param name="clientData">Arbitrary data that is passed back to you in the CompletionDelegate</param>
		/// <param name="completionDelegate">This function is called when the query player stats operation completes.</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the operation completes successfully
		/// <see cref="Result.InvalidParameters" /> if any of the options are incorrect
		/// <see cref="Result.InvalidUser" /> if target user ID is missing or incorrect
		/// </returns>
		public void QueryStats(QueryStatsOptions options, object clientData, OnQueryStatsCompleteCallback completionDelegate)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<QueryStatsOptionsInternal, QueryStatsOptions>(ref optionsAddress, options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnQueryStatsCompleteCallbackInternal(OnQueryStatsCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			EOS_Stats_QueryStats(InnerHandle, optionsAddress, clientDataAddress, completionDelegateInternal);

			Helper.TryMarshalDispose(ref optionsAddress);
		}

		[MonoPInvokeCallback(typeof(OnIngestStatCompleteCallbackInternal))]
		internal static void OnIngestStatCompleteCallbackInternalImplementation(System.IntPtr data)
		{
			OnIngestStatCompleteCallback callback;
			IngestStatCompleteCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback<OnIngestStatCompleteCallback, IngestStatCompleteCallbackInfoInternal, IngestStatCompleteCallbackInfo>(data, out callback, out callbackInfo))
			{
				callback(callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnQueryStatsCompleteCallbackInternal))]
		internal static void OnQueryStatsCompleteCallbackInternalImplementation(System.IntPtr data)
		{
			OnQueryStatsCompleteCallback callback;
			OnQueryStatsCompleteCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback<OnQueryStatsCompleteCallback, OnQueryStatsCompleteCallbackInfoInternal, OnQueryStatsCompleteCallbackInfo>(data, out callback, out callbackInfo))
			{
				callback(callbackInfo);
			}
		}

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern Result EOS_Stats_CopyStatByIndex(System.IntPtr handle, System.IntPtr options, ref System.IntPtr outStat);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern Result EOS_Stats_CopyStatByName(System.IntPtr handle, System.IntPtr options, ref System.IntPtr outStat);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern uint EOS_Stats_GetStatsCount(System.IntPtr handle, System.IntPtr options);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern void EOS_Stats_IngestStat(System.IntPtr handle, System.IntPtr options, System.IntPtr clientData, OnIngestStatCompleteCallbackInternal completionDelegate);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern void EOS_Stats_QueryStats(System.IntPtr handle, System.IntPtr options, System.IntPtr clientData, OnQueryStatsCompleteCallbackInternal completionDelegate);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern void EOS_Stats_Stat_Release(System.IntPtr stat);
	}
}