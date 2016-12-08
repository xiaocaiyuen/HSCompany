using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.Comm;
using System.Text;
using YDT.BLL;
using YDT.Model;
using YDT.DAL;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

using YDT.Utility.Extensions;

namespace YDT.Web.Manage.Sys
{
    public partial class DealersDealersInfoEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定下拉框
                BindDDL();
                //加载页面信息
                BindShow();
            }
        }

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        private void BindDDL()
        {
            Sys_DataDictBLL bllDataDict = new Sys_DataDictBLL();
            Common_BLL bllComm = new Common_BLL();
            //类型
            List<Sys_DataDict> listDealersInfo_Type = bllDataDict.FindWhere(p => p.DataDict_Code == "4504" || p.DataDict_Code == "4505").OrderBy(p => p.DataDict_Sequence).ToList();
            listDealersInfo_Type.Insert(0, new Sys_DataDict { DataDict_Name = "==请选择==", DataDict_Code = "" });
            this.ddlDealersInfo_Type.DataSource = listDealersInfo_Type;
            this.ddlDealersInfo_Type.DataTextField = "DataDict_Name";
            this.ddlDealersInfo_Type.DataValueField = "DataDict_Code";
            this.ddlDealersInfo_Type.DataBind();
            //合作类型
            List<Sys_DataDict> listDealersInfo_CooperativeType = bllDataDict.FindWhere(p => p.DataDict_ParentCode == "58").OrderBy(p => p.DataDict_Sequence).ToList();
            listDealersInfo_CooperativeType.Insert(0, new Sys_DataDict { DataDict_Name = "==请选择==", DataDict_Code = "" });
            this.ddlDealersInfo_CooperativeType.DataSource = listDealersInfo_CooperativeType;
            this.ddlDealersInfo_CooperativeType.DataTextField = "DataDict_Name";
            this.ddlDealersInfo_CooperativeType.DataValueField = "DataDict_Code";
            this.ddlDealersInfo_CooperativeType.DataBind();
            //所属区域1
            DataTable listDealersInfo_Area1 = bllComm.GetDataBySQL("SELECT AreaDistribution_ID,AreaDistribution_Name FROM dbo.Business_AreaDistribution GROUP BY AreaDistribution_ID,AreaDistribution_Name");
            DataRow newRow;
            newRow = listDealersInfo_Area1.NewRow();
            newRow["AreaDistribution_ID"] = "";
            newRow["AreaDistribution_Name"] = Constant.DrpChoiceName;
            listDealersInfo_Area1.Rows.InsertAt(newRow, 0);
            this.ddlDealersInfo_Area1.DataSource = listDealersInfo_Area1;
            this.ddlDealersInfo_Area1.DataTextField = "AreaDistribution_Name";
            this.ddlDealersInfo_Area1.DataValueField = "AreaDistribution_ID";
            this.ddlDealersInfo_Area1.DataBind();

            List<Sys_Hierarchy> HierarchyList = new List<Sys_Hierarchy>();
            HierarchyList = new Sys_HierarchyBLL().FindWhere(p => p.Hierarchy_ParentCode == "01" && p.Hierarchy_IsDel == false).OrderBy(p => p.Hierarchy_Sequence).ToList();
            HierarchyList.Insert(0, new Sys_Hierarchy { Hierarchy_Name = Constant.DrpChoiceName, Hierarchy_Code = "" });
            ddlDealersInfo_HierarchyOne.DataSource = HierarchyList;
            ddlDealersInfo_HierarchyOne.DataTextField = "Hierarchy_Name";
            ddlDealersInfo_HierarchyOne.DataValueField = "Hierarchy_Code";
            ddlDealersInfo_HierarchyOne.DataBind();

            HierarchyList = new Sys_HierarchyBLL().FindWhere(p => p.Hierarchy_ParentCode == "02" && p.Hierarchy_IsDel == false).OrderBy(p => p.Hierarchy_Sequence).ToList();
            HierarchyList.Insert(0, new Sys_Hierarchy { Hierarchy_Name = Constant.DrpChoiceName, Hierarchy_Code = "" });
            ddlDealersInfo_HierarchyTwo.DataSource = HierarchyList;
            ddlDealersInfo_HierarchyTwo.DataTextField = "Hierarchy_Name";
            ddlDealersInfo_HierarchyTwo.DataValueField = "Hierarchy_Code";
            ddlDealersInfo_HierarchyTwo.DataBind();
        }

        /// <summary>
        /// 加载页面信息
        /// </summary>
        private void BindShow()
        {
            // 附件信息
            Session.Remove("ww");
            this.File.FileSizeLimit = "3000";
            this.File.FilesNname = "File";
            this.File.FileSessionID = "ww";
            this.File.FileType = "经销商资料";

            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                string products = string.Empty;
                Common_BLL bllComm = new Common_BLL();
                Dealers_DealersInfoBLL bllDealers_DealersInfo = new Dealers_DealersInfoBLL();
                Business_DealersProductBLL bllBusiness_DealersProduct = new Business_DealersProductBLL();
                Business_DealersBrandBLL bllDealersBrand = new Business_DealersBrandBLL();
                Product_ProductDefinitionBLL bllProduct = new Product_ProductDefinitionBLL();
                List< Product_ProductDefinition> ProductDefinition = bllProduct.FindWhere(p => p.ProductDefinition_Deleted == false&&p.ProductDefinition_ExpirationDate > DateTime.Now);
                //根据经销商编号获取经销商信息
                Dealers_DealersInfo model = bllDealers_DealersInfo.Find(p => p.DealersInfoID == id);

                //获取经销商关联的产品
                List<Business_DealersProduct> DealersProduct = bllBusiness_DealersProduct.FindWhere(p => p.DealersProduct_DealersID == id);
                if (DealersProduct.Count != 0)
                {
                    foreach (Business_DealersProduct Product in DealersProduct)
                    {
                        for (int i = 0; i < ProductDefinition.Count;i++ )
                        {
                            if (ProductDefinition[i].ProductDefinition_ID == Product.DealersProduct_ProductID)
                            {
                                products += Product.DealersProduct_ProductID + ",";
                            }
                        }
                        
                     
                    }
                    if (!products.IsNullOrEmpty())
                    {
                        products = products.Substring(0, products.Length - 1);
                    }
                }
                if (model == null)
                {
                    MessageBox.ShowAndRedirect(this, "该数据不存在！", "DealersDealersInfoList.aspx");
                }
                else
                {
                    FormModel.SetForm<Dealers_DealersInfo>(this, model, "txt");
                    FormModel.SetForm<Dealers_DealersInfo>(this, model, "ddl");
                    txtDealersInfo_CLDate.Text = model.DealersInfo_CLDate == null ? "" : Convert.ToDateTime(model.DealersInfo_CLDate).ToString("yyyy年MM月dd日");
                    txtDealersInfo_ValidityPeriod.Text = model.DealersInfo_ValidityPeriod == null ? "" : Convert.ToDateTime(model.DealersInfo_ValidityPeriod).ToString("yyyy年MM月dd日");
                    txtDealersInfo_CooperativeValidity.Text = model.DealersInfo_CooperativeValidity == null ? "" : Convert.ToDateTime(model.DealersInfo_CooperativeValidity).ToString("yyyy年MM月dd日");
                    hidProductID_New.Value = products;
                    hidProductID_Old.Value = products;

                    string[] area = null;
                    if (!string.IsNullOrEmpty(model.DealersInfo_Area))
                    {
                        area = model.DealersInfo_Area.Split(',');

                        //所属区域1
                        ddlDealersInfo_Area1.SelectedValue = area[0];
                        //所属区域2
                        DataTable listDealersInfo_Area2 = bllComm.GetDataBySQL("SELECT B.AreaId,B.Area_Name FROM dbo.Business_AreaDistribution A LEFT JOIN dbo.Business_Area B ON A.AreaDistribution_AreaID=B.AreaId WHERE A.AreaDistribution_ID='" + area[0] + "' AND B.Area_Depth=1");
                        this.ddlDealersInfo_Area2.DataSource = listDealersInfo_Area2;
                        this.ddlDealersInfo_Area2.DataTextField = "Area_Name";
                        this.ddlDealersInfo_Area2.DataValueField = "AreaId";
                        this.ddlDealersInfo_Area2.DataBind();
                        ddlDealersInfo_Area2.SelectedValue = area[1];
                        //所属区域3
                        DataTable listDealersInfo_Area3 = bllComm.GetDataBySQL("SELECT CONVERT(varchar(50),AreaId) as  AreaIds,Area_Name FROM dbo.Business_Area WHERE Area_ParentId=(SELECT Area_ID FROM dbo.Business_Area WHERE  AreaId='" + area[1] + "')");
                        this.ddlDealersInfo_Area3.DataSource = listDealersInfo_Area3;
                        this.ddlDealersInfo_Area3.DataTextField = "Area_Name";
                        this.ddlDealersInfo_Area3.DataValueField = "AreaIds";
                        this.ddlDealersInfo_Area3.DataBind();
                        ddlDealersInfo_Area3.SelectedValue = area[2];
                    }

                    //获取经销商管理的车辆品牌
                    List<Business_DealersBrand> dtBrand = bllDealersBrand.FindWhere(p => p.DealersBrand_DealersID == id).OrderBy(p => p.DealersBrand_BrandCNName).ToList();
                    if (dtBrand.Count > 0)
                    {
                        string str = string.Empty;
                        string brandID = string.Empty;
                        string brandName = string.Empty;
                        string brandNameLength = "0";
                        for (int i = 0; i < dtBrand.Count; i++)
                        {
                            brandID = dtBrand[i].DealersBrand_BrandID;
                            brandName = dtBrand[i].DealersBrand_BrandCNName;
                            brandNameLength = (brandName.Length * 12 + 10).ToString();

                            str = "<div id='" + brandID + "' class='div_Brand' style='width:" + brandNameLength + "px;'><span>" + brandName + "</span><img id='img_" + brandID + "' title='删除' onclick='RemoveBrand(\"" + brandID + "\",\"" + brandName + "\")' src='../../Images/buttons/btn_delete.gif'></img></div>";

                            hidBrandID.Value += brandID + ",";
                            hidBrandName.Value += brandName + ",";
                            hidBrandInfo.Value += str;
                        }
                    }
                }
                this.File.FileOperationID = id;

            }
            else { this.File.FileOperationID = Guid.NewGuid().ToString(); }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            string id = Request.QueryString["id"];
            string msg = string.Empty;
            string products = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                if (string.IsNullOrEmpty(hidProductID_Old.Value))
                {
                    products = hidProductID_New.Value;
                }
                else
                {
                    // 获取更新后产品编号
                    products = GetProductID(id);
                    if (products == "0")
                    {
                        return;
                    }
                }

                Dealers_DealersInfoBLL bllDealers_DealersInfo = new Dealers_DealersInfoBLL();
                //获取经销商详细信息
                Dealers_DealersInfo Dealers_DealersInfo = bllDealers_DealersInfo.Find(p => p.DealersInfoID == id);
                FormModel.GetForm<Dealers_DealersInfo>(this, Dealers_DealersInfo, "txt");
                FormModel.GetForm<Dealers_DealersInfo>(this, Dealers_DealersInfo, "ddl");
                Dealers_DealersInfo.DealersInfo_Area = ddlDealersInfo_Area1.SelectedValue + "," + ddlDealersInfo_Area2.SelectedValue + "," + ddlDealersInfo_Area3.SelectedValue;
                Dealers_DealersInfo.DealersInfo_UpdateTime = DateTime.Now;
                Dealers_DealersInfo.DealersInfo_UpdateUserID = base.CurrUserInfo().UserID;

                List<Business_DealersProduct> listDealersProduct = new List<Business_DealersProduct>();
                string[] product_ID = products.Split(',');
                for (int i = 0; i < product_ID.Length; i++)
                {
                    //获取经销商所管理的产品
                    Business_DealersProduct Business_DealersProduct = new Business_DealersProduct();
                    Business_DealersProduct.DealersProduct_ID = Guid.NewGuid().ToString();
                    Business_DealersProduct.DealersProduct_DealersID = id;
                    Business_DealersProduct.DealersProduct_ProductID = product_ID[i];
                    Business_DealersProduct.DealersProduct_UpdateTime = DateTime.Now;
                    Business_DealersProduct.DealersProduct_UpdateUserID = base.CurrUserInfo().UserID;
                    listDealersProduct.Add(Business_DealersProduct);
                }

                List<Business_DealersBrand> listDealersBrand = new List<Business_DealersBrand>();
                if (!string.IsNullOrEmpty(hidBrandID.Value))
                {
                    string[] brand_ID = hidBrandID.Value.Substring(0, hidBrandID.Value.Length - 1).Split(',');
                    string[] brand_Name = hidBrandName.Value.Substring(0, hidBrandName.Value.Length - 1).Split(',');
                    for (int i = 0; i < brand_ID.Length; i++)
                    {
                        //获取经销商所管理的品牌
                        Business_DealersBrand Business_DealersBrand = new Business_DealersBrand();
                        Business_DealersBrand.DealersBrand_ID = Guid.NewGuid().ToString();
                        Business_DealersBrand.DealersBrand_DealersID = id;
                        Business_DealersBrand.DealersBrand_BrandID = brand_ID[i];
                        Business_DealersBrand.DealersBrand_BrandCNName = brand_Name[i];
                        Business_DealersBrand.DealersBrand_UpdateTime = DateTime.Now;
                        Business_DealersBrand.DealersBrand_UpdateUserID = base.CurrUserInfo().UserID;
                        listDealersBrand.Add(Business_DealersBrand);
                    }
                }

                //更新经销商
                bool bol = bllDealers_DealersInfo.Update(Dealers_DealersInfo, listDealersProduct, listDealersBrand);
                if (bol)
                {
                    new Sys_ModelFileBLL().AddList(Session["ww"] as List<Sys_ModelFile>, out msg);
                    MessageBox.ShowAndRedirect(this, "保存成功！", "DealersDealersInfoList.aspx");
                }
                else
                {
                    MessageBox.Show(this, "保存失败！");
                }
            }
            else
            {
                MessageBox.Show(this, msg);
            }
        }

        /// <summary>
        /// 获取更新后产品编号
        /// </summary>
        /// <returns></returns>
        private string GetProductID(string dealersInfoID)
        {
            //更新后产品编号
            string[] productID_New = hidProductID_New.Value.Split(',');
            //更新前产品编号
            string[] productID_Old = hidProductID_Old.Value.Split(',');
            string productID = string.Empty;
            bool flag = false;
            if (!hidProductID_New.Value.Equals(hidProductID_Old.Value))
            {
                for (int i = 0; i < productID_Old.Length; i++)
                {
                    for (int j = 0; j < productID_New.Length; j++)
                    {
                        if (productID_Old[i].Equals(productID_New[j]))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        productID += productID_Old[i] + ",";
                    }
                }
                //如果存在编辑后，有被取消的产品
                if (!string.IsNullOrEmpty(productID))
                {
                    Common_BLL bll = new Common_BLL();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat(@"SELECT  D.ApplyBasis_ProductDefinitionID ,
                                                  E.ProductDefinition_Name ,
                                                  COUNT(1) AS Number
                                          FROM    dbo.Dealers_DealersInfo A
                                                  JOIN dbo.Sys_Department B ON A.DealersInfo_DeptID = B.DepartmentID
                                                  JOIN dbo.Sys_UserInfo C ON B.Department_Code = C.UserInfo_DepCode
                                                  JOIN dbo.Business_ApplyBasis D ON C.UserInfoID = D.ApplyBasis_UpdateUserID
                                                  LEFT JOIN dbo.Product_ProductDefinition E ON D.ApplyBasis_ProductDefinitionID = E.ProductDefinition_ID
                                          WHERE   A.DealersInfoID = '{0}'
                                                  AND D.WorkflowTasksSetp NOT IN ( 0, 7 )
                                                  AND D.ApplyBasis_IsDelete = 0
                                                  AND D.ApplyBasis_ProductDefinitionID IN (
                                                  SELECT  string
                                                  FROM    dbo.Split('{1}', ',') )
                                          GROUP BY D.ApplyBasis_ProductDefinitionID ,
                                                  E.ProductDefinition_Name ", dealersInfoID, productID);

                    //查询被取消的产品中，该经销商下所有用户是否有未走完申请流程的申请单
                    DataTable dt = bll.GetDataBySQL(strSql.ToString());
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show(this, "该经销商所关联的产品有未完成申请流程的申请单，无法保存！");
                            productID = "0";
                        }
                        else
                        {
                            productID = hidProductID_New.Value;
                        }
                    }
                }
                else
                {
                    productID = hidProductID_New.Value;
                }
            }
            else
            {
                productID = hidProductID_Old.Value;
            }

            return productID;
        }

        protected void ddlDealersInfo_Area1_TextChanged(object sender, EventArgs e)
        {
            Common_BLL bllComm = new Common_BLL();
            //所属区域2
            DataTable listDealersInfo_Area2 = bllComm.GetDataBySQL("SELECT CONVERT(varchar(50),B.AreaId) as AreaId,B.Area_Name FROM dbo.Business_AreaDistribution A LEFT JOIN dbo.Business_Area B ON A.AreaDistribution_AreaID=B.AreaId WHERE A.AreaDistribution_ID='" + ddlDealersInfo_Area1.SelectedValue + "' AND B.Area_Depth=1");
            if (listDealersInfo_Area2.Rows.Count > 0)
            {
                DataRow newRow;
                newRow = listDealersInfo_Area2.NewRow();
                newRow["AreaId"] = "";
                newRow["Area_Name"] = Constant.DrpChoiceName;
                listDealersInfo_Area2.Rows.InsertAt(newRow, 0);
            }
            this.ddlDealersInfo_Area2.DataSource = listDealersInfo_Area2;
            this.ddlDealersInfo_Area2.DataTextField = "Area_Name";
            this.ddlDealersInfo_Area2.DataValueField = "AreaId";
            this.ddlDealersInfo_Area2.DataBind();
            //所属区域3
            DataTable listDealersInfo_Area3 = bllComm.GetDataBySQL("SELECT CONVERT(varchar(50),AreaId) as AreaId,Area_Name FROM dbo.Business_Area WHERE Area_ParentId=(SELECT Area_ID FROM dbo.Business_Area WHERE  AreaId='" + ddlDealersInfo_Area2.SelectedValue + "')");
            if (listDealersInfo_Area3.Rows.Count >0)
            {
                DataRow newRow1;
                newRow1 = listDealersInfo_Area3.NewRow();
                newRow1["AreaId"] = "";
                newRow1["Area_Name"] = Constant.DrpChoiceName;
                listDealersInfo_Area3.Rows.InsertAt(newRow1, 0);
            }
            this.ddlDealersInfo_Area3.DataSource = listDealersInfo_Area3;
            this.ddlDealersInfo_Area3.DataTextField = "Area_Name";
            this.ddlDealersInfo_Area3.DataValueField = "AreaId";
            this.ddlDealersInfo_Area3.DataBind();
        }

        protected void ddlDealersInfo_Area2_TextChanged(object sender, EventArgs e)
        {
            Common_BLL bllComm = new Common_BLL();
            //所属区域3
            DataTable listDealersInfo_Area3 = bllComm.GetDataBySQL("SELECT CONVERT(varchar(50),AreaId) as AreaId,Area_Name FROM dbo.Business_Area WHERE Area_ParentId=(SELECT Area_ID FROM dbo.Business_Area WHERE  AreaId='" + ddlDealersInfo_Area2.SelectedValue + "')");
            DataRow newRow;
            newRow = listDealersInfo_Area3.NewRow();
            newRow["AreaId"] = "";
            newRow["Area_Name"] = Constant.DrpChoiceName;
            listDealersInfo_Area3.Rows.InsertAt(newRow, 0);
            this.ddlDealersInfo_Area3.DataSource = listDealersInfo_Area3;
            this.ddlDealersInfo_Area3.DataTextField = "Area_Name";
            this.ddlDealersInfo_Area3.DataValueField = "AreaId";
            this.ddlDealersInfo_Area3.DataBind();
        }
    }
}