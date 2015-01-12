/**  版本信息模板在安装目录下，可自行修改。
* Sys_ChCode.cs
*
* 功 能： N/A
* 类 名： Sys_ChCode
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:46   N/A    初版
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
	/// Sys_ChCode:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_ChCode
	{
		public Sys_ChCode()
		{}
		#region Model
		private int _codeid;
		private string _chinese;
		private string _pycode;
		private string _wbcode;
		/// <summary>
		/// 
		/// </summary>
		public int CodeID
		{
			set{ _codeid=value;}
			get{return _codeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Chinese
		{
			set{ _chinese=value;}
			get{return _chinese;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PYCode
		{
			set{ _pycode=value;}
			get{return _pycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WBCode
		{
			set{ _wbcode=value;}
			get{return _wbcode;}
		}
		#endregion Model

	}
}

