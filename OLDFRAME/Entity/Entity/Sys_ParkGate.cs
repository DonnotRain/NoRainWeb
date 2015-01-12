/**  版本信息模板在安装目录下，可自行修改。
* Sys_ParkGate.cs
*
* 功 能： N/A
* 类 名： Sys_ParkGate
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:57   N/A    初版
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
	/// Sys_ParkGate:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_ParkGate
	{
		public Sys_ParkGate()
		{}
		#region Model
		private int _id;
		private string _parkcode;
		private string _gatename;
		private string _gateno;
		private string _ipaddress;
		private int? _gatetype=0;
		private int? _linkstatus=0;
		private int? _sort=0;
		private int? _timesusercount=0;
		private byte[] _gateimage;
		private string _extendcol1;
		private string _extendcol2;
		private string _extendcol3;
		private string _extendcol4;
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
		public string ParkCode
		{
			set{ _parkcode=value;}
			get{return _parkcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GateName
		{
			set{ _gatename=value;}
			get{return _gatename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GateNo
		{
			set{ _gateno=value;}
			get{return _gateno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IPAddress
		{
			set{ _ipaddress=value;}
			get{return _ipaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? GateType
		{
			set{ _gatetype=value;}
			get{return _gatetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LinkStatus
		{
			set{ _linkstatus=value;}
			get{return _linkstatus;}
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
		public int? TimesUserCount
		{
			set{ _timesusercount=value;}
			get{return _timesusercount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] GateImage
		{
			set{ _gateimage=value;}
			get{return _gateimage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ExtendCol1
		{
			set{ _extendcol1=value;}
			get{return _extendcol1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ExtendCol2
		{
			set{ _extendcol2=value;}
			get{return _extendcol2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ExtendCol3
		{
			set{ _extendcol3=value;}
			get{return _extendcol3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ExtendCol4
		{
			set{ _extendcol4=value;}
			get{return _extendcol4;}
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

