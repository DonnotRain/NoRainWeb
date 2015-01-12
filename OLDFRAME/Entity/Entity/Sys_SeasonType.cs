/**  版本信息模板在安装目录下，可自行修改。
* Sys_SeasonType.cs
*
* 功 能： N/A
* 类 名： Sys_SeasonType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:30:08   N/A    初版
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
	/// Sys_SeasonType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_SeasonType
	{
		public Sys_SeasonType()
		{}
		#region Model
		private int _id;
		private int? _seasontype;
		private DateTime? _begdate;
		private DateTime? _enddate;
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
		public int? SeasonType
		{
			set{ _seasontype=value;}
			get{return _seasontype;}
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
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

