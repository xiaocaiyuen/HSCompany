<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuditAreaMappingSetting.aspx.cs" Inherits="YDT.Web.Manage.Sys.AuditAreaMappingSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Styles/default.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/common.js" type="text/javascript"></script>
    <script src="../../Scripts/ApprovalPool/AuditAreaMapping.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var type = "<%=AuditAreaMappingModel==null?"":AuditAreaMappingModel.AuditAreaMapping_AreaCategory%>";
            $("input[name='m'][value='" + type + "']").attr("checked", true);
            var values = "<%=AuditAreaMappingModel==null?"":AuditAreaMappingModel.AuditAreaMapping_AreaCategoryValue%>";
            ChangeValues(type, values,'<%=hidRoleID.Value%>','<%=hidUserID.Value%>');
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidRoleID" runat="server" />
        <asp:HiddenField ID="hidUserID" runat="server" />
        <asp:HiddenField ID="SaveData" runat="server" />
        <div style="display: none;">
            <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" UseSubmitBehavior="false" />
        </div>
        <div>
            <span>
                <input type="radio" name="m" value="1" onclick="radioClick(this);" />按区域</span>
            <div id="div1" style="display: none; padding-left: 20px;">
                <%AreaList.ForEach(item =>
                    {
                    if (AuditAreaMappingModel_List.Count > 0)
                    {

                        if (AuditAreaMappingModel.IsNotNull())
                        {
                            if (AuditAreaMappingModel.AuditAreaMapping_AreaCategoryValue.Contains(item.AreaDistribution_ID))
                            { %>
                <input type="checkbox" name="chk1" checked="checked" value="<%=item.AreaDistribution_ID%>" /><%=item.AreaDistribution_Name%>
                <%}
                    else
                    {
                        var boolOk = true;
                        var AuditAreaMappingModel_disabled = AuditAreaMappingModel_List.Where(p => p.AuditAreaMapping_UserID != hidUserID.Value).ToList();
                        foreach (var v in AuditAreaMappingModel_disabled)
                        {
                            if (v.AuditAreaMapping_AreaCategoryValue.Contains(item.AreaDistribution_ID))
                            {
                                boolOk = false;
                                break;
                            }
                        };

                        if (boolOk)
                        {%>
                <input type="checkbox" name="chk1" value="<%=item.AreaDistribution_ID%>" /><%=item.AreaDistribution_Name%>
                <%}
                        }
                    }
                    else
                    {
                        var boolOk = true;
                        var AuditAreaMappingModel_disabled = AuditAreaMappingModel_List.Where(p => p.AuditAreaMapping_UserID != hidUserID.Value).ToList();
                        foreach (var v in AuditAreaMappingModel_disabled)
                        {
                            if (v.AuditAreaMapping_AreaCategoryValue.Contains(item.AreaDistribution_ID))
                            {
                                boolOk = false;
                                break;
                            }
                        };

                        if (boolOk)
                        {%>
                <input type="checkbox" name="chk1" value="<%=item.AreaDistribution_ID%>" /><%=item.AreaDistribution_Name%>
                <%}%>
                <%}
                    }
                    else
                    {
                %>
                <input type="checkbox" name="chk1" value="<%=item.AreaDistribution_ID%>" /><%=item.AreaDistribution_Name%>
                <%
                      }
                  }
                  ); %>
            </div>
            <hr style="height: 1px; border: none; border-top: 1px dashed #0066CC;" />
            <span>
                <input type="radio" name="m" value="2" onclick="radioClick(this,'<%=hidRoleID.Value%>    ','<%=hidUserID.Value%>    ');" />按省份</span>
            <div id="div2" style="display: none; width: 500px; height: 300px; padding-left: 20px; overflow: auto;">
                <ul id="tt2">
                </ul>
            </div>
            <hr style="height: 1px; border: none; border-top: 1px dashed #0066CC;" />
            <span>
                <input type="radio" name="m" value="3" onclick="radioClick(this);" />按层级1</span>
            <div id="div3" style="display: none; padding-left: 20px;">
                <%HierarchyList1.ForEach(item =>
                    {
                    if (AuditAreaMappingModel_List.Count > 0)
                    {

                        if (AuditAreaMappingModel.IsNotNull())
                        {
                            if (AuditAreaMappingModel.AuditAreaMapping_AreaCategoryValue.Contains(item.Hierarchy_Code))
                            { %>
                <input type="checkbox" name="chk3" checked="checked" value="<%=item.Hierarchy_Code%>" /><%=item.Hierarchy_Name%>
                <%}
                    else
                    {
                        var boolOk = true;
                        var AuditAreaMappingModel_disabled = AuditAreaMappingModel_List.Where(p => p.AuditAreaMapping_UserID != hidUserID.Value).ToList();
                        foreach (var v in AuditAreaMappingModel_disabled)
                        {
                            if (v.AuditAreaMapping_AreaCategoryValue.Contains(item.Hierarchy_Code))
                            {
                                boolOk = false;
                                break;
                            }
                        };

                        if (boolOk)
                        {%>
                <input type="checkbox" name="chk3" value="<%=item.Hierarchy_Code%>" /><%=item.Hierarchy_Name%>
                <%}


                        }
                    }
                    else
                    {
                        var boolOk = true;
                        var AuditAreaMappingModel_disabled = AuditAreaMappingModel_List.Where(p => p.AuditAreaMapping_UserID != hidUserID.Value).ToList();
                        foreach (var v in AuditAreaMappingModel_disabled)
                        {
                            if (v.AuditAreaMapping_AreaCategoryValue.Contains(item.Hierarchy_Code))
                            {
                                boolOk = false;
                                break;
                            }
                        };

                        if (boolOk)
                        {%>
                <input type="checkbox" name="chk3" value="<%=item.Hierarchy_Code%>" /><%=item.Hierarchy_Name%>
                <%}%>

                <%}
                    }
                    else
                    {
                %>
                <input type="checkbox" name="chk3" value="<%=item.Hierarchy_Code%>" /><%=item.Hierarchy_Name%>
                <%
                      }

                  }); %>
            </div>
            <hr style="height: 1px; border: none; border-top: 1px dashed #0066CC;" />
            <span>
                <input type="radio" name="m" value="4" onclick="radioClick(this);" />按层级2</span>
            <div id="div4" style="display: none; padding-left: 20px;">
                <%HierarchyList2.ForEach(item =>
                    {
                    if (AuditAreaMappingModel_List.Count > 0)
                    {

                        if (AuditAreaMappingModel.IsNotNull())
                        {
                            if (AuditAreaMappingModel.AuditAreaMapping_AreaCategoryValue.Contains(item.Hierarchy_Code))
                            { %>
                <input type="checkbox" name="chk4" checked="checked" value="<%=item.Hierarchy_Code%>" /><%=item.Hierarchy_Name%>
                <%}
                    else
                    {
                        var boolOk = true;
                        var AuditAreaMappingModel_disabled = AuditAreaMappingModel_List.Where(p => p.AuditAreaMapping_UserID != hidUserID.Value).ToList();
                        foreach (var v in AuditAreaMappingModel_disabled)
                        {
                            if (v.AuditAreaMapping_AreaCategoryValue.Contains(item.Hierarchy_Code))
                            {
                                boolOk = false;
                                break;
                            }
                        };

                        if (boolOk)
                        {%>
                <input type="checkbox" name="chk4" value="<%=item.Hierarchy_Code%>" /><%=item.Hierarchy_Name%>
                <%}
                        }
                    }
                    else
                    {var boolOk = true;
                        var AuditAreaMappingModel_disabled = AuditAreaMappingModel_List.Where(p => p.AuditAreaMapping_UserID != hidUserID.Value).ToList();
                        foreach (var v in AuditAreaMappingModel_disabled)
                        {
                            if (v.AuditAreaMapping_AreaCategoryValue.Contains(item.Hierarchy_Code))
                            {
                                boolOk = false;
                                break;
                            }
                        };

                        if (boolOk)
                        {%>
                <input type="checkbox" name="chk4" value="<%=item.Hierarchy_Code%>" /><%=item.Hierarchy_Name%>
                <%}%>
                <%}
                    }
                    else
                    {
                %>
                <input type="checkbox" name="chk4" value="<%=item.Hierarchy_Code%>" /><%=item.Hierarchy_Name%>
                <%
                      }
                  }); %>
            </div>
            <hr style="height: 1px; border: none; border-top: 1px dashed #0066CC;" />
        </div>
        <div style="text-align: center;">
            <a href="#" class="easyui-linkbutton" onclick="Save('linkSubmit','btn_Submit',1);">保存</a>
        </div>
    </form>
</body>
</html>
