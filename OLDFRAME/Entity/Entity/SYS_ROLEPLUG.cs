/**  版本信息模板在安装目录下，可自行修改。
* SYS_ROLEPLUG.cs
*
* 功 能： N/A
* 类 名： SYS_ROLEPLUG
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:30:05   N/A    初版
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
	/// SYS_ROLEPLUG:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SYS_ROLEPLUG
	{
		public SYS_ROLEPLUG()
		{}
		#region Model
		private int _id;
		private int? _roleid;
		private int? _plugid;
		private int? _rightflag;
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
		public int? ROLEID
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PLUGID
		{
			set{ _plugid=value;}
			get{return _plugid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RIGHTFlag
		{
			set{ _rightflag=value;}
			get{return _rightflag;}
		}
		#endregion Model

	}
}

