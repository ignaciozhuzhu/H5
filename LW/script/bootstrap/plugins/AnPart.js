var AnPart = new Object();
AnPart.alert = function (Type, Str, InsertID, IsHide,t) {
    var classname = 'alert-info';
    var infoname = '温馨提示';
    switch (Type) {
        case 1:
            infoname = '通过';
            classname = 'alert-success';
            break;
        case 2:
            infoname = '温馨提示';
            classname = 'alert-info';
            break;
        case 3:
            infoname = '警告';
            classname = 'alert-warning';
            break;
        case 4:
            infoname = '错误';
            classname = 'alert-danger';
            break;
    }
    var hidebtn = "";
    var iidcc1 = Math.random().toString().substring(3, 8);
    if (IsHide == 1) {
        hidebtn = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\"><span aria-hidden=\"true\">&times;</span><span class=\"sr-only\">Close</span></button>";
    }
    var s = "<div class=\"alert " + classname + " alert-dismissible\" role=\"alert\"  id='" + iidcc1 + "'> " + hidebtn + "  <strong>" + infoname + "：</strong>" + Str + "</div>";
    $(InsertID).children().hide("slow", function () {
        //$(InsertID).empty();
    });
    $(InsertID).append(s);
    if (t != 0) {
        setTimeout('$(\"#'+ iidcc1+'\").hide(\"slow\")', t);
    }

}