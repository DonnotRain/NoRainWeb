/**  版本信息模板在安装目录下，可自行修改。
* Sys_TicketModelGroup.cs
*
* 功 能： N/A
* 类 名： Sys_TicketModelGroup
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:30:15   N/A    初版
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
	/// Sys_TicketModelGroup:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_TicketModelGroup
	{
		public Sys_TicketModelGroup()
		{}
		#region Model
		private int _id;
		private string _ticketmodelgroupcode;
		private string _ticketmodelgroupname;
		private int? _recflag=1;
		private int? _useflag=1;
		private int? _sort=0;
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
		public string TicketModelGroupCode
		{
			set{ _ticketmodelgroupcode=value;}
			get{return _ticketmodelgroupcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TicketModelGroupName
		{
			set{ _ticketmodelgroupname=value;}
			get{return _ticketmodelgroupname;}
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
		public int? UseFlag
		{
			set{ _useflag=value;}
			get{return _useflag;}
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

