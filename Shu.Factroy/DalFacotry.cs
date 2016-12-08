 

using Shu.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shu.Factroy
{
    public partial class AbstractFactory
    {
	    public static ISys_AreaDal CreateSys_AreaDal()
        {
		   string fullClassName = NameSpace + ".Sys_AreaDal";
           return CreateInstance(fullClassName) as ISys_AreaDal;
        }
	    public static ISys_ConfigDal CreateSys_ConfigDal()
        {
		   string fullClassName = NameSpace + ".Sys_ConfigDal";
           return CreateInstance(fullClassName) as ISys_ConfigDal;
        }
	    public static ISys_DataDictDal CreateSys_DataDictDal()
        {
		   string fullClassName = NameSpace + ".Sys_DataDictDal";
           return CreateInstance(fullClassName) as ISys_DataDictDal;
        }
	    public static ISys_DepartmentDal CreateSys_DepartmentDal()
        {
		   string fullClassName = NameSpace + ".Sys_DepartmentDal";
           return CreateInstance(fullClassName) as ISys_DepartmentDal;
        }
	    public static ISys_DesktopDal CreateSys_DesktopDal()
        {
		   string fullClassName = NameSpace + ".Sys_DesktopDal";
           return CreateInstance(fullClassName) as ISys_DesktopDal;
        }
	    public static ISys_HolidayDal CreateSys_HolidayDal()
        {
		   string fullClassName = NameSpace + ".Sys_HolidayDal";
           return CreateInstance(fullClassName) as ISys_HolidayDal;
        }
	    public static ISys_LogDal CreateSys_LogDal()
        {
		   string fullClassName = NameSpace + ".Sys_LogDal";
           return CreateInstance(fullClassName) as ISys_LogDal;
        }
	    public static ISys_MenuDal CreateSys_MenuDal()
        {
		   string fullClassName = NameSpace + ".Sys_MenuDal";
           return CreateInstance(fullClassName) as ISys_MenuDal;
        }
	    public static ISys_MessageDal CreateSys_MessageDal()
        {
		   string fullClassName = NameSpace + ".Sys_MessageDal";
           return CreateInstance(fullClassName) as ISys_MessageDal;
        }
	    public static ISys_ModelFileDal CreateSys_ModelFileDal()
        {
		   string fullClassName = NameSpace + ".Sys_ModelFileDal";
           return CreateInstance(fullClassName) as ISys_ModelFileDal;
        }
	    public static ISys_ModuleDal CreateSys_ModuleDal()
        {
		   string fullClassName = NameSpace + ".Sys_ModuleDal";
           return CreateInstance(fullClassName) as ISys_ModuleDal;
        }
	    public static ISys_NoticeDal CreateSys_NoticeDal()
        {
		   string fullClassName = NameSpace + ".Sys_NoticeDal";
           return CreateInstance(fullClassName) as ISys_NoticeDal;
        }
	    public static ISys_PendingMatterDal CreateSys_PendingMatterDal()
        {
		   string fullClassName = NameSpace + ".Sys_PendingMatterDal";
           return CreateInstance(fullClassName) as ISys_PendingMatterDal;
        }
	    public static ISys_PostDal CreateSys_PostDal()
        {
		   string fullClassName = NameSpace + ".Sys_PostDal";
           return CreateInstance(fullClassName) as ISys_PostDal;
        }
	    public static ISys_PostChangeDal CreateSys_PostChangeDal()
        {
		   string fullClassName = NameSpace + ".Sys_PostChangeDal";
           return CreateInstance(fullClassName) as ISys_PostChangeDal;
        }
	    public static ISys_RoleDal CreateSys_RoleDal()
        {
		   string fullClassName = NameSpace + ".Sys_RoleDal";
           return CreateInstance(fullClassName) as ISys_RoleDal;
        }
	    public static ISys_RolePurviewDal CreateSys_RolePurviewDal()
        {
		   string fullClassName = NameSpace + ".Sys_RolePurviewDal";
           return CreateInstance(fullClassName) as ISys_RolePurviewDal;
        }
	    public static ISys_SeeChargeDal CreateSys_SeeChargeDal()
        {
		   string fullClassName = NameSpace + ".Sys_SeeChargeDal";
           return CreateInstance(fullClassName) as ISys_SeeChargeDal;
        }
	    public static ISys_SettingDal CreateSys_SettingDal()
        {
		   string fullClassName = NameSpace + ".Sys_SettingDal";
           return CreateInstance(fullClassName) as ISys_SettingDal;
        }
	    public static ISys_UploadDataTypeDal CreateSys_UploadDataTypeDal()
        {
		   string fullClassName = NameSpace + ".Sys_UploadDataTypeDal";
           return CreateInstance(fullClassName) as ISys_UploadDataTypeDal;
        }
	    public static ISys_UserChargeDepDal CreateSys_UserChargeDepDal()
        {
		   string fullClassName = NameSpace + ".Sys_UserChargeDepDal";
           return CreateInstance(fullClassName) as ISys_UserChargeDepDal;
        }
	    public static ISys_UserInfoDal CreateSys_UserInfoDal()
        {
		   string fullClassName = NameSpace + ".Sys_UserInfoDal";
           return CreateInstance(fullClassName) as ISys_UserInfoDal;
        }
	    public static IView_PendingMatterToRolePurviewDal CreateView_PendingMatterToRolePurviewDal()
        {
		   string fullClassName = NameSpace + ".View_PendingMatterToRolePurviewDal";
           return CreateInstance(fullClassName) as IView_PendingMatterToRolePurviewDal;
        }
	    public static IView_Sys_DeskTtopDal CreateView_Sys_DeskTtopDal()
        {
		   string fullClassName = NameSpace + ".View_Sys_DeskTtopDal";
           return CreateInstance(fullClassName) as IView_Sys_DeskTtopDal;
        }
	    public static IView_Sys_LogDal CreateView_Sys_LogDal()
        {
		   string fullClassName = NameSpace + ".View_Sys_LogDal";
           return CreateInstance(fullClassName) as IView_Sys_LogDal;
        }
	    public static IView_Sys_MenuDal CreateView_Sys_MenuDal()
        {
		   string fullClassName = NameSpace + ".View_Sys_MenuDal";
           return CreateInstance(fullClassName) as IView_Sys_MenuDal;
        }
	    public static IView_Sys_MenuRelatedRoleDal CreateView_Sys_MenuRelatedRoleDal()
        {
		   string fullClassName = NameSpace + ".View_Sys_MenuRelatedRoleDal";
           return CreateInstance(fullClassName) as IView_Sys_MenuRelatedRoleDal;
        }
	    public static IView_Sys_PostDal CreateView_Sys_PostDal()
        {
		   string fullClassName = NameSpace + ".View_Sys_PostDal";
           return CreateInstance(fullClassName) as IView_Sys_PostDal;
        }
	    public static IView_Sys_RolePurviewDal CreateView_Sys_RolePurviewDal()
        {
		   string fullClassName = NameSpace + ".View_Sys_RolePurviewDal";
           return CreateInstance(fullClassName) as IView_Sys_RolePurviewDal;
        }
	    public static IView_Sys_RolePurviewAndMenuDal CreateView_Sys_RolePurviewAndMenuDal()
        {
		   string fullClassName = NameSpace + ".View_Sys_RolePurviewAndMenuDal";
           return CreateInstance(fullClassName) as IView_Sys_RolePurviewAndMenuDal;
        }
	    public static IView_Sys_UserInfoDal CreateView_Sys_UserInfoDal()
        {
		   string fullClassName = NameSpace + ".View_Sys_UserInfoDal";
           return CreateInstance(fullClassName) as IView_Sys_UserInfoDal;
        }
	}
	
}