/**  版本信息模板在安装目录下，可自行修改。
* SYS_OPERATORLOG.cs
*
* 功 能： N/A
* 类 名： SYS_OPERATORLOG
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:53   N/A    初版
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
	/// SYS_OPERATORLOG:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SYS_OPERATORLOG
	{
		public SYS_OPERATORLOG()
		{}
		#region Model
		private int _id;
		private DateTime? _ddate;
		private string _loglevel;
		private string _logmain;
		private string _logcontent;
		private string _operatorid;
		private string _plugid;
		private string _funname;
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
		public DateTime? DDATE
		{
			set{ _ddate=value;}
			get{return _ddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LOGLEVEL
		{
			set{ _loglevel=value;}
			get{return _loglevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LOGMAIN
		{
			set{ _logmain=value;}
			get{return _logmain;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LOGCONTENT
		{
			set{ _logcontent=value;}
			get{return _logcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OPERATORID
		{
			set{ _operatorid=value;}
			get{return _operatorid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PLUGID
		{
			set{ _plugid=value;}
			get{return _plugid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FUNNAME
		{
			set{ _funname=value;}
			get{return _funname;}
		}
		#endregion Model

	}
}

