/**
 * @description Manage the Modal that show the Make sale alert
 * @author TheLastRKoch
 * @date 05/05/19
 */

$("#MakeSaleAL").click(function () {

    if (GetCookie("CashBoxClosingFeature") != "Closed") {
        window.location = "/Sell/MakeSale";
    }
    else {
        $('#CashBoxClosing_MakeSale').modal({
            show: true
        }); 
    }
});