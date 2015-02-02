/**  版本信息模板在安装目录下，可自行修改。
* Sys_TicketModel.cs
*
* 功 能： N/A
* 类 名： Sys_TicketModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:30:11   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace MPAPI.Model.Entity
{
	/// <summary>
	/// Sys_TicketModel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_TicketModel
	{
		public Sys_TicketModel()
		{}
		#region Model
		private int _id;
		private string _ticketmodelcode;
		private string _webticketmodelcode;
		private string _ticketmodelname;
		private string _ticketmodelengname;
		private string _ticketmodellevel;
		private string _ticketsalemodel;
		private int? _ticketunitcount;
		private string _ticketmodeltype;
		private string _ticketmodelkind;
		private string _ticketmodelgroupcode;
		private string _ticketmodelpycode;
		private int? _playsoundflag;
		private string _barcodetype;
		private int? _seasontype;
		private decimal? _pricesum;
		private int? _rebate;
		private decimal? _rebateprice;
		private int? _buylimitcount=1;
		private int? _daterangeflag=0;
		private int? _validdays=1;
		private DateTime? _begdate;
		private DateTime? _enddate;
		private int? _beforedays=0;
		private string _seatareacode;
		private decimal? _rewards=0M;
		private int? _touristcount=1;
		private int? _sort;
		private int? _useflag;
		private int? _recflag=1;
		private int? _transflag=1;
		private int? _monflag=1;
		private int? _tuesflag=1;
		private int? _wedflag=1;
		private int? _thursflag=1;
		private int? _fridayflag=1;
		private int? _satflag=1;
		private int? _sunflag=1;
		private int? _printflag=1;
		private int? _depositflag=0;
		private int? _verifyfingerflag=0;
		private DateTime? _createtime;
		private string _createperson;
		private int? _auditflag;
		private DateTime? _audittime;
		private string _auditor;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketModelCode
		{
			set{ _ticketmodelcode=value;}
			get{return _ticketmodelcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WebTicketModelCode
		{
			set{ _webticketmodelcode=value;}
			get{return _webticketmodelcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketModelName
		{
			set{ _ticketmodelname=value;}
			get{return _ticketmodelname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketModelEngName
		{
			set{ _ticketmodelengname=value;}
			get{return _ticketmodelengname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketModelLevel
		{
			set{ _ticketmodellevel=value;}
			get{return _ticketmodellevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketSaleModel
		{
			set{ _ticketsalemodel=value;}
			get{return _ticketsalemodel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TicketUnitCount
		{
			set{ _ticketunitcount=value;}
			get{return _ticketunitcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketModelType
		{
			set{ _ticketmodeltype=value;}
			get{return _ticketmodeltype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketModelKind
		{
			set{ _ticketmodelkind=value;}
			get{return _ticketmodelkind;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketModelGroupCode
		{
			set{ _ticketmodelgroupcode=value;}
			get{return _ticketmodelgroupcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketModelPYCode
		{
			set{ _ticketmodelpycode=value;}
			get{return _ticketmodelpycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PlaySoundFlag
		{
			set{ _playsoundflag=value;}
			get{return _playsoundflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BarCodeType
		{
			set{ _barcodetype=value;}
			get{return _barcodetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SeasonType
		{
			set{ _seasontype=value;}
			get{return _seasontype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PriceSum
		{
			set{ _pricesum=value;}
			get{return _pricesum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Rebate
		{
			set{ _rebate=value;}
			get{return _rebate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? RebatePrice
		{
			set{ _rebateprice=value;}
			get{return _rebateprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BuyLimitCount
		{
			set{ _buylimitcount=value;}
			get{return _buylimitcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DateRangeFlag
		{
			set{ _daterangeflag=value;}
			get{return _daterangeflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ValidDays
		{
			set{ _validdays=value;}
			get{return _validdays;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? BegDate
		{
			set{ _begdate=value;}
			get{return _begdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BeforeDays
		{
			set{ _beforedays=value;}
			get{return _beforedays;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SeatAreaCode
		{
			set{ _seatareacode=value;}
			get{return _seatareacode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Rewards
		{
			set{ _rewards=value;}
			get{return _rewards;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TouristCount
		{
			set{ _touristcount=value;}
			get{return _touristcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UseFlag
		{
			set{ _useflag=value;}
			get{return _useflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RecFlag
		{
			set{ _recflag=value;}
			get{return _recflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TransFlag
		{
			set{ _transflag=value;}
			get{return _transflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? MonFlag
		{
			set{ _monflag=value;}
			get{return _monflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TuesFlag
		{
			set{ _tuesflag=value;}
			get{return _tuesflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? WedFlag
		{
			set{ _wedflag=value;}
			get{return _wedflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ThursFlag
		{
			set{ _thursflag=value;}
			get{return _thursflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FridayFlag
		{
			set{ _fridayflag=value;}
			get{return _fridayflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SatFlag
		{
			set{ _satflag=value;}
			get{return _satflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SunFlag
		{
			set{ _sunflag=value;}
			get{return _sunflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PrintFlag
		{
			set{ _printflag=value;}
			get{return _printflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DepositFlag
		{
			set{ _depositflag=value;}
			get{return _depositflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? VerifyFingerFlag
		{
			set{ _verifyfingerflag=value;}
			get{return _verifyfingerflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreatePerson
		{
			set{ _createperson=value;}
			get{return _createperson;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AuditFlag
		{
			set{ _auditflag=value;}
			get{return _auditflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AuditTime
		{
			set{ _audittime=value;}
			get{return _audittime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Auditor
		{
			set{ _auditor=value;}
			get{return _auditor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

