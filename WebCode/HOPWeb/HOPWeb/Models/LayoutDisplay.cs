﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using HuaweiSoftware.WQT.WebBase;
using HuaweiSoftware.WQT.IBll;
using WQTRights;

namespace BusinessWeb.Areas.Back.Models
{

    public class PlugDisplay
    {
        public string PlugName { get; set; }

        public string PlugCode { get; set; }

        public string PlugUrl { get; set; }

        public string ImageClass { get; set; }

        public List<PlugDisplay> Children { get; set; }

    }

    public class PlugParser
    {
        private static ICommonSecurityBLL m_bll = DPResolver.Resolver<ICommonSecurityBLL>();
        private static IParameterBll m_parametebll = DPResolver.Resolver<IParameterBll>();
        public static List<PlugDisplay> Parser()
        {
            var funcList = m_bll.FindAll<Function>(string.Empty).ToList();  //不进行权限控制时
           // var funcList = new List<Function>();

            var roleBLL = DPResolver.Resolver<IRoleBLL>();
            //获取当前用户全部角色的权限
            var roleInfos = roleBLL.GetUserRoles(SysContext.UserId);

            var functionBLL = DPResolver.Resolver<IFunctionBLL>();

            roleInfos.ToList().ForEach(m =>
            {
                var currentRoleFuncs = functionBLL.GetRoleFunctions(m.ID).OrderBy(model => model.Sort);
                currentRoleFuncs.ToList().ForEach(func =>
                {
                    if (!funcList.Select(model => model.ID).Contains(func.ID))
                    {
                        funcList.Add(func);
                    }
                });
            });

            //顶级为-1 类型为1
            return GetChildrenPlug(funcList.Where(m => m.FunctionType == 1).OrderBy(m => m.Sort).ToList(), -1);
        }

        private static List<PlugDisplay> GetChildrenPlug(List<Function> srcPlugs, int parentId)
        {
            var items = srcPlugs.Where(m => m.PID == parentId).Select(item => new PlugDisplay()
            {
                PlugName = item.Name,
                PlugCode = item.ControlID,
                PlugUrl = item.Path,
                ImageClass = item.ImageIndex,
                Children = GetChildrenPlug(srcPlugs, item.ID)
            });

            return items.ToList();
        }

        public static List<Function> GetSubFunctionByUrl(string url)
        {
            //url = url.ToUpper();
            //var functionBLL = new FunctionBLL();
            //var currentFunction = functionBLL.Find<Function>(m => m.Path != null & m.Path.ToUpper().Contains(url));

            ////获取当前用户全部角色的权限
            //var roleInfos = SecurityHelper.SecurityInst.GetRolesByUser(SecurityHelper.CurrentUserName);
            //var funcList = new List<Function>();

            //roleInfos.ToList().ForEach(m =>
            //{
            //    var currentRoleFuncs = functionBLL.GetRoleFunctions(m.ID);
            //    currentRoleFuncs.ToList().ForEach(func =>
            //    {
            //        if (!funcList.Select(model => model.ID).Contains(func.ID))
            //        {
            //            funcList.Add(func);
            //        }
            //    });
            //});

            //var functionList = currentFunction != null
            //    ? funcList.Where(m => m.PID == currentFunction.ID
            //                          && m.FunctionType.HasValue && m.FunctionType.Value == 3).ToList()
            //    : new List<Function>();

            //return functionList;
            return null;
        }

        public static string GetSysName()
        {
            var sysName = "";
            try
            {
                sysName = m_parametebll.GetSysName("系统名称").Value;
            }
            catch
            {
                //此时可能是未注册
            }
            return sysName;
        }
    }
}