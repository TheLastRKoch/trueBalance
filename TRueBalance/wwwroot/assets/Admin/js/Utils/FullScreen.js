$("#btnFullScreen").click(function () {
    ActivateDesactivateFullScreen();
});

/*
*  Hide the nav to show as full screen
*/
function ActivateDesactivateFullScreen() {
    $("header").toggle();
    var x = document.getElementById("divWrapper");
    if (x.style.paddingTop === "" || x.style.paddingTop === "130px") {
        x.style.paddingTop = "0px";
    } else {
        x.style.paddingTop = "130px";
    }
}




