/**  版本信息模板在安装目录下，可自行修改。
* Sys_SeasonDay.cs
*
* 功 能： N/A
* 类 名： Sys_SeasonDay
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:30:07   N/A    初版
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
	/// Sys_SeasonDay:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_SeasonDay
	{
		public Sys_SeasonDay()
		{}
		#region Model
		private int? _seasontype;
		private DateTime? _seasonday;
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
		public DateTime? SeasonDay
		{
			set{ _seasonday=value;}
			get{return _seasonday;}
		}
		#endregion Model

	}
}

