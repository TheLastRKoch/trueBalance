function GetDateWithFormat(Separator) {
    var DateNow = new Date();
    return DateNow.getDate() + Separator + (DateNow.getMonth() + 1) + Separator + DateNow.getFullYear();
}