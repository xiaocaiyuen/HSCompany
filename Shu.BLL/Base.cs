﻿ 
using Shu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shu.BLL
{
	public partial class D_BuildBLL :BaseBLL<D_Build>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.D_BuildDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.D_BuildDal;
        }
    }   
	public partial class SSO_UserAuthOperatesBLL :BaseBLL<SSO_UserAuthOperates>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.SSO_UserAuthOperatesDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.SSO_UserAuthOperatesDal;
        }
    }   
	public partial class SSO_UserAuthSessionsBLL :BaseBLL<SSO_UserAuthSessions>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.SSO_UserAuthSessionsDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.SSO_UserAuthSessionsDal;
        }
    }   
	public partial class Sys_AreaBLL :BaseBLL<Sys_Area>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_AreaDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_AreaDal;
        }
    }   
	public partial class Sys_ConfigBLL :BaseBLL<Sys_Config>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_ConfigDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_ConfigDal;
        }
    }   
	public partial class Sys_DataDictBLL :BaseBLL<Sys_DataDict>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_DataDictDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_DataDictDal;
        }
    }   
	public partial class Sys_DepartmentBLL :BaseBLL<Sys_Department>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_DepartmentDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_DepartmentDal;
        }
    }   
	public partial class Sys_DesktopBLL :BaseBLL<Sys_Desktop>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_DesktopDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_DesktopDal;
        }
    }   
	public partial class Sys_HolidayBLL :BaseBLL<Sys_Holiday>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_HolidayDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_HolidayDal;
        }
    }   
	public partial class Sys_IconsBLL :BaseBLL<Sys_Icons>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_IconsDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_IconsDal;
        }
    }   
	public partial class Sys_JalendarBLL :BaseBLL<Sys_Jalendar>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_JalendarDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_JalendarDal;
        }
    }   
	public partial class Sys_LogBLL :BaseBLL<Sys_Log>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_LogDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_LogDal;
        }
    }   
	public partial class Sys_MenuBLL :BaseBLL<Sys_Menu>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_MenuDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_MenuDal;
        }
    }   
	public partial class Sys_MenuOperatingButtonBLL :BaseBLL<Sys_MenuOperatingButton>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_MenuOperatingButtonDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_MenuOperatingButtonDal;
        }
    }   
	public partial class Sys_MessageBLL :BaseBLL<Sys_Message>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_MessageDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_MessageDal;
        }
    }   
	public partial class Sys_ModelFileBLL :BaseBLL<Sys_ModelFile>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_ModelFileDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_ModelFileDal;
        }
    }   
	public partial class Sys_ModuleBLL :BaseBLL<Sys_Module>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_ModuleDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_ModuleDal;
        }
    }   
	public partial class Sys_NoticeBLL :BaseBLL<Sys_Notice>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_NoticeDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_NoticeDal;
        }
    }   
	public partial class Sys_OperatingButtonBLL :BaseBLL<Sys_OperatingButton>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_OperatingButtonDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_OperatingButtonDal;
        }
    }   
	public partial class Sys_PendingMatterBLL :BaseBLL<Sys_PendingMatter>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_PendingMatterDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_PendingMatterDal;
        }
    }   
	public partial class Sys_PostBLL :BaseBLL<Sys_Post>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_PostDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_PostDal;
        }
    }   
	public partial class Sys_PostChangeBLL :BaseBLL<Sys_PostChange>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_PostChangeDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_PostChangeDal;
        }
    }   
	public partial class Sys_RoleBLL :BaseBLL<Sys_Role>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_RoleDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_RoleDal;
        }
    }   
	public partial class Sys_RolePurviewBLL :BaseBLL<Sys_RolePurview>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_RolePurviewDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_RolePurviewDal;
        }
    }   
	public partial class Sys_SeeChargeBLL :BaseBLL<Sys_SeeCharge>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_SeeChargeDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_SeeChargeDal;
        }
    }   
	public partial class Sys_SettingBLL :BaseBLL<Sys_Setting>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_SettingDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_SettingDal;
        }
    }   
	public partial class Sys_UploadDataTypeBLL :BaseBLL<Sys_UploadDataType>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_UploadDataTypeDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_UploadDataTypeDal;
        }
    }   
	public partial class Sys_UserChargeDepBLL :BaseBLL<Sys_UserChargeDep>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_UserChargeDepDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_UserChargeDepDal;
        }
    }   
	public partial class Sys_UserInfoBLL :BaseBLL<Sys_UserInfo>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Sys_UserInfoDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Sys_UserInfoDal;
        }
    }   
	public partial class View_PendingMatterToRolePurviewBLL :BaseExtendsBLL<View_PendingMatterToRolePurview>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_PendingMatterToRolePurviewDal;
        }
    }   
	public partial class View_Sys_DeskTtopBLL :BaseExtendsBLL<View_Sys_DeskTtop>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Sys_DeskTtopDal;
        }
    }   
	public partial class View_Sys_LogBLL :BaseExtendsBLL<View_Sys_Log>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Sys_LogDal;
        }
    }   
	public partial class View_Sys_MenuBLL :BaseExtendsBLL<View_Sys_Menu>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Sys_MenuDal;
        }
    }   
	public partial class View_Sys_MenuRelatedRoleBLL :BaseExtendsBLL<View_Sys_MenuRelatedRole>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Sys_MenuRelatedRoleDal;
        }
    }   
	public partial class View_Sys_PostBLL :BaseExtendsBLL<View_Sys_Post>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Sys_PostDal;
        }
    }   
	public partial class View_Sys_RolePurviewBLL :BaseExtendsBLL<View_Sys_RolePurview>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Sys_RolePurviewDal;
        }
    }   
	public partial class View_Sys_RolePurviewAndMenuBLL :BaseExtendsBLL<View_Sys_RolePurviewAndMenu>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Sys_RolePurviewAndMenuDal;
        }
    }   
	public partial class View_Sys_UserInfoBLL :BaseExtendsBLL<View_Sys_UserInfo>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Sys_UserInfoDal;
        }
    }   
	public partial class View_Workflow_NodeBusiExBLL :BaseExtendsBLL<View_Workflow_NodeBusiEx>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Workflow_NodeBusiExDal;
        }
    }   
	public partial class View_Workflow_NodeConditionBLL :BaseExtendsBLL<View_Workflow_NodeCondition>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Workflow_NodeConditionDal;
        }
    }   
	public partial class View_Workflow_NodeConfigExBLL :BaseExtendsBLL<View_Workflow_NodeConfigEx>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Workflow_NodeConfigExDal;
        }
    }   
	public partial class View_Workflow_NodeRouteBLL :BaseExtendsBLL<View_Workflow_NodeRoute>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Workflow_NodeRouteDal;
        }
    }   
	public partial class View_Workflow_RoleAACTDefinitionBLL :BaseExtendsBLL<View_Workflow_RoleAACTDefinition>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Workflow_RoleAACTDefinitionDal;
        }
    }   
	public partial class View_Workflow_TasksExBLL :BaseExtendsBLL<View_Workflow_TasksEx>
    {
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.View_Workflow_TasksExDal;
        }
    }   
	public partial class Workflow_AuditActionDefinitionBLL :BaseBLL<Workflow_AuditActionDefinition>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Workflow_AuditActionDefinitionDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Workflow_AuditActionDefinitionDal;
        }
    }   
	public partial class Workflow_NodeBusiExBLL :BaseBLL<Workflow_NodeBusiEx>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Workflow_NodeBusiExDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Workflow_NodeBusiExDal;
        }
    }   
	public partial class Workflow_NodeConditionBLL :BaseBLL<Workflow_NodeCondition>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Workflow_NodeConditionDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Workflow_NodeConditionDal;
        }
    }   
	public partial class Workflow_NodeConfigExBLL :BaseBLL<Workflow_NodeConfigEx>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Workflow_NodeConfigExDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Workflow_NodeConfigExDal;
        }
    }   
	public partial class Workflow_NodeRouteBLL :BaseBLL<Workflow_NodeRoute>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Workflow_NodeRouteDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Workflow_NodeRouteDal;
        }
    }   
	public partial class Workflow_RoleAACTDefinitionBLL :BaseBLL<Workflow_RoleAACTDefinition>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Workflow_RoleAACTDefinitionDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Workflow_RoleAACTDefinitionDal;
        }
    }   
	public partial class Workflow_TasksExBLL :BaseBLL<Workflow_TasksEx>
    {
		public override void SetCurrentDal()
        {
            Dal = this.DBSession.Workflow_TasksExDal;
        }
	    public override void SetExtendsCurrentDal()
        {
            DalExtends = this.DBSession.Workflow_TasksExDal;
        }
    }   
	
}