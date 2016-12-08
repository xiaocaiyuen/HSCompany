<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCFilesImage.ascx.cs" Inherits="Shu.Manage.UserControls.UCFilesImage" %>

<script type="text/javascript">
    var <%=this.FilesNname %>loaded = false;
    $(function () {
        if (!<%=this.FilesNname %>loaded) {
            <%=this.FilesNname %>loaded = true;
            var auth = "<% = Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value %>";
            var ASPSESSID = "<%= Session.SessionID %>";
            $('#<%=this.FilesNname %>_FileUpload2').uploadify({
                'swf': '<%=ResolveUrl("~/JQuploadify/uploadify.swf") %>',
                'uploader': '/Handler/FileHandler.ashx?ASPSESSID=' + ASPSESSID + '&Option=addbeijing&OperationID=' + $("#<%=this.FilesNname %>_Hidden_OperationID").val() + '&SessionID=' + $("#<%=this.FilesNname %>_Hidden_SessionID").val() + '&FileType=' + escape($("#<%=this.FilesNname %>_Hidden_FileType").val()),
                'fileTypeDesc': '<%=this.FileTypeDesc %>',     //描述    
                'fileTypeExts': '<%=this.FileTypeExts %>',     //文件类型    
                'fileSizeLimit': '<%=this.FileSizeLimit %>MB',         //文件大小    
                'buttonText': '<%=this.FileButtonText %>',
                'multi': '<%=this.FileMulti %>',
                'scriptData': { ASPSESSID: ASPSESSID, AUTHID: auth },
                'onUploadSuccess': function (file, data, response) {
                    var attach = eval('(' + data + ')');
                    if (attach.filadd == "true") {
                        var addHtml = "<tr id='" + attach.id + "'>" +
                                "<td style=\"overflow:hidden;text-overflow:ellipsis;word-break:keep-all;white-space:nowrap;\" >" +
                                "<a href='" + attach.fileUrl + "'>" + attach.filename + "</a>" +
                                "</td>" +
                                "<td>" + attach.filesize + "</td>" +
                                "<td width='120px'>" + attach.uploaddate + "</td>" +
                                "<td width='130px'><a href='" + attach.fileUrl + "' class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-download'\" >下载</a>&nbsp;&nbsp;<a style=\"cursor:pointer\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-remove'\"  onclick=\"<%=this.FilesNname %>_deletr('" + attach.id + "');\">删除</a>&nbsp;&nbsp;<a style=\"cursor:pointer\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-remove'\"  onclick=\" Imagebeijing('" + attach.url + "', '" + attach.filename + "');\">预览</a></td>" +
                              "</tr>";
                        $("#<%=this.FilesNname %>_fileBody").append(addHtml);
                        $.messager.progress('close');
                        $('.easyui-linkbutton').linkbutton({});
                    } else {
                        
                        if (attach.name=="2")
                        {
                            $.messager.progress('close');
                            alert(attach.filename);
                           
                        }
                        else
                        {
                            $.messager.progress('close');
                            $.messager.alert('提示', '"' + attach.filename + '"文件上传失败，请重新上传！', 'error');
                          
                        }
                        
                    }
                },
                //检测FLASH失败调用
                'onFallback': function () {
                    alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
                },
                'onUploadError': function (file, errorCode, errorMsg, errorString) { alert('The file ' + file.name + ' could not be uploaded: ' + errorString + '  errorMsg:' + errorMsg + '  errorCode:' + errorCode + ''); }
            });
            $("#<%=this.FilesNname %>_lbl_fileMessage").html("(最大支持<%=this.FileSizeLimit %>MB附件上传)"); 
            $("#<%=this.FilesNname %>_fenbanlv").html("(支持分辨率为<%=this.FileFBL %>)");
            $("#<%=this.FilesNname %>_ft").html("文件类型：<%=this.FileTypeExts %>");
            var OperationID = $("#<%=this.FilesNname %>_Hidden_OperationID").val();
            //alert(OperationID);
            if (OperationID != "") {
                $.ajax({
                    type: 'POST',
                    dataType: "json",
                    async: false,
                    url: "/Handler/FileHandler.ashx?OperationID=" + OperationID + "&FileType=" + escape($("#<%=this.FilesNname %>_Hidden_FileType").val()) + "&Option=select",
                    success: function (json) {
                        //alert(json);
                        $.each(json, function (i, item) {
                            var addHtml = "<tr id='" + item.id + "'>" +
                          "<td width='200px'>" +
                          "<a href='/Files/Download.aspx?path=" + item.fileUrl + "&filename=" + item.filename + "'>" + item.filename + "</a>" +
                          "</td>" +
                          "<td width='60px'>" + item.filesize + "</td>" +
                          "<td width='130px'>" + item.uploaddate + "</td>" +
                          "<td width='130px' ><a href='/Files/Download.aspx?path=" + item.fileUrl + "&filename=" + item.filename + "' class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-download'\" >下载</a>&nbsp;&nbsp;<a style=\"cursor:pointer\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-remove'\" onclick=\"<%=this.FilesNname %>_deletr('" + item.id + "');\">删除</a>&nbsp;&nbsp;<a style=\"cursor:pointer\" class=\"easyui-linkbutton\" data-options=\"plain:true,iconCls:'icon-remove'\"  onclick=\"  Imagebeijing('" + item.fileUrl + "', '" + item.filename + "');\">预览</a></td>" +
                          "</tr>";
                            $("#<%=this.FilesNname %>_fileBody").append(addHtml);
                        });
                    }
                });
                $('.easyui-linkbutton').linkbutton({});
            }
        }
    });
    function <%=this.FilesNname %>_deletr(id) {
        $.messager.confirm('提示', '您想要删除这条附件吗？', function (r) {
            if (r) {
                $.ajax({
                    type: 'POST',
                    url: '/Handler/FileDeleHandler.ashx?id=' + id + '&SessionID=' + $("#<%=this.FilesNname %>_Hidden_SessionID").val() + '&FileType=' + escape($("#<%=this.FilesNname %>_Hidden_FileType").val()) + '&Option=Dele',
                    success: function (msg) {
                        // alert(msg);
                        if (msg == "1") {

                            $('#' + id).remove();
                            $.messager.alert('提示', '删除成功！', 'warning');

                        } else {

                            $.messager.alert('提示', '删除失败！', 'error');
                        }
                    }
                });
            }
        });
    }

    function Imagebeijing(imageUrl, dss) {
      //  alert(imageUrl+"-"+dss);
        parent.ImageShows(imageUrl, dss);
        //alert(imageUrl + "-" + dss);
        //$('#ui_Image').css('display', '');
        //$('#ui_Image').dialog('open');
        //$('#view_Image').attr('src', '/Windows/Win_Images.aspx?imageUrl=' + imageUrl + '&uls=' + dss);

    }
    window.onunload = function () {
        try {
            $("#<%=this.FilesNname %>_FileUpload2").empty();
        } catch (e) {

        }
    }


</script>
<asp:HiddenField ID="Hidden_fileTypeDesc" runat="server"  />
<asp:HiddenField ID="Hidden_fileTypeExts" runat="server" />
<asp:HiddenField ID="Hidden_FileSizeLimit" runat="server"  />
<asp:HiddenField ID="Hidden_ButtonText" runat="server"  />
<asp:HiddenField ID="Hidden_FileNmae" runat="server" />
<asp:HiddenField ID="Hidden_OperationID" runat="server" />
<asp:HiddenField ID="Hidden_SessionID" runat="server" />
<asp:HiddenField ID="Hidden_Multi" runat="server"/>
<asp:HiddenField ID="Hidden_FileType" runat="server"/>
<asp:HiddenField ID="Hidden_FileFBL" runat="server"/>

<div>
    <div id="<%=this.FilesNname %>_fileTable">
        <table width="100%" class="tab" border="0" style="table-layout:fixed"  cellspacing="1" id="<%=this.FilesNname %>_fileBody">
            <tr>
                <th width='300px'>
                    文件名
                </th>
                <th width='70px'>
                    文件大小
                </th>
                <th width='120px'>
                    上传时间
                </th>
                <th width='130px' >
                    操作
                </th>
            </tr>
        </table>
    </div>
    <table width="100%" border="0" cellpadding="10" cellspacing="1">
        <tr>
            <td>
                <span style='color: red' id="<%=this.FilesNname %>_lbl_fileMessage"></span>
            </td>
            <td>
                <%--<div id="p" class="easyui-progressbar" data-options="value:0" style="width: 300px;">
                </div>--%>
            </td>
        </tr>
         <tr>
            <td>
                <span style='color: red' id="<%=this.FilesNname %>_fenbanlv"></span>
            </td>
            <td>
                <%--<div id="p" class="easyui-progressbar" data-options="value:0" style="width: 300px;">
                </div>--%>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <font color="red" id="<%=this.FilesNname %>_ft"></font>
            </td>
        </tr>
    </table>
    <asp:FileUpload ID="FileUpload2" runat="server" />
</div>
<div id="ww" style="display: none">
</div>
