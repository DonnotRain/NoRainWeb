/**  版本信息模板在安装目录下，可自行修改。
* Sys_ParkPic.cs
*
* 功 能： N/A
* 类 名： Sys_ParkPic
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:58   N/A    初版
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
	/// Sys_ParkPic:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_ParkPic
	{
		public Sys_ParkPic()
		{}
		#region Model
		private string _parkcode;
		private byte[] _pic;
		private byte[] _checkbackpic;
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
		public byte[] Pic
		{
			set{ _pic=value;}
			get{return _pic;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] CheckBackpic
		{
			set{ _checkbackpic=value;}
			get{return _checkbackpic;}
		}
		#endregion Model

	}
}

