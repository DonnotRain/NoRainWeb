/**  版本信息模板在安装目录下，可自行修改。
* Sys_Area.cs
*
* 功 能： N/A
* 类 名： Sys_Area
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:44   N/A    初版
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
	/// Sys_Area:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_Area
	{
		public Sys_Area()
		{}
		#region Model
		private int _id;
		private int? _parentid;
		private string _areacode;
		private string _areaname;
		private int? _sort=0;
		private string _remark;
		private int? _recflag=1;
		private int? _transflag=1;
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
		public int? ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AreaCode
		{
			set{ _areacode=value;}
			get{return _areacode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AreaName
		{
			set{ _areaname=value;}
			get{return _areaname;}
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
		#endregion Model

	}
}

