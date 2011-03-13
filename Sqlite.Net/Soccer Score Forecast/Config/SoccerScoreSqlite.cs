// 
//  ____  _     __  __      _        _ 
// |  _ \| |__ |  \/  | ___| |_ __ _| |
// | | | | '_ \| |\/| |/ _ \ __/ _` | |
// | |_| | |_) | |  | |  __/ || (_| | |
// |____/|_.__/|_|  |_|\___|\__\__,_|_|
//
// Auto-generated from SoccerScoreSqlite on 2011-03-11 22:44:04Z.
// Please visit http://code.google.com/p/dblinq2007/ for more information.
//
namespace Soccer_Score_Forecast.LinqSql
{
	using System;
	using System.ComponentModel;
	using System.Data;
#if MONO_STRICT
	using System.Data.Linq;
#else   // MONO_STRICT
	using DbLinq.Data.Linq;
	using DbLinq.Vendor;
#endif  // MONO_STRICT
	using System.Data.Linq.Mapping;
	using System.Diagnostics;
	
	
	public partial class SoccerScoreSqlite : DataContext
	{
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		#endregion
		
		
		public SoccerScoreSqlite(string connectionString) : 
				base(connectionString)
		{
			this.OnCreated();
		}
		
		public SoccerScoreSqlite(string connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			this.OnCreated();
		}
		
		public SoccerScoreSqlite(IDbConnection connection, MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			this.OnCreated();
		}
		
		public Table<LiveAibo> LiveAibo
		{
			get
			{
				return this.GetTable<LiveAibo>();
			}
		}
		
		public Table<LiveOkOO> LiveOkOO
		{
			get
			{
				return this.GetTable<LiveOkOO>();
			}
		}
		
		public Table<LiveTable> LiveTable
		{
			get
			{
				return this.GetTable<LiveTable>();
			}
		}
		
		public Table<LiveTableLib> LiveTableLib
		{
			get
			{
				return this.GetTable<LiveTableLib>();
			}
		}
		
		public Table<MatchAnalysisCollection> MatchAnalysisCollection
		{
			get
			{
				return this.GetTable<MatchAnalysisCollection>();
			}
		}
		
		public Table<MatchAnalysisResult> MatchAnalysisResult
		{
			get
			{
				return this.GetTable<MatchAnalysisResult>();
			}
		}
		
		public Table<MatchTableXPath> MatchTableXPath
		{
			get
			{
				return this.GetTable<MatchTableXPath>();
			}
		}
		
		public Table<ResultTB> ResultTB
		{
			get
			{
				return this.GetTable<ResultTB>();
			}
		}
		
		public Table<ResultTBLib> ResultTBLib
		{
			get
			{
				return this.GetTable<ResultTBLib>();
			}
		}
	}
	
	#region Start MONO_STRICT
#if MONO_STRICT

	public partial class SoccerScoreSqlite
	{
		
		public SoccerScoreSqlite(IDbConnection connection) : 
				base(connection)
		{
			this.OnCreated();
		}
	}
	#region End MONO_STRICT
	#endregion
#else     // MONO_STRICT
	
	public partial class SoccerScoreSqlite
	{
		
		public SoccerScoreSqlite(IDbConnection connection) : 
				base(connection, new DbLinq.Sqlite.SqliteVendor())
		{
			this.OnCreated();
		}
		
		public SoccerScoreSqlite(IDbConnection connection, IVendor sqlDialect) : 
				base(connection, sqlDialect)
		{
			this.OnCreated();
		}
		
		public SoccerScoreSqlite(IDbConnection connection, MappingSource mappingSource, IVendor sqlDialect) : 
				base(connection, mappingSource, sqlDialect)
		{
			this.OnCreated();
		}
	}
	#region End Not MONO_STRICT
	#endregion
#endif     // MONO_STRICT
	#endregion
	
	[Table(Name="main.live_Aibo")]
	public partial class LiveAibo : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _leagueName;
		
		private int _liveAiboID;
		
		private string _matchOrder1aWayName;
		
		private string _matchOrder1hAndicapNumber;
		
		private string _matchOrder1hOmeName;
		
		private string _matchTime;
		
		private string _value;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnLeagueNameChanged();
		
		partial void OnLeagueNameChanging(string value);
		
		partial void OnLiveAiboIDChanged();
		
		partial void OnLiveAiboIDChanging(int value);
		
		partial void OnMatchOrder1aWayNameChanged();
		
		partial void OnMatchOrder1aWayNameChanging(string value);
		
		partial void OnMatchOrder1hAndicapNumberChanged();
		
		partial void OnMatchOrder1hAndicapNumberChanging(string value);
		
		partial void OnMatchOrder1hOmeNameChanged();
		
		partial void OnMatchOrder1hOmeNameChanging(string value);
		
		partial void OnMatchTimeChanged();
		
		partial void OnMatchTimeChanging(string value);
		
		partial void OnValueChanged();
		
		partial void OnValueChanging(string value);
		#endregion
		
		
		public LiveAibo()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_leagueName", Name="LeagueName", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string LeagueName
		{
			get
			{
				return this._leagueName;
			}
			set
			{
				if (((_leagueName == value) 
							== false))
				{
					this.OnLeagueNameChanging(value);
					this.SendPropertyChanging();
					this._leagueName = value;
					this.SendPropertyChanged("LeagueName");
					this.OnLeagueNameChanged();
				}
			}
		}
		
		[Column(Storage="_liveAiboID", Name="live_Aibo_id", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int LiveAiboID
		{
			get
			{
				return this._liveAiboID;
			}
			set
			{
				if ((_liveAiboID != value))
				{
					this.OnLiveAiboIDChanging(value);
					this.SendPropertyChanging();
					this._liveAiboID = value;
					this.SendPropertyChanged("LiveAiboID");
					this.OnLiveAiboIDChanged();
				}
			}
		}
		
		[Column(Storage="_matchOrder1aWayName", Name="MatchOrder1_AwayName", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MatchOrder1aWayName
		{
			get
			{
				return this._matchOrder1aWayName;
			}
			set
			{
				if (((_matchOrder1aWayName == value) 
							== false))
				{
					this.OnMatchOrder1aWayNameChanging(value);
					this.SendPropertyChanging();
					this._matchOrder1aWayName = value;
					this.SendPropertyChanged("MatchOrder1aWayName");
					this.OnMatchOrder1aWayNameChanged();
				}
			}
		}
		
		[Column(Storage="_matchOrder1hAndicapNumber", Name="MatchOrder1_HandicapNumber", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MatchOrder1hAndicapNumber
		{
			get
			{
				return this._matchOrder1hAndicapNumber;
			}
			set
			{
				if (((_matchOrder1hAndicapNumber == value) 
							== false))
				{
					this.OnMatchOrder1hAndicapNumberChanging(value);
					this.SendPropertyChanging();
					this._matchOrder1hAndicapNumber = value;
					this.SendPropertyChanged("MatchOrder1hAndicapNumber");
					this.OnMatchOrder1hAndicapNumberChanged();
				}
			}
		}
		
		[Column(Storage="_matchOrder1hOmeName", Name="MatchOrder1_HomeName", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MatchOrder1hOmeName
		{
			get
			{
				return this._matchOrder1hOmeName;
			}
			set
			{
				if (((_matchOrder1hOmeName == value) 
							== false))
				{
					this.OnMatchOrder1hOmeNameChanging(value);
					this.SendPropertyChanging();
					this._matchOrder1hOmeName = value;
					this.SendPropertyChanged("MatchOrder1hOmeName");
					this.OnMatchOrder1hOmeNameChanged();
				}
			}
		}
		
		[Column(Storage="_matchTime", Name="match_time", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MatchTime
		{
			get
			{
				return this._matchTime;
			}
			set
			{
				if (((_matchTime == value) 
							== false))
				{
					this.OnMatchTimeChanging(value);
					this.SendPropertyChanging();
					this._matchTime = value;
					this.SendPropertyChanged("MatchTime");
					this.OnMatchTimeChanged();
				}
			}
		}
		
		[Column(Storage="_value", Name="value", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (((_value == value) 
							== false))
				{
					this.OnValueChanging(value);
					this.SendPropertyChanging();
					this._value = value;
					this.SendPropertyChanged("Value");
					this.OnValueChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.live_okoo")]
	public partial class LiveOkOO : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _leagueName;
		
		private int _liveOkOoid;
		
		private string _match1dRawn;
		
		private string _match1lOst;
		
		private string _match1wIn;
		
		private string _matchInfo;
		
		private string _matchOrder1aWayName;
		
		private string _matchOrder1hAndicapNumber;
		
		private string _matchOrder1hOmeName;
		
		private string _matchTime;
		
		private string _ok10;
		
		private string _ok11;
		
		private string _ok12;
		
		private System.Nullable<int> _value;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnLeagueNameChanged();
		
		partial void OnLeagueNameChanging(string value);
		
		partial void OnLiveOkOOIDChanged();
		
		partial void OnLiveOkOOIDChanging(int value);
		
		partial void OnMatch1dRawnChanged();
		
		partial void OnMatch1dRawnChanging(string value);
		
		partial void OnMatch1lOstChanged();
		
		partial void OnMatch1lOstChanging(string value);
		
		partial void OnMatch1wInChanged();
		
		partial void OnMatch1wInChanging(string value);
		
		partial void OnMatchInfoChanged();
		
		partial void OnMatchInfoChanging(string value);
		
		partial void OnMatchOrder1aWayNameChanged();
		
		partial void OnMatchOrder1aWayNameChanging(string value);
		
		partial void OnMatchOrder1hAndicapNumberChanged();
		
		partial void OnMatchOrder1hAndicapNumberChanging(string value);
		
		partial void OnMatchOrder1hOmeNameChanged();
		
		partial void OnMatchOrder1hOmeNameChanging(string value);
		
		partial void OnMatchTimeChanged();
		
		partial void OnMatchTimeChanging(string value);
		
		partial void OnOk10Changed();
		
		partial void OnOk10Changing(string value);
		
		partial void OnOk11Changed();
		
		partial void OnOk11Changing(string value);
		
		partial void OnOk12Changed();
		
		partial void OnOk12Changing(string value);
		
		partial void OnValueChanged();
		
		partial void OnValueChanging(System.Nullable<int> value);
		#endregion
		
		
		public LiveOkOO()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_leagueName", Name="LeagueName", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string LeagueName
		{
			get
			{
				return this._leagueName;
			}
			set
			{
				if (((_leagueName == value) 
							== false))
				{
					this.OnLeagueNameChanging(value);
					this.SendPropertyChanging();
					this._leagueName = value;
					this.SendPropertyChanged("LeagueName");
					this.OnLeagueNameChanged();
				}
			}
		}
		
		[Column(Storage="_liveOkOoid", Name="live_okoo_id", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int LiveOkOOID
		{
			get
			{
				return this._liveOkOoid;
			}
			set
			{
				if ((_liveOkOoid != value))
				{
					this.OnLiveOkOOIDChanging(value);
					this.SendPropertyChanging();
					this._liveOkOoid = value;
					this.SendPropertyChanged("LiveOkOOID");
					this.OnLiveOkOOIDChanged();
				}
			}
		}
		
		[Column(Storage="_match1dRawn", Name="Match_1_Drawn", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Match1dRawn
		{
			get
			{
				return this._match1dRawn;
			}
			set
			{
				if (((_match1dRawn == value) 
							== false))
				{
					this.OnMatch1dRawnChanging(value);
					this.SendPropertyChanging();
					this._match1dRawn = value;
					this.SendPropertyChanged("Match1dRawn");
					this.OnMatch1dRawnChanged();
				}
			}
		}
		
		[Column(Storage="_match1lOst", Name="Match_1_Lost", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Match1lOst
		{
			get
			{
				return this._match1lOst;
			}
			set
			{
				if (((_match1lOst == value) 
							== false))
				{
					this.OnMatch1lOstChanging(value);
					this.SendPropertyChanging();
					this._match1lOst = value;
					this.SendPropertyChanged("Match1lOst");
					this.OnMatch1lOstChanged();
				}
			}
		}
		
		[Column(Storage="_match1wIn", Name="Match_1_Win", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Match1wIn
		{
			get
			{
				return this._match1wIn;
			}
			set
			{
				if (((_match1wIn == value) 
							== false))
				{
					this.OnMatch1wInChanging(value);
					this.SendPropertyChanging();
					this._match1wIn = value;
					this.SendPropertyChanged("Match1wIn");
					this.OnMatch1wInChanged();
				}
			}
		}
		
		[Column(Storage="_matchInfo", Name="MatchInfo", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MatchInfo
		{
			get
			{
				return this._matchInfo;
			}
			set
			{
				if (((_matchInfo == value) 
							== false))
				{
					this.OnMatchInfoChanging(value);
					this.SendPropertyChanging();
					this._matchInfo = value;
					this.SendPropertyChanged("MatchInfo");
					this.OnMatchInfoChanged();
				}
			}
		}
		
		[Column(Storage="_matchOrder1aWayName", Name="MatchOrder1_AwayName", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MatchOrder1aWayName
		{
			get
			{
				return this._matchOrder1aWayName;
			}
			set
			{
				if (((_matchOrder1aWayName == value) 
							== false))
				{
					this.OnMatchOrder1aWayNameChanging(value);
					this.SendPropertyChanging();
					this._matchOrder1aWayName = value;
					this.SendPropertyChanged("MatchOrder1aWayName");
					this.OnMatchOrder1aWayNameChanged();
				}
			}
		}
		
		[Column(Storage="_matchOrder1hAndicapNumber", Name="MatchOrder1_HandicapNumber", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MatchOrder1hAndicapNumber
		{
			get
			{
				return this._matchOrder1hAndicapNumber;
			}
			set
			{
				if (((_matchOrder1hAndicapNumber == value) 
							== false))
				{
					this.OnMatchOrder1hAndicapNumberChanging(value);
					this.SendPropertyChanging();
					this._matchOrder1hAndicapNumber = value;
					this.SendPropertyChanged("MatchOrder1hAndicapNumber");
					this.OnMatchOrder1hAndicapNumberChanged();
				}
			}
		}
		
		[Column(Storage="_matchOrder1hOmeName", Name="MatchOrder1_HomeName", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MatchOrder1hOmeName
		{
			get
			{
				return this._matchOrder1hOmeName;
			}
			set
			{
				if (((_matchOrder1hOmeName == value) 
							== false))
				{
					this.OnMatchOrder1hOmeNameChanging(value);
					this.SendPropertyChanging();
					this._matchOrder1hOmeName = value;
					this.SendPropertyChanged("MatchOrder1hOmeName");
					this.OnMatchOrder1hOmeNameChanged();
				}
			}
		}
		
		[Column(Storage="_matchTime", Name="match_time", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MatchTime
		{
			get
			{
				return this._matchTime;
			}
			set
			{
				if (((_matchTime == value) 
							== false))
				{
					this.OnMatchTimeChanging(value);
					this.SendPropertyChanging();
					this._matchTime = value;
					this.SendPropertyChanged("MatchTime");
					this.OnMatchTimeChanged();
				}
			}
		}
		
		[Column(Storage="_ok10", Name="ok_1_0", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Ok10
		{
			get
			{
				return this._ok10;
			}
			set
			{
				if (((_ok10 == value) 
							== false))
				{
					this.OnOk10Changing(value);
					this.SendPropertyChanging();
					this._ok10 = value;
					this.SendPropertyChanged("Ok10");
					this.OnOk10Changed();
				}
			}
		}
		
		[Column(Storage="_ok11", Name="ok_1_1", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Ok11
		{
			get
			{
				return this._ok11;
			}
			set
			{
				if (((_ok11 == value) 
							== false))
				{
					this.OnOk11Changing(value);
					this.SendPropertyChanging();
					this._ok11 = value;
					this.SendPropertyChanged("Ok11");
					this.OnOk11Changed();
				}
			}
		}
		
		[Column(Storage="_ok12", Name="ok_1_2", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Ok12
		{
			get
			{
				return this._ok12;
			}
			set
			{
				if (((_ok12 == value) 
							== false))
				{
					this.OnOk12Changing(value);
					this.SendPropertyChanging();
					this._ok12 = value;
					this.SendPropertyChanged("Ok12");
					this.OnOk12Changed();
				}
			}
		}
		
		[Column(Storage="_value", Name="value", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if ((_value != value))
				{
					this.OnValueChanging(value);
					this.SendPropertyChanging();
					this._value = value;
					this.SendPropertyChanged("Value");
					this.OnValueChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.live_Table")]
	public partial class LiveTable : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _awayTeam;
		
		private string _awayTeamBig;
		
		private string _fullTimeScore;
		
		private string _halfTimeScore;
		
		private string _homeTeam;
		
		private string _homeTeamBig;
		
		private string _htmlpOsition;
		
		private int _liveTableID;
		
		private string _matchType;
		
		private string _sdAte;
		
		private string _status;
		
		private string _stIme;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAwayTeamChanged();
		
		partial void OnAwayTeamChanging(string value);
		
		partial void OnAwayTeamBigChanged();
		
		partial void OnAwayTeamBigChanging(string value);
		
		partial void OnFullTimeScoreChanged();
		
		partial void OnFullTimeScoreChanging(string value);
		
		partial void OnHalfTimeScoreChanged();
		
		partial void OnHalfTimeScoreChanging(string value);
		
		partial void OnHomeTeamChanged();
		
		partial void OnHomeTeamChanging(string value);
		
		partial void OnHomeTeamBigChanged();
		
		partial void OnHomeTeamBigChanging(string value);
		
		partial void OnHTMLPositionChanged();
		
		partial void OnHTMLPositionChanging(string value);
		
		partial void OnLiveTableIDChanged();
		
		partial void OnLiveTableIDChanging(int value);
		
		partial void OnMatchTypeChanged();
		
		partial void OnMatchTypeChanging(string value);
		
		partial void OnSDateChanged();
		
		partial void OnSDateChanging(string value);
		
		partial void OnStatusChanged();
		
		partial void OnStatusChanging(string value);
		
		partial void OnSTimeChanged();
		
		partial void OnSTimeChanging(string value);
		#endregion
		
		
		public LiveTable()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_awayTeam", Name="away_team", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AwayTeam
		{
			get
			{
				return this._awayTeam;
			}
			set
			{
				if (((_awayTeam == value) 
							== false))
				{
					this.OnAwayTeamChanging(value);
					this.SendPropertyChanging();
					this._awayTeam = value;
					this.SendPropertyChanged("AwayTeam");
					this.OnAwayTeamChanged();
				}
			}
		}
		
		[Column(Storage="_awayTeamBig", Name="away_team_big", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AwayTeamBig
		{
			get
			{
				return this._awayTeamBig;
			}
			set
			{
				if (((_awayTeamBig == value) 
							== false))
				{
					this.OnAwayTeamBigChanging(value);
					this.SendPropertyChanging();
					this._awayTeamBig = value;
					this.SendPropertyChanged("AwayTeamBig");
					this.OnAwayTeamBigChanged();
				}
			}
		}
		
		[Column(Storage="_fullTimeScore", Name="full_time_score", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string FullTimeScore
		{
			get
			{
				return this._fullTimeScore;
			}
			set
			{
				if (((_fullTimeScore == value) 
							== false))
				{
					this.OnFullTimeScoreChanging(value);
					this.SendPropertyChanging();
					this._fullTimeScore = value;
					this.SendPropertyChanged("FullTimeScore");
					this.OnFullTimeScoreChanged();
				}
			}
		}
		
		[Column(Storage="_halfTimeScore", Name="half_time_score", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HalfTimeScore
		{
			get
			{
				return this._halfTimeScore;
			}
			set
			{
				if (((_halfTimeScore == value) 
							== false))
				{
					this.OnHalfTimeScoreChanging(value);
					this.SendPropertyChanging();
					this._halfTimeScore = value;
					this.SendPropertyChanged("HalfTimeScore");
					this.OnHalfTimeScoreChanged();
				}
			}
		}
		
		[Column(Storage="_homeTeam", Name="home_team", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HomeTeam
		{
			get
			{
				return this._homeTeam;
			}
			set
			{
				if (((_homeTeam == value) 
							== false))
				{
					this.OnHomeTeamChanging(value);
					this.SendPropertyChanging();
					this._homeTeam = value;
					this.SendPropertyChanged("HomeTeam");
					this.OnHomeTeamChanged();
				}
			}
		}
		
		[Column(Storage="_homeTeamBig", Name="home_team_big", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HomeTeamBig
		{
			get
			{
				return this._homeTeamBig;
			}
			set
			{
				if (((_homeTeamBig == value) 
							== false))
				{
					this.OnHomeTeamBigChanging(value);
					this.SendPropertyChanging();
					this._homeTeamBig = value;
					this.SendPropertyChanged("HomeTeamBig");
					this.OnHomeTeamBigChanged();
				}
			}
		}
		
		[Column(Storage="_htmlpOsition", Name="html_position", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HTMLPosition
		{
			get
			{
				return this._htmlpOsition;
			}
			set
			{
				if (((_htmlpOsition == value) 
							== false))
				{
					this.OnHTMLPositionChanging(value);
					this.SendPropertyChanging();
					this._htmlpOsition = value;
					this.SendPropertyChanged("HTMLPosition");
					this.OnHTMLPositionChanged();
				}
			}
		}
		
		[Column(Storage="_liveTableID", Name="live_table_id", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int LiveTableID
		{
			get
			{
				return this._liveTableID;
			}
			set
			{
				if ((_liveTableID != value))
				{
					this.OnLiveTableIDChanging(value);
					this.SendPropertyChanging();
					this._liveTableID = value;
					this.SendPropertyChanged("LiveTableID");
					this.OnLiveTableIDChanged();
				}
			}
		}
		
		[Column(Storage="_matchType", Name="match_type", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MatchType
		{
			get
			{
				return this._matchType;
			}
			set
			{
				if (((_matchType == value) 
							== false))
				{
					this.OnMatchTypeChanging(value);
					this.SendPropertyChanging();
					this._matchType = value;
					this.SendPropertyChanged("MatchType");
					this.OnMatchTypeChanged();
				}
			}
		}
		
		[Column(Storage="_sdAte", Name="s_date", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string SDate
		{
			get
			{
				return this._sdAte;
			}
			set
			{
				if (((_sdAte == value) 
							== false))
				{
					this.OnSDateChanging(value);
					this.SendPropertyChanging();
					this._sdAte = value;
					this.SendPropertyChanged("SDate");
					this.OnSDateChanged();
				}
			}
		}
		
		[Column(Storage="_status", Name="status", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Status
		{
			get
			{
				return this._status;
			}
			set
			{
				if (((_status == value) 
							== false))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		[Column(Storage="_stIme", Name="s_time", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string STime
		{
			get
			{
				return this._stIme;
			}
			set
			{
				if (((_stIme == value) 
							== false))
				{
					this.OnSTimeChanging(value);
					this.SendPropertyChanging();
					this._stIme = value;
					this.SendPropertyChanged("STime");
					this.OnSTimeChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.live_Table_lib")]
	public partial class LiveTableLib : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private System.Nullable<int> _awayRedCard;
		
		private string _awayTeam;
		
		private System.Nullable<int> _awayTeamBig;
		
		private System.Nullable<int> _fullAwayGoals;
		
		private System.Nullable<int> _fullHomeGoals;
		
		private System.Nullable<int> _halfAwayGoals;
		
		private System.Nullable<int> _halfHomeGoals;
		
		private System.Nullable<int> _homeRedCard;
		
		private string _homeTeam;
		
		private System.Nullable<int> _homeTeamBig;
		
		private System.Nullable<int> _htmlpOsition;
		
		private int _liveTableLibID;
		
		private System.Nullable<System.DateTime> _matchTime;
		
		private string _matchType;
		
		private string _status;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAwayRedCardChanged();
		
		partial void OnAwayRedCardChanging(System.Nullable<int> value);
		
		partial void OnAwayTeamChanged();
		
		partial void OnAwayTeamChanging(string value);
		
		partial void OnAwayTeamBigChanged();
		
		partial void OnAwayTeamBigChanging(System.Nullable<int> value);
		
		partial void OnFullAwayGoalsChanged();
		
		partial void OnFullAwayGoalsChanging(System.Nullable<int> value);
		
		partial void OnFullHomeGoalsChanged();
		
		partial void OnFullHomeGoalsChanging(System.Nullable<int> value);
		
		partial void OnHalfAwayGoalsChanged();
		
		partial void OnHalfAwayGoalsChanging(System.Nullable<int> value);
		
		partial void OnHalfHomeGoalsChanged();
		
		partial void OnHalfHomeGoalsChanging(System.Nullable<int> value);
		
		partial void OnHomeRedCardChanged();
		
		partial void OnHomeRedCardChanging(System.Nullable<int> value);
		
		partial void OnHomeTeamChanged();
		
		partial void OnHomeTeamChanging(string value);
		
		partial void OnHomeTeamBigChanged();
		
		partial void OnHomeTeamBigChanging(System.Nullable<int> value);
		
		partial void OnHTMLPositionChanged();
		
		partial void OnHTMLPositionChanging(System.Nullable<int> value);
		
		partial void OnLiveTableLibIDChanged();
		
		partial void OnLiveTableLibIDChanging(int value);
		
		partial void OnMatchTimeChanged();
		
		partial void OnMatchTimeChanging(System.Nullable<System.DateTime> value);
		
		partial void OnMatchTypeChanged();
		
		partial void OnMatchTypeChanging(string value);
		
		partial void OnStatusChanged();
		
		partial void OnStatusChanging(string value);
		#endregion
		
		
		public LiveTableLib()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_awayRedCard", Name="away_red_card", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> AwayRedCard
		{
			get
			{
				return this._awayRedCard;
			}
			set
			{
				if ((_awayRedCard != value))
				{
					this.OnAwayRedCardChanging(value);
					this.SendPropertyChanging();
					this._awayRedCard = value;
					this.SendPropertyChanged("AwayRedCard");
					this.OnAwayRedCardChanged();
				}
			}
		}
		
		[Column(Storage="_awayTeam", Name="away_team", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AwayTeam
		{
			get
			{
				return this._awayTeam;
			}
			set
			{
				if (((_awayTeam == value) 
							== false))
				{
					this.OnAwayTeamChanging(value);
					this.SendPropertyChanging();
					this._awayTeam = value;
					this.SendPropertyChanged("AwayTeam");
					this.OnAwayTeamChanged();
				}
			}
		}
		
		[Column(Storage="_awayTeamBig", Name="away_team_big", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> AwayTeamBig
		{
			get
			{
				return this._awayTeamBig;
			}
			set
			{
				if ((_awayTeamBig != value))
				{
					this.OnAwayTeamBigChanging(value);
					this.SendPropertyChanging();
					this._awayTeamBig = value;
					this.SendPropertyChanged("AwayTeamBig");
					this.OnAwayTeamBigChanged();
				}
			}
		}
		
		[Column(Storage="_fullAwayGoals", Name="full_away_goals", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> FullAwayGoals
		{
			get
			{
				return this._fullAwayGoals;
			}
			set
			{
				if ((_fullAwayGoals != value))
				{
					this.OnFullAwayGoalsChanging(value);
					this.SendPropertyChanging();
					this._fullAwayGoals = value;
					this.SendPropertyChanged("FullAwayGoals");
					this.OnFullAwayGoalsChanged();
				}
			}
		}
		
		[Column(Storage="_fullHomeGoals", Name="full_home_goals", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> FullHomeGoals
		{
			get
			{
				return this._fullHomeGoals;
			}
			set
			{
				if ((_fullHomeGoals != value))
				{
					this.OnFullHomeGoalsChanging(value);
					this.SendPropertyChanging();
					this._fullHomeGoals = value;
					this.SendPropertyChanged("FullHomeGoals");
					this.OnFullHomeGoalsChanged();
				}
			}
		}
		
		[Column(Storage="_halfAwayGoals", Name="half_away_goals", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> HalfAwayGoals
		{
			get
			{
				return this._halfAwayGoals;
			}
			set
			{
				if ((_halfAwayGoals != value))
				{
					this.OnHalfAwayGoalsChanging(value);
					this.SendPropertyChanging();
					this._halfAwayGoals = value;
					this.SendPropertyChanged("HalfAwayGoals");
					this.OnHalfAwayGoalsChanged();
				}
			}
		}
		
		[Column(Storage="_halfHomeGoals", Name="half_home_goals", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> HalfHomeGoals
		{
			get
			{
				return this._halfHomeGoals;
			}
			set
			{
				if ((_halfHomeGoals != value))
				{
					this.OnHalfHomeGoalsChanging(value);
					this.SendPropertyChanging();
					this._halfHomeGoals = value;
					this.SendPropertyChanged("HalfHomeGoals");
					this.OnHalfHomeGoalsChanged();
				}
			}
		}
		
		[Column(Storage="_homeRedCard", Name="home_red_card", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> HomeRedCard
		{
			get
			{
				return this._homeRedCard;
			}
			set
			{
				if ((_homeRedCard != value))
				{
					this.OnHomeRedCardChanging(value);
					this.SendPropertyChanging();
					this._homeRedCard = value;
					this.SendPropertyChanged("HomeRedCard");
					this.OnHomeRedCardChanged();
				}
			}
		}
		
		[Column(Storage="_homeTeam", Name="home_team", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HomeTeam
		{
			get
			{
				return this._homeTeam;
			}
			set
			{
				if (((_homeTeam == value) 
							== false))
				{
					this.OnHomeTeamChanging(value);
					this.SendPropertyChanging();
					this._homeTeam = value;
					this.SendPropertyChanged("HomeTeam");
					this.OnHomeTeamChanged();
				}
			}
		}
		
		[Column(Storage="_homeTeamBig", Name="home_team_big", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> HomeTeamBig
		{
			get
			{
				return this._homeTeamBig;
			}
			set
			{
				if ((_homeTeamBig != value))
				{
					this.OnHomeTeamBigChanging(value);
					this.SendPropertyChanging();
					this._homeTeamBig = value;
					this.SendPropertyChanged("HomeTeamBig");
					this.OnHomeTeamBigChanged();
				}
			}
		}
		
		[Column(Storage="_htmlpOsition", Name="html_position", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> HTMLPosition
		{
			get
			{
				return this._htmlpOsition;
			}
			set
			{
				if ((_htmlpOsition != value))
				{
					this.OnHTMLPositionChanging(value);
					this.SendPropertyChanging();
					this._htmlpOsition = value;
					this.SendPropertyChanged("HTMLPosition");
					this.OnHTMLPositionChanged();
				}
			}
		}
		
		[Column(Storage="_liveTableLibID", Name="live_table_lib_id", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int LiveTableLibID
		{
			get
			{
				return this._liveTableLibID;
			}
			set
			{
				if ((_liveTableLibID != value))
				{
					this.OnLiveTableLibIDChanging(value);
					this.SendPropertyChanging();
					this._liveTableLibID = value;
					this.SendPropertyChanged("LiveTableLibID");
					this.OnLiveTableLibIDChanged();
				}
			}
		}
		
		[Column(Storage="_matchTime", Name="match_time", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> MatchTime
		{
			get
			{
				return this._matchTime;
			}
			set
			{
				if ((_matchTime != value))
				{
					this.OnMatchTimeChanging(value);
					this.SendPropertyChanging();
					this._matchTime = value;
					this.SendPropertyChanged("MatchTime");
					this.OnMatchTimeChanged();
				}
			}
		}
		
		[Column(Storage="_matchType", Name="match_type", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MatchType
		{
			get
			{
				return this._matchType;
			}
			set
			{
				if (((_matchType == value) 
							== false))
				{
					this.OnMatchTypeChanging(value);
					this.SendPropertyChanging();
					this._matchType = value;
					this.SendPropertyChanged("MatchType");
					this.OnMatchTypeChanged();
				}
			}
		}
		
		[Column(Storage="_status", Name="status", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Status
		{
			get
			{
				return this._status;
			}
			set
			{
				if (((_status == value) 
							== false))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.match_analysis_collection")]
	public partial class MatchAnalysisCollection : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _analysisCollectionID;
		
		private System.Nullable<int> _liveTableLibID;
		
		private System.Nullable<int> _resultTblIbID;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAnalysisCollectionIDChanged();
		
		partial void OnAnalysisCollectionIDChanging(int value);
		
		partial void OnLiveTableLibIDChanged();
		
		partial void OnLiveTableLibIDChanging(System.Nullable<int> value);
		
		partial void OnResultTBLibIDChanged();
		
		partial void OnResultTBLibIDChanging(System.Nullable<int> value);
		#endregion
		
		
		public MatchAnalysisCollection()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_analysisCollectionID", Name="analysis_collection_id", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int AnalysisCollectionID
		{
			get
			{
				return this._analysisCollectionID;
			}
			set
			{
				if ((_analysisCollectionID != value))
				{
					this.OnAnalysisCollectionIDChanging(value);
					this.SendPropertyChanging();
					this._analysisCollectionID = value;
					this.SendPropertyChanged("AnalysisCollectionID");
					this.OnAnalysisCollectionIDChanged();
				}
			}
		}
		
		[Column(Storage="_liveTableLibID", Name="live_table_lib_id", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> LiveTableLibID
		{
			get
			{
				return this._liveTableLibID;
			}
			set
			{
				if ((_liveTableLibID != value))
				{
					this.OnLiveTableLibIDChanging(value);
					this.SendPropertyChanging();
					this._liveTableLibID = value;
					this.SendPropertyChanged("LiveTableLibID");
					this.OnLiveTableLibIDChanged();
				}
			}
		}
		
		[Column(Storage="_resultTblIbID", Name="result_tb_lib_id", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> ResultTBLibID
		{
			get
			{
				return this._resultTblIbID;
			}
			set
			{
				if ((_resultTblIbID != value))
				{
					this.OnResultTBLibIDChanging(value);
					this.SendPropertyChanging();
					this._resultTblIbID = value;
					this.SendPropertyChanged("ResultTBLibID");
					this.OnResultTBLibIDChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.match_analysis_result")]
	public partial class MatchAnalysisResult : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private int _analysisResultID;
		
		private System.Nullable<float> _awayGoals;
		
		private System.Nullable<float> _fitGoals;
		
		private System.Nullable<float> _fitOddEven;
		
		private System.Nullable<float> _fitWinLoss;
		
		private System.Nullable<int> _homeD;
		
		private System.Nullable<float> _homeGoals;
		
		private System.Nullable<int> _homeL;
		
		private System.Nullable<int> _homeW;
		
		private decimal _liveTableLibID;
		
		private string _preAlgorithm;
		
		private System.Nullable<int> _preMatchCount;
		
		private string _resultFit;
		
		private string _resultGoals;
		
		private System.Nullable<decimal> _resultTblIbID;
		
		private string _resultWdl;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAnalysisResultIDChanged();
		
		partial void OnAnalysisResultIDChanging(int value);
		
		partial void OnAwayGoalsChanged();
		
		partial void OnAwayGoalsChanging(System.Nullable<float> value);
		
		partial void OnFitGoalsChanged();
		
		partial void OnFitGoalsChanging(System.Nullable<float> value);
		
		partial void OnFitOddEvenChanged();
		
		partial void OnFitOddEvenChanging(System.Nullable<float> value);
		
		partial void OnFitWinLossChanged();
		
		partial void OnFitWinLossChanging(System.Nullable<float> value);
		
		partial void OnHomeDChanged();
		
		partial void OnHomeDChanging(System.Nullable<int> value);
		
		partial void OnHomeGoalsChanged();
		
		partial void OnHomeGoalsChanging(System.Nullable<float> value);
		
		partial void OnHomeLChanged();
		
		partial void OnHomeLChanging(System.Nullable<int> value);
		
		partial void OnHomeWChanged();
		
		partial void OnHomeWChanging(System.Nullable<int> value);
		
		partial void OnLiveTableLibIDChanged();
		
		partial void OnLiveTableLibIDChanging(decimal value);
		
		partial void OnPreAlgorithmChanged();
		
		partial void OnPreAlgorithmChanging(string value);
		
		partial void OnPreMatchCountChanged();
		
		partial void OnPreMatchCountChanging(System.Nullable<int> value);
		
		partial void OnResultFitChanged();
		
		partial void OnResultFitChanging(string value);
		
		partial void OnResultGoalsChanged();
		
		partial void OnResultGoalsChanging(string value);
		
		partial void OnResultTBLibIDChanged();
		
		partial void OnResultTBLibIDChanging(System.Nullable<decimal> value);
		
		partial void OnResultWDLChanged();
		
		partial void OnResultWDLChanging(string value);
		#endregion
		
		
		public MatchAnalysisResult()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_analysisResultID", Name="analysis_result_id", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int AnalysisResultID
		{
			get
			{
				return this._analysisResultID;
			}
			set
			{
				if ((_analysisResultID != value))
				{
					this.OnAnalysisResultIDChanging(value);
					this.SendPropertyChanging();
					this._analysisResultID = value;
					this.SendPropertyChanged("AnalysisResultID");
					this.OnAnalysisResultIDChanged();
				}
			}
		}
		
		[Column(Storage="_awayGoals", Name="away_goals", DbType="float", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<float> AwayGoals
		{
			get
			{
				return this._awayGoals;
			}
			set
			{
				if ((_awayGoals != value))
				{
					this.OnAwayGoalsChanging(value);
					this.SendPropertyChanging();
					this._awayGoals = value;
					this.SendPropertyChanged("AwayGoals");
					this.OnAwayGoalsChanged();
				}
			}
		}
		
		[Column(Storage="_fitGoals", Name="fit_goals", DbType="float", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<float> FitGoals
		{
			get
			{
				return this._fitGoals;
			}
			set
			{
				if ((_fitGoals != value))
				{
					this.OnFitGoalsChanging(value);
					this.SendPropertyChanging();
					this._fitGoals = value;
					this.SendPropertyChanged("FitGoals");
					this.OnFitGoalsChanged();
				}
			}
		}
		
		[Column(Storage="_fitOddEven", Name="fit_odd_even", DbType="float", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<float> FitOddEven
		{
			get
			{
				return this._fitOddEven;
			}
			set
			{
				if ((_fitOddEven != value))
				{
					this.OnFitOddEvenChanging(value);
					this.SendPropertyChanging();
					this._fitOddEven = value;
					this.SendPropertyChanged("FitOddEven");
					this.OnFitOddEvenChanged();
				}
			}
		}
		
		[Column(Storage="_fitWinLoss", Name="fit_win_loss", DbType="float", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<float> FitWinLoss
		{
			get
			{
				return this._fitWinLoss;
			}
			set
			{
				if ((_fitWinLoss != value))
				{
					this.OnFitWinLossChanging(value);
					this.SendPropertyChanging();
					this._fitWinLoss = value;
					this.SendPropertyChanged("FitWinLoss");
					this.OnFitWinLossChanged();
				}
			}
		}
		
		[Column(Storage="_homeD", Name="home_d", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> HomeD
		{
			get
			{
				return this._homeD;
			}
			set
			{
				if ((_homeD != value))
				{
					this.OnHomeDChanging(value);
					this.SendPropertyChanging();
					this._homeD = value;
					this.SendPropertyChanged("HomeD");
					this.OnHomeDChanged();
				}
			}
		}
		
		[Column(Storage="_homeGoals", Name="home_goals", DbType="float", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<float> HomeGoals
		{
			get
			{
				return this._homeGoals;
			}
			set
			{
				if ((_homeGoals != value))
				{
					this.OnHomeGoalsChanging(value);
					this.SendPropertyChanging();
					this._homeGoals = value;
					this.SendPropertyChanged("HomeGoals");
					this.OnHomeGoalsChanged();
				}
			}
		}
		
		[Column(Storage="_homeL", Name="home_l", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> HomeL
		{
			get
			{
				return this._homeL;
			}
			set
			{
				if ((_homeL != value))
				{
					this.OnHomeLChanging(value);
					this.SendPropertyChanging();
					this._homeL = value;
					this.SendPropertyChanged("HomeL");
					this.OnHomeLChanged();
				}
			}
		}
		
		[Column(Storage="_homeW", Name="home_w", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> HomeW
		{
			get
			{
				return this._homeW;
			}
			set
			{
				if ((_homeW != value))
				{
					this.OnHomeWChanging(value);
					this.SendPropertyChanging();
					this._homeW = value;
					this.SendPropertyChanged("HomeW");
					this.OnHomeWChanged();
				}
			}
		}
		
		[Column(Storage="_liveTableLibID", Name="live_table_lib_id", DbType="numeric", AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public decimal LiveTableLibID
		{
			get
			{
				return this._liveTableLibID;
			}
			set
			{
				if ((_liveTableLibID != value))
				{
					this.OnLiveTableLibIDChanging(value);
					this.SendPropertyChanging();
					this._liveTableLibID = value;
					this.SendPropertyChanged("LiveTableLibID");
					this.OnLiveTableLibIDChanged();
				}
			}
		}
		
		[Column(Storage="_preAlgorithm", Name="pre_algorithm", DbType="nvarchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string PreAlgorithm
		{
			get
			{
				return this._preAlgorithm;
			}
			set
			{
				if (((_preAlgorithm == value) 
							== false))
				{
					this.OnPreAlgorithmChanging(value);
					this.SendPropertyChanging();
					this._preAlgorithm = value;
					this.SendPropertyChanged("PreAlgorithm");
					this.OnPreAlgorithmChanged();
				}
			}
		}
		
		[Column(Storage="_preMatchCount", Name="pre_match_count", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> PreMatchCount
		{
			get
			{
				return this._preMatchCount;
			}
			set
			{
				if ((_preMatchCount != value))
				{
					this.OnPreMatchCountChanging(value);
					this.SendPropertyChanging();
					this._preMatchCount = value;
					this.SendPropertyChanged("PreMatchCount");
					this.OnPreMatchCountChanged();
				}
			}
		}
		
		[Column(Storage="_resultFit", Name="result_fit", DbType="nvarchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ResultFit
		{
			get
			{
				return this._resultFit;
			}
			set
			{
				if (((_resultFit == value) 
							== false))
				{
					this.OnResultFitChanging(value);
					this.SendPropertyChanging();
					this._resultFit = value;
					this.SendPropertyChanged("ResultFit");
					this.OnResultFitChanged();
				}
			}
		}
		
		[Column(Storage="_resultGoals", Name="result_goals", DbType="nvarchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ResultGoals
		{
			get
			{
				return this._resultGoals;
			}
			set
			{
				if (((_resultGoals == value) 
							== false))
				{
					this.OnResultGoalsChanging(value);
					this.SendPropertyChanging();
					this._resultGoals = value;
					this.SendPropertyChanged("ResultGoals");
					this.OnResultGoalsChanged();
				}
			}
		}
		
		[Column(Storage="_resultTblIbID", Name="result_tb_lib_id", DbType="numeric", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<decimal> ResultTBLibID
		{
			get
			{
				return this._resultTblIbID;
			}
			set
			{
				if ((_resultTblIbID != value))
				{
					this.OnResultTBLibIDChanging(value);
					this.SendPropertyChanging();
					this._resultTblIbID = value;
					this.SendPropertyChanged("ResultTBLibID");
					this.OnResultTBLibIDChanged();
				}
			}
		}
		
		[Column(Storage="_resultWdl", Name="result_wdl", DbType="nvarchar(20)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string ResultWDL
		{
			get
			{
				return this._resultWdl;
			}
			set
			{
				if (((_resultWdl == value) 
							== false))
				{
					this.OnResultWDLChanging(value);
					this.SendPropertyChanging();
					this._resultWdl = value;
					this.SendPropertyChanged("ResultWDL");
					this.OnResultWDLChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.match_table_xpath")]
	public partial class MatchTableXPath : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _maXtAbleIdvAlue;
		
		private string _maXtAbleXpAth;
		
		private System.Nullable<int> _orderTableID;
		
		private string _secondTableIdvAlue;
		
		private string _secondTableXpAth;
		
		private string _uriHost;
		
		private int _uriHostID;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnMaXTableIDValueChanged();
		
		partial void OnMaXTableIDValueChanging(string value);
		
		partial void OnMaXTableXPathChanged();
		
		partial void OnMaXTableXPathChanging(string value);
		
		partial void OnOrderTableIDChanged();
		
		partial void OnOrderTableIDChanging(System.Nullable<int> value);
		
		partial void OnSecondTableIDValueChanged();
		
		partial void OnSecondTableIDValueChanging(string value);
		
		partial void OnSecondTableXPathChanged();
		
		partial void OnSecondTableXPathChanging(string value);
		
		partial void OnUriHostChanged();
		
		partial void OnUriHostChanging(string value);
		
		partial void OnUriHostIDChanged();
		
		partial void OnUriHostIDChanging(int value);
		#endregion
		
		
		public MatchTableXPath()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_maXtAbleIdvAlue", Name="max_table_id_value", DbType="nvarchar(500)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MaXTableIDValue
		{
			get
			{
				return this._maXtAbleIdvAlue;
			}
			set
			{
				if (((_maXtAbleIdvAlue == value) 
							== false))
				{
					this.OnMaXTableIDValueChanging(value);
					this.SendPropertyChanging();
					this._maXtAbleIdvAlue = value;
					this.SendPropertyChanged("MaXTableIDValue");
					this.OnMaXTableIDValueChanged();
				}
			}
		}
		
		[Column(Storage="_maXtAbleXpAth", Name="max_table_xpath", DbType="nvarchar(500)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MaXTableXPath
		{
			get
			{
				return this._maXtAbleXpAth;
			}
			set
			{
				if (((_maXtAbleXpAth == value) 
							== false))
				{
					this.OnMaXTableXPathChanging(value);
					this.SendPropertyChanging();
					this._maXtAbleXpAth = value;
					this.SendPropertyChanged("MaXTableXPath");
					this.OnMaXTableXPathChanged();
				}
			}
		}
		
		[Column(Storage="_orderTableID", Name="order_table_id", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> OrderTableID
		{
			get
			{
				return this._orderTableID;
			}
			set
			{
				if ((_orderTableID != value))
				{
					this.OnOrderTableIDChanging(value);
					this.SendPropertyChanging();
					this._orderTableID = value;
					this.SendPropertyChanged("OrderTableID");
					this.OnOrderTableIDChanged();
				}
			}
		}
		
		[Column(Storage="_secondTableIdvAlue", Name="second_table_id_value", DbType="nvarchar(500)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string SecondTableIDValue
		{
			get
			{
				return this._secondTableIdvAlue;
			}
			set
			{
				if (((_secondTableIdvAlue == value) 
							== false))
				{
					this.OnSecondTableIDValueChanging(value);
					this.SendPropertyChanging();
					this._secondTableIdvAlue = value;
					this.SendPropertyChanged("SecondTableIDValue");
					this.OnSecondTableIDValueChanged();
				}
			}
		}
		
		[Column(Storage="_secondTableXpAth", Name="second_table_xpath", DbType="nvarchar(500)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string SecondTableXPath
		{
			get
			{
				return this._secondTableXpAth;
			}
			set
			{
				if (((_secondTableXpAth == value) 
							== false))
				{
					this.OnSecondTableXPathChanging(value);
					this.SendPropertyChanging();
					this._secondTableXpAth = value;
					this.SendPropertyChanged("SecondTableXPath");
					this.OnSecondTableXPathChanged();
				}
			}
		}
		
		[Column(Storage="_uriHost", Name="uri_host", DbType="nvarchar(500)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string UriHost
		{
			get
			{
				return this._uriHost;
			}
			set
			{
				if (((_uriHost == value) 
							== false))
				{
					this.OnUriHostChanging(value);
					this.SendPropertyChanging();
					this._uriHost = value;
					this.SendPropertyChanged("UriHost");
					this.OnUriHostChanged();
				}
			}
		}
		
		[Column(Storage="_uriHostID", Name="uri_host_id", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int UriHostID
		{
			get
			{
				return this._uriHostID;
			}
			set
			{
				if ((_uriHostID != value))
				{
					this.OnUriHostIDChanging(value);
					this.SendPropertyChanging();
					this._uriHostID = value;
					this.SendPropertyChanged("UriHostID");
					this.OnUriHostIDChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.result_tb")]
	public partial class ResultTB : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private string _awayTeam;
		
		private string _awayTeamBig;
		
		private string _fullTimeScore;
		
		private string _halfTimeScore;
		
		private string _homeTeam;
		
		private string _homeTeamBig;
		
		private string _htmlpOsition;
		
		private string _matchType;
		
		private string _odds;
		
		private int _resultTbid;
		
		private string _sdAte;
		
		private string _stIme;
		
		private string _winLossBig;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAwayTeamChanged();
		
		partial void OnAwayTeamChanging(string value);
		
		partial void OnAwayTeamBigChanged();
		
		partial void OnAwayTeamBigChanging(string value);
		
		partial void OnFullTimeScoreChanged();
		
		partial void OnFullTimeScoreChanging(string value);
		
		partial void OnHalfTimeScoreChanged();
		
		partial void OnHalfTimeScoreChanging(string value);
		
		partial void OnHomeTeamChanged();
		
		partial void OnHomeTeamChanging(string value);
		
		partial void OnHomeTeamBigChanged();
		
		partial void OnHomeTeamBigChanging(string value);
		
		partial void OnHTMLPositionChanged();
		
		partial void OnHTMLPositionChanging(string value);
		
		partial void OnMatchTypeChanged();
		
		partial void OnMatchTypeChanging(string value);
		
		partial void OnOddsChanged();
		
		partial void OnOddsChanging(string value);
		
		partial void OnResultTBIDChanged();
		
		partial void OnResultTBIDChanging(int value);
		
		partial void OnSDateChanged();
		
		partial void OnSDateChanging(string value);
		
		partial void OnSTimeChanged();
		
		partial void OnSTimeChanging(string value);
		
		partial void OnWinLossBigChanged();
		
		partial void OnWinLossBigChanging(string value);
		#endregion
		
		
		public ResultTB()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_awayTeam", Name="away_team", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AwayTeam
		{
			get
			{
				return this._awayTeam;
			}
			set
			{
				if (((_awayTeam == value) 
							== false))
				{
					this.OnAwayTeamChanging(value);
					this.SendPropertyChanging();
					this._awayTeam = value;
					this.SendPropertyChanged("AwayTeam");
					this.OnAwayTeamChanged();
				}
			}
		}
		
		[Column(Storage="_awayTeamBig", Name="away_team_big", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AwayTeamBig
		{
			get
			{
				return this._awayTeamBig;
			}
			set
			{
				if (((_awayTeamBig == value) 
							== false))
				{
					this.OnAwayTeamBigChanging(value);
					this.SendPropertyChanging();
					this._awayTeamBig = value;
					this.SendPropertyChanged("AwayTeamBig");
					this.OnAwayTeamBigChanged();
				}
			}
		}
		
		[Column(Storage="_fullTimeScore", Name="full_time_score", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string FullTimeScore
		{
			get
			{
				return this._fullTimeScore;
			}
			set
			{
				if (((_fullTimeScore == value) 
							== false))
				{
					this.OnFullTimeScoreChanging(value);
					this.SendPropertyChanging();
					this._fullTimeScore = value;
					this.SendPropertyChanged("FullTimeScore");
					this.OnFullTimeScoreChanged();
				}
			}
		}
		
		[Column(Storage="_halfTimeScore", Name="half_time_score", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HalfTimeScore
		{
			get
			{
				return this._halfTimeScore;
			}
			set
			{
				if (((_halfTimeScore == value) 
							== false))
				{
					this.OnHalfTimeScoreChanging(value);
					this.SendPropertyChanging();
					this._halfTimeScore = value;
					this.SendPropertyChanged("HalfTimeScore");
					this.OnHalfTimeScoreChanged();
				}
			}
		}
		
		[Column(Storage="_homeTeam", Name="home_team", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HomeTeam
		{
			get
			{
				return this._homeTeam;
			}
			set
			{
				if (((_homeTeam == value) 
							== false))
				{
					this.OnHomeTeamChanging(value);
					this.SendPropertyChanging();
					this._homeTeam = value;
					this.SendPropertyChanged("HomeTeam");
					this.OnHomeTeamChanged();
				}
			}
		}
		
		[Column(Storage="_homeTeamBig", Name="home_team_big", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HomeTeamBig
		{
			get
			{
				return this._homeTeamBig;
			}
			set
			{
				if (((_homeTeamBig == value) 
							== false))
				{
					this.OnHomeTeamBigChanging(value);
					this.SendPropertyChanging();
					this._homeTeamBig = value;
					this.SendPropertyChanged("HomeTeamBig");
					this.OnHomeTeamBigChanged();
				}
			}
		}
		
		[Column(Storage="_htmlpOsition", Name="html_position", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HTMLPosition
		{
			get
			{
				return this._htmlpOsition;
			}
			set
			{
				if (((_htmlpOsition == value) 
							== false))
				{
					this.OnHTMLPositionChanging(value);
					this.SendPropertyChanging();
					this._htmlpOsition = value;
					this.SendPropertyChanged("HTMLPosition");
					this.OnHTMLPositionChanged();
				}
			}
		}
		
		[Column(Storage="_matchType", Name="match_type", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MatchType
		{
			get
			{
				return this._matchType;
			}
			set
			{
				if (((_matchType == value) 
							== false))
				{
					this.OnMatchTypeChanging(value);
					this.SendPropertyChanging();
					this._matchType = value;
					this.SendPropertyChanged("MatchType");
					this.OnMatchTypeChanged();
				}
			}
		}
		
		[Column(Storage="_odds", Name="odds", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Odds
		{
			get
			{
				return this._odds;
			}
			set
			{
				if (((_odds == value) 
							== false))
				{
					this.OnOddsChanging(value);
					this.SendPropertyChanging();
					this._odds = value;
					this.SendPropertyChanged("Odds");
					this.OnOddsChanged();
				}
			}
		}
		
		[Column(Storage="_resultTbid", Name="result_tb_id", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ResultTBID
		{
			get
			{
				return this._resultTbid;
			}
			set
			{
				if ((_resultTbid != value))
				{
					this.OnResultTBIDChanging(value);
					this.SendPropertyChanging();
					this._resultTbid = value;
					this.SendPropertyChanged("ResultTBID");
					this.OnResultTBIDChanged();
				}
			}
		}
		
		[Column(Storage="_sdAte", Name="s_date", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string SDate
		{
			get
			{
				return this._sdAte;
			}
			set
			{
				if (((_sdAte == value) 
							== false))
				{
					this.OnSDateChanging(value);
					this.SendPropertyChanging();
					this._sdAte = value;
					this.SendPropertyChanged("SDate");
					this.OnSDateChanged();
				}
			}
		}
		
		[Column(Storage="_stIme", Name="s_time", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string STime
		{
			get
			{
				return this._stIme;
			}
			set
			{
				if (((_stIme == value) 
							== false))
				{
					this.OnSTimeChanging(value);
					this.SendPropertyChanging();
					this._stIme = value;
					this.SendPropertyChanged("STime");
					this.OnSTimeChanged();
				}
			}
		}
		
		[Column(Storage="_winLossBig", Name="win_loss_big", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string WinLossBig
		{
			get
			{
				return this._winLossBig;
			}
			set
			{
				if (((_winLossBig == value) 
							== false))
				{
					this.OnWinLossBigChanging(value);
					this.SendPropertyChanging();
					this._winLossBig = value;
					this.SendPropertyChanged("WinLossBig");
					this.OnWinLossBigChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[Table(Name="main.result_tb_lib")]
	public partial class ResultTBLib : System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	{
		
		private static System.ComponentModel.PropertyChangingEventArgs emptyChangingEventArgs = new System.ComponentModel.PropertyChangingEventArgs("");
		
		private System.Nullable<int> _awayRedCard;
		
		private string _awayTeam;
		
		private System.Nullable<int> _awayTeamBig;
		
		private System.Nullable<int> _fullAwayGoals;
		
		private System.Nullable<int> _fullHomeGoals;
		
		private System.Nullable<int> _halfAwayGoals;
		
		private System.Nullable<int> _halfHomeGoals;
		
		private System.Nullable<int> _homeRedCard;
		
		private string _homeTeam;
		
		private System.Nullable<int> _homeTeamBig;
		
		private System.Nullable<int> _htmlpOsition;
		
		private System.Nullable<System.DateTime> _matchTime;
		
		private string _matchType;
		
		private string _odds;
		
		private int _resultTblIbID;
		
		private string _winLossBig;
		
		#region Extensibility Method Declarations
		partial void OnCreated();
		
		partial void OnAwayRedCardChanged();
		
		partial void OnAwayRedCardChanging(System.Nullable<int> value);
		
		partial void OnAwayTeamChanged();
		
		partial void OnAwayTeamChanging(string value);
		
		partial void OnAwayTeamBigChanged();
		
		partial void OnAwayTeamBigChanging(System.Nullable<int> value);
		
		partial void OnFullAwayGoalsChanged();
		
		partial void OnFullAwayGoalsChanging(System.Nullable<int> value);
		
		partial void OnFullHomeGoalsChanged();
		
		partial void OnFullHomeGoalsChanging(System.Nullable<int> value);
		
		partial void OnHalfAwayGoalsChanged();
		
		partial void OnHalfAwayGoalsChanging(System.Nullable<int> value);
		
		partial void OnHalfHomeGoalsChanged();
		
		partial void OnHalfHomeGoalsChanging(System.Nullable<int> value);
		
		partial void OnHomeRedCardChanged();
		
		partial void OnHomeRedCardChanging(System.Nullable<int> value);
		
		partial void OnHomeTeamChanged();
		
		partial void OnHomeTeamChanging(string value);
		
		partial void OnHomeTeamBigChanged();
		
		partial void OnHomeTeamBigChanging(System.Nullable<int> value);
		
		partial void OnHTMLPositionChanged();
		
		partial void OnHTMLPositionChanging(System.Nullable<int> value);
		
		partial void OnMatchTimeChanged();
		
		partial void OnMatchTimeChanging(System.Nullable<System.DateTime> value);
		
		partial void OnMatchTypeChanged();
		
		partial void OnMatchTypeChanging(string value);
		
		partial void OnOddsChanged();
		
		partial void OnOddsChanging(string value);
		
		partial void OnResultTBLibIDChanged();
		
		partial void OnResultTBLibIDChanging(int value);
		
		partial void OnWinLossBigChanged();
		
		partial void OnWinLossBigChanging(string value);
		#endregion
		
		
		public ResultTBLib()
		{
			this.OnCreated();
		}
		
		[Column(Storage="_awayRedCard", Name="away_red_card", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> AwayRedCard
		{
			get
			{
				return this._awayRedCard;
			}
			set
			{
				if ((_awayRedCard != value))
				{
					this.OnAwayRedCardChanging(value);
					this.SendPropertyChanging();
					this._awayRedCard = value;
					this.SendPropertyChanged("AwayRedCard");
					this.OnAwayRedCardChanged();
				}
			}
		}
		
		[Column(Storage="_awayTeam", Name="away_team", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string AwayTeam
		{
			get
			{
				return this._awayTeam;
			}
			set
			{
				if (((_awayTeam == value) 
							== false))
				{
					this.OnAwayTeamChanging(value);
					this.SendPropertyChanging();
					this._awayTeam = value;
					this.SendPropertyChanged("AwayTeam");
					this.OnAwayTeamChanged();
				}
			}
		}
		
		[Column(Storage="_awayTeamBig", Name="away_team_big", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> AwayTeamBig
		{
			get
			{
				return this._awayTeamBig;
			}
			set
			{
				if ((_awayTeamBig != value))
				{
					this.OnAwayTeamBigChanging(value);
					this.SendPropertyChanging();
					this._awayTeamBig = value;
					this.SendPropertyChanged("AwayTeamBig");
					this.OnAwayTeamBigChanged();
				}
			}
		}
		
		[Column(Storage="_fullAwayGoals", Name="full_away_goals", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> FullAwayGoals
		{
			get
			{
				return this._fullAwayGoals;
			}
			set
			{
				if ((_fullAwayGoals != value))
				{
					this.OnFullAwayGoalsChanging(value);
					this.SendPropertyChanging();
					this._fullAwayGoals = value;
					this.SendPropertyChanged("FullAwayGoals");
					this.OnFullAwayGoalsChanged();
				}
			}
		}
		
		[Column(Storage="_fullHomeGoals", Name="full_home_goals", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> FullHomeGoals
		{
			get
			{
				return this._fullHomeGoals;
			}
			set
			{
				if ((_fullHomeGoals != value))
				{
					this.OnFullHomeGoalsChanging(value);
					this.SendPropertyChanging();
					this._fullHomeGoals = value;
					this.SendPropertyChanged("FullHomeGoals");
					this.OnFullHomeGoalsChanged();
				}
			}
		}
		
		[Column(Storage="_halfAwayGoals", Name="half_away_goals", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> HalfAwayGoals
		{
			get
			{
				return this._halfAwayGoals;
			}
			set
			{
				if ((_halfAwayGoals != value))
				{
					this.OnHalfAwayGoalsChanging(value);
					this.SendPropertyChanging();
					this._halfAwayGoals = value;
					this.SendPropertyChanged("HalfAwayGoals");
					this.OnHalfAwayGoalsChanged();
				}
			}
		}
		
		[Column(Storage="_halfHomeGoals", Name="half_home_goals", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> HalfHomeGoals
		{
			get
			{
				return this._halfHomeGoals;
			}
			set
			{
				if ((_halfHomeGoals != value))
				{
					this.OnHalfHomeGoalsChanging(value);
					this.SendPropertyChanging();
					this._halfHomeGoals = value;
					this.SendPropertyChanged("HalfHomeGoals");
					this.OnHalfHomeGoalsChanged();
				}
			}
		}
		
		[Column(Storage="_homeRedCard", Name="home_red_card", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> HomeRedCard
		{
			get
			{
				return this._homeRedCard;
			}
			set
			{
				if ((_homeRedCard != value))
				{
					this.OnHomeRedCardChanging(value);
					this.SendPropertyChanging();
					this._homeRedCard = value;
					this.SendPropertyChanged("HomeRedCard");
					this.OnHomeRedCardChanged();
				}
			}
		}
		
		[Column(Storage="_homeTeam", Name="home_team", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string HomeTeam
		{
			get
			{
				return this._homeTeam;
			}
			set
			{
				if (((_homeTeam == value) 
							== false))
				{
					this.OnHomeTeamChanging(value);
					this.SendPropertyChanging();
					this._homeTeam = value;
					this.SendPropertyChanged("HomeTeam");
					this.OnHomeTeamChanged();
				}
			}
		}
		
		[Column(Storage="_homeTeamBig", Name="home_team_big", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> HomeTeamBig
		{
			get
			{
				return this._homeTeamBig;
			}
			set
			{
				if ((_homeTeamBig != value))
				{
					this.OnHomeTeamBigChanging(value);
					this.SendPropertyChanging();
					this._homeTeamBig = value;
					this.SendPropertyChanged("HomeTeamBig");
					this.OnHomeTeamBigChanged();
				}
			}
		}
		
		[Column(Storage="_htmlpOsition", Name="html_position", DbType="integer", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<int> HTMLPosition
		{
			get
			{
				return this._htmlpOsition;
			}
			set
			{
				if ((_htmlpOsition != value))
				{
					this.OnHTMLPositionChanging(value);
					this.SendPropertyChanging();
					this._htmlpOsition = value;
					this.SendPropertyChanged("HTMLPosition");
					this.OnHTMLPositionChanged();
				}
			}
		}
		
		[Column(Storage="_matchTime", Name="match_time", DbType="datetime", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public System.Nullable<System.DateTime> MatchTime
		{
			get
			{
				return this._matchTime;
			}
			set
			{
				if ((_matchTime != value))
				{
					this.OnMatchTimeChanging(value);
					this.SendPropertyChanging();
					this._matchTime = value;
					this.SendPropertyChanged("MatchTime");
					this.OnMatchTimeChanged();
				}
			}
		}
		
		[Column(Storage="_matchType", Name="match_type", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string MatchType
		{
			get
			{
				return this._matchType;
			}
			set
			{
				if (((_matchType == value) 
							== false))
				{
					this.OnMatchTypeChanging(value);
					this.SendPropertyChanging();
					this._matchType = value;
					this.SendPropertyChanged("MatchType");
					this.OnMatchTypeChanged();
				}
			}
		}
		
		[Column(Storage="_odds", Name="odds", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string Odds
		{
			get
			{
				return this._odds;
			}
			set
			{
				if (((_odds == value) 
							== false))
				{
					this.OnOddsChanging(value);
					this.SendPropertyChanging();
					this._odds = value;
					this.SendPropertyChanged("Odds");
					this.OnOddsChanged();
				}
			}
		}
		
		[Column(Storage="_resultTblIbID", Name="result_tb_lib_id", DbType="integer", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.Never, CanBeNull=false)]
		[DebuggerNonUserCode()]
		public int ResultTBLibID
		{
			get
			{
				return this._resultTblIbID;
			}
			set
			{
				if ((_resultTblIbID != value))
				{
					this.OnResultTBLibIDChanging(value);
					this.SendPropertyChanging();
					this._resultTblIbID = value;
					this.SendPropertyChanged("ResultTBLibID");
					this.OnResultTBLibIDChanged();
				}
			}
		}
		
		[Column(Storage="_winLossBig", Name="win_loss_big", DbType="nvarchar(50)", AutoSync=AutoSync.Never)]
		[DebuggerNonUserCode()]
		public string WinLossBig
		{
			get
			{
				return this._winLossBig;
			}
			set
			{
				if (((_winLossBig == value) 
							== false))
				{
					this.OnWinLossBigChanging(value);
					this.SendPropertyChanging();
					this._winLossBig = value;
					this.SendPropertyChanged("WinLossBig");
					this.OnWinLossBigChanged();
				}
			}
		}
		
		public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			System.ComponentModel.PropertyChangingEventHandler h = this.PropertyChanging;
			if ((h != null))
			{
				h(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler h = this.PropertyChanged;
			if ((h != null))
			{
				h(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
