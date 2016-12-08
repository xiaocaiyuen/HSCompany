<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataReduction.aspx.cs"
    Inherits="YDT.Web.Manage.Sys.DataReduction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Styles/girdview/grid.css" rel="stylesheet" type="text/css" />
     <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <style type="text/css">
        /* 全局样式 */
        body
        {
            margin: 0px;
            padding: 0px;
            background-color: #ffffff;
            color: #000;
            text-decoration: none;
            font: normal 12px/22px Verdana, Geneva, sans-serif;
        }
        div, dl, dt, dd, ol, ul, li, h1, h2, h3, h4, h5, h6, pre, form, fieldset, input, textarea, blockquote, p
        {
            padding: 0;
            margin: 0;
        }
        h1, h2, h3, h4, h5, h6
        {
            font-size: 12px;
            font-style: normal;
            font-weight: normal;
            font-variant: normal;
        }
        ol, ul, li
        {
            list-style: none;
        }
        div
        {
            background: url(none);
            margin: 0px;
            padding: 0px;
            border-style: none;
        }
        img
        {
            vertical-align: top;
            border-top: 0;
            border-right: 0;
            border-bottom: 0;
            border-left: 0;
        }
        a
        {
            font-size: 12px;
            color: #000;
            text-decoration: none;
        }
        a:visited
        {
            text-decoration: none;
        }
        a:hover
        {
            color: #F00;
            text-decoration: underline;
        }
        a:active
        {
            color: #999;
        }
        
        /* 主Table样式 */
        .maintable
        {
            width: 100%;
            padding: 0px;
            margin: 0px;
            border: 0px;
        }
        
        
        /* 查询条件区域 */
        
        .list_Index
        {
            border-bottom: 1px solid #AADAEE;
            text-align: left;
            width: 100%;
        }
        
        .list_Index th
        {
            text-align: right;
            width: 100px;
        }
        
        
        .list_Index td
        {
            padding-left: 5px;
            height: 35px;
            vertical-align: middle;
        }
        
        .list_Index td image
        {
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#gridFiles").find("a").each(function () {
                if ($(this).html() == "编辑" || $(this).html() == "Edit") {
                    $(this).click(function () {
                        return confirm('该操作将覆盖您之前的数据，不可逆转。是否继续？');
                    })
                    $(this).html("还原")
                }
            })
        })

        

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <table cellpadding="0" cellspacing="0" class="maintable">
            <tr>
                <td>
                    <asp:GridView ID="gridFiles" runat="server" BackColor="White" border="0" CellSpacing="0" CellPadding="1" GridLines="None" Height="100%"
                        Width="100%" CssClass="grid" AutoGenerateColumns="false" DataKeyNames="fileName"  
                        OnRowEditing="gridFiles_RowEditing">
                        <Columns>
                            <asp:BoundField DataField="fileName" HeaderText="文件名" />
                            <asp:BoundField DataField="createTime" HeaderText="创建时间" />
                            <asp:BoundField DataField="filePath" HeaderText="文件路径" />
                            <asp:CommandField HeaderText="操作" ShowEditButton="True" />
                        </Columns>
                        <EmptyDataTemplate>
                            <table width="100%" class="grid" cellpadding="0" cellspacing="0" border="0"  style="height:100%;width:100%;border-collapse:collapse;">
                                <tr>
                                    <th>
                                        文件名
                                    </th>
                                    <th>
                                        创建时间
                                    </th>
                                    <th>
                                        文件路径
                                    </th>
                                    <th>
                                        操作
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        暂无备份数据...
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
