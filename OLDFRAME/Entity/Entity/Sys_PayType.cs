/**  版本信息模板在安装目录下，可自行修改。
* Sys_PayType.cs
*
* 功 能： N/A
* 类 名： Sys_PayType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:59   N/A    初版
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
	/// Sys_PayType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_PayType
	{
		public Sys_PayType()
		{}
		#region Model
		private int _id;
		private string _paytypecode;
		private string _paytypename;
		private string _clienttype;
		private int? _rectype=1;
		private int? _useflag=1;
		private decimal? _sort=0M;
		private string _operatorname;
		private DateTime? _recmodtime= DateTime.Now;
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
		public string PayTypeCode
		{
			set{ _paytypecode=value;}
			get{return _paytypecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PayTypeName
		{
			set{ _paytypename=value;}
			get{return _paytypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ClientType
		{
			set{ _clienttype=value;}
			get{return _clienttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RecType
		{
			set{ _rectype=value;}
			get{return _rectype;}
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
		public decimal? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
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

