/**
 * @description Manage the swich skin from the checkboxes
 * @author TheLastRKoch
 * @date 05/05/19
 */

function SetSwitchState(FeatureName, value) {
    if (value !== GetSwitchState(FeatureName)) {
        $("#" + FeatureName + " > span").trigger('click');
    }
}

function GetSwitchState(FeatureName) {
    var LeftValue = $("#" + FeatureName + " > span >small").css("left")
    if (LeftValue === "20px") {
        return true
    }
    return false
}