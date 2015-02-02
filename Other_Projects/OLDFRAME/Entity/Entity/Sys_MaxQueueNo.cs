﻿/**  版本信息模板在安装目录下，可自行修改。
* Sys_MaxQueueNo.cs
*
* 功 能： N/A
* 类 名： Sys_MaxQueueNo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/5/28 18:29:50   N/A    初版
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
	/// Sys_MaxQueueNo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Sys_MaxQueueNo
	{
		public Sys_MaxQueueNo()
		{}
		#region Model
		private int _recid;
		private string _parkcode;
		private int? _maxqueueno;
		private string _periodtime;
		private int? _queuecount;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int RecID
		{
			set{ _recid=value;}
			get{return _recid;}
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
		public int? MaxQueueNo
		{
			set{ _maxqueueno=value;}
			get{return _maxqueueno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PeriodTime
		{
			set{ _periodtime=value;}
			get{return _periodtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? QueueCount
		{
			set{ _queuecount=value;}
			get{return _queuecount;}
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

