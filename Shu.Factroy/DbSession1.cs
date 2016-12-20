 
using Shu.DAL;
using Shu.IDAL;
using Shu.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shu.Factroy
{
	public partial class DBSession : IDBSession
    {
		private ISys_AreaDal _Sys_AreaDal;
        public ISys_AreaDal Sys_AreaDal
        {
            get
            {
                if(_Sys_AreaDal == null)
                {
                    _Sys_AreaDal = AbstractFactory.CreateSys_AreaDal();
                }
                return _Sys_AreaDal;
            }
            set { _Sys_AreaDal = value; }
        }
		private ISys_ConfigDal _Sys_ConfigDal;
        public ISys_ConfigDal Sys_ConfigDal
        {
            get
            {
                if(_Sys_ConfigDal == null)
                {
                    _Sys_ConfigDal = AbstractFactory.CreateSys_ConfigDal();
                }
                return _Sys_ConfigDal;
            }
            set { _Sys_ConfigDal = value; }
        }
		private ISys_DataDictDal _Sys_DataDictDal;
        public ISys_DataDictDal Sys_DataDictDal
        {
            get
            {
                if(_Sys_DataDictDal == null)
                {
                    _Sys_DataDictDal = AbstractFactory.CreateSys_DataDictDal();
                }
                return _Sys_DataDictDal;
            }
            set { _Sys_DataDictDal = value; }
        }
		private ISys_DepartmentDal _Sys_DepartmentDal;
        public ISys_DepartmentDal Sys_DepartmentDal
        {
            get
            {
                if(_Sys_DepartmentDal == null)
                {
                    _Sys_DepartmentDal = AbstractFactory.CreateSys_DepartmentDal();
                }
                return _Sys_DepartmentDal;
            }
            set { _Sys_DepartmentDal = value; }
        }
		private ISys_DesktopDal _Sys_DesktopDal;
        public ISys_DesktopDal Sys_DesktopDal
        {
            get
            {
                if(_Sys_DesktopDal == null)
                {
                    _Sys_DesktopDal = AbstractFactory.CreateSys_DesktopDal();
                }
                return _Sys_DesktopDal;
            }
            set { _Sys_DesktopDal = value; }
        }
		private ISys_HolidayDal _Sys_HolidayDal;
        public ISys_HolidayDal Sys_HolidayDal
        {
            get
            {
                if(_Sys_HolidayDal == null)
                {
                    _Sys_HolidayDal = AbstractFactory.CreateSys_HolidayDal();
                }
                return _Sys_HolidayDal;
            }
            set { _Sys_HolidayDal = value; }
        }
		private ISys_IconsDal _Sys_IconsDal;
        public ISys_IconsDal Sys_IconsDal
        {
            get
            {
                if(_Sys_IconsDal == null)
                {
                    _Sys_IconsDal = AbstractFactory.CreateSys_IconsDal();
                }
                return _Sys_IconsDal;
            }
            set { _Sys_IconsDal = value; }
        }
		private ISys_LogDal _Sys_LogDal;
        public ISys_LogDal Sys_LogDal
        {
            get
            {
                if(_Sys_LogDal == null)
                {
                    _Sys_LogDal = AbstractFactory.CreateSys_LogDal();
                }
                return _Sys_LogDal;
            }
            set { _Sys_LogDal = value; }
        }
		private ISys_MenuDal _Sys_MenuDal;
        public ISys_MenuDal Sys_MenuDal
        {
            get
            {
                if(_Sys_MenuDal == null)
                {
                    _Sys_MenuDal = AbstractFactory.CreateSys_MenuDal();
                }
                return _Sys_MenuDal;
            }
            set { _Sys_MenuDal = value; }
        }
		private ISys_MenuOperatingButtonDal _Sys_MenuOperatingButtonDal;
        public ISys_MenuOperatingButtonDal Sys_MenuOperatingButtonDal
        {
            get
            {
                if(_Sys_MenuOperatingButtonDal == null)
                {
                    _Sys_MenuOperatingButtonDal = AbstractFactory.CreateSys_MenuOperatingButtonDal();
                }
                return _Sys_MenuOperatingButtonDal;
            }
            set { _Sys_MenuOperatingButtonDal = value; }
        }
		private ISys_MessageDal _Sys_MessageDal;
        public ISys_MessageDal Sys_MessageDal
        {
            get
            {
                if(_Sys_MessageDal == null)
                {
                    _Sys_MessageDal = AbstractFactory.CreateSys_MessageDal();
                }
                return _Sys_MessageDal;
            }
            set { _Sys_MessageDal = value; }
        }
		private ISys_ModelFileDal _Sys_ModelFileDal;
        public ISys_ModelFileDal Sys_ModelFileDal
        {
            get
            {
                if(_Sys_ModelFileDal == null)
                {
                    _Sys_ModelFileDal = AbstractFactory.CreateSys_ModelFileDal();
                }
                return _Sys_ModelFileDal;
            }
            set { _Sys_ModelFileDal = value; }
        }
		private ISys_ModuleDal _Sys_ModuleDal;
        public ISys_ModuleDal Sys_ModuleDal
        {
            get
            {
                if(_Sys_ModuleDal == null)
                {
                    _Sys_ModuleDal = AbstractFactory.CreateSys_ModuleDal();
                }
                return _Sys_ModuleDal;
            }
            set { _Sys_ModuleDal = value; }
        }
		private ISys_NoticeDal _Sys_NoticeDal;
        public ISys_NoticeDal Sys_NoticeDal
        {
            get
            {
                if(_Sys_NoticeDal == null)
                {
                    _Sys_NoticeDal = AbstractFactory.CreateSys_NoticeDal();
                }
                return _Sys_NoticeDal;
            }
            set { _Sys_NoticeDal = value; }
        }
		private ISys_OperatingButtonDal _Sys_OperatingButtonDal;
        public ISys_OperatingButtonDal Sys_OperatingButtonDal
        {
            get
            {
                if(_Sys_OperatingButtonDal == null)
                {
                    _Sys_OperatingButtonDal = AbstractFactory.CreateSys_OperatingButtonDal();
                }
                return _Sys_OperatingButtonDal;
            }
            set { _Sys_OperatingButtonDal = value; }
        }
		private ISys_PendingMatterDal _Sys_PendingMatterDal;
        public ISys_PendingMatterDal Sys_PendingMatterDal
        {
            get
            {
                if(_Sys_PendingMatterDal == null)
                {
                    _Sys_PendingMatterDal = AbstractFactory.CreateSys_PendingMatterDal();
                }
                return _Sys_PendingMatterDal;
            }
            set { _Sys_PendingMatterDal = value; }
        }
		private ISys_PostDal _Sys_PostDal;
        public ISys_PostDal Sys_PostDal
        {
            get
            {
                if(_Sys_PostDal == null)
                {
                    _Sys_PostDal = AbstractFactory.CreateSys_PostDal();
                }
                return _Sys_PostDal;
            }
            set { _Sys_PostDal = value; }
        }
		private ISys_PostChangeDal _Sys_PostChangeDal;
        public ISys_PostChangeDal Sys_PostChangeDal
        {
            get
            {
                if(_Sys_PostChangeDal == null)
                {
                    _Sys_PostChangeDal = AbstractFactory.CreateSys_PostChangeDal();
                }
                return _Sys_PostChangeDal;
            }
            set { _Sys_PostChangeDal = value; }
        }
		private ISys_RoleDal _Sys_RoleDal;
        public ISys_RoleDal Sys_RoleDal
        {
            get
            {
                if(_Sys_RoleDal == null)
                {
                    _Sys_RoleDal = AbstractFactory.CreateSys_RoleDal();
                }
                return _Sys_RoleDal;
            }
            set { _Sys_RoleDal = value; }
        }
		private ISys_RolePurviewDal _Sys_RolePurviewDal;
        public ISys_RolePurviewDal Sys_RolePurviewDal
        {
            get
            {
                if(_Sys_RolePurviewDal == null)
                {
                    _Sys_RolePurviewDal = AbstractFactory.CreateSys_RolePurviewDal();
                }
                return _Sys_RolePurviewDal;
            }
            set { _Sys_RolePurviewDal = value; }
        }
		private ISys_SeeChargeDal _Sys_SeeChargeDal;
        public ISys_SeeChargeDal Sys_SeeChargeDal
        {
            get
            {
                if(_Sys_SeeChargeDal == null)
                {
                    _Sys_SeeChargeDal = AbstractFactory.CreateSys_SeeChargeDal();
                }
                return _Sys_SeeChargeDal;
            }
            set { _Sys_SeeChargeDal = value; }
        }
		private ISys_SettingDal _Sys_SettingDal;
        public ISys_SettingDal Sys_SettingDal
        {
            get
            {
                if(_Sys_SettingDal == null)
                {
                    _Sys_SettingDal = AbstractFactory.CreateSys_SettingDal();
                }
                return _Sys_SettingDal;
            }
            set { _Sys_SettingDal = value; }
        }
		private ISys_UploadDataTypeDal _Sys_UploadDataTypeDal;
        public ISys_UploadDataTypeDal Sys_UploadDataTypeDal
        {
            get
            {
                if(_Sys_UploadDataTypeDal == null)
                {
                    _Sys_UploadDataTypeDal = AbstractFactory.CreateSys_UploadDataTypeDal();
                }
                return _Sys_UploadDataTypeDal;
            }
            set { _Sys_UploadDataTypeDal = value; }
        }
		private ISys_UserChargeDepDal _Sys_UserChargeDepDal;
        public ISys_UserChargeDepDal Sys_UserChargeDepDal
        {
            get
            {
                if(_Sys_UserChargeDepDal == null)
                {
                    _Sys_UserChargeDepDal = AbstractFactory.CreateSys_UserChargeDepDal();
                }
                return _Sys_UserChargeDepDal;
            }
            set { _Sys_UserChargeDepDal = value; }
        }
		private ISys_UserInfoDal _Sys_UserInfoDal;
        public ISys_UserInfoDal Sys_UserInfoDal
        {
            get
            {
                if(_Sys_UserInfoDal == null)
                {
                    _Sys_UserInfoDal = AbstractFactory.CreateSys_UserInfoDal();
                }
                return _Sys_UserInfoDal;
            }
            set { _Sys_UserInfoDal = value; }
        }
		private IView_PendingMatterToRolePurviewDal _View_PendingMatterToRolePurviewDal;
        public IView_PendingMatterToRolePurviewDal View_PendingMatterToRolePurviewDal
        {
            get
            {
                if(_View_PendingMatterToRolePurviewDal == null)
                {
                    _View_PendingMatterToRolePurviewDal = AbstractFactory.CreateView_PendingMatterToRolePurviewDal();
                }
                return _View_PendingMatterToRolePurviewDal;
            }
            set { _View_PendingMatterToRolePurviewDal = value; }
        }
		private IView_Sys_DeskTtopDal _View_Sys_DeskTtopDal;
        public IView_Sys_DeskTtopDal View_Sys_DeskTtopDal
        {
            get
            {
                if(_View_Sys_DeskTtopDal == null)
                {
                    _View_Sys_DeskTtopDal = AbstractFactory.CreateView_Sys_DeskTtopDal();
                }
                return _View_Sys_DeskTtopDal;
            }
            set { _View_Sys_DeskTtopDal = value; }
        }
		private IView_Sys_LogDal _View_Sys_LogDal;
        public IView_Sys_LogDal View_Sys_LogDal
        {
            get
            {
                if(_View_Sys_LogDal == null)
                {
                    _View_Sys_LogDal = AbstractFactory.CreateView_Sys_LogDal();
                }
                return _View_Sys_LogDal;
            }
            set { _View_Sys_LogDal = value; }
        }
		private IView_Sys_MenuDal _View_Sys_MenuDal;
        public IView_Sys_MenuDal View_Sys_MenuDal
        {
            get
            {
                if(_View_Sys_MenuDal == null)
                {
                    _View_Sys_MenuDal = AbstractFactory.CreateView_Sys_MenuDal();
                }
                return _View_Sys_MenuDal;
            }
            set { _View_Sys_MenuDal = value; }
        }
		private IView_Sys_MenuRelatedRoleDal _View_Sys_MenuRelatedRoleDal;
        public IView_Sys_MenuRelatedRoleDal View_Sys_MenuRelatedRoleDal
        {
            get
            {
                if(_View_Sys_MenuRelatedRoleDal == null)
                {
                    _View_Sys_MenuRelatedRoleDal = AbstractFactory.CreateView_Sys_MenuRelatedRoleDal();
                }
                return _View_Sys_MenuRelatedRoleDal;
            }
            set { _View_Sys_MenuRelatedRoleDal = value; }
        }
		private IView_Sys_PostDal _View_Sys_PostDal;
        public IView_Sys_PostDal View_Sys_PostDal
        {
            get
            {
                if(_View_Sys_PostDal == null)
                {
                    _View_Sys_PostDal = AbstractFactory.CreateView_Sys_PostDal();
                }
                return _View_Sys_PostDal;
            }
            set { _View_Sys_PostDal = value; }
        }
		private IView_Sys_RolePurviewDal _View_Sys_RolePurviewDal;
        public IView_Sys_RolePurviewDal View_Sys_RolePurviewDal
        {
            get
            {
                if(_View_Sys_RolePurviewDal == null)
                {
                    _View_Sys_RolePurviewDal = AbstractFactory.CreateView_Sys_RolePurviewDal();
                }
                return _View_Sys_RolePurviewDal;
            }
            set { _View_Sys_RolePurviewDal = value; }
        }
		private IView_Sys_RolePurviewAndMenuDal _View_Sys_RolePurviewAndMenuDal;
        public IView_Sys_RolePurviewAndMenuDal View_Sys_RolePurviewAndMenuDal
        {
            get
            {
                if(_View_Sys_RolePurviewAndMenuDal == null)
                {
                    _View_Sys_RolePurviewAndMenuDal = AbstractFactory.CreateView_Sys_RolePurviewAndMenuDal();
                }
                return _View_Sys_RolePurviewAndMenuDal;
            }
            set { _View_Sys_RolePurviewAndMenuDal = value; }
        }
		private IView_Sys_UserInfoDal _View_Sys_UserInfoDal;
        public IView_Sys_UserInfoDal View_Sys_UserInfoDal
        {
            get
            {
                if(_View_Sys_UserInfoDal == null)
                {
                    _View_Sys_UserInfoDal = AbstractFactory.CreateView_Sys_UserInfoDal();
                }
                return _View_Sys_UserInfoDal;
            }
            set { _View_Sys_UserInfoDal = value; }
        }
	}	
}