/**
 * Require: lib/Jquery
 * Require: vendor/html2canvas.js
 * Require: vendor/FileSaver.js
 * Require: utils/TimeManagment.js
 */

$(document).ready(function () {
    $("#btnScreenShot").click(function () {
        TakeScreenShoot(GetDateWithFormat("-") + ".png");
    });

    function TakeScreenShoot(FileName) {
        html2canvas(document.body).then(function (canvas) {
            canvas.toBlob(function (blob) {
                saveAs(blob, FileName);
            });
        });
    }
});