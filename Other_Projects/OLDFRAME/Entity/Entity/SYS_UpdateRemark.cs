/**  版本信息模板在安装目录下，可自行修改。
* SYS_UpdateRemark.cs
*
* 功 能： N/A
* 类 名： SYS_UpdateRemark
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:30:17   N/A    初版
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
	/// SYS_UpdateRemark:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SYS_UpdateRemark
	{
		public SYS_UpdateRemark()
		{}
		#region Model
		private int _id;
		private DateTime? _updatedate;
		private string _updatetopic;
		private string _updatecontent;
		private string _updatename;
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
		public DateTime? UpdateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UpdateTopic
		{
			set{ _updatetopic=value;}
			get{return _updatetopic;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UpdateContent
		{
			set{ _updatecontent=value;}
			get{return _updatecontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UpdateName
		{
			set{ _updatename=value;}
			get{return _updatename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Operatorname
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

