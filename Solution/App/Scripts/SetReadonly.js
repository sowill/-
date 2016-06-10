/* File Created: 十月 24, 2012 */

function inputReadOnly() {
    $("input[class*=text-box]")
       .each(function () {
           $(this).attr("readonly", "readonly");
           $(this).attr("style", "color:#0078ad;font-weight:bold");
       });

    $("select")
       .each(function () {
           $(this).attr("disabled", "disabled");
           $(this).attr("style", "color:#0078ad;font-weight:bold");
       });

}