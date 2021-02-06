
var NotifyDanger = (content) => {
    $.notify(content, { color: "#fff", background: "#D44950", delay: 5000, align: "right", verticalAlign: "top" });
}


var NotifySuccess = (content) => {
    $.notify(content, { color: "#fff", background: "#20D67B", align: "right", verticalAlign: "top" });
}


var NotifyWarning = (content) => {
    $.notify(content, { color: "#fff", background: "#d45800", delay: 5000, align: "right", verticalAlign: "top" });
}


//Loading bar
var ShowLoadingBar = function () {
    NProgress.inc();
};

var HideLoadingBar = function () {
    setTimeout(function () {
        NProgress.done();
    }, 1500);
}

var FadeLoadingBar = function () {
    ShowLoadingBar();
    HideLoadingBar();
}
