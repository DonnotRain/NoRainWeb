/**  版本信息模板在安装目录下，可自行修改。
* Sys_TicketModelDetail.cs
*
* 功 能： N/A
* 类 名： Sys_TicketModelDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:30:13   N/A    初版
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
	/// Sys_TicketModelDetail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_TicketModelDetail
	{
		public Sys_TicketModelDetail()
		{}
		#region Model
		private int _id;
		private int _ticketid;
		private int? _ticketmodelid;
		private string _ticketmodelcode;
		private int? _recflag=1;
		private int? _transflag=1;
		private decimal? _dividepercent=0M;
		private decimal? _dividevalue=0M;
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
		public int TicketID
		{
			set{ _ticketid=value;}
			get{return _ticketid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TicketModelID
		{
			set{ _ticketmodelid=value;}
			get{return _ticketmodelid;}
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
		public decimal? DividePercent
		{
			set{ _dividepercent=value;}
			get{return _dividepercent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DivideValue
		{
			set{ _dividevalue=value;}
			get{return _dividevalue;}
		}
		#endregion Model

	}
}

