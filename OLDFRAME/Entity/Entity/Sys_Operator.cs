/**  版本信息模板在安装目录下，可自行修改。
* Sys_Operator.cs
*
* 功 能： N/A
* 类 名： Sys_Operator
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:51   N/A    初版
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
	/// Sys_Operator:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_Operator
	{
		public Sys_Operator()
		{}
		#region Model
		private int _id;
		private string _operatorcode;
		private string _operatorname;
		private string _operatorpass;
		private int? _isticketseller=0;
		private int? _isadmin=0;
		private int? _isstockman=0;
		private int? _tdticketflag=1;
		private int? _skticketflag=1;
		private int? _conticketflag=1;
		private int? _parkpowerflag=1;
		private int? _deptid;
		private string _deptcode;
		private string _parkcode="0";
		private string _storagecode="0";
		private string _telno;
		private int? _useflag=1;
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
		public string OperatorCode
		{
			set{ _operatorcode=value;}
			get{return _operatorcode;}
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
		public string OperatorPass
		{
			set{ _operatorpass=value;}
			get{return _operatorpass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsTicketSeller
		{
			set{ _isticketseller=value;}
			get{return _isticketseller;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsAdmin
		{
			set{ _isadmin=value;}
			get{return _isadmin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsStockMan
		{
			set{ _isstockman=value;}
			get{return _isstockman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TDTicketFlag
		{
			set{ _tdticketflag=value;}
			get{return _tdticketflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SKTicketFlag
		{
			set{ _skticketflag=value;}
			get{return _skticketflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ConTicketFlag
		{
			set{ _conticketflag=value;}
			get{return _conticketflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ParkPowerFlag
		{
			set{ _parkpowerflag=value;}
			get{return _parkpowerflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DEPTID
		{
			set{ _deptid=value;}
			get{return _deptid;}
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
		public string ParkCode
		{
			set{ _parkcode=value;}
			get{return _parkcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StorageCode
		{
			set{ _storagecode=value;}
			get{return _storagecode;}
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
		public int? UseFlag
		{
			set{ _useflag=value;}
			get{return _useflag;}
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

