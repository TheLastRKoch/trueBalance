/**
 * @author <RKoch3196> [<TheLastRKochgmail.com>]
 */

/*
global $
*/

/*
 * This method change give a preview of the User picture
 */

$(document).ready(function () {
    $('#txtImgURL').on('input change keyup paste', function () {
        var CustomSource;
        if (/^(http|https|ftp):\/\/[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$/i.test($(this).val())) {
            CustomSource = $(this).val();
        }
        else {
            CustomSource = 'https://www.gassho-hemp.com/wp-content/uploads/2018/02/default-user-image-300x300.x76424.png';
        }
        $('#ProfileImg').attr('src', CustomSource)
    });
});

