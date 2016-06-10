





//using(['messager', 'window', 'accordion', 'tabs'],
//	function tabClose() {
//	    //	    alert("aaaaaaaaaaaa");
//var o="客户资料";
//	    $('#tabs').tabs('close', o);   
//	});
function tabClose() {
var subtitle = $(".tabs-inner", parent.document).children(".tabs-closable").text();
//    alert(o);
    easyloader.locale = "zh_CN"; 
    using(['messager', 'window', 'accordion', 'tabs'],
    	function tabClose() {
    	    alert(subtitle);
    	    $('#tabs').tabs('close', subtitle);     
    	});
   
}