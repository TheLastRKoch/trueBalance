/**
 * @author <TheLastRKoch> [<TheLastRKoch@gmail.com>]
 */

/*
global $
*/

$(document).ready(function () {

    //Class variables & Utils
    //#region
    var SelectedProducts = [];
    var MealsNameList = [];
    var MealsIDList = [];
    var Index = 0;
    var Windows;
    var i = 1;

    function PrintInvoice(InvoiceID) {
        var PrintWindow = window.open("/Invoice/Print/" + InvoiceID);
    }

    function GenerateSellForTest() {
        return {
            Hour: '22:22',
            Cash: '1000',
            ClientName: 'Sergio',
            MealsIdList: [1, 2, 3],
            MealsNameList: ["Huevo", "Pan", "Papas"],
            Observations: "You win",
            PaymentType: "Efectivo",
            TakeawayType: "ComerAqui"
        };
    }

    function isEmptyOrSpaces(str) {
        return str === null || str.match(/^ *$/) !== null;
    }

    function OpenOrderScreen() {
        Windows = window.open('/Order/Screen');
    }

    function CleanScreen() {
        //Clean the class lists
        SelectedProducts = [];
        MealsIDList = [];
        MealsNameList = [];

        //Clean the inputs
        $("#txtClientName").val("");
        $("#txtClientName").prop("disabled", false);
        $("#Amount").html("₡0");
        $("#txtChange").html("₡0");
        $("#txtDescription").val("");
        $("#txtPayment").val("0");
        $("#txtPayment").prop("disabled", false);

        //Put radios to default
        $("input[name=PaymentType][value=Efectivo]").prop('checked', true);
        $("input[name=TakeawayType][value=Llevar]").prop('checked', true);

        //Clean the checks in the product table
        $("#datatable-buttons > tbody > tr > td > label > input[type=checkbox]").removeAttr("checked");
        $("#datatable-buttons > tbody > tr > td > input[type=number]").prop('disabled', true);
        $("#datatable-buttons > tbody > tr > td > input[type=number]").val("1");

        //Clean the checks in the AmountTable
        $("#AmountTable > thead > .CustomRow").remove();

        CleanSeachBox();

        $("#txtClientName").focus();

        //Scroll to top
        $('html,body').scrollTop(0);
    }

    $("#txtClientName").focus();
    //#endregion

    //Gets & Sets
    //#region	
    function GetMealsNameItems() {
        $('#AmountTable > thead  > tr > .ProductListItem').each(function () {
            MealsNameList.push($(this).html());
        });
    }

    function GetOrderType() {
        return $('input[name=TakeawayType]:checked').val();
    }

    function GetHour() {
        d = new Date();
        utc = d.getTime() + d.getTimezoneOffset() * 60000;
        nd = new Date(utc + 3600000 * '-6');
        return nd.getHours() + ":" + nd.getMinutes();
    }

    function GetTypeOfPayment() {
        return $('input[name=PaymentType]:checked').val();
    }

    function GetOrderID() {
        var ID = 0;
        $.ajax({
            type: 'get',
            url: '/order/GetLastID',
            async: false,
            success: function (response) {
                ID = response;
            },
            error: function (error) {
                console.log(error);
            }
        });
        return ID;
    }

    //Get all the data from the Current Sell
    function GetSellData() {
        //Fetch the name of the products from the Amount Table 
        GetMealsNameItems();
        return {
            Hour: GetHour(),
            Cash: $("#txtPayment").val(),
            ClientName: $("#txtClientName").val(),
            MealsIDList: SelectedProducts,
            MealsNameList: MealsNameList,
            Observations: $("#txtDescription").val(),
            PaymentType: GetTypeOfPayment(),
            TakeawayType: GetOrderType()
        };
    }
    //#endregion

    //OrderControl
    //#region    

    //Auto start the OrderScreen
    OpenOrderScreen();

    function AddNewOrder(SellObject) {
        //Get the template code
        Template = Windows.$('#base').html();
        //implant the template code
        Windows.$('#frmOrders').append("<div id='New' class=\"col-xl-4 col-md-6 col-xs-12\">" + Template + "<\/div>");
        //Add the border
        if (SellObject.TakeawayType === "Llevar") {
            Windows.$('#New #divBorder').addClass("card-danger");
        }
        //Add the client name
        Windows.$('#New #spcClient').html(SellObject.ClientName);
        //Add the time created
        Windows.$('#New #spcHour').html(SellObject.Hour);
        //Add the products from a array
        for (j = 0; j < SellObject.MealsNameList.length; j++) {
            Windows.$('#New #spcProducts').append("<li class=\"list-group-item\">" + SellObject.MealsNameList[j] + "</li>\n");
        }
        if (isEmptyOrSpaces(SellObject.Observations)) {
            Windows.$('#New #spcDescription').remove();
        } else {
            //Add the description
            Windows.$('#New #spcDescription').val(SellObject.Observations);
        }
        //Add the buttom
        Windows.$('#New button').attr('name', GetOrderID());
        Windows.$('#New button').attr('onclick', "DeliveryOrder(" + GetOrderID() + ")");
        //Delete the New id to evit conflicts
        Windows.$('#New #divProduct').css("width", "");
        Windows.$("#New").removeAttr('id');
        i++;
    }

    $('#maker').click(function () {
        AddOrderToScreen();
    });

    $('#btnOpenOrder').click(function () {
        OpenOrderScreen();
    });

    //Close the OrderScreen if MakeSale closes
    $(window).unload(function () {
        Windows.close();
    });

    //#endregion

    //Sell Control
    //#region


    /**
     * 
     * General functions
     *
     */

    function HideShowCleanButton(state) {
        if (state === true) {
            $("#btnClear").show();
        } else {
            $("#btnClear").hide();
        }
    }

    /**
     * Try to keep the tables with the same height
     **/
    function ChangeDivTableHeight() {
        var divAmountTableHeight = $("#divAmountTable").height();
        if (divAmountTableHeight < 400) {
            $("#divSellTable").height(400);
        } else {
            var size = $("#divAmountTable").height();
            $("#divSellTable").height(size - 120);
        }
    }

    /**
     * Make the search of the product with the search bar
     */
    function MakeTheSearch() {
        // Declare variables 
        var input, filter, table, tr, td, i;
        input = document.getElementById("txtProductSearch");
        filter = input.value.toUpperCase();
        table = document.getElementById("datatable-buttons");
        tr = table.getElementsByTagName("tr");


        // Loop through all table rows, and hide those who don't match the search query
        for (i = 0; i < tr.length; i++) {
            td = tr[i].getElementsByTagName("td")[0];
            if (td) {
                if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                } else {
                    tr[i].style.display = "none";
                }
            }
        }
    }

    //this function search if the meal is in the list
    function SearchItem(chSelected) {
        for (var i = 0; i < SelectedProducts.length; i++) {
            if (SelectedProducts[i] === chSelected.id) {
                Index = i;
                return true;
            }
        }
    }

    //this function add rows to the table
    function AddToTable(chSelected) {

        var Table = document.getElementById("AmountTable");
        var rowCount = $('#AmountTable tr').length;

        var row = Table.insertRow(rowCount);
        row.setAttribute("id", "row" + chSelected.id);
        row.setAttribute("class", "CustomRow");
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        var cell4 = row.insertCell(3);
        cell1.innerHTML = 1;
        cell2.innerHTML = chSelected.text;
        cell2.setAttribute("class", "ProductListItem");
        cell3.innerHTML = chSelected.value;

        var link = document.createElement("a");
        link.setAttribute("id", chSelected.id);
        link.className = "btn btn-danger";
        var linkText = document.createTextNode("X");
        link.appendChild(linkText);
        cell4.appendChild(link);
    }

    //this function remove the rows of the table
    function RemoveToTable(id) {
        var Myid = "#row" + id;
        $(Myid).remove();
    }

    function RemoveFromQuantity(id) {
        var Myid = "#Quantity" + id;
        var total = parseInt($(Myid).val());
        if (total > 0) {
            total = total - 1;
        }
        $(Myid).val(total);
    }

    //this function are the core of the script, add or delete the meals to the list
    function AddDeleteProductOfList(chSelected) {
        if (SearchItem(chSelected) === true) {
            for (var i = 0; i < chSelected.quantity; i++) {
                SelectedProducts.splice(Index, 1);
                MealsIDList.splice(Index, 1);
                RemoveToTable(chSelected.id);
                EnableQuatityInput(chSelected.id, false);
            }
        } else {
            for (var j = 0; j < chSelected.quantity; j++) {
                SelectedProducts.push(chSelected.id);
                MealsIDList.push(chSelected.value);
                AddToTable(chSelected);
                EnableQuatityInput(chSelected.id, true);
            }
        }
        ChangeDivTableHeight();
    }

    //this function calculate the total amount
    function CalculateAmount() {
        var TotalAmount = 0;
        for (var i = 0; i < MealsIDList.length; i++) {
            TotalAmount = parseInt(TotalAmount) + parseInt(MealsIDList[i]);
        }
        $('#Amount').text("₡" + TotalAmount);
    }

    //this function enable / disable the input quantity 
    function EnableQuatityInput(id, state) {
        var Myid = "#Quantity" + id;
        var Cant = $(Myid).val();
        if (state === true) {
            $(Myid).val("1");
            $(Myid).removeAttr("disabled");
        } else {
            if (Cant === "0") {
                $(Myid).val("1");
                $(Myid).attr("disabled", "disabled");
            }
        }
    }

    //this funcion clear the check on deleted meal
    function UncheckMeal(id) {
        var MyID = "#Quantity" + id;
        var Cant = $(MyID).val();
        if (Cant === "0") {
            var Myid = "#" + id;
            $(Myid).attr('checked', false);
            $(Myid).attr("alt", "1");
        }
    }

    //This function get all the information of meal in the HTML
    function GetMealData(checkboxId) {
        var Myid = "#" + checkboxId;
        var chMeal = $(Myid);
        var MealId = chMeal.attr("id");
        var MealName = chMeal.attr("text");
        var MealPrice = chMeal.attr("value");
        var MealQuantity = chMeal.attr("alt");

        if (MealQuantity === undefined) {
            MealQuantity = 1;
        }

        var chSelected = {
            id: MealId,
            value: MealPrice,
            text: MealName,
            quantity: MealQuantity
        };
        return chSelected;
    }

    function ShowSellButon(state) {
        if (state === true) {

            $("#btnSell").removeAttr("disabled");
        } else {
            $("#btnSell").attr("disabled", "disabled");
        }
    }

    function ManageSellButon() {
        var rowCount = $('#AmountTable tr').length;
        if (rowCount > 1) {
            ShowSellButon(true);
        } else {
            ShowSellButon(false);
        }
    }

    //This function save the sell to DB
    function SaveTheSellToDB(SellObject) {
        console.log(SellObject);
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: '/Sell/MakeSale',
            dataType: "json",
            data: JSON.stringify(SellObject),
            async: false,
            success: function (response) {

            },
            error: function (error) {
                console.log(error);
            }
        });
        //Clean the lists and recover all the elements to make a new sell
        CleanScreen();

        //Show the Message
        AddMessageDiv("Success", "Perfecto!!", "Listo todo bueno todo correcto");
    }

    function CleanSeachBox() {
        document.getElementById("txtProductSearch").value = "";
        HideShowCleanButton(false);
        MakeTheSearch();
    }

    /**
     * 
     * Triggers
     *
     */

    $("#btnTest").click(function () {

        let SellObj = GenerateSellForTest();

        console.log(SellObj);

        //SaveTheSellToDB(SellObj);

    });

    /**
     * Event when write to search the product show the clean button and make a search
     */
    $("#txtProductSearch").keydown(function () {
        HideShowCleanButton(true);
        MakeTheSearch();
    });

    /**
     * Event when click on the clean button clean the seach bar, the table and hide the button
     */
    $('#btnClear').click(function () {
        CleanSeachBox();
    });

    $('input[type=radio][name=PaymentType]').change(function () {
        if (this.value === 'Tarjeta') {
            $("#txtPayment").attr("disabled", "disabled");
        } else {
            $("#txtPayment").removeAttr("disabled");
        }
    });

    $(document).change(function () {
        ManageSellButon();
    });

    //This function control the multiple meals adding
    $(document).on("input", "input", function () {
        var ElementType = $(this).attr("type");
        if (ElementType === "number") {
            //get the txtQuantity data
            var MealQuantity = $(this).val();
            var ElementId = $(this).attr("id");
            var MealId = ElementId.substr(8, ElementId.length - 1);

            //get the Meal data
            var MyId = "#" + MealId;
            var chSelected = GetMealData(MealId);
            AddDeleteProductOfList(chSelected);
            chSelected.quantity = MealQuantity;
            $(MyId).attr("alt", MealQuantity);
            AddDeleteProductOfList(chSelected);
            $(this).val(MealQuantity);
            CalculateAmount();
        }
    });

    //This function contol the delete by buton "X"
    $(document).on("click", 'a', function () {
        var IdElement = $(this).attr('id');
        var CurrentIndex = SelectedProducts.findIndex(x => x === IdElement);
        RemoveFromQuantity(IdElement);
        RemoveToTable(IdElement);
        SelectedProducts.splice(CurrentIndex, 1);
        MealsIDList.splice(CurrentIndex, 1);
        CalculateAmount();
        UncheckMeal(IdElement);
        EnableQuatityInput(IdElement, false);
        ManageSellButon();
    });

    //This function control the adding by checkbox
    $(".form-check-input").click(function () {
        var chSelected = GetMealData($(this).attr('id'));
        AddDeleteProductOfList(chSelected);
        CalculateAmount();
    });

    $("#txtPayment").on('input', function () {
        var Total = 0;
        var Amount = $("#Amount").text();
        Amount = Amount.substring(1, Amount.length);
        Amount = parseInt(Amount);
        var Cash = $(this).val();
        Cash = parseInt(Cash);
        Total = Cash - Amount;
        if (Total > 0) {

            $("#txtChange").text("₡" + Total);
        } else {
            $("#txtChange").text("₡" + 0);
        }
    });

    $('#txtClientName').on('input', function (e) {
        if (Windows === undefined) {
            AddMessageDiv("Warning", "Cuaidado!!", "La pagina de ordenes no ha sido abierta");
        }
    });

    $('#btnSell').click(function () {

        //Get the data from the current sell
        var SellData = GetSellData();

        console.log(SellData);

        //Disable the submit button to evit multiple request
        $(this).find("button[type='submit']").prop('disabled', true);

        //Save the mealList to Glo.MealsList
        SaveTheSellToDB(SellData);

        //Add the order to the order screen
        AddNewOrder(SellData);

        //Print Invoice
        PrintInvoice(GetOrderID());

    });
    //#endregion

});