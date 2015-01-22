﻿/**  版本信息模板在安装目录下，可自行修改。
* Sys_OperatorLogin.cs
*
* 功 能： N/A
* 类 名： Sys_OperatorLogin
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:54   N/A    初版
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
	/// Sys_OperatorLogin:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_OperatorLogin
	{
		public Sys_OperatorLogin()
		{}
		#region Model
		private string _operatorcode;
		private string _lastloginip;
		private DateTime? _lastlogintime;
		private int? _loginstatus=1;
		private string _remark;
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
		public string LastLoginIP
		{
			set{ _lastloginip=value;}
			get{return _lastloginip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastLoginTime
		{
			set{ _lastlogintime=value;}
			get{return _lastlogintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LoginStatus
		{
			set{ _loginstatus=value;}
			get{return _loginstatus;}
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
