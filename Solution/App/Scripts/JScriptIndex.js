function dateConvert(value) {
    var reg = new RegExp('/', 'g');
    var d = eval('new ' + value.replace(reg, ''));
    return new Date(d).format('yyyy-MM-dd')
}
function returnParent(value) {//获取子窗体返回值
    var parent = window.dialogArguments; //获取父页面
    //parent.location.reload(); //刷新父页面
    if (parent != null && parent != "undefined") {
        window.returnValue = value; //返回值
        window.close(); //关闭子页面
    }
    return;
}
$(function () {
    //时间格式化
    Date.prototype.format = function (format) {
        /*
        * eg:format="yyyy-MM-dd hh:mm:ss";
        */
        if (!format) {
            format = "yyyy-MM-dd hh:mm:ss";
        }

        var o = {
            "M+": this.getMonth() + 1, // month
            "d+": this.getDate(), // day
            "h+": this.getHours(), // hour
            "m+": this.getMinutes(), // minute
            "s+": this.getSeconds(), // second
            "q+": Math.floor((this.getMonth() + 3) / 3), // quarter
            "S": this.getMilliseconds()
            // millisecond
        };

        if (/(y+)/.test(format)) {
            format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }

        for (var k in o) {
            if (new RegExp("(" + k + ")").test(format)) {
                format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
        return format;
    };
    $.extend($.fn.datagrid.methods, {

        addToolbarItem: function (jq, items) {

            return jq.each(function () {

                var toolbar = $(this).parent().prev("div.datagrid-toolbar");

                for (var i = 0; i < items.length; i++) {

                    var item = items[i];
                    var btn = $("<a href=\"javascript:void(0)\"></a>");
                    btn[0].onclick = eval(item.handler || function () { });
                    btn.css("float", "left").appendTo(toolbar).linkbutton($.extend({}, item, { plain: true }));
                }
                toolbar = null;

            });

        },

        removeToolbarItem: function (jq, param) {

            return jq.each(function () {
                var btns = $(this).parent().prev("div.datagrid-toolbar").children("a");
                var cbtn = null;
                if (typeof param == "number") {
                    cbtn = btns.eq(param);
                } else if (typeof param == "string") {

                    var text = null;
                    btns.each(function () {
                        text = $(this).data().linkbutton.options.text;
                        if (text == param) {
                            cbtn = $(this);
                            text = null;
                            return;
                        }
                    });
                }

                if (cbtn) {
                    var prev = cbtn.prev()[0];
                    var next = cbtn.next()[0];
                    if (prev && next && prev.nodeName == "DIV" && prev.nodeName == next.nodeName) {
                        $(prev).remove();
                    } else if (next && next.nodeName == "DIV") {
                        $(next).remove();
                    } else if (prev && prev.nodeName == "DIV") {
                        $(prev).remove();
                    }
                    cbtn.remove();
                    cbtn = null;
                }
            });
        }
    });
})