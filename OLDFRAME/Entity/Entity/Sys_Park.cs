/**  版本信息模板在安装目录下，可自行修改。
* Sys_Park.cs
*
* 功 能： N/A
* 类 名： Sys_Park
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
	/// Sys_Park:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_Park
	{
		public Sys_Park()
		{}
		#region Model
		private int _id;
		private int? _parkpid;
		private string _parkcode;
		private string _parkfullname;
		private string _parkshortname;
		private string _parkengname;
		private string _leader;
		private string _telno;
		private string _faxno;
		private string _address;
		private int? _sort=0;
		private int? _recflag=1;
		private int? _transflag=1;
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
		public int? ParkPID
		{
			set{ _parkpid=value;}
			get{return _parkpid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParkCode
		{
			set{ _parkcode=value;}
			get{return _parkcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParkFullName
		{
			set{ _parkfullname=value;}
			get{return _parkfullname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParkShortName
		{
			set{ _parkshortname=value;}
			get{return _parkshortname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ParkEngName
		{
			set{ _parkengname=value;}
			get{return _parkengname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Leader
		{
			set{ _leader=value;}
			get{return _leader;}
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
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
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

