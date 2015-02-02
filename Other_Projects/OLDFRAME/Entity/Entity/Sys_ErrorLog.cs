/**  版本信息模板在安装目录下，可自行修改。
* Sys_ErrorLog.cs
*
* 功 能： N/A
* 类 名： Sys_ErrorLog
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:48   N/A    初版
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
	/// Sys_ErrorLog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_ErrorLog
	{
		public Sys_ErrorLog()
		{}
		#region Model
		private string _recver;
		private string _billno;
		private string _errmsg;
		private DateTime? _errortime= DateTime.Now;
		private string _optorcode;
		/// <summary>
		/// 
		/// </summary>
		public string RECVER
		{
			set{ _recver=value;}
			get{return _recver;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BILLNO
		{
			set{ _billno=value;}
			get{return _billno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ERRMSG
		{
			set{ _errmsg=value;}
			get{return _errmsg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ERRORTIME
		{
			set{ _errortime=value;}
			get{return _errortime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OPTORCODE
		{
			set{ _optorcode=value;}
			get{return _optorcode;}
		}
		#endregion Model

	}
}

