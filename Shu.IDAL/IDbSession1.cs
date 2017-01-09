 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shu.IDAL
{
	public partial interface IDBSession
    {
		ID_BuildDal D_BuildDal{get;set;}
		ISSO_UserAuthOperatesDal SSO_UserAuthOperatesDal{get;set;}
		ISSO_UserAuthSessionsDal SSO_UserAuthSessionsDal{get;set;}
		ISys_AreaDal Sys_AreaDal{get;set;}
		ISys_ConfigDal Sys_ConfigDal{get;set;}
		ISys_DataDictDal Sys_DataDictDal{get;set;}
		ISys_DepartmentDal Sys_DepartmentDal{get;set;}
		ISys_DesktopDal Sys_DesktopDal{get;set;}
		ISys_HolidayDal Sys_HolidayDal{get;set;}
		ISys_IconsDal Sys_IconsDal{get;set;}
		ISys_JalendarDal Sys_JalendarDal{get;set;}
		ISys_LogDal Sys_LogDal{get;set;}
		ISys_MenuDal Sys_MenuDal{get;set;}
		ISys_MenuOperatingButtonDal Sys_MenuOperatingButtonDal{get;set;}
		ISys_MessageDal Sys_MessageDal{get;set;}
		ISys_ModelFileDal Sys_ModelFileDal{get;set;}
		ISys_ModuleDal Sys_ModuleDal{get;set;}
		ISys_NoticeDal Sys_NoticeDal{get;set;}
		ISys_OperatingButtonDal Sys_OperatingButtonDal{get;set;}
		ISys_PendingMatterDal Sys_PendingMatterDal{get;set;}
		ISys_PostDal Sys_PostDal{get;set;}
		ISys_PostChangeDal Sys_PostChangeDal{get;set;}
		ISys_RoleDal Sys_RoleDal{get;set;}
		ISys_RolePurviewDal Sys_RolePurviewDal{get;set;}
		ISys_SeeChargeDal Sys_SeeChargeDal{get;set;}
		ISys_SettingDal Sys_SettingDal{get;set;}
		ISys_UploadDataTypeDal Sys_UploadDataTypeDal{get;set;}
		ISys_UserChargeDepDal Sys_UserChargeDepDal{get;set;}
		ISys_UserInfoDal Sys_UserInfoDal{get;set;}
		IView_PendingMatterToRolePurviewDal View_PendingMatterToRolePurviewDal{get;set;}
		IView_Sys_DeskTtopDal View_Sys_DeskTtopDal{get;set;}
		IView_Sys_LogDal View_Sys_LogDal{get;set;}
		IView_Sys_MenuDal View_Sys_MenuDal{get;set;}
		IView_Sys_MenuRelatedRoleDal View_Sys_MenuRelatedRoleDal{get;set;}
		IView_Sys_PostDal View_Sys_PostDal{get;set;}
		IView_Sys_RolePurviewDal View_Sys_RolePurviewDal{get;set;}
		IView_Sys_RolePurviewAndMenuDal View_Sys_RolePurviewAndMenuDal{get;set;}
		IView_Sys_UserInfoDal View_Sys_UserInfoDal{get;set;}
		IView_Workflow_NodeBusiExDal View_Workflow_NodeBusiExDal{get;set;}
		IView_Workflow_NodeConditionDal View_Workflow_NodeConditionDal{get;set;}
		IView_Workflow_NodeConfigExDal View_Workflow_NodeConfigExDal{get;set;}
		IView_Workflow_NodeRouteDal View_Workflow_NodeRouteDal{get;set;}
		IView_Workflow_RoleAACTDefinitionDal View_Workflow_RoleAACTDefinitionDal{get;set;}
		IView_Workflow_TasksExDal View_Workflow_TasksExDal{get;set;}
		IWorkflow_AuditActionDefinitionDal Workflow_AuditActionDefinitionDal{get;set;}
		IWorkflow_NodeBusiExDal Workflow_NodeBusiExDal{get;set;}
		IWorkflow_NodeConditionDal Workflow_NodeConditionDal{get;set;}
		IWorkflow_NodeConfigExDal Workflow_NodeConfigExDal{get;set;}
		IWorkflow_NodeRouteDal Workflow_NodeRouteDal{get;set;}
		IWorkflow_RoleAACTDefinitionDal Workflow_RoleAACTDefinitionDal{get;set;}
		IWorkflow_TasksExDal Workflow_TasksExDal{get;set;}
	}	
}