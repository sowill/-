/* File Created: 十月 24, 2012 */
(function ($) {

    $.fn.val2 = $.fn.val;

    $.fn.emptyValue = function (arg) {
        alert( $(this));
        this.each(function () {
            var input = $(this);
            var options = arg;
            if (typeof options == "string") {
                options = { empty: options }
            }
            options = jQuery.extend({
                empty: input.attr("data-empty") || "",
                className: "gray"
            }, options);
            input.attr("data-empty", options.empty);
            return input.focus(function () {
                $(this).removeClass(options.className);
                if ($(this).val2() == options.empty) {
                    $(this).val2("");
                }
            }).blur(function () {
                if ($(this).val2() == "") {
                    $(this).val2(options.empty);
                }
                $(this).addClass(options.className);
            }).blur();
        });
    };
    //重写jquery val方法，增加data-empty过滤
    $.fn.val = function () {
        alert("bbbbb");
        var value = $(this).val2.apply(this, arguments);
        var empty = $(this).attr("data-empty");
        if (typeof empty != "undefined" && empty == value) {
            value = "";
        }
        return value;
    };
})(jQuery);