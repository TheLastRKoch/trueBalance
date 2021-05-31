/**
 * @author <RKoch3196> [<sergio31.96@gmail.com>]
 */

/*
global $
*/

/**
 * Deletes the order from screen and save the state in DB
 * @ButtonName
 */
function DeliveryOrder(ButtonName) {
    var button = $('button[name=\'' + ButtonName + '\']');
    button.parentsUntil(".col-xl-4.col-md-6.col-xs-12").parent().remove();
    var model = { "OrderID": ButtonName };
    console.log(JSON.stringify(model));
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: '/Order/RemoveAddFromQueue',
        dataType: "json",
        data: JSON.stringify(model),
        async: false,
        success: function (response) {
            console.log(response);
        },
        error: function (error) {
            console.log(error);
        }
    });
}