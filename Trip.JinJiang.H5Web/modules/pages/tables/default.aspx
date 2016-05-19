<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Trip.JinJiang.H5Web.modules.pages.tables._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ajax图片上传</title>
    <%--<script src="js/jquery-1.3.2.min.js" type="text/javascript"></script>--%>
    <script src="http://libs.baidu.com/jquery/1.3.2/jquery.min.js" type="text/javascript"></script>
    <script src="../../js/jquery-form.js" type="text/javascript"></script>

    <script type="text/javascript">        
        function upload() {
        var path = document.getElementById("File1").value;  
        var img = document.getElementById("img1");  
        if($.trim(path)==""){  
            alert("请选择要上传的文件");  
            return;  
            }  
              
        $("#form1").ajaxSubmit({  
            success: function (str) {
                if(str!=null && str!="undefined"){  
                    if (str == "1") {alert("上传成功");document.getElementById("img1").src="images/logo.jpg?"+new Date();/*上传后刷新图片*/}  
                    else if(str=="2"){alert("只能上传jpg或png格式的图片");}  
                    else if(str=="3"){alert("图片不能大于1M");}  
                    else if(str=="4"){alert("请选择要上传的文件");}  
                    else {alert('操作失败！');}  
                }  
                else alert('操作失败！');  
            },  
            error: function (error) {alert(error);},  
            url:'Handler2.ashx', /*设置post提交到的页面*/  
            type: "post", /*设置表单以post方法提交*/  
            dataType: "text" /*设置返回值类型为文本*/  
        });  
    }        
    </script>
</head>
<body>
    <form id="form1">
        <input id="File1" name="File1" type="file" />
        <input id="iptUp" type="button" value="上传" onclick="upload()" />
    </form>
</body>
</html>
