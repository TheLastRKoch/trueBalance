    /**
     * Add a alertMessage to the screen with a custom message
     * @param {any} Type
     * @param {any} Message
     */
    function AddMessageDiv(Type,Title, Message) {
        var CustomClass = "";

        switch (Type) {
            case "Success":
                CustomClass = "alert-success"
                break;
            case "Error":
                CustomClass = "alert-danger"
                break;
            case "Warning":
                CustomClass = "alert-warning"
                break;
            default:
                CustomClass = "alert-info"
                break;
        }

        $(".wrapper > .container").prepend("<div id=\"divMessage\" class=\"alert " +
            CustomClass + " alert-dismissible fade in\" role=\"alert\">");
        $("#divMessage").append("<button id=\"btnMessage\" type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">");
        $("#btnMessage").append("<span aria-hidden=\"true\">×");
        $("#divMessage").append("<strong>" + Title +"</strong>  ");
        $("#divMessage").append(Message);
    }
