$("#AStatics").click(function () {
    if (GetCookie("CashBoxClosingFeature") === "") {
        $('#myModal').modal({
            show: true
        });
    } else {
        window.location.href = '/Overview/Summary';
    }
});

$('#MakeCashBoxClosingFeature').click(function () {
    window.location.href = '/Overview/ActiveteCashBoxClosingFeature';
});