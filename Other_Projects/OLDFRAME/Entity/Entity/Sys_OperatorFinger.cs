/**  版本信息模板在安装目录下，可自行修改。
* Sys_OperatorFinger.cs
*
* 功 能： N/A
* 类 名： Sys_OperatorFinger
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:52   N/A    初版
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
	/// Sys_OperatorFinger:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_OperatorFinger
	{
		public Sys_OperatorFinger()
		{}
		#region Model
		private string _operatorcode;
		private string _finger1;
		private string _finger2;
		private string _finger3;
		private string _finger4;
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
		public string Finger1
		{
			set{ _finger1=value;}
			get{return _finger1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Finger2
		{
			set{ _finger2=value;}
			get{return _finger2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Finger3
		{
			set{ _finger3=value;}
			get{return _finger3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Finger4
		{
			set{ _finger4=value;}
			get{return _finger4;}
		}
		#endregion Model

	}
}

