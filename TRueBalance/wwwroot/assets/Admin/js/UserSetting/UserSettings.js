/**
 * @description Manage the triggers and the functions of the user settings page
 * @author TheLastRKoch
 * @date 03/05/19
 * @dependecies CookieManagment.js, SwitchManagment.js
 */

$(document).ready(function () {

    var i = 0;

    function SetCashBoxClosing() {
        if (GetCookie("CashBoxClosingFeature") === "") {
            SetSwitchState("CashBoxClosingFeature", false);
        }
        else {
            SetSwitchState("CashBoxClosingFeature", true);
            i = 1;
        }
    }

    $("#CashBoxClosing > span").click(function () {
        clickCheckbox = document.querySelector('#CashBoxClosingFeature');
        if (i >= 1) {
            if (clickCheckbox.checked) {
                AddCookie("CashBoxClosingFeature", "Closed", 8);
            }
            else {
                RemoveCookie("CashBoxClosingFeature");
            }
        }
        i++;
    });

    SetCashBoxClosing();
});