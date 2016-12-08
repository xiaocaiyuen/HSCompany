<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealersDealersInfoShow.aspx.cs" Inherits="YDT.Web.Manage.Sys.DealersDealersInfoShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>

    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <%--<link href="/Styles/List.css" rel="stylesheet" type="text/css" />--%>
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script charset="utf-8" src="/JQuploadify/jquery.uploadify.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="/Scripts/lightbox2/css/screen.css" rel="stylesheet" />
    <script type="text/javascript">
        function showImage(imageUrl) {
            parent.ImageShow(imageUrl)
        }
        function showImages(imageUrl, ds) {
            parent.ImageShows(imageUrl, ds)
        }
    </script>
    <style type="text/css">
        .tab th {
            width: 110px;
        }

        .tab td {
            width: 240px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div id="titleScore" class="easyui-panel" title="信息查看" style="width: auto; min-width: 800px; height: auto; text-align: center">
            <table width="1050" class="tab" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <th>所属品牌厂商
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblDealersInfo_BrandIDStr" runat="server"></asp:Label>
                    </td>
                    <th>简称
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_AbbName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>名称
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_FullName" runat="server"></asp:Label>
                    </td>
                    
                    <th>类型
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_TypeName" runat="server"></asp:Label>
                    </td>
                     <th>银行名称
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_AccountBank" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                   
                    <th>开户银行名称
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_AccountBankName" runat="server"></asp:Label>
                    </td>
                    <th>银行开户户名
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_AccountName" runat="server"></asp:Label>
                       
                    </td>
                    <th>银行账号
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_BankAccountNumber" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>税务登记号
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_TaxRegistrationNum" runat="server"></asp:Label>
                    </td>
                    <th>联系方式
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_Contact" runat="server"></asp:Label>
                    </td>
                    <th>传真
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_Fax" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>企业性质
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_Property" runat="server"></asp:Label>
                    </td>
                    <th>营业执照注册号
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_LicenseNum" runat="server"></asp:Label>
                    </td>
                    <th>成立日期
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_CLDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>有效期
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_ValidityPeriod" runat="server"></asp:Label>
                    </td>
                    <th>注册资本（万元）
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_RegisteredCapital" runat="server"></asp:Label>
                    </td>
                    <th>实收资本（万元）
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_RealCapital" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>邮编
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_ZipCode" runat="server"></asp:Label>
                    </td>
                    <th>注册地址
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_RegAddress" runat="server"></asp:Label>
                    </td>
                    <th>所属区域
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_AreaName" runat="server"></asp:Label>
                        层级一：<asp:Label ID="lblDealersInfo_HierarchyOne" runat="server"></asp:Label>
                        层级二：<asp:Label ID="lblDealersInfo_HierarchyTwo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>组织机构代码号
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_OranCode" runat="server"></asp:Label>
                    </td>
                    <th>网址
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_Website" runat="server"></asp:Label>
                    </td>
                    <th>地址
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_Address" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>经营范围
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_ScopeBusiness" runat="server"></asp:Label>
                    </td>
                    <th>所属行业
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_BelongIndustry" runat="server"></asp:Label>
                    </td>
                    <th>其他信息
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_OtherInfo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>档案协议编号
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_FileProtocol" runat="server"></asp:Label>
                    </td>
                    <th>合作有效期
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_CooperativeValidity" runat="server"></asp:Label>
                    </td>
                    <th>合作类型
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_CooperativeTypeName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>返佣比例
                    </th>
                    <td>
                        <asp:Label ID="lblDealersInfo_ReturnRatio" runat="server"></asp:Label>
                    </td>
                    <th>产品
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lblDealersInfo_Product" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        <%
                            List<YDT.Model.Sys_ModelFile> newsfiles = bllFiles.FindWhere("1=1 and File_OperationID='" + Request.QueryString["id"] + "'");
                            var ZLfiles = newsfiles.FindAll(p => (p.File_Extension.ToLower() == ".jpg" || p.File_Extension.ToLower() == ".gif" || p.File_Extension.ToLower() == ".png" || p.File_Extension.ToLower() == ".bmp" || p.File_Extension.ToLower() == ".jpeg"));
                            var imgurls1 = "";
                            var ids1 = "";
                            if (ZLfiles.Count > 0)
                            {
                                foreach (YDT.Model.Sys_ModelFile file in ZLfiles)
                                {
                                    imgurls1 += file.File_Path + "|";
                                    ids1 += file.FileID + ",";
                                }
                            }
                            if (ids1.Length > 0)
                            {
                                ids1 = ids1.Substring(0, ids1.Length - 1);
                            }

                            var filenames1 = "经销商资料(" + model.DealersInfo_FullName + ")";
                            filenames1 = System.Web.HttpUtility.UrlDecode(filenames1, System.Text.Encoding.UTF8);
                        %>
                
                        附件:
                        <br />
                         <%  if (ids1.Length > 0)
                            {
                        %>
                        <a href="/Files/DownloadZip.aspx?ids=<%=ids1 %>&SessionID=ww&filename=<%=filenames1 %>)">批量下载</a>
                         <%  } %>
                    </th>
                    <td colspan="5">

                        <%  if (ZLfiles.Count > 0)
                            {
                        %>
                        <fieldset class="listfieldset">
                            <legend></legend>
                            <div class="image-row">
                                <div class="image-set">
                                    <%
                                foreach (YDT.Model.Sys_ModelFile file in ZLfiles)
                                {
                                    var aFirstName = file.File_Name.Substring(file.File_Name.LastIndexOf("\\") + 1, (file.File_Name.LastIndexOf(".") - file.File_Name.LastIndexOf("\\") - 1));

                                    var imgtitle = "";
                                    if (aFirstName.Length > 10)
                                    {
                                        imgtitle = aFirstName.Substring(0, 10) + "..";
                                    }
                                    else
                                    {
                                        imgtitle = aFirstName;
                                    }
                                    %>
                                    <a class="example-image-link" href="javascript:showImages('<%=file.File_Path %>','<%=imgurls1 %>')" data-lightbox="example-set" title="<%=aFirstName %>">
                                        <div style="text-align: center;">
                                            <div>
                                                <img class="example-image" src="<%=file.File_Path %>" alt="<%=aFirstName %>" />


                                            </div>
                                            <div style="margin-top: 12px; margin-bottom: 5px;"><%=imgtitle %></div>
                                        </div>
                                    </a>
                                    <%}%>
                                </div>
                            </div>
                        </fieldset>
                        <%} %>
                    </td>
                </tr>
                <tr>
                    <th>品牌
                    </th>
                    <td colspan="5">
                        <asp:Label ID="lblDealersInfo_Brand" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            </div>
            <table width="1050" border="0" cellspacing="0" cellpadding="0" class="for">
                <tr>
                    <th>
                        <img width="50" height="24" style="cursor: pointer;" onclick="javascript:window.parent.Closetab()" alt="" src="/Images/buttons/win_guanbi.gif" />
                    </th>
                </tr>
            </table>
        </center>
    </form>
</body>
</html>
