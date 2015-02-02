/**  版本信息模板在安装目录下，可自行修改。
* Sys_FeeType.cs
*
* 功 能： N/A
* 类 名： Sys_FeeType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:49   N/A    初版
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
	/// Sys_FeeType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_FeeType
	{
		public Sys_FeeType()
		{}
		#region Model
		private int _id;
		private string _feename;
		private string _feecode;
		private decimal? _feesum;
		private DateTime? _recmodtime;
		private int? _useflag;
		private int? _recflag=1;
		private string _operatorname;
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
		public string FeeName
		{
			set{ _feename=value;}
			get{return _feename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FeeCode
		{
			set{ _feecode=value;}
			get{return _feecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? FeeSum
		{
			set{ _feesum=value;}
			get{return _feesum;}
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
		public string operatorName
		{
			set{ _operatorname=value;}
			get{return _operatorname;}
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

