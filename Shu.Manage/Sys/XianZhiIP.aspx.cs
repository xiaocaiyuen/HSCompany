using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YDT.BLL;
using YDT.Comm;
using YDT.Model;

namespace YDT.Web.Manage.Sys
{
    public partial class XianZhiIP : BasePage
    {
      
        Sys_XianZhiIP XianZhi = new Sys_XianZhiIP();
        Sys_XianZhiIPBLL XianZhibll = new Sys_XianZhiIPBLL();
        string id = "";
        protected void Page_Load(object sender, EventArgs e)
        {

           
            if (!IsPostBack)
            {
                id = Request["id"];
                if (Request["type"] == "AddButton")
                {
                    id = "";
                }
                show();
            }
        }

        public void show()
        {
           
                if (!string.IsNullOrEmpty(id))
                {
                    XianZhi = XianZhibll.Find(p => p.XianZhiIPID == id);
                    if (XianZhi != null)
                    {
                        this.txtkaishi.Text = XianZhi.XianZhiIP_StartIP;
                        this.txtjieshu.Text = XianZhi.XianZhiIP_EndIP;


                    }
                }
           

        }
        /// 保存
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object obj, EventArgs e)
        {
            
            string msg = string.Empty;
            id = Request["id"];
            if (string.IsNullOrEmpty(id))
            {
                XianZhi.XianZhiIPID = Guid.NewGuid().ToString();

                XianZhi.XianZhiIP_StartIP = this.txtkaishi.Text;
                XianZhi.XianZhiIP_EndIP = this.txtjieshu.Text;
                XianZhi.XianZhiIP_AddTime = DateTime.Now;
                if (XianZhibll.Add(XianZhi, out msg))
                {


                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('添加成功！');coed('1')", true);

                }
                else
                {
                    MessageBox.Show(this, msg);
                }
            }
            else
            {

                XianZhi = XianZhibll.Find(p => p.XianZhiIPID == id);

                XianZhi.XianZhiIP_StartIP = this.txtkaishi.Text;
                XianZhi.XianZhiIP_EndIP = this.txtjieshu.Text;
                XianZhi.XianZhiIP_AddTime = DateTime.Now;
                if (XianZhibll.Update(XianZhi, out msg))
                {



                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('修改成功！');coed('2')", true);


                }
                else
                {
                    MessageBox.Show(this, msg);
                }
            }
        }
    }
}