// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Achievements
{
	public sealed partial class AchievementsInterface : Handle
	{
		public AchievementsInterface()
		{
		}

		public AchievementsInterface(System.IntPtr innerHandle) : base(innerHandle)
		{
		}

		/// <summary>
		/// Timestamp value representing an undefined UnlockTime for <see cref="PlayerAchievement" /> and <see cref="UnlockedAchievement" />
		/// </summary>
		public const int AchievementUnlocktimeUndefined = -1;

		/// <summary>
		/// The most recent version of the <see cref="AddNotifyAchievementsUnlocked" /> API.
		/// </summary>
		public const int AddnotifyachievementsunlockedApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="AddNotifyAchievementsUnlockedV2" /> API.
		/// </summary>
		public const int Addnotifyachievementsunlockedv2ApiLatest = 2;

		/// <summary>
		/// The most recent version of the <see cref="CopyAchievementDefinitionV2ByAchievementIdOptions" /> struct.
		/// </summary>
		public const int Copyachievementdefinitionv2ByachievementidApiLatest = 2;

		/// <summary>
		/// The most recent version of the <see cref="CopyAchievementDefinitionByIndexOptions" /> struct.
		/// </summary>
		public const int Copyachievementdefinitionv2ByindexApiLatest = 2;

		/// <summary>
		/// The most recent version of the <see cref="CopyAchievementDefinitionByAchievementIdOptions" /> struct.
		/// </summary>
		public const int CopydefinitionbyachievementidApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="CopyAchievementDefinitionByIndexOptions" /> struct.
		/// </summary>
		public const int CopydefinitionbyindexApiLatest = 1;

		/// <summary>
		/// DEPRECATED! Use <see cref="Copyachievementdefinitionv2ByachievementidApiLatest" /> instead.
		/// </summary>
		public const int Copydefinitionv2ByachievementidApiLatest = Copyachievementdefinitionv2ByachievementidApiLatest;

		/// <summary>
		/// DEPRECATED! Use <see cref="Copyachievementdefinitionv2ByindexApiLatest" /> instead.
		/// </summary>
		public const int Copydefinitionv2ByindexApiLatest = Copyachievementdefinitionv2ByindexApiLatest;

		/// <summary>
		/// The most recent version of the <see cref="CopyPlayerAchievementByAchievementIdOptions" /> struct.
		/// </summary>
		public const int CopyplayerachievementbyachievementidApiLatest = 2;

		/// <summary>
		/// The most recent version of the <see cref="CopyPlayerAchievementByIndexOptions" /> struct.
		/// </summary>
		public const int CopyplayerachievementbyindexApiLatest = 2;

		/// <summary>
		/// The most recent version of the <see cref="CopyUnlockedAchievementByAchievementIdOptions" /> struct.
		/// </summary>
		public const int CopyunlockedachievementbyachievementidApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="CopyUnlockedAchievementByIndexOptions" /> struct.
		/// </summary>
		public const int CopyunlockedachievementbyindexApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="Definition" /> struct.
		/// </summary>
		public const int DefinitionApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="DefinitionV2" /> struct.
		/// </summary>
		public const int Definitionv2ApiLatest = 2;

		/// <summary>
		/// The most recent version of the <see cref="GetAchievementDefinitionCount" /> API.
		/// </summary>
		public const int GetachievementdefinitioncountApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="GetPlayerAchievementCount" /> API.
		/// </summary>
		public const int GetplayerachievementcountApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="GetUnlockedAchievementCount" /> API.
		/// </summary>
		public const int GetunlockedachievementcountApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="PlayerAchievement" /> struct.
		/// </summary>
		public const int PlayerachievementApiLatest = 2;

		public const int PlayerstatinfoApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="QueryDefinitions" /> struct.
		/// </summary>
		public const int QuerydefinitionsApiLatest = 3;

		/// <summary>
		/// The most recent version of the <see cref="QueryPlayerAchievements" /> struct.
		/// </summary>
		public const int QueryplayerachievementsApiLatest = 2;

		/// <summary>
		/// DEPRECATED! Use <see cref="StatthresholdsApiLatest" /> instead.
		/// </summary>
		public const int StatthresholdApiLatest = StatthresholdsApiLatest;

		/// <summary>
		/// The most recent version of the <see cref="StatThresholds" /> struct.
		/// </summary>
		public const int StatthresholdsApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="UnlockAchievements" /> struct.
		/// </summary>
		public const int UnlockachievementsApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="UnlockedAchievement" /> struct.
		/// </summary>
		public const int UnlockedachievementApiLatest = 1;

		/// <summary>
		/// DEPRECATED! Use <see cref="AddNotifyAchievementsUnlockedV2" /> instead.
		/// 
		/// Register to receive achievement unlocked notifications.
		/// @note must call <see cref="RemoveNotifyAchievementsUnlocked" /> to remove the notification
		/// <seealso cref="RemoveNotifyAchievementsUnlocked" />
		/// </summary>
		/// <param name="options">Structure containing information about the achievement unlocked notification</param>
		/// <param name="clientData">Arbitrary data that is passed back to you in the CompletionDelegate</param>
		/// <param name="notificationFn">A callback that is fired when an achievement unlocked notification for a user has been received</param>
		/// <returns>
		/// handle representing the registered callback
		/// </returns>
		public ulong AddNotifyAchievementsUnlocked(AddNotifyAchievementsUnlockedOptions options, object clientData, OnAchievementsUnlockedCallback notificationFn)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<AddNotifyAchievementsUnlockedOptionsInternal, AddNotifyAchievementsUnlockedOptions>(ref optionsAddress, options);

			var clientDataAddress = System.IntPtr.Zero;

			var notificationFnInternal = new OnAchievementsUnlockedCallbackInternal(OnAchievementsUnlockedCallbackInternalImplementation);
			Helper.AddCallback(ref clientDataAddress, clientData, notificationFn, notificationFnInternal);

			var funcResult = EOS_Achievements_AddNotifyAchievementsUnlocked(InnerHandle, optionsAddress, clientDataAddress, notificationFnInternal);

			Helper.TryMarshalDispose(ref optionsAddress);

			Helper.TryAssignNotificationIdToCallback(clientDataAddress, funcResult);

			return funcResult;
		}

		/// <summary>
		/// Register to receive achievement unlocked notifications.
		/// @note must call <see cref="RemoveNotifyAchievementsUnlocked" /> to remove the notification
		/// <seealso cref="RemoveNotifyAchievementsUnlocked" />
		/// </summary>
		/// <param name="options">Structure containing information about the achievement unlocked notification</param>
		/// <param name="clientData">Arbitrary data that is passed back to you in the CompletionDelegate</param>
		/// <param name="notificationFn">A callback that is fired when an achievement unlocked notification for a user has been received</param>
		/// <returns>
		/// handle representing the registered callback
		/// </returns>
		public ulong AddNotifyAchievementsUnlockedV2(AddNotifyAchievementsUnlockedV2Options options, object clientData, OnAchievementsUnlockedCallbackV2 notificationFn)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<AddNotifyAchievementsUnlockedV2OptionsInternal, AddNotifyAchievementsUnlockedV2Options>(ref optionsAddress, options);

			var clientDataAddress = System.IntPtr.Zero;

			var notificationFnInternal = new OnAchievementsUnlockedCallbackV2Internal(OnAchievementsUnlockedCallbackV2InternalImplementation);
			Helper.AddCallback(ref clientDataAddress, clientData, notificationFn, notificationFnInternal);

			var funcResult = EOS_Achievements_AddNotifyAchievementsUnlockedV2(InnerHandle, optionsAddress, clientDataAddress, notificationFnInternal);

			Helper.TryMarshalDispose(ref optionsAddress);

			Helper.TryAssignNotificationIdToCallback(clientDataAddress, funcResult);

			return funcResult;
		}

		/// <summary>
		/// DEPRECATED! Use <see cref="CopyAchievementDefinitionV2ByAchievementId" /> instead.
		/// 
		/// Fetches an achievement definition from a given achievement ID.
		/// <seealso cref="Release" />
		/// <seealso cref="CopyAchievementDefinitionV2ByAchievementId" />
		/// </summary>
		/// <param name="options">Structure containing the achievement ID being accessed</param>
		/// <param name="outDefinition">The achievement definition for the given achievement ID, if it exists and is valid, use <see cref="Release" /> when finished</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the information is available and passed out in OutDefinition
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.NotFound" /> if the achievement definition is not found
		/// </returns>
		public Result CopyAchievementDefinitionByAchievementId(CopyAchievementDefinitionByAchievementIdOptions options, out Definition outDefinition)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<CopyAchievementDefinitionByAchievementIdOptionsInternal, CopyAchievementDefinitionByAchievementIdOptions>(ref optionsAddress, options);

			var outDefinitionAddress = System.IntPtr.Zero;

			var funcResult = EOS_Achievements_CopyAchievementDefinitionByAchievementId(InnerHandle, optionsAddress, ref outDefinitionAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			if (Helper.TryMarshalGet<DefinitionInternal, Definition>(outDefinitionAddress, out outDefinition))
			{
				EOS_Achievements_Definition_Release(outDefinitionAddress);
			}

			return funcResult;
		}

		/// <summary>
		/// DEPRECATED! Use <see cref="CopyAchievementDefinitionV2ByIndex" /> instead.
		/// 
		/// Fetches an achievement definition from a given index.
		/// <seealso cref="CopyAchievementDefinitionV2ByIndex" />
		/// <seealso cref="Release" />
		/// </summary>
		/// <param name="options">Structure containing the index being accessed</param>
		/// <param name="outDefinition">The achievement definition for the given index, if it exists and is valid, use <see cref="Release" /> when finished</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the information is available and passed out in OutDefinition
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.NotFound" /> if the achievement definition is not found
		/// </returns>
		public Result CopyAchievementDefinitionByIndex(CopyAchievementDefinitionByIndexOptions options, out Definition outDefinition)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<CopyAchievementDefinitionByIndexOptionsInternal, CopyAchievementDefinitionByIndexOptions>(ref optionsAddress, options);

			var outDefinitionAddress = System.IntPtr.Zero;

			var funcResult = EOS_Achievements_CopyAchievementDefinitionByIndex(InnerHandle, optionsAddress, ref outDefinitionAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			if (Helper.TryMarshalGet<DefinitionInternal, Definition>(outDefinitionAddress, out outDefinition))
			{
				EOS_Achievements_Definition_Release(outDefinitionAddress);
			}

			return funcResult;
		}

		/// <summary>
		/// Fetches an achievement definition from a given achievement ID.
		/// <seealso cref="Release" />
		/// </summary>
		/// <param name="options">Structure containing the achievement ID being accessed</param>
		/// <param name="outDefinition">The achievement definition for the given achievement ID, if it exists and is valid, use <see cref="Release" /> when finished</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the information is available and passed out in OutDefinition
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.NotFound" /> if the achievement definition is not found
		/// <see cref="Result.InvalidProductUserID" /> if any of the userid options are incorrect
		/// </returns>
		public Result CopyAchievementDefinitionV2ByAchievementId(CopyAchievementDefinitionV2ByAchievementIdOptions options, out DefinitionV2 outDefinition)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<CopyAchievementDefinitionV2ByAchievementIdOptionsInternal, CopyAchievementDefinitionV2ByAchievementIdOptions>(ref optionsAddress, options);

			var outDefinitionAddress = System.IntPtr.Zero;

			var funcResult = EOS_Achievements_CopyAchievementDefinitionV2ByAchievementId(InnerHandle, optionsAddress, ref outDefinitionAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			if (Helper.TryMarshalGet<DefinitionV2Internal, DefinitionV2>(outDefinitionAddress, out outDefinition))
			{
				EOS_Achievements_DefinitionV2_Release(outDefinitionAddress);
			}

			return funcResult;
		}

		/// <summary>
		/// Fetches an achievement definition from a given index.
		/// <seealso cref="Release" />
		/// </summary>
		/// <param name="options">Structure containing the index being accessed</param>
		/// <param name="outDefinition">The achievement definition for the given index, if it exists and is valid, use <see cref="Release" /> when finished</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the information is available and passed out in OutDefinition
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.NotFound" /> if the achievement definition is not found
		/// <see cref="Result.InvalidProductUserID" /> if any of the userid options are incorrect
		/// </returns>
		public Result CopyAchievementDefinitionV2ByIndex(CopyAchievementDefinitionV2ByIndexOptions options, out DefinitionV2 outDefinition)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<CopyAchievementDefinitionV2ByIndexOptionsInternal, CopyAchievementDefinitionV2ByIndexOptions>(ref optionsAddress, options);

			var outDefinitionAddress = System.IntPtr.Zero;

			var funcResult = EOS_Achievements_CopyAchievementDefinitionV2ByIndex(InnerHandle, optionsAddress, ref outDefinitionAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			if (Helper.TryMarshalGet<DefinitionV2Internal, DefinitionV2>(outDefinitionAddress, out outDefinition))
			{
				EOS_Achievements_DefinitionV2_Release(outDefinitionAddress);
			}

			return funcResult;
		}

		/// <summary>
		/// Fetches a player achievement from a given achievement ID.
		/// <seealso cref="Release" />
		/// </summary>
		/// <param name="options">Structure containing the Epic Online Services Account ID and achievement ID being accessed</param>
		/// <param name="outAchievement">The player achievement data for the given achievement ID, if it exists and is valid, use <see cref="Release" /> when finished</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the information is available and passed out in OutAchievement
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.NotFound" /> if the player achievement is not found
		/// <see cref="Result.InvalidProductUserID" /> if you pass an invalid user ID
		/// </returns>
		public Result CopyPlayerAchievementByAchievementId(CopyPlayerAchievementByAchievementIdOptions options, out PlayerAchievement outAchievement)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<CopyPlayerAchievementByAchievementIdOptionsInternal, CopyPlayerAchievementByAchievementIdOptions>(ref optionsAddress, options);

			var outAchievementAddress = System.IntPtr.Zero;

			var funcResult = EOS_Achievements_CopyPlayerAchievementByAchievementId(InnerHandle, optionsAddress, ref outAchievementAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			if (Helper.TryMarshalGet<PlayerAchievementInternal, PlayerAchievement>(outAchievementAddress, out outAchievement))
			{
				EOS_Achievements_PlayerAchievement_Release(outAchievementAddress);
			}

			return funcResult;
		}

		/// <summary>
		/// Fetches a player achievement from a given index.
		/// <seealso cref="Release" />
		/// </summary>
		/// <param name="options">Structure containing the Epic Online Services Account ID and index being accessed</param>
		/// <param name="outAchievement">The player achievement data for the given index, if it exists and is valid, use <see cref="Release" /> when finished</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the information is available and passed out in OutAchievement
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.NotFound" /> if the player achievement is not found
		/// <see cref="Result.InvalidProductUserID" /> if you pass an invalid user ID
		/// </returns>
		public Result CopyPlayerAchievementByIndex(CopyPlayerAchievementByIndexOptions options, out PlayerAchievement outAchievement)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<CopyPlayerAchievementByIndexOptionsInternal, CopyPlayerAchievementByIndexOptions>(ref optionsAddress, options);

			var outAchievementAddress = System.IntPtr.Zero;

			var funcResult = EOS_Achievements_CopyPlayerAchievementByIndex(InnerHandle, optionsAddress, ref outAchievementAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			if (Helper.TryMarshalGet<PlayerAchievementInternal, PlayerAchievement>(outAchievementAddress, out outAchievement))
			{
				EOS_Achievements_PlayerAchievement_Release(outAchievementAddress);
			}

			return funcResult;
		}

		/// <summary>
		/// DEPRECATED! Use <see cref="CopyPlayerAchievementByAchievementId" /> instead.
		/// 
		/// Fetches an unlocked achievement from a given achievement ID.
		/// <seealso cref="Release" />
		/// </summary>
		/// <param name="options">Structure containing the Epic Online Services Account ID and achievement ID being accessed</param>
		/// <param name="outAchievement">The unlocked achievement data for the given achievement ID, if it exists and is valid, use <see cref="Release" /> when finished</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the information is available and passed out in OutAchievement
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.NotFound" /> if the unlocked achievement is not found
		/// </returns>
		public Result CopyUnlockedAchievementByAchievementId(CopyUnlockedAchievementByAchievementIdOptions options, out UnlockedAchievement outAchievement)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<CopyUnlockedAchievementByAchievementIdOptionsInternal, CopyUnlockedAchievementByAchievementIdOptions>(ref optionsAddress, options);

			var outAchievementAddress = System.IntPtr.Zero;

			var funcResult = EOS_Achievements_CopyUnlockedAchievementByAchievementId(InnerHandle, optionsAddress, ref outAchievementAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			if (Helper.TryMarshalGet<UnlockedAchievementInternal, UnlockedAchievement>(outAchievementAddress, out outAchievement))
			{
				EOS_Achievements_UnlockedAchievement_Release(outAchievementAddress);
			}

			return funcResult;
		}

		/// <summary>
		/// DEPRECATED! Use <see cref="CopyPlayerAchievementByAchievementId" /> instead.
		/// 
		/// Fetches an unlocked achievement from a given index.
		/// <seealso cref="Release" />
		/// </summary>
		/// <param name="options">Structure containing the Epic Online Services Account ID and index being accessed</param>
		/// <param name="outAchievement">The unlocked achievement data for the given index, if it exists and is valid, use <see cref="Release" /> when finished</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the information is available and passed out in OutAchievement
		/// <see cref="Result.InvalidParameters" /> if you pass a null pointer for the out parameter
		/// <see cref="Result.NotFound" /> if the unlocked achievement is not found
		/// </returns>
		public Result CopyUnlockedAchievementByIndex(CopyUnlockedAchievementByIndexOptions options, out UnlockedAchievement outAchievement)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<CopyUnlockedAchievementByIndexOptionsInternal, CopyUnlockedAchievementByIndexOptions>(ref optionsAddress, options);

			var outAchievementAddress = System.IntPtr.Zero;

			var funcResult = EOS_Achievements_CopyUnlockedAchievementByIndex(InnerHandle, optionsAddress, ref outAchievementAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			if (Helper.TryMarshalGet<UnlockedAchievementInternal, UnlockedAchievement>(outAchievementAddress, out outAchievement))
			{
				EOS_Achievements_UnlockedAchievement_Release(outAchievementAddress);
			}

			return funcResult;
		}

		/// <summary>
		/// Fetch the number of achievement definitions that are cached locally.
		/// <seealso cref="CopyAchievementDefinitionByIndex" />
		/// </summary>
		/// <param name="options">The Options associated with retrieving the achievement definition count</param>
		/// <returns>
		/// Number of achievement definitions or 0 if there is an error
		/// </returns>
		public uint GetAchievementDefinitionCount(GetAchievementDefinitionCountOptions options)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<GetAchievementDefinitionCountOptionsInternal, GetAchievementDefinitionCountOptions>(ref optionsAddress, options);

			var funcResult = EOS_Achievements_GetAchievementDefinitionCount(InnerHandle, optionsAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			return funcResult;
		}

		/// <summary>
		/// Fetch the number of player achievements that are cached locally.
		/// <seealso cref="CopyPlayerAchievementByIndex" />
		/// </summary>
		/// <param name="options">The Options associated with retrieving the player achievement count</param>
		/// <returns>
		/// Number of player achievements or 0 if there is an error
		/// </returns>
		public uint GetPlayerAchievementCount(GetPlayerAchievementCountOptions options)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<GetPlayerAchievementCountOptionsInternal, GetPlayerAchievementCountOptions>(ref optionsAddress, options);

			var funcResult = EOS_Achievements_GetPlayerAchievementCount(InnerHandle, optionsAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			return funcResult;
		}

		/// <summary>
		/// DEPRECATED! Use <see cref="GetPlayerAchievementCount" />, <see cref="CopyPlayerAchievementByIndex" /> and filter for unlocked instead.
		/// 
		/// Fetch the number of unlocked achievements that are cached locally.
		/// <seealso cref="CopyUnlockedAchievementByIndex" />
		/// </summary>
		/// <param name="options">The Options associated with retrieving the unlocked achievement count</param>
		/// <returns>
		/// Number of unlocked achievements or 0 if there is an error
		/// </returns>
		public uint GetUnlockedAchievementCount(GetUnlockedAchievementCountOptions options)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<GetUnlockedAchievementCountOptionsInternal, GetUnlockedAchievementCountOptions>(ref optionsAddress, options);

			var funcResult = EOS_Achievements_GetUnlockedAchievementCount(InnerHandle, optionsAddress);

			Helper.TryMarshalDispose(ref optionsAddress);

			return funcResult;
		}

		/// <summary>
		/// Query for a list of definitions for all existing achievements, including localized text, icon IDs and whether an achievement is hidden.
		/// 
		/// @note When the Social Overlay is enabled then this will be called automatically. The Social Overlay is enabled by default (see <see cref="Platform.PlatformFlags.DisableSocialOverlay" />).
		/// </summary>
		/// <param name="options">Structure containing information about the application whose achievement definitions we're retrieving.</param>
		/// <param name="clientData">Arbitrary data that is passed back to you in the CompletionDelegate</param>
		/// <param name="completionDelegate">This function is called when the query definitions operation completes.</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the operation completes successfully
		/// <see cref="Result.InvalidParameters" /> if any of the options are incorrect
		/// </returns>
		public void QueryDefinitions(QueryDefinitionsOptions options, object clientData, OnQueryDefinitionsCompleteCallback completionDelegate)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<QueryDefinitionsOptionsInternal, QueryDefinitionsOptions>(ref optionsAddress, options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnQueryDefinitionsCompleteCallbackInternal(OnQueryDefinitionsCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			EOS_Achievements_QueryDefinitions(InnerHandle, optionsAddress, clientDataAddress, completionDelegateInternal);

			Helper.TryMarshalDispose(ref optionsAddress);
		}

		/// <summary>
		/// Query for a list of achievements for a specific player, including progress towards completion for each achievement.
		/// 
		/// @note When the Social Overlay is enabled then this will be called automatically. The Social Overlay is enabled by default (see <see cref="Platform.PlatformFlags.DisableSocialOverlay" />).
		/// </summary>
		/// <param name="options">Structure containing information about the player whose achievements we're retrieving.</param>
		/// <param name="clientData">Arbitrary data that is passed back to you in the CompletionDelegate</param>
		/// <param name="completionDelegate">This function is called when the query player achievements operation completes.</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the operation completes successfully
		/// <see cref="Result.InvalidProductUserID" /> if any of the userid options are incorrect
		/// <see cref="Result.InvalidParameters" /> if any of the other options are incorrect
		/// </returns>
		public void QueryPlayerAchievements(QueryPlayerAchievementsOptions options, object clientData, OnQueryPlayerAchievementsCompleteCallback completionDelegate)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<QueryPlayerAchievementsOptionsInternal, QueryPlayerAchievementsOptions>(ref optionsAddress, options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnQueryPlayerAchievementsCompleteCallbackInternal(OnQueryPlayerAchievementsCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			EOS_Achievements_QueryPlayerAchievements(InnerHandle, optionsAddress, clientDataAddress, completionDelegateInternal);

			Helper.TryMarshalDispose(ref optionsAddress);
		}

		/// <summary>
		/// Unregister from receiving achievement unlocked notifications.
		/// <seealso cref="AddNotifyAchievementsUnlocked" />
		/// </summary>
		/// <param name="inId">Handle representing the registered callback</param>
		public void RemoveNotifyAchievementsUnlocked(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);

			EOS_Achievements_RemoveNotifyAchievementsUnlocked(InnerHandle, inId);
		}

		/// <summary>
		/// Unlocks a number of achievements for a specific player.
		/// </summary>
		/// <param name="options">Structure containing information about the achievements and the player whose achievements we're unlocking.</param>
		/// <param name="clientData">Arbitrary data that is passed back to you in the CompletionDelegate</param>
		/// <param name="completionDelegate">This function is called when the unlock achievements operation completes.</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the operation completes successfully
		/// <see cref="Result.InvalidParameters" /> if any of the options are incorrect
		/// </returns>
		public void UnlockAchievements(UnlockAchievementsOptions options, object clientData, OnUnlockAchievementsCompleteCallback completionDelegate)
		{
			System.IntPtr optionsAddress = new System.IntPtr();
			Helper.TryMarshalSet<UnlockAchievementsOptionsInternal, UnlockAchievementsOptions>(ref optionsAddress, options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnUnlockAchievementsCompleteCallbackInternal(OnUnlockAchievementsCompleteCallbackInternalImplementation);
			Helper.AddCallback(ref clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			EOS_Achievements_UnlockAchievements(InnerHandle, optionsAddress, clientDataAddress, completionDelegateInternal);

			Helper.TryMarshalDispose(ref optionsAddress);
		}

		[MonoPInvokeCallback(typeof(OnAchievementsUnlockedCallbackInternal))]
		internal static void OnAchievementsUnlockedCallbackInternalImplementation(System.IntPtr data)
		{
			OnAchievementsUnlockedCallback callback;
			OnAchievementsUnlockedCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback<OnAchievementsUnlockedCallback, OnAchievementsUnlockedCallbackInfoInternal, OnAchievementsUnlockedCallbackInfo>(data, out callback, out callbackInfo))
			{
				callback(callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnAchievementsUnlockedCallbackV2Internal))]
		internal static void OnAchievementsUnlockedCallbackV2InternalImplementation(System.IntPtr data)
		{
			OnAchievementsUnlockedCallbackV2 callback;
			OnAchievementsUnlockedCallbackV2Info callbackInfo;
			if (Helper.TryGetAndRemoveCallback<OnAchievementsUnlockedCallbackV2, OnAchievementsUnlockedCallbackV2InfoInternal, OnAchievementsUnlockedCallbackV2Info>(data, out callback, out callbackInfo))
			{
				callback(callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnQueryDefinitionsCompleteCallbackInternal))]
		internal static void OnQueryDefinitionsCompleteCallbackInternalImplementation(System.IntPtr data)
		{
			OnQueryDefinitionsCompleteCallback callback;
			OnQueryDefinitionsCompleteCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback<OnQueryDefinitionsCompleteCallback, OnQueryDefinitionsCompleteCallbackInfoInternal, OnQueryDefinitionsCompleteCallbackInfo>(data, out callback, out callbackInfo))
			{
				callback(callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnQueryPlayerAchievementsCompleteCallbackInternal))]
		internal static void OnQueryPlayerAchievementsCompleteCallbackInternalImplementation(System.IntPtr data)
		{
			OnQueryPlayerAchievementsCompleteCallback callback;
			OnQueryPlayerAchievementsCompleteCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback<OnQueryPlayerAchievementsCompleteCallback, OnQueryPlayerAchievementsCompleteCallbackInfoInternal, OnQueryPlayerAchievementsCompleteCallbackInfo>(data, out callback, out callbackInfo))
			{
				callback(callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnUnlockAchievementsCompleteCallbackInternal))]
		internal static void OnUnlockAchievementsCompleteCallbackInternalImplementation(System.IntPtr data)
		{
			OnUnlockAchievementsCompleteCallback callback;
			OnUnlockAchievementsCompleteCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback<OnUnlockAchievementsCompleteCallback, OnUnlockAchievementsCompleteCallbackInfoInternal, OnUnlockAchievementsCompleteCallbackInfo>(data, out callback, out callbackInfo))
			{
				callback(callbackInfo);
			}
		}

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern ulong EOS_Achievements_AddNotifyAchievementsUnlocked(System.IntPtr handle, System.IntPtr options, System.IntPtr clientData, OnAchievementsUnlockedCallbackInternal notificationFn);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern ulong EOS_Achievements_AddNotifyAchievementsUnlockedV2(System.IntPtr handle, System.IntPtr options, System.IntPtr clientData, OnAchievementsUnlockedCallbackV2Internal notificationFn);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern Result EOS_Achievements_CopyAchievementDefinitionByAchievementId(System.IntPtr handle, System.IntPtr options, ref System.IntPtr outDefinition);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern Result EOS_Achievements_CopyAchievementDefinitionByIndex(System.IntPtr handle, System.IntPtr options, ref System.IntPtr outDefinition);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern Result EOS_Achievements_CopyAchievementDefinitionV2ByAchievementId(System.IntPtr handle, System.IntPtr options, ref System.IntPtr outDefinition);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern Result EOS_Achievements_CopyAchievementDefinitionV2ByIndex(System.IntPtr handle, System.IntPtr options, ref System.IntPtr outDefinition);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern Result EOS_Achievements_CopyPlayerAchievementByAchievementId(System.IntPtr handle, System.IntPtr options, ref System.IntPtr outAchievement);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern Result EOS_Achievements_CopyPlayerAchievementByIndex(System.IntPtr handle, System.IntPtr options, ref System.IntPtr outAchievement);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern Result EOS_Achievements_CopyUnlockedAchievementByAchievementId(System.IntPtr handle, System.IntPtr options, ref System.IntPtr outAchievement);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern Result EOS_Achievements_CopyUnlockedAchievementByIndex(System.IntPtr handle, System.IntPtr options, ref System.IntPtr outAchievement);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern uint EOS_Achievements_GetAchievementDefinitionCount(System.IntPtr handle, System.IntPtr options);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern uint EOS_Achievements_GetPlayerAchievementCount(System.IntPtr handle, System.IntPtr options);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern uint EOS_Achievements_GetUnlockedAchievementCount(System.IntPtr handle, System.IntPtr options);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern void EOS_Achievements_QueryDefinitions(System.IntPtr handle, System.IntPtr options, System.IntPtr clientData, OnQueryDefinitionsCompleteCallbackInternal completionDelegate);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern void EOS_Achievements_QueryPlayerAchievements(System.IntPtr handle, System.IntPtr options, System.IntPtr clientData, OnQueryPlayerAchievementsCompleteCallbackInternal completionDelegate);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern void EOS_Achievements_DefinitionV2_Release(System.IntPtr achievementDefinition);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern void EOS_Achievements_PlayerAchievement_Release(System.IntPtr achievement);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern void EOS_Achievements_Definition_Release(System.IntPtr achievementDefinition);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern void EOS_Achievements_UnlockedAchievement_Release(System.IntPtr achievement);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern void EOS_Achievements_RemoveNotifyAchievementsUnlocked(System.IntPtr handle, ulong inId);

		[System.Runtime.InteropServices.DllImport(Config.BinaryName)]
		internal static extern void EOS_Achievements_UnlockAchievements(System.IntPtr handle, System.IntPtr options, System.IntPtr clientData, OnUnlockAchievementsCompleteCallbackInternal completionDelegate);
	}
}