/**  版本信息模板在安装目录下，可自行修改。
* Sys_Plug.cs
*
* 功 能： N/A
* 类 名： Sys_Plug
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:30:00   N/A    初版
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
	/// Sys_Plug:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_Plug
	{
		public Sys_Plug()
		{}
		#region Model
		private int _id;
		private string _plugcode;
		private string _plugname;
		private string _plugfile;
		private int? _parentid=1;
		private int? _plugtype;
		private int? _sort;
		private int? _state;
		private string _version;
		private string _shortcut;
		private int? _imageindex;
		private int? _grouphead;
		private int? _refreshdata=1;
		private int? _savedata=0;
		private int? _savetoexcel=1;
		private int? _importdata=0;
		private int? _exportdata=0;
		private int? _printsetup=1;
		private int? _printflag=1;
		private int? _printpreview=1;
		private int? _findvalue=0;
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
		public string PlugCode
		{
			set{ _plugcode=value;}
			get{return _plugcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PlugName
		{
			set{ _plugname=value;}
			get{return _plugname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PLUGFILE
		{
			set{ _plugfile=value;}
			get{return _plugfile;}
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
		public int? PlugType
		{
			set{ _plugtype=value;}
			get{return _plugtype;}
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
		public int? STATE
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string VERSION
		{
			set{ _version=value;}
			get{return _version;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SHORTCUT
		{
			set{ _shortcut=value;}
			get{return _shortcut;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IMAGEINDEX
		{
			set{ _imageindex=value;}
			get{return _imageindex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? GROUPHEAD
		{
			set{ _grouphead=value;}
			get{return _grouphead;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? REFRESHDATA
		{
			set{ _refreshdata=value;}
			get{return _refreshdata;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SAVEDATA
		{
			set{ _savedata=value;}
			get{return _savedata;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SAVETOEXCEL
		{
			set{ _savetoexcel=value;}
			get{return _savetoexcel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IMPORTDATA
		{
			set{ _importdata=value;}
			get{return _importdata;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EXPORTDATA
		{
			set{ _exportdata=value;}
			get{return _exportdata;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PRINTSETUP
		{
			set{ _printsetup=value;}
			get{return _printsetup;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PRINTFLAG
		{
			set{ _printflag=value;}
			get{return _printflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PRINTPREVIEW
		{
			set{ _printpreview=value;}
			get{return _printpreview;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FINDVALUE
		{
			set{ _findvalue=value;}
			get{return _findvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string REMARK
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

