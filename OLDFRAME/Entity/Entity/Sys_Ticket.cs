/**  版本信息模板在安装目录下，可自行修改。
* Sys_Ticket.cs
*
* 功 能： N/A
* 类 名： Sys_Ticket
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:30:10   N/A    初版
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
	/// Sys_Ticket:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_Ticket
	{
		public Sys_Ticket()
		{}
		#region Model
		private int _id;
		private string _ticketcode;
		private string _ticketfullname;
		private string _ticketshortname;
		private string _ticketengname;
		private string _tickettype;
		private string _ticketkind;
		private int? _seasontype=1;
		private decimal? _price;
		private string _parkcode;
		private DateTime? _createtime;
		private string _createperson;
		private int? _auditflag;
		private DateTime? _audittime;
		private string _auditor;
		private int? _recflag=1;
		private int? _transflag=1;
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
		public string TicketCode
		{
			set{ _ticketcode=value;}
			get{return _ticketcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketFullName
		{
			set{ _ticketfullname=value;}
			get{return _ticketfullname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketShortName
		{
			set{ _ticketshortname=value;}
			get{return _ticketshortname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketEngName
		{
			set{ _ticketengname=value;}
			get{return _ticketengname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketType
		{
			set{ _tickettype=value;}
			get{return _tickettype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketKind
		{
			set{ _ticketkind=value;}
			get{return _ticketkind;}
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
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParkCode
		{
			set{ _parkcode=value;}
			get{return _parkcode;}
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
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

