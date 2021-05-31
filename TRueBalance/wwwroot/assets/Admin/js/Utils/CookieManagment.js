/**
 * @description File to manipulate Cookies
 * @author TheLastRKoch
 * @date 02/05/19
 */


function AddCookie(cname, cvalue, hours) {
    var d = new Date();
    d.setHours(d.getHours() + hours);
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}


function GetCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function RemoveCookie(cname) {
    var d = new Date();
    var Time = d.setDate(d.getDate() - 10);
    var expires = 'expires=' + d.toUTCString();
    document.cookie = cname + '=; ' + expires + ";path=/";
}