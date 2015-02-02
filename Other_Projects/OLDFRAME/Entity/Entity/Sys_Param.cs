/**  版本信息模板在安装目录下，可自行修改。
* Sys_Param.cs
*
* 功 能： N/A
* 类 名： Sys_Param
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:56   N/A    初版
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
	/// Sys_Param:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_Param
	{
		public Sys_Param()
		{}
		#region Model
		private int _id;
		private string _paramcode;
		private string _paramname;
		private string _paramvalue;
		private string _operatorname;
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
		public string ParamCode
		{
			set{ _paramcode=value;}
			get{return _paramcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParamName
		{
			set{ _paramname=value;}
			get{return _paramname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParamValue
		{
			set{ _paramvalue=value;}
			get{return _paramvalue;}
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
		public DateTime? RecModTime
		{
			set{ _recmodtime=value;}
			get{return _recmodtime;}
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

