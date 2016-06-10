function fast_click(WindowName, Url) {
 

    $("#" + WindowName).find("div iframe").attr("src", Url);
    var tWindow = $("#" + WindowName);
    tWindow.data('tWindow').center();
    tWindow.data('tWindow').open();
}