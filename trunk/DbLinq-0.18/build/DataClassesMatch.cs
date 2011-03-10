#region Auto-generated classes for SoccerScoreSqlite database on 2011-03-11 00:39:06Z

//
//  ____  _     __  __      _        _
// |  _ \| |__ |  \/  | ___| |_ __ _| |
// | | | | '_ \| |\/| |/ _ \ __/ _` | |
// | |_| | |_) | |  | |  __/ || (_| | |
// |____/|_.__/|_|  |_|\___|\__\__,_|_|
//
// Auto-generated from SoccerScoreSqlite on 2011-03-11 00:39:06Z
// Please visit http://linq.to/db for more information

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using DbLinq.Data.Linq;
using DbLinq.Linq;
using DbLinq.Linq.Mapping;

namespace Soccer_Score_Forecast.LinqSql
{
	public partial class SoccerScoreSqlite : DbLinq.Data.Linq.DataContext
	{
		public SoccerScoreSqlite(System.Data.IDbConnection connection)
		: base(connection, new DbLinq.Sqlite.SqliteVendor())
		{
		}

		public SoccerScoreSqlite(System.Data.IDbConnection connection, DbLinq.Vendor.IVendor vendor)
		: base(connection, vendor)
		{
		}

		public Table<LiveAibo> LiveAibo { get { return GetTable<LiveAibo>(); } }
		public Table<LiveOkOO> LiveOkOO { get { return GetTable<LiveOkOO>(); } }
		public Table<LiveTable> LiveTable { get { return GetTable<LiveTable>(); } }
		public Table<LiveTableLib> LiveTableLib { get { return GetTable<LiveTableLib>(); } }
		public Table<MatchAnalysisCollection> MatchAnalysisCollection { get { return GetTable<MatchAnalysisCollection>(); } }
		public Table<MatchAnalysisResult> MatchAnalysisResult { get { return GetTable<MatchAnalysisResult>(); } }
		public Table<MatchTableXPath> MatchTableXPath { get { return GetTable<MatchTableXPath>(); } }
		public Table<ResultTB> ResultTB { get { return GetTable<ResultTB>(); } }
		public Table<ResultTBLib> ResultTBLib { get { return GetTable<ResultTBLib>(); } }

	}

	[Table(Name = "main.live_Aibo")]
	public partial class LiveAibo
	{
		#region string LeagueName

		private string _leagueName;
		[DebuggerNonUserCode]
		[Column(Storage = "_leagueName", Name = "LeagueName", DbType = "nvarchar(50)")]
		public string LeagueName
		{
			get
			{
				return _leagueName;
			}
			set
			{
				if (value != _leagueName)
				{
					_leagueName = value;
				}
			}
		}

		#endregion

		#region int LiveAiboID

		private int _liveAiboID;
		[DebuggerNonUserCode]
		[Column(Storage = "_liveAiboID", Name = "live_Aibo_id", DbType = "integer", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int LiveAiboID
		{
			get
			{
				return _liveAiboID;
			}
			set
			{
				if (value != _liveAiboID)
				{
					_liveAiboID = value;
				}
			}
		}

		#endregion

		#region string MatchOrder1aWayName

		private string _matchOrder1aWayName;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchOrder1aWayName", Name = "MatchOrder1_AwayName", DbType = "nvarchar(50)")]
		public string MatchOrder1aWayName
		{
			get
			{
				return _matchOrder1aWayName;
			}
			set
			{
				if (value != _matchOrder1aWayName)
				{
					_matchOrder1aWayName = value;
				}
			}
		}

		#endregion

		#region string MatchOrder1hAndicapNumber

		private string _matchOrder1hAndicapNumber;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchOrder1hAndicapNumber", Name = "MatchOrder1_HandicapNumber", DbType = "nvarchar(50)")]
		public string MatchOrder1hAndicapNumber
		{
			get
			{
				return _matchOrder1hAndicapNumber;
			}
			set
			{
				if (value != _matchOrder1hAndicapNumber)
				{
					_matchOrder1hAndicapNumber = value;
				}
			}
		}

		#endregion

		#region string MatchOrder1hOmeName

		private string _matchOrder1hOmeName;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchOrder1hOmeName", Name = "MatchOrder1_HomeName", DbType = "nvarchar(50)")]
		public string MatchOrder1hOmeName
		{
			get
			{
				return _matchOrder1hOmeName;
			}
			set
			{
				if (value != _matchOrder1hOmeName)
				{
					_matchOrder1hOmeName = value;
				}
			}
		}

		#endregion

		#region string MatchTime

		private string _matchTime;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchTime", Name = "match_time", DbType = "nvarchar(50)")]
		public string MatchTime
		{
			get
			{
				return _matchTime;
			}
			set
			{
				if (value != _matchTime)
				{
					_matchTime = value;
				}
			}
		}

		#endregion

		#region string Value

		private string _value;
		[DebuggerNonUserCode]
		[Column(Storage = "_value", Name = "value", DbType = "nvarchar(50)")]
		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				if (value != _value)
				{
					_value = value;
				}
			}
		}

		#endregion

	}

	[Table(Name = "main.live_okoo")]
	public partial class LiveOkOO
	{
		#region string LeagueName

		private string _leagueName;
		[DebuggerNonUserCode]
		[Column(Storage = "_leagueName", Name = "LeagueName", DbType = "nvarchar(50)")]
		public string LeagueName
		{
			get
			{
				return _leagueName;
			}
			set
			{
				if (value != _leagueName)
				{
					_leagueName = value;
				}
			}
		}

		#endregion

		#region int LiveOkOOID

		private int _liveOkOoid;
		[DebuggerNonUserCode]
		[Column(Storage = "_liveOkOoid", Name = "live_okoo_id", DbType = "integer", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int LiveOkOOID
		{
			get
			{
				return _liveOkOoid;
			}
			set
			{
				if (value != _liveOkOoid)
				{
					_liveOkOoid = value;
				}
			}
		}

		#endregion

		#region string Match1dRawn

		private string _match1dRawn;
		[DebuggerNonUserCode]
		[Column(Storage = "_match1dRawn", Name = "Match_1_Drawn", DbType = "nvarchar(50)")]
		public string Match1dRawn
		{
			get
			{
				return _match1dRawn;
			}
			set
			{
				if (value != _match1dRawn)
				{
					_match1dRawn = value;
				}
			}
		}

		#endregion

		#region string Match1lOst

		private string _match1lOst;
		[DebuggerNonUserCode]
		[Column(Storage = "_match1lOst", Name = "Match_1_Lost", DbType = "nvarchar(50)")]
		public string Match1lOst
		{
			get
			{
				return _match1lOst;
			}
			set
			{
				if (value != _match1lOst)
				{
					_match1lOst = value;
				}
			}
		}

		#endregion

		#region string Match1wIn

		private string _match1wIn;
		[DebuggerNonUserCode]
		[Column(Storage = "_match1wIn", Name = "Match_1_Win", DbType = "nvarchar(50)")]
		public string Match1wIn
		{
			get
			{
				return _match1wIn;
			}
			set
			{
				if (value != _match1wIn)
				{
					_match1wIn = value;
				}
			}
		}

		#endregion

		#region string MatchInfo

		private string _matchInfo;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchInfo", Name = "MatchInfo", DbType = "nvarchar(50)")]
		public string MatchInfo
		{
			get
			{
				return _matchInfo;
			}
			set
			{
				if (value != _matchInfo)
				{
					_matchInfo = value;
				}
			}
		}

		#endregion

		#region string MatchOrder1aWayName

		private string _matchOrder1aWayName;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchOrder1aWayName", Name = "MatchOrder1_AwayName", DbType = "nvarchar(50)")]
		public string MatchOrder1aWayName
		{
			get
			{
				return _matchOrder1aWayName;
			}
			set
			{
				if (value != _matchOrder1aWayName)
				{
					_matchOrder1aWayName = value;
				}
			}
		}

		#endregion

		#region string MatchOrder1hAndicapNumber

		private string _matchOrder1hAndicapNumber;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchOrder1hAndicapNumber", Name = "MatchOrder1_HandicapNumber", DbType = "nvarchar(50)")]
		public string MatchOrder1hAndicapNumber
		{
			get
			{
				return _matchOrder1hAndicapNumber;
			}
			set
			{
				if (value != _matchOrder1hAndicapNumber)
				{
					_matchOrder1hAndicapNumber = value;
				}
			}
		}

		#endregion

		#region string MatchOrder1hOmeName

		private string _matchOrder1hOmeName;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchOrder1hOmeName", Name = "MatchOrder1_HomeName", DbType = "nvarchar(50)")]
		public string MatchOrder1hOmeName
		{
			get
			{
				return _matchOrder1hOmeName;
			}
			set
			{
				if (value != _matchOrder1hOmeName)
				{
					_matchOrder1hOmeName = value;
				}
			}
		}

		#endregion

		#region string MatchTime

		private string _matchTime;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchTime", Name = "match_time", DbType = "nvarchar(50)")]
		public string MatchTime
		{
			get
			{
				return _matchTime;
			}
			set
			{
				if (value != _matchTime)
				{
					_matchTime = value;
				}
			}
		}

		#endregion

		#region string Ok10

		private string _ok10;
		[DebuggerNonUserCode]
		[Column(Storage = "_ok10", Name = "ok_1_0", DbType = "nvarchar(50)")]
		public string Ok10
		{
			get
			{
				return _ok10;
			}
			set
			{
				if (value != _ok10)
				{
					_ok10 = value;
				}
			}
		}

		#endregion

		#region string Ok11

		private string _ok11;
		[DebuggerNonUserCode]
		[Column(Storage = "_ok11", Name = "ok_1_1", DbType = "nvarchar(50)")]
		public string Ok11
		{
			get
			{
				return _ok11;
			}
			set
			{
				if (value != _ok11)
				{
					_ok11 = value;
				}
			}
		}

		#endregion

		#region string Ok12

		private string _ok12;
		[DebuggerNonUserCode]
		[Column(Storage = "_ok12", Name = "ok_1_2", DbType = "nvarchar(50)")]
		public string Ok12
		{
			get
			{
				return _ok12;
			}
			set
			{
				if (value != _ok12)
				{
					_ok12 = value;
				}
			}
		}

		#endregion

		#region int? Value

		private int? _value;
		[DebuggerNonUserCode]
		[Column(Storage = "_value", Name = "value", DbType = "integer")]
		public int? Value
		{
			get
			{
				return _value;
			}
			set
			{
				if (value != _value)
				{
					_value = value;
				}
			}
		}

		#endregion

	}

	[Table(Name = "main.live_Table")]
	public partial class LiveTable
	{
		#region string AwayTeam

		private string _awayTeam;
		[DebuggerNonUserCode]
		[Column(Storage = "_awayTeam", Name = "away_team", DbType = "nvarchar(50)")]
		public string AwayTeam
		{
			get
			{
				return _awayTeam;
			}
			set
			{
				if (value != _awayTeam)
				{
					_awayTeam = value;
				}
			}
		}

		#endregion

		#region string AwayTeamBig

		private string _awayTeamBig;
		[DebuggerNonUserCode]
		[Column(Storage = "_awayTeamBig", Name = "away_team_big", DbType = "nvarchar(50)")]
		public string AwayTeamBig
		{
			get
			{
				return _awayTeamBig;
			}
			set
			{
				if (value != _awayTeamBig)
				{
					_awayTeamBig = value;
				}
			}
		}

		#endregion

		#region string FullTimeScore

		private string _fullTimeScore;
		[DebuggerNonUserCode]
		[Column(Storage = "_fullTimeScore", Name = "full_time_score", DbType = "nvarchar(50)")]
		public string FullTimeScore
		{
			get
			{
				return _fullTimeScore;
			}
			set
			{
				if (value != _fullTimeScore)
				{
					_fullTimeScore = value;
				}
			}
		}

		#endregion

		#region string HalfTimeScore

		private string _halfTimeScore;
		[DebuggerNonUserCode]
		[Column(Storage = "_halfTimeScore", Name = "half_time_score", DbType = "nvarchar(50)")]
		public string HalfTimeScore
		{
			get
			{
				return _halfTimeScore;
			}
			set
			{
				if (value != _halfTimeScore)
				{
					_halfTimeScore = value;
				}
			}
		}

		#endregion

		#region string HomeTeam

		private string _homeTeam;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeTeam", Name = "home_team", DbType = "nvarchar(50)")]
		public string HomeTeam
		{
			get
			{
				return _homeTeam;
			}
			set
			{
				if (value != _homeTeam)
				{
					_homeTeam = value;
				}
			}
		}

		#endregion

		#region string HomeTeamBig

		private string _homeTeamBig;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeTeamBig", Name = "home_team_big", DbType = "nvarchar(50)")]
		public string HomeTeamBig
		{
			get
			{
				return _homeTeamBig;
			}
			set
			{
				if (value != _homeTeamBig)
				{
					_homeTeamBig = value;
				}
			}
		}

		#endregion

		#region string HTMLPosition

		private string _htmlpOsition;
		[DebuggerNonUserCode]
		[Column(Storage = "_htmlpOsition", Name = "html_position", DbType = "nvarchar(50)")]
		public string HTMLPosition
		{
			get
			{
				return _htmlpOsition;
			}
			set
			{
				if (value != _htmlpOsition)
				{
					_htmlpOsition = value;
				}
			}
		}

		#endregion

		#region int LiveTableID

		private int _liveTableID;
		[DebuggerNonUserCode]
		[Column(Storage = "_liveTableID", Name = "live_table_id", DbType = "integer", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int LiveTableID
		{
			get
			{
				return _liveTableID;
			}
			set
			{
				if (value != _liveTableID)
				{
					_liveTableID = value;
				}
			}
		}

		#endregion

		#region string MatchType

		private string _matchType;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchType", Name = "match_type", DbType = "nvarchar(50)")]
		public string MatchType
		{
			get
			{
				return _matchType;
			}
			set
			{
				if (value != _matchType)
				{
					_matchType = value;
				}
			}
		}

		#endregion

		#region string SDate

		private string _sdAte;
		[DebuggerNonUserCode]
		[Column(Storage = "_sdAte", Name = "s_date", DbType = "nvarchar(50)")]
		public string SDate
		{
			get
			{
				return _sdAte;
			}
			set
			{
				if (value != _sdAte)
				{
					_sdAte = value;
				}
			}
		}

		#endregion

		#region string Status

		private string _status;
		[DebuggerNonUserCode]
		[Column(Storage = "_status", Name = "status", DbType = "nvarchar(50)")]
		public string Status
		{
			get
			{
				return _status;
			}
			set
			{
				if (value != _status)
				{
					_status = value;
				}
			}
		}

		#endregion

		#region string STime

		private string _stIme;
		[DebuggerNonUserCode]
		[Column(Storage = "_stIme", Name = "s_time", DbType = "nvarchar(50)")]
		public string STime
		{
			get
			{
				return _stIme;
			}
			set
			{
				if (value != _stIme)
				{
					_stIme = value;
				}
			}
		}

		#endregion

	}

	[Table(Name = "main.live_Table_lib")]
	public partial class LiveTableLib
	{
		#region int? AwayRedCard

		private int? _awayRedCard;
		[DebuggerNonUserCode]
		[Column(Storage = "_awayRedCard", Name = "away_red_card", DbType = "integer")]
		public int? AwayRedCard
		{
			get
			{
				return _awayRedCard;
			}
			set
			{
				if (value != _awayRedCard)
				{
					_awayRedCard = value;
				}
			}
		}

		#endregion

		#region string AwayTeam

		private string _awayTeam;
		[DebuggerNonUserCode]
		[Column(Storage = "_awayTeam", Name = "away_team", DbType = "nvarchar(50)")]
		public string AwayTeam
		{
			get
			{
				return _awayTeam;
			}
			set
			{
				if (value != _awayTeam)
				{
					_awayTeam = value;
				}
			}
		}

		#endregion

		#region int? AwayTeamBig

		private int? _awayTeamBig;
		[DebuggerNonUserCode]
		[Column(Storage = "_awayTeamBig", Name = "away_team_big", DbType = "integer")]
		public int? AwayTeamBig
		{
			get
			{
				return _awayTeamBig;
			}
			set
			{
				if (value != _awayTeamBig)
				{
					_awayTeamBig = value;
				}
			}
		}

		#endregion

		#region int? FullAwayGoals

		private int? _fullAwayGoals;
		[DebuggerNonUserCode]
		[Column(Storage = "_fullAwayGoals", Name = "full_away_goals", DbType = "integer")]
		public int? FullAwayGoals
		{
			get
			{
				return _fullAwayGoals;
			}
			set
			{
				if (value != _fullAwayGoals)
				{
					_fullAwayGoals = value;
				}
			}
		}

		#endregion

		#region int? FullHomeGoals

		private int? _fullHomeGoals;
		[DebuggerNonUserCode]
		[Column(Storage = "_fullHomeGoals", Name = "full_home_goals", DbType = "integer")]
		public int? FullHomeGoals
		{
			get
			{
				return _fullHomeGoals;
			}
			set
			{
				if (value != _fullHomeGoals)
				{
					_fullHomeGoals = value;
				}
			}
		}

		#endregion

		#region int? HalfAwayGoals

		private int? _halfAwayGoals;
		[DebuggerNonUserCode]
		[Column(Storage = "_halfAwayGoals", Name = "half_away_goals", DbType = "integer")]
		public int? HalfAwayGoals
		{
			get
			{
				return _halfAwayGoals;
			}
			set
			{
				if (value != _halfAwayGoals)
				{
					_halfAwayGoals = value;
				}
			}
		}

		#endregion

		#region int? HalfHomeGoals

		private int? _halfHomeGoals;
		[DebuggerNonUserCode]
		[Column(Storage = "_halfHomeGoals", Name = "half_home_goals", DbType = "integer")]
		public int? HalfHomeGoals
		{
			get
			{
				return _halfHomeGoals;
			}
			set
			{
				if (value != _halfHomeGoals)
				{
					_halfHomeGoals = value;
				}
			}
		}

		#endregion

		#region int? HomeRedCard

		private int? _homeRedCard;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeRedCard", Name = "home_red_card", DbType = "integer")]
		public int? HomeRedCard
		{
			get
			{
				return _homeRedCard;
			}
			set
			{
				if (value != _homeRedCard)
				{
					_homeRedCard = value;
				}
			}
		}

		#endregion

		#region string HomeTeam

		private string _homeTeam;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeTeam", Name = "home_team", DbType = "nvarchar(50)")]
		public string HomeTeam
		{
			get
			{
				return _homeTeam;
			}
			set
			{
				if (value != _homeTeam)
				{
					_homeTeam = value;
				}
			}
		}

		#endregion

		#region int? HomeTeamBig

		private int? _homeTeamBig;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeTeamBig", Name = "home_team_big", DbType = "integer")]
		public int? HomeTeamBig
		{
			get
			{
				return _homeTeamBig;
			}
			set
			{
				if (value != _homeTeamBig)
				{
					_homeTeamBig = value;
				}
			}
		}

		#endregion

		#region int? HTMLPosition

		private int? _htmlpOsition;
		[DebuggerNonUserCode]
		[Column(Storage = "_htmlpOsition", Name = "html_position", DbType = "integer")]
		public int? HTMLPosition
		{
			get
			{
				return _htmlpOsition;
			}
			set
			{
				if (value != _htmlpOsition)
				{
					_htmlpOsition = value;
				}
			}
		}

		#endregion

		#region int LiveTableLibID

		private int _liveTableLibID;
		[DebuggerNonUserCode]
		[Column(Storage = "_liveTableLibID", Name = "live_table_lib_id", DbType = "integer", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int LiveTableLibID
		{
			get
			{
				return _liveTableLibID;
			}
			set
			{
				if (value != _liveTableLibID)
				{
					_liveTableLibID = value;
				}
			}
		}

		#endregion

		#region System.DateTime? MatchTime

		private System.DateTime? _matchTime;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchTime", Name = "match_time", DbType = "datetime")]
		public System.DateTime? MatchTime
		{
			get
			{
				return _matchTime;
			}
			set
			{
				if (value != _matchTime)
				{
					_matchTime = value;
				}
			}
		}

		#endregion

		#region string MatchType

		private string _matchType;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchType", Name = "match_type", DbType = "nvarchar(50)")]
		public string MatchType
		{
			get
			{
				return _matchType;
			}
			set
			{
				if (value != _matchType)
				{
					_matchType = value;
				}
			}
		}

		#endregion

		#region string Status

		private string _status;
		[DebuggerNonUserCode]
		[Column(Storage = "_status", Name = "status", DbType = "nvarchar(50)")]
		public string Status
		{
			get
			{
				return _status;
			}
			set
			{
				if (value != _status)
				{
					_status = value;
				}
			}
		}

		#endregion

	}

	[Table(Name = "main.match_analysis_collection")]
	public partial class MatchAnalysisCollection
	{
		#region int AnalysisCollectionID

		private int _analysisCollectionID;
		[DebuggerNonUserCode]
		[Column(Storage = "_analysisCollectionID", Name = "analysis_collection_id", DbType = "integer", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int AnalysisCollectionID
		{
			get
			{
				return _analysisCollectionID;
			}
			set
			{
				if (value != _analysisCollectionID)
				{
					_analysisCollectionID = value;
				}
			}
		}

		#endregion

		#region int? LiveTableLibID

		private int? _liveTableLibID;
		[DebuggerNonUserCode]
		[Column(Storage = "_liveTableLibID", Name = "live_table_lib_id", DbType = "integer")]
		public int? LiveTableLibID
		{
			get
			{
				return _liveTableLibID;
			}
			set
			{
				if (value != _liveTableLibID)
				{
					_liveTableLibID = value;
				}
			}
		}

		#endregion

		#region int? ResultTBLibID

		private int? _resultTblIbID;
		[DebuggerNonUserCode]
		[Column(Storage = "_resultTblIbID", Name = "result_tb_lib_id", DbType = "integer")]
		public int? ResultTBLibID
		{
			get
			{
				return _resultTblIbID;
			}
			set
			{
				if (value != _resultTblIbID)
				{
					_resultTblIbID = value;
				}
			}
		}

		#endregion

	}

	[Table(Name = "main.match_analysis_result")]
	public partial class MatchAnalysisResult
	{
		#region int AnalysisResultID

		private int _analysisResultID;
		[DebuggerNonUserCode]
		[Column(Storage = "_analysisResultID", Name = "analysis_result_id", DbType = "integer", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int AnalysisResultID
		{
			get
			{
				return _analysisResultID;
			}
			set
			{
				if (value != _analysisResultID)
				{
					_analysisResultID = value;
				}
			}
		}

		#endregion

		#region float? AwayGoals

		private float? _awayGoals;
		[DebuggerNonUserCode]
		[Column(Storage = "_awayGoals", Name = "away_goals", DbType = "float")]
		public float? AwayGoals
		{
			get
			{
				return _awayGoals;
			}
			set
			{
				if (value != _awayGoals)
				{
					_awayGoals = value;
				}
			}
		}

		#endregion

		#region float? FitGoals

		private float? _fitGoals;
		[DebuggerNonUserCode]
		[Column(Storage = "_fitGoals", Name = "fit_goals", DbType = "float")]
		public float? FitGoals
		{
			get
			{
				return _fitGoals;
			}
			set
			{
				if (value != _fitGoals)
				{
					_fitGoals = value;
				}
			}
		}

		#endregion

		#region float? FitOddEven

		private float? _fitOddEven;
		[DebuggerNonUserCode]
		[Column(Storage = "_fitOddEven", Name = "fit_odd_even", DbType = "float")]
		public float? FitOddEven
		{
			get
			{
				return _fitOddEven;
			}
			set
			{
				if (value != _fitOddEven)
				{
					_fitOddEven = value;
				}
			}
		}

		#endregion

		#region float? FitWinLoss

		private float? _fitWinLoss;
		[DebuggerNonUserCode]
		[Column(Storage = "_fitWinLoss", Name = "fit_win_loss", DbType = "float")]
		public float? FitWinLoss
		{
			get
			{
				return _fitWinLoss;
			}
			set
			{
				if (value != _fitWinLoss)
				{
					_fitWinLoss = value;
				}
			}
		}

		#endregion

		#region int? HomeD

		private int? _homeD;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeD", Name = "home_d", DbType = "integer")]
		public int? HomeD
		{
			get
			{
				return _homeD;
			}
			set
			{
				if (value != _homeD)
				{
					_homeD = value;
				}
			}
		}

		#endregion

		#region float? HomeGoals

		private float? _homeGoals;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeGoals", Name = "home_goals", DbType = "float")]
		public float? HomeGoals
		{
			get
			{
				return _homeGoals;
			}
			set
			{
				if (value != _homeGoals)
				{
					_homeGoals = value;
				}
			}
		}

		#endregion

		#region int? HomeL

		private int? _homeL;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeL", Name = "home_l", DbType = "integer")]
		public int? HomeL
		{
			get
			{
				return _homeL;
			}
			set
			{
				if (value != _homeL)
				{
					_homeL = value;
				}
			}
		}

		#endregion

		#region int? HomeW

		private int? _homeW;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeW", Name = "home_w", DbType = "integer")]
		public int? HomeW
		{
			get
			{
				return _homeW;
			}
			set
			{
				if (value != _homeW)
				{
					_homeW = value;
				}
			}
		}

		#endregion

		#region decimal LiveTableLibID

		private decimal _liveTableLibID;
		[DebuggerNonUserCode]
		[Column(Storage = "_liveTableLibID", Name = "live_table_lib_id", DbType = "numeric", CanBeNull = false)]
		public decimal LiveTableLibID
		{
			get
			{
				return _liveTableLibID;
			}
			set
			{
				if (value != _liveTableLibID)
				{
					_liveTableLibID = value;
				}
			}
		}

		#endregion

		#region string PreAlgorithm

		private string _preAlgorithm;
		[DebuggerNonUserCode]
		[Column(Storage = "_preAlgorithm", Name = "pre_algorithm", DbType = "nvarchar(20)")]
		public string PreAlgorithm
		{
			get
			{
				return _preAlgorithm;
			}
			set
			{
				if (value != _preAlgorithm)
				{
					_preAlgorithm = value;
				}
			}
		}

		#endregion

		#region int? PreMatchCount

		private int? _preMatchCount;
		[DebuggerNonUserCode]
		[Column(Storage = "_preMatchCount", Name = "pre_match_count", DbType = "integer")]
		public int? PreMatchCount
		{
			get
			{
				return _preMatchCount;
			}
			set
			{
				if (value != _preMatchCount)
				{
					_preMatchCount = value;
				}
			}
		}

		#endregion

		#region string ResultFit

		private string _resultFit;
		[DebuggerNonUserCode]
		[Column(Storage = "_resultFit", Name = "result_fit", DbType = "nvarchar(20)")]
		public string ResultFit
		{
			get
			{
				return _resultFit;
			}
			set
			{
				if (value != _resultFit)
				{
					_resultFit = value;
				}
			}
		}

		#endregion

		#region string ResultGoals

		private string _resultGoals;
		[DebuggerNonUserCode]
		[Column(Storage = "_resultGoals", Name = "result_goals", DbType = "nvarchar(20)")]
		public string ResultGoals
		{
			get
			{
				return _resultGoals;
			}
			set
			{
				if (value != _resultGoals)
				{
					_resultGoals = value;
				}
			}
		}

		#endregion

		#region decimal? ResultTBLibID

		private decimal? _resultTblIbID;
		[DebuggerNonUserCode]
		[Column(Storage = "_resultTblIbID", Name = "result_tb_lib_id", DbType = "numeric")]
		public decimal? ResultTBLibID
		{
			get
			{
				return _resultTblIbID;
			}
			set
			{
				if (value != _resultTblIbID)
				{
					_resultTblIbID = value;
				}
			}
		}

		#endregion

		#region string ResultWDL

		private string _resultWdl;
		[DebuggerNonUserCode]
		[Column(Storage = "_resultWdl", Name = "result_wdl", DbType = "nvarchar(20)")]
		public string ResultWDL
		{
			get
			{
				return _resultWdl;
			}
			set
			{
				if (value != _resultWdl)
				{
					_resultWdl = value;
				}
			}
		}

		#endregion

	}

	[Table(Name = "main.match_table_xpath")]
	public partial class MatchTableXPath
	{
		#region string MaXTableIDValue

		private string _maXtAbleIdvAlue;
		[DebuggerNonUserCode]
		[Column(Storage = "_maXtAbleIdvAlue", Name = "max_table_id_value", DbType = "nvarchar(500)")]
		public string MaXTableIDValue
		{
			get
			{
				return _maXtAbleIdvAlue;
			}
			set
			{
				if (value != _maXtAbleIdvAlue)
				{
					_maXtAbleIdvAlue = value;
				}
			}
		}

		#endregion

		#region string MaXTableXPath

		private string _maXtAbleXpAth;
		[DebuggerNonUserCode]
		[Column(Storage = "_maXtAbleXpAth", Name = "max_table_xpath", DbType = "nvarchar(500)")]
		public string MaXTableXPath
		{
			get
			{
				return _maXtAbleXpAth;
			}
			set
			{
				if (value != _maXtAbleXpAth)
				{
					_maXtAbleXpAth = value;
				}
			}
		}

		#endregion

		#region int? OrderTableID

		private int? _orderTableID;
		[DebuggerNonUserCode]
		[Column(Storage = "_orderTableID", Name = "order_table_id", DbType = "integer")]
		public int? OrderTableID
		{
			get
			{
				return _orderTableID;
			}
			set
			{
				if (value != _orderTableID)
				{
					_orderTableID = value;
				}
			}
		}

		#endregion

		#region string SecondTableIDValue

		private string _secondTableIdvAlue;
		[DebuggerNonUserCode]
		[Column(Storage = "_secondTableIdvAlue", Name = "second_table_id_value", DbType = "nvarchar(500)")]
		public string SecondTableIDValue
		{
			get
			{
				return _secondTableIdvAlue;
			}
			set
			{
				if (value != _secondTableIdvAlue)
				{
					_secondTableIdvAlue = value;
				}
			}
		}

		#endregion

		#region string SecondTableXPath

		private string _secondTableXpAth;
		[DebuggerNonUserCode]
		[Column(Storage = "_secondTableXpAth", Name = "second_table_xpath", DbType = "nvarchar(500)")]
		public string SecondTableXPath
		{
			get
			{
				return _secondTableXpAth;
			}
			set
			{
				if (value != _secondTableXpAth)
				{
					_secondTableXpAth = value;
				}
			}
		}

		#endregion

		#region string UriHost

		private string _uriHost;
		[DebuggerNonUserCode]
		[Column(Storage = "_uriHost", Name = "uri_host", DbType = "nvarchar(500)")]
		public string UriHost
		{
			get
			{
				return _uriHost;
			}
			set
			{
				if (value != _uriHost)
				{
					_uriHost = value;
				}
			}
		}

		#endregion

		#region int UriHostID

		private int _uriHostID;
		[DebuggerNonUserCode]
		[Column(Storage = "_uriHostID", Name = "uri_host_id", DbType = "integer", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int UriHostID
		{
			get
			{
				return _uriHostID;
			}
			set
			{
				if (value != _uriHostID)
				{
					_uriHostID = value;
				}
			}
		}

		#endregion

	}

	[Table(Name = "main.result_tb")]
	public partial class ResultTB
	{
		#region string AwayTeam

		private string _awayTeam;
		[DebuggerNonUserCode]
		[Column(Storage = "_awayTeam", Name = "away_team", DbType = "nvarchar(50)")]
		public string AwayTeam
		{
			get
			{
				return _awayTeam;
			}
			set
			{
				if (value != _awayTeam)
				{
					_awayTeam = value;
				}
			}
		}

		#endregion

		#region string AwayTeamBig

		private string _awayTeamBig;
		[DebuggerNonUserCode]
		[Column(Storage = "_awayTeamBig", Name = "away_team_big", DbType = "nvarchar(50)")]
		public string AwayTeamBig
		{
			get
			{
				return _awayTeamBig;
			}
			set
			{
				if (value != _awayTeamBig)
				{
					_awayTeamBig = value;
				}
			}
		}

		#endregion

		#region string FullTimeScore

		private string _fullTimeScore;
		[DebuggerNonUserCode]
		[Column(Storage = "_fullTimeScore", Name = "full_time_score", DbType = "nvarchar(50)")]
		public string FullTimeScore
		{
			get
			{
				return _fullTimeScore;
			}
			set
			{
				if (value != _fullTimeScore)
				{
					_fullTimeScore = value;
				}
			}
		}

		#endregion

		#region string HalfTimeScore

		private string _halfTimeScore;
		[DebuggerNonUserCode]
		[Column(Storage = "_halfTimeScore", Name = "half_time_score", DbType = "nvarchar(50)")]
		public string HalfTimeScore
		{
			get
			{
				return _halfTimeScore;
			}
			set
			{
				if (value != _halfTimeScore)
				{
					_halfTimeScore = value;
				}
			}
		}

		#endregion

		#region string HomeTeam

		private string _homeTeam;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeTeam", Name = "home_team", DbType = "nvarchar(50)")]
		public string HomeTeam
		{
			get
			{
				return _homeTeam;
			}
			set
			{
				if (value != _homeTeam)
				{
					_homeTeam = value;
				}
			}
		}

		#endregion

		#region string HomeTeamBig

		private string _homeTeamBig;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeTeamBig", Name = "home_team_big", DbType = "nvarchar(50)")]
		public string HomeTeamBig
		{
			get
			{
				return _homeTeamBig;
			}
			set
			{
				if (value != _homeTeamBig)
				{
					_homeTeamBig = value;
				}
			}
		}

		#endregion

		#region string HTMLPosition

		private string _htmlpOsition;
		[DebuggerNonUserCode]
		[Column(Storage = "_htmlpOsition", Name = "html_position", DbType = "nvarchar(50)")]
		public string HTMLPosition
		{
			get
			{
				return _htmlpOsition;
			}
			set
			{
				if (value != _htmlpOsition)
				{
					_htmlpOsition = value;
				}
			}
		}

		#endregion

		#region string MatchType

		private string _matchType;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchType", Name = "match_type", DbType = "nvarchar(50)")]
		public string MatchType
		{
			get
			{
				return _matchType;
			}
			set
			{
				if (value != _matchType)
				{
					_matchType = value;
				}
			}
		}

		#endregion

		#region string Odds

		private string _odds;
		[DebuggerNonUserCode]
		[Column(Storage = "_odds", Name = "odds", DbType = "nvarchar(50)")]
		public string Odds
		{
			get
			{
				return _odds;
			}
			set
			{
				if (value != _odds)
				{
					_odds = value;
				}
			}
		}

		#endregion

		#region int ResultTBID

		private int _resultTbid;
		[DebuggerNonUserCode]
		[Column(Storage = "_resultTbid", Name = "result_tb_id", DbType = "integer", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int ResultTBID
		{
			get
			{
				return _resultTbid;
			}
			set
			{
				if (value != _resultTbid)
				{
					_resultTbid = value;
				}
			}
		}

		#endregion

		#region string SDate

		private string _sdAte;
		[DebuggerNonUserCode]
		[Column(Storage = "_sdAte", Name = "s_date", DbType = "nvarchar(50)")]
		public string SDate
		{
			get
			{
				return _sdAte;
			}
			set
			{
				if (value != _sdAte)
				{
					_sdAte = value;
				}
			}
		}

		#endregion

		#region string STime

		private string _stIme;
		[DebuggerNonUserCode]
		[Column(Storage = "_stIme", Name = "s_time", DbType = "nvarchar(50)")]
		public string STime
		{
			get
			{
				return _stIme;
			}
			set
			{
				if (value != _stIme)
				{
					_stIme = value;
				}
			}
		}

		#endregion

		#region string WinLossBig

		private string _winLossBig;
		[DebuggerNonUserCode]
		[Column(Storage = "_winLossBig", Name = "win_loss_big", DbType = "nvarchar(50)")]
		public string WinLossBig
		{
			get
			{
				return _winLossBig;
			}
			set
			{
				if (value != _winLossBig)
				{
					_winLossBig = value;
				}
			}
		}

		#endregion

	}

	[Table(Name = "main.result_tb_lib")]
	public partial class ResultTBLib
	{
		#region int? AwayRedCard

		private int? _awayRedCard;
		[DebuggerNonUserCode]
		[Column(Storage = "_awayRedCard", Name = "away_red_card", DbType = "integer")]
		public int? AwayRedCard
		{
			get
			{
				return _awayRedCard;
			}
			set
			{
				if (value != _awayRedCard)
				{
					_awayRedCard = value;
				}
			}
		}

		#endregion

		#region string AwayTeam

		private string _awayTeam;
		[DebuggerNonUserCode]
		[Column(Storage = "_awayTeam", Name = "away_team", DbType = "nvarchar(50)")]
		public string AwayTeam
		{
			get
			{
				return _awayTeam;
			}
			set
			{
				if (value != _awayTeam)
				{
					_awayTeam = value;
				}
			}
		}

		#endregion

		#region int? AwayTeamBig

		private int? _awayTeamBig;
		[DebuggerNonUserCode]
		[Column(Storage = "_awayTeamBig", Name = "away_team_big", DbType = "integer")]
		public int? AwayTeamBig
		{
			get
			{
				return _awayTeamBig;
			}
			set
			{
				if (value != _awayTeamBig)
				{
					_awayTeamBig = value;
				}
			}
		}

		#endregion

		#region int? FullAwayGoals

		private int? _fullAwayGoals;
		[DebuggerNonUserCode]
		[Column(Storage = "_fullAwayGoals", Name = "full_away_goals", DbType = "integer")]
		public int? FullAwayGoals
		{
			get
			{
				return _fullAwayGoals;
			}
			set
			{
				if (value != _fullAwayGoals)
				{
					_fullAwayGoals = value;
				}
			}
		}

		#endregion

		#region int? FullHomeGoals

		private int? _fullHomeGoals;
		[DebuggerNonUserCode]
		[Column(Storage = "_fullHomeGoals", Name = "full_home_goals", DbType = "integer")]
		public int? FullHomeGoals
		{
			get
			{
				return _fullHomeGoals;
			}
			set
			{
				if (value != _fullHomeGoals)
				{
					_fullHomeGoals = value;
				}
			}
		}

		#endregion

		#region int? HalfAwayGoals

		private int? _halfAwayGoals;
		[DebuggerNonUserCode]
		[Column(Storage = "_halfAwayGoals", Name = "half_away_goals", DbType = "integer")]
		public int? HalfAwayGoals
		{
			get
			{
				return _halfAwayGoals;
			}
			set
			{
				if (value != _halfAwayGoals)
				{
					_halfAwayGoals = value;
				}
			}
		}

		#endregion

		#region int? HalfHomeGoals

		private int? _halfHomeGoals;
		[DebuggerNonUserCode]
		[Column(Storage = "_halfHomeGoals", Name = "half_home_goals", DbType = "integer")]
		public int? HalfHomeGoals
		{
			get
			{
				return _halfHomeGoals;
			}
			set
			{
				if (value != _halfHomeGoals)
				{
					_halfHomeGoals = value;
				}
			}
		}

		#endregion

		#region int? HomeRedCard

		private int? _homeRedCard;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeRedCard", Name = "home_red_card", DbType = "integer")]
		public int? HomeRedCard
		{
			get
			{
				return _homeRedCard;
			}
			set
			{
				if (value != _homeRedCard)
				{
					_homeRedCard = value;
				}
			}
		}

		#endregion

		#region string HomeTeam

		private string _homeTeam;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeTeam", Name = "home_team", DbType = "nvarchar(50)")]
		public string HomeTeam
		{
			get
			{
				return _homeTeam;
			}
			set
			{
				if (value != _homeTeam)
				{
					_homeTeam = value;
				}
			}
		}

		#endregion

		#region int? HomeTeamBig

		private int? _homeTeamBig;
		[DebuggerNonUserCode]
		[Column(Storage = "_homeTeamBig", Name = "home_team_big", DbType = "integer")]
		public int? HomeTeamBig
		{
			get
			{
				return _homeTeamBig;
			}
			set
			{
				if (value != _homeTeamBig)
				{
					_homeTeamBig = value;
				}
			}
		}

		#endregion

		#region int? HTMLPosition

		private int? _htmlpOsition;
		[DebuggerNonUserCode]
		[Column(Storage = "_htmlpOsition", Name = "html_position", DbType = "integer")]
		public int? HTMLPosition
		{
			get
			{
				return _htmlpOsition;
			}
			set
			{
				if (value != _htmlpOsition)
				{
					_htmlpOsition = value;
				}
			}
		}

		#endregion

		#region System.DateTime? MatchTime

		private System.DateTime? _matchTime;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchTime", Name = "match_time", DbType = "datetime")]
		public System.DateTime? MatchTime
		{
			get
			{
				return _matchTime;
			}
			set
			{
				if (value != _matchTime)
				{
					_matchTime = value;
				}
			}
		}

		#endregion

		#region string MatchType

		private string _matchType;
		[DebuggerNonUserCode]
		[Column(Storage = "_matchType", Name = "match_type", DbType = "nvarchar(50)")]
		public string MatchType
		{
			get
			{
				return _matchType;
			}
			set
			{
				if (value != _matchType)
				{
					_matchType = value;
				}
			}
		}

		#endregion

		#region string Odds

		private string _odds;
		[DebuggerNonUserCode]
		[Column(Storage = "_odds", Name = "odds", DbType = "nvarchar(50)")]
		public string Odds
		{
			get
			{
				return _odds;
			}
			set
			{
				if (value != _odds)
				{
					_odds = value;
				}
			}
		}

		#endregion

		#region int ResultTBLibID

		private int _resultTblIbID;
		[DebuggerNonUserCode]
		[Column(Storage = "_resultTblIbID", Name = "result_tb_lib_id", DbType = "integer", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
		public int ResultTBLibID
		{
			get
			{
				return _resultTblIbID;
			}
			set
			{
				if (value != _resultTblIbID)
				{
					_resultTblIbID = value;
				}
			}
		}

		#endregion

		#region string WinLossBig

		private string _winLossBig;
		[DebuggerNonUserCode]
		[Column(Storage = "_winLossBig", Name = "win_loss_big", DbType = "nvarchar(50)")]
		public string WinLossBig
		{
			get
			{
				return _winLossBig;
			}
			set
			{
				if (value != _winLossBig)
				{
					_winLossBig = value;
				}
			}
		}

		#endregion

	}
}
