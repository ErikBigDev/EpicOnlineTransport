// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices
{
	/// <summary>
	/// All comparison operators associated with parameters in a search query
	/// </summary>
	public enum ComparisonOp : int
	{
		/// <summary>
		/// Value must equal the one stored on the lobby/session
		/// </summary>
		Equal = 0,
		/// <summary>
		/// Value must not equal the one stored on the lobby/session
		/// </summary>
		Notequal = 1,
		/// <summary>
		/// Value must be strictly greater than the one stored on the lobby/session
		/// </summary>
		Greaterthan = 2,
		/// <summary>
		/// Value must be greater than or equal to the one stored on the lobby/session
		/// </summary>
		Greaterthanorequal = 3,
		/// <summary>
		/// Value must be strictly less than the one stored on the lobby/session
		/// </summary>
		Lessthan = 4,
		/// <summary>
		/// Value must be less than or equal to the one stored on the lobby/session
		/// </summary>
		Lessthanorequal = 5,
		/// <summary>
		/// Prefer values nearest the one specified ie. abs(SearchValue-SessionValue) closest to 0
		/// </summary>
		Distance = 6,
		/// <summary>
		/// Value stored on the lobby/session may be any from a specified list
		/// </summary>
		Anyof = 7,
		/// <summary>
		/// Value stored on the lobby/session may NOT be any from a specified list
		/// </summary>
		Notanyof = 8,
		/// <summary>
		/// This one value is a part of a collection
		/// </summary>
		Oneof = 9,
		/// <summary>
		/// This one value is NOT part of a collection
		/// </summary>
		Notoneof = 10,
		/// <summary>
		/// This value is a CASE SENSITIVE substring of an attribute stored on the lobby/session
		/// </summary>
		Contains = 11
	}
}