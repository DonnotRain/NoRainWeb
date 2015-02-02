/**  版本信息模板在安装目录下，可自行修改。
* SYS_FreeTicketPower.cs
*
* 功 能： N/A
* 类 名： SYS_FreeTicketPower
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:49   N/A    初版
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
	/// SYS_FreeTicketPower:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SYS_FreeTicketPower
	{
		public SYS_FreeTicketPower()
		{}
		#region Model
		private int _id;
		private string _operatorcode;
		private string _operatorname;
		private int? _halfticketcount=0;
		private int? _freeticketcount=0;
		private DateTime? _begindate;
		private DateTime? _enddate;
		private DateTime? _recmodtime;
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
		public string OperatorCode
		{
			set{ _operatorcode=value;}
			get{return _operatorcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperatorName
		{
			set{ _operatorname=value;}
			get{return _operatorname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? HalfTicketCount
		{
			set{ _halfticketcount=value;}
			get{return _halfticketcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FreeTicketCount
		{
			set{ _freeticketcount=value;}
			get{return _freeticketcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? BeginDate
		{
			set{ _begindate=value;}
			get{return _begindate;}
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
		public DateTime? RecModTime
		{
			set{ _recmodtime=value;}
			get{return _recmodtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReMark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

