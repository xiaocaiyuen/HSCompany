using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using YDT.BLL;
using YDT.Model;
using System.Data;
namespace YDT.Web.Manage.Sys
{
    public partial class UploadDataType : BasePage
    {
        Sys_UploadDataTypeBLL updateTypeBll = new Sys_UploadDataTypeBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Show();
            }
        }

        protected void Show()
        {
            List<Sys_UploadDataType> typeList = updateTypeBll.FindWhere(" UploadDataType_IsDelete='0'").OrderBy(p => p.UploadDataType_SortNo).ToList();

            //使用存储过程查看
            Common_BLL ComBLL = new Common_BLL();
            List<ParameterModel> ParameterModel = null;
            List<ParameterModel> ParameterList = new List<ParameterModel>();
            ParameterList.Add(new ParameterModel { ParamName = "ProductType", ParamValue = "2801", ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
            DataSet ds = ComBLL.ExecStoredProcedures(new StoredProcModel { ProcName = "P_GetUploadDataType", ParameterList = ParameterList }, out ParameterModel);
            List<ParameterModel> ParameterModel2 = null;
            List<ParameterModel> ParameterList2 = new List<ParameterModel>();
            ParameterList2.Add(new ParameterModel { ParamName = "ProductType", ParamValue = "2802", ParamMode = ParamEnumMode.InMode, ParamType = DbType.String });
            DataSet ds2 = ComBLL.ExecStoredProcedures(new StoredProcModel { ProcName = "P_GetUploadDataType", ParameterList = ParameterList2 }, out ParameterModel2);


            string str1 = "";
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                List<Sys_UploadDataType> typeList1 = typeList.FindAll(p => p.UploadDataType_ProductType == "2801" && p.UploadDataType_ProcessStage == ds.Tables[0].Rows[j][0].ToString());
                for (int i = 0; i < typeList1.Count; i++)
                {
                    str1 += "<tr>";
                    str1 += "<td style=\"text-align:center;\">" + typeList1[i].UploadDataType_ProcessStage + "</td>";
                    str1 += "<td><input type=\"text\" id=\"txtTypeName" + i + "\" maxlength=\"50\"  class=\"required validate[required]\"  style=\"text-align: center; width:100%;\" value=\"" + typeList1[i].UploadDataType_Name + "\" /></td>";
                    str1 += "<td><input type=\"text\" id=\"txtCode" + i + "\" maxlength=\"20\"  class=\"required validate[required]\"  value=\"" + typeList1[i].UploadDataType_TypeCode + "\" /></td>";
                    str1 += "<td><input type=\"text\" id=\"txtCode" + i + "\" maxlength=\"120\"    style=\"width:100%;\" value=\"" + typeList1[i].UploadDataType_DataType + "\" /></td>";
                    str1 += "<td style=\"display:none;\"><input type=\"text\" id=\"txtID" + i + "\"  value=\"" + typeList1[i].UploadDataType_ID + "\" /></td>";
                    str1 += "<td style=\"text-align:center;\"><a onclick=\"deleteRow(this)\" title=\"删除\"><img src=\"../../Images/buttons/cancel.png\" /></a></td>";
                    str1 += "</tr>";
                }
            }


            this.LitContent1.Text = str1;




            string str2 = "";
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                List<Sys_UploadDataType> typeList2 = typeList.FindAll(p => p.UploadDataType_ProductType == "2802" && p.UploadDataType_ProcessStage == ds.Tables[0].Rows[j][0].ToString());
                for (int i = 0; i < typeList2.Count; i++)
                {
                    str2 += "<tr>";
                    str2 += "<td style=\"text-align:center;\">" + typeList2[i].UploadDataType_ProcessStage + "</td>";
                    str2 += "<td><input type=\"text\" id=\"txtTypeName" + i + "\" maxlength=\"50\"  class=\"required validate[required]\" style=\"text-align: center; width:100%;\" value=\"" + typeList2[i].UploadDataType_Name + "\" /></td>";
                    str2 += "<td><input type=\"text\" id=\"txtCode" + i + "\" maxlength=\"20\"  class=\"required validate[required]\"  value=\"" + typeList2[i].UploadDataType_TypeCode + "\" /></td>";
                    str2 += "<td><input type=\"text\" id=\"txtDataType" + i + "\" maxlength=\"120\"   style=\"width:100%;\" value=\"" + typeList2[i].UploadDataType_DataType + "\" /></td>";
                    str2 += "<td style=\"display:none;\"><input type=\"text\" id=\"txtID" + i + "\"  value=\"" + typeList2[i].UploadDataType_ID + "\" /></td>";
                    str2 += "<td style=\"text-align:center;\"><a onclick=\"deleteRow(this)\" title=\"删除\"><img src=\"../../Images/buttons/cancel.png\" /></a></td>";
                    str2 += "</tr>";
                }
            }


            this.LitContent2.Text = str2;

        }

        protected void btn_Save_Click(object sender, ImageClickEventArgs e)
        {
            List<Sys_UploadDataType> typeList = updateTypeBll.FindWhere(" UploadDataType_IsDelete='0'").OrderBy(p => p.UploadDataType_SortNo).ToList();

            List<Sys_UploadDataType> updatetypeList = new List<Sys_UploadDataType>();

            List<Sys_UploadDataType> addtypeList = new List<Sys_UploadDataType>();


            string data1 = this.hid_Data1.Value;
            string[] da1 = data1.Split('|');
            string ids = "";
            for (int i = 0; i < da1.Count(); i++)
            {

                var da = da1[i].Split(',');
                ids += da[4] + ",";
                Sys_UploadDataType types = typeList.Find(p => p.UploadDataType_ID == da[4]);
                if (types != null)
                {
                    types.UploadDataType_Name = da[1];
                    types.UploadDataType_TypeCode = da[2];
                    types.UploadDataType_DataType = da[3];
                    updatetypeList.Add(types);
                }
                else
                {
                    types = new Sys_UploadDataType();
                    types.UploadDataType_ID = Guid.NewGuid().ToString();
                    types.UploadDataType_ProductType = "2801";
                    types.UploadDataType_ProcessStage = da[0];
                    types.UploadDataType_Name = da[1];
                    types.UploadDataType_TypeCode = da[2];
                    types.UploadDataType_DataType = da[3];
                    types.UploadDataType_IsDelete = false;

                    List<Sys_UploadDataType> countList = typeList.FindAll(p => p.UploadDataType_ProcessStage == da[0] && p.UploadDataType_ProductType == "2801").OrderByDescending(p => p.UploadDataType_SortNo).ToList();
                    int sort = 1;
                    if (countList.Count > 0)
                    {
                        sort = Convert.ToInt32(countList[0].UploadDataType_SortNo) + 1;
                    }
                    types.UploadDataType_SortNo = sort;

                    addtypeList.Add(types);
                }
            }

            string data2 = this.hid_Data2.Value;
            string[] da2 = data2.Split('|');
            for (int i = 0; i < da2.Count(); i++)
            {
                var da = da2[i].Split(',');
                ids += da[4] + ",";
                Sys_UploadDataType types = typeList.Find(p => p.UploadDataType_ID == da[4]);
                if (types != null)
                {
                    types.UploadDataType_Name = da[1];
                    types.UploadDataType_TypeCode = da[2];
                    types.UploadDataType_DataType = da[3];
                    updatetypeList.Add(types);
                }
                else
                {
                    types = new Sys_UploadDataType();
                    types.UploadDataType_ID = Guid.NewGuid().ToString();
                    types.UploadDataType_ProductType = "2802";
                    types.UploadDataType_ProcessStage = da[0];
                    types.UploadDataType_Name = da[1];
                    types.UploadDataType_TypeCode = da[2];
                    types.UploadDataType_DataType = da[3];
                    types.UploadDataType_IsDelete = false;

                    List<Sys_UploadDataType> countList = typeList.FindAll(p => p.UploadDataType_ProcessStage == da[0] && p.UploadDataType_ProductType == "2802").OrderByDescending(p => p.UploadDataType_SortNo).ToList();
                    int sort = 1;
                    if (countList.Count > 0)
                    {
                        sort = Convert.ToInt32(countList[0].UploadDataType_SortNo) + 1;
                    }
                    types.UploadDataType_SortNo = sort;

                    addtypeList.Add(types);
                }

            }


            for (int i = 0; i < typeList.Count; i++)
            {
                Sys_UploadDataType types = typeList[i];
                if (!ids.Contains(types.UploadDataType_ID))
                {
                    types.UploadDataType_IsDelete = true;
                    updatetypeList.Add(types);
                }
            }
            int result = 0;
            string msg = "";
            bool bol1 = updateTypeBll.AddList(addtypeList, out msg);
            if (!bol1)
            {
                result += 1;
            }
            bool bol2 = updateTypeBll.UpdateList(updatetypeList, out msg);
            if (!bol2)
            {
                result += 1;
            }
            if (result == 0)
            {
                new Common_BLL().AddLog("系统管理>>资料类别维护", "", "修改", "修改成功！", base.CurrUserInfo().UserID, base.CurrUserInfo().DepartmentCode);
                MessageBox.ShowAndRedirect(this, "保存成功！", "UploadDataType.aspx");
            }
            else
            {
                new Common_BLL().AddLog("系统管理>>资料类别维护", "", "修改", "修改失败！", base.CurrUserInfo().UserID, base.CurrUserInfo().DepartmentCode);
                MessageBox.Show(this, "保存失败！");
            }
        }

    }
}