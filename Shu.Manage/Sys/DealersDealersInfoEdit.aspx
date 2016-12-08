<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealersDealersInfoEdit.aspx.cs" Inherits="YDT.Web.Manage.Sys.DealersDealersInfoEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="/Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/Scripts/linqJson/linq.min.js" type="text/javascript"></script>
    <link href="/Styles/table.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/List.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/UI/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
     <link href="/Scripts/UI/themes/icon.css" rel="stylesheet" />
    <script src="/Scripts/DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script charset="utf-8" src="/JQuploadify/jquery.uploadify.js" type="text/javascript"></script>
    <script src="/Scripts/UI/jquery.easyui.min.js" type="text/javascript"></script>

    <link href="/JQuploadify/uploadify.css" rel="stylesheet" />
    <script charset="utf-8" src="/JQuploadify/jquery.uploadify.js" type="text/javascript"></script>
    <%--录入验证 --%>
    <link href="/Styles/validationEngine.jquery.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Validate/2.6.2Ciaoca/jquery.validationEngine-zh_CN.js" type="text/javascript" charset="utf-8"></script>
    <style type="text/css">
        .tab th {
            width: 110px;
        }

        .tab td {
            width: 240px;
        }

        .div_Brand {
            background-color: rgb(245, 167, 111);
            padding: 1px 1px 1px 1px;
            border: 1px solid #FF6600;
            margin-left: 10px;
            margin-top: 2px;
            margin-bottom: 3px;
            float: left;
            height: 20px;
            cursor: default;
        }

            .div_Brand img {
                padding-top: 2px;
                float: right;
                cursor: pointer;
            }

            .div_Brand:hover {
                padding: 0px 0px 0px 0px;
                border: 2px solid #FF9900;
                background-color: rgb(245, 207, 155);
            }
    </style>
    <script type="text/javascript">
        var str_BrandID = "";
        var str_BrandName = "";
        var str_BrandInfo = "";
        var data = null;

        $(function () {
            $("#form1").validationEngine({ promptPosition: "centerRight", validationEventTriggers: "keyup blur" });
            //加载产品下拉框
            LoadProducts();
            //加载车辆品牌下拉框
            LoadBrands();
            //加载已选择品牌
            $("#td_Brand").append(document.getElementById("hidBrandInfo").value);
            str_BrandID = document.getElementById("hidBrandID").value;
            str_BrandName = document.getElementById("hidBrandName").value;


        });

        //加载产品下拉框
        function LoadProducts() {
            $("#ddlDealersInfo_Product").combobox({
                url: "/Handler/ProductCalculator.ashx?Method=GetProductDDL2&t=" + Math.random(),
                valueField: "ProductDefinition_ID",
                textField: "ProductDefinition_Name",
                multiple: "true",
                editable: false,
                width: 615,
                onLoadSuccess: function () {
                    var productID = document.getElementById("hidProductID_New").value;
                    if (productID != "") {
                        $("#ddlDealersInfo_Product").combobox("setValues", productID.split(','));
                    }
                },
                onSelect: function () {
                    document.getElementById("hidProductID_New").value = $("#ddlDealersInfo_Product").combobox("getValues");

                },
                onUnselect: function () {
                    document.getElementById("hidProductID_New").value = $("#ddlDealersInfo_Product").combobox("getValues");
                }
            });
        }

        //加载车辆品牌下拉框
        function LoadBrands() {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                async: false,
                url: '/Handler/VehicleHandler.ashx?Option=DealersBrand',
                success: function (msg) {
                    data = msg;
                }
            });
            var data1 = [{ id: "", text: "所有" }].concat(data);//将‘所有’设置为第一个选项
            var dataStr = [],
                dataStr1 = [];
            for (var i = 0; i < data1.length; i++) {
                if (i != 0) {
                    dataStr.push(data1[i].id);
                }
                dataStr1.push(data1[i].id);
            }
            //dataStr.sort();//将值由小到大排序
            //dataStr1.sort();
            var $test = $("#ddlVehicle_BrandID");

            $test.combobox({
                //url: "/Handler/VehicleHandler.ashx?Option=DealersBrand",
                data: data1,
                valueField: 'id',
                textField: 'text',
                editable: true,
                multiple: true,
                width: 155,
                //success:function(data){
                //    data = data;
                //    alert(data);
                //},
                onSelect: function (r) {
                    data = $test.combobox('getData');
                    if (r.id == "") {//当选的是‘所有’这个选项
                        $test.combobox("setValues", dataStr1).combobox("setText", '所有');//
                    } else {
                        var valArr = $test.combobox("getValues");
                        valArr.sort();//将值由小到大排序 以保持一致
                        if (valArr.join(',') == dataStr1.join(',') || valArr.join(',') == dataStr1.join(',')) {
                            $test.combobox("setValues", data).combobox("setText", '所有');//
                        }
                    }
                },
                onUnselect: function (r) {
                    if (r.id == '') {//当取消选择的是‘所有’这个选项
                        $test.combobox("setValues", []).combobox("setText", '');
                    } else {
                        var valArr = $test.combobox("getValues");
                        if (valArr[0] == "") {
                            valArr.shift();
                            $test.combobox("setValues", valArr);
                        }
                    }
                },
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].toLowerCase().indexOf(q.toLowerCase()) >= 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
                }
            });
        }

        //添加车辆品牌
        function AddBrand() {
            var str = "";
            str_BrandID = "";
            str_BrandName = "";
            str_BrandInfo = "";
            //品牌编号
            //var brandID = $('#ddlVehicle_BrandID').combobox("getValue");
            var brandIDs = $("#ddlVehicle_BrandID").combobox("getValues");//.join(",").replace(/^,/, "")//将第一个逗号去掉;
            //品牌名称
            //var brandName = $('#ddlVehicle_BrandID').combobox("getText");
            //var div_Width = brandName.length * 12 + 10 * 1;
            
            if (brandIDs.length < 1) {
                alert("请选择要添加的车辆品牌！");
                return;
            }
           

            $.each(brandIDs, function (index, value) {
                var brandID = value;
                var brandArray = Enumerable.From(data)
                .Where(function (x) { return x.id == value })
                .ToArray();
                if (brandArray.length > 0) {
                    var brandName = brandArray[0].text;

                    var div_Width = brandName.length * 12 + 10 * 1;
                    if (document.getElementById(brandID) == null && value != '') {
                        str = "<div id='" + brandID + "' class='div_Brand' style='width:" + div_Width + "px;'><span>" + brandName + "</span><img id='img_" + brandID + "' title='删除' onclick='RemoveBrand(\"" + brandID + "\",\"" + brandName + "\")' src='../../Images/buttons/btn_delete.gif'></img></div>";
                        $("#td_Brand").append(str);
                        str_BrandID += brandID + ",";
                        str_BrandName += brandName + ",";
                    }
                }
                //else {
                //    alert("此车辆品牌已经添加，不可重复添加！");
                //}
            });

     
            document.getElementById("hidBrandID").value = str_BrandID;
            document.getElementById("hidBrandName").value = str_BrandName;
            document.getElementById("hidBrandInfo").value = $("#td_Brand").html();
        }

        function ClearBrand()
        {
            $("#td_Brand").html("");
            $("#hidBrandID").val("");
            $("#hidBrandName").val("");
            $("#hidBrandInfo").val("");
            
        }

        //移除车辆品牌
        function RemoveBrand(brand_ID, brand_Name) {
            $("#" + brand_ID).remove();
            str_BrandID = str_BrandID.replace(brand_ID + ",", "");
            str_BrandName = str_BrandName.replace(brand_Name + ",", "");
            document.getElementById("hidBrandID").value = str_BrandID;
            document.getElementById("hidBrandName").value = str_BrandName;
            document.getElementById("hidBrandInfo").value = $("#td_Brand").html();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidProductID_New" runat="server" />
        <asp:HiddenField ID="hidProductID_Old" runat="server" />
        <asp:HiddenField ID="hidBrandID" runat="server" />
        <asp:HiddenField ID="hidBrandName" runat="server" />
        <asp:HiddenField ID="hidBrandInfo" runat="server" />
        <center>
            <div id="titleScore" class="easyui-panel" title="信息维护" style="width: auto; min-width: 800px; height: auto; text-align: center">
            <table width="1050" class="tab" border="0" cellpadding="1" cellspacing="1">
                <tr>
                    <th>所属品牌厂商
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtDealersInfo_BrandIDStr" runat="server" CssClass="input4" style="width:97%;"></asp:TextBox>
                    </td>
                     <th>简称<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_AbbName" runat="server" CssClass="input4 validate[required,maxSize[50]]" MaxLength="50" Style="width: 220px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>名称<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_FullName" runat="server" CssClass="input4 validate[required,maxSize[100]]" MaxLength="100" ReadOnly="true" Style="width: 220px;"></asp:TextBox>
                    </td>
                   
                    <th>类型<font color="red">*</font>
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlDealersInfo_Type" runat="server" CssClass="input4 validate[required]" Style="width: 150px;">
                        </asp:DropDownList>
                    </td>
                      <th>银行名称<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_AccountBank" runat="server" CssClass="input4 validate[required,maxSize[50]]" MaxLength="50" Style="width: 220px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>开户银行<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_AccountBankName" runat="server" CssClass="input4 validate[required,maxSize[50]]" MaxLength="50" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>银行开户户名<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_AccountName" runat="server" CssClass="input4 validate[required,maxSize[50]]" MaxLength="50" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>银行账号<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_BankAccountNumber" runat="server" CssClass="input4 validate[required,custom[onlyNumberSp],maxSize[20]]" MaxLength="20" Style="width: 220px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>税务登记号
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_TaxRegistrationNum" runat="server" CssClass="input4 validate[maxSize[50]]" MaxLength="50" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>联系方式
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_Contact" runat="server" CssClass="input4 validate[custom[Telephone],maxSize[15]]" MaxLength="15" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>传真
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_Fax" runat="server" CssClass="input4 validate[custom[fax],maxSize[15]]" MaxLength="15" Style="width: 220px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>企业性质
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_Property" runat="server" CssClass="input4 validate[maxSize[50]]" MaxLength="50" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>营业执照注册号
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_LicenseNum" runat="server" CssClass="input4 validate[maxSize[50]]" MaxLength="50" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>成立日期
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_CLDate" runat="server" CssClass="input4" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})" Style="width: 220px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>有效期
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_ValidityPeriod" runat="server" CssClass="input4" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>注册资本
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_RegisteredCapital" runat="server" CssClass="input4 validate[custom[onlyNumber],maxSize[18]]" MaxLength="18" Style="width: 170px;"></asp:TextBox><span style="font-size: 13px;">（万元）</span>
                    </td>
                    <th>实收资本
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_RealCapital" runat="server" CssClass="input4 validate[custom[onlyNumber],maxSize[18]]" MaxLength="18" Style="width: 170px;"></asp:TextBox><span style="font-size: 13px;">（万元）</span>
                    </td>
                </tr>
                <tr>
                    <th>邮编
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_ZipCode" runat="server" CssClass="input4 validate[custom[chinaZip],maxSize[10]]" MaxLength="10" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>注册地址
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_RegAddress" runat="server" CssClass="input4 validate[maxSize[200]]" MaxLength="200" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>所属区域<font color="red">*</font>
                    </th>
                    <td>
                        <div style="width:250px;">
                        <asp:DropDownList ID="ddlDealersInfo_Area1" runat="server" CssClass="input4 validate[required]" Style="width: 80px;" OnTextChanged="ddlDealersInfo_Area1_TextChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlDealersInfo_Area2" runat="server" CssClass="input4 validate[required]" Style="width: 80px;" OnTextChanged="ddlDealersInfo_Area2_TextChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlDealersInfo_Area3" runat="server" CssClass="input4 validate[required]" Style="width: 80px;">
                        </asp:DropDownList>
                        </div>
                        <div style="width:250px;">
                        层级一<asp:DropDownList ID="ddlDealersInfo_HierarchyOne" runat="server" CssClass="input4" Style="width: 80px;">
                        </asp:DropDownList>
                        层级二<asp:DropDownList ID="ddlDealersInfo_HierarchyTwo" runat="server" CssClass="input4" Style="width: 80px;">
                        </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th>组织机构代码号
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_OranCode" runat="server" CssClass="input4 validate[maxSize[50]]" MaxLength="50" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>网址
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_Website" runat="server" CssClass="input4 validate[custom[url],maxSize[50]]" MaxLength="50" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>地址<font color="red">*</font>
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_Address" runat="server" CssClass="input4 validate[maxSize[200]]" MaxLength="200" Style="width: 220px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>经营范围
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_ScopeBusiness" runat="server" CssClass="input4 validate[maxSize[100]]" MaxLength="100" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>所属行业
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_BelongIndustry" runat="server" CssClass="input4 validate[maxSize[50]]" MaxLength="50" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>其他信息
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_OtherInfo" runat="server" CssClass="input4 validate[maxSize[100]]" MaxLength="100" Style="width: 220px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>档案协议编号
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_FileProtocol" runat="server" CssClass="input4 validate[maxSize[100]]" MaxLength="100" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>合作有效期
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_CooperativeValidity" runat="server" CssClass="input4" onclick="WdatePicker({dateFmt:'yyyy年MM月dd日'})" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>合作类型<font color="red">*</font>
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlDealersInfo_CooperativeType" runat="server" CssClass="input4 validate[required]" Style="width: 150px;">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>返佣比例
                    </th>
                    <td>
                        <asp:TextBox ID="txtDealersInfo_ReturnRatio" runat="server" CssClass="input4 validate[maxSize[10]]" MaxLength="10" Style="width: 220px;"></asp:TextBox>
                    </td>
                    <th>产品
                    </th>
                    <td colspan="3">
                        <select id="ddlDealersInfo_Product" class="easyui-combobox" validtype="ComBoBox">
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>附件</th>
                    <td colspan="5">
                        <UC:File ID="File" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>品牌
                    </th>
                    <td>
                        <%-- <table style="width: 100%;">
                            <tr>
                                <td  style="width:20%;">--%>
                        <select id="ddlVehicle_BrandID" class="easyui-combobox"></select>
                        <a id="linkSave" href="javascript:void(0)" onclick="AddBrand()" class="easyui-linkbutton">添加</a>
                        <a id="linkClear" href="javascript:void(0)" onclick="ClearBrand()" class="easyui-linkbutton">清空</a>
                        <%--</td>
                                <td id="td_Brand" style="width:80%;">
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                    <td colspan="4" id="td_Brand"></td>
                </tr>
            </table>
            </div>
            <table width="1050" border="0" cellspacing="0" cellpadding="0" class="for">
                <tr>
                    <th>
                        <asp:ImageButton ID="btn_Save" runat="server" ImageUrl="~/Images/buttons/baocun.gif"
                            OnClick="btnAdd_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp; <a id="FanHui" href="DealersDealersInfoList.aspx">
                        <asp:Image ID="btn_FH" runat="server" ImageUrl="~/Images/buttons/fanHui.gif" /></a>
                    </th>
                </tr>
            </table>
        </center>
    </form>
</body>
</html>
