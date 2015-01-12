/**  版本信息模板在安装目录下，可自行修改。
* Sys_Dept.cs
*
* 功 能： N/A
* 类 名： Sys_Dept
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:47   N/A    初版
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
	/// Sys_Dept:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_Dept
	{
		public Sys_Dept()
		{}
		#region Model
		private int _id;
		private int? _parentid;
		private string _deptcode;
		private string _deptname;
		private string _deptleader;
		private string _telno;
		private string _faxno;
		private string _email;
		private int? _sort=0;
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
		public int? PARENTID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeptCode
		{
			set{ _deptcode=value;}
			get{return _deptcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeptName
		{
			set{ _deptname=value;}
			get{return _deptname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeptLeader
		{
			set{ _deptleader=value;}
			get{return _deptleader;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TelNo
		{
			set{ _telno=value;}
			get{return _telno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FaxNo
		{
			set{ _faxno=value;}
			get{return _faxno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? sort
		{
			set{ _sort=value;}
			get{return _sort;}
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

