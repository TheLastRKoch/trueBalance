//The size of the paper in mm
const PaperSize = 32;
//The char that separate the titles with the data
const Spacer = ".";

function RemoveElement(id) {
    var element = document.getElementById(id);
    element.parentNode.removeChild(element);
}

function RenderHeader(obj) {
    AppendGap();
    AppendCenter("pOrganizationName", obj.organizationName);
    AppendCenter("pLegalPerson", obj.legalPerson);
    AppendCenter("pIDLegalPerson", obj.iDLegalPerson);
    AppendCenter("pPhoneNumber", obj.phoneNumber);
    AppendCenter("pAddress", obj.address);
}

function RenderBody(obj) {
    AppendSpacer("pInvoiceID", obj.invoiceID);
    AppendSpacer("pDate", obj.date);
    AppendSpacer("pVendor", obj.vendor);
    AppendSpacer("pClientName", obj.clientName);
}

function RenderProductList(obj) {
    AppendLeft("pProductsName", "Precio");

    for (var i = 0; i < obj.products.length; i++) {
        var divProducts = document.getElementById("divProducts");
        var CurrentElement = document.createElement("P");
        CurrentElement.setAttribute("id", "Product" + i);
        CurrentElement.innerHTML = obj.products[i].name.toString();
        divProducts.appendChild(CurrentElement);
        AppendSpacer("Product" + i, obj.products[i].price.toString());
    }
}

function RenderPaymentInfo(obj) {
    AppendSpacer("pPaymentType", obj.paymentType);
    AppendSpacer("pTotalToPay", obj.totalToPay);
    if (obj.paymentType !== "Tarjeta") {
        AppendSpacer("pCash", obj.cash);
        AppendSpacer("pClientChange", obj.clientChange);
    }
    else {
        RemoveElement("pCash");
        RemoveElement("pClientChange");
    }
}

function RenderFooter(obj) {
    AppendCenter("plegalInformation", obj.legalInformation);
    AppendCenter("pCodeInfo", obj.codeInfo);
    AppendCenter("pSendoff", obj.sendoff);
}

// Add the infomation to the p element.
function AppendLeft(id, Text) {
    var SpacerSize = PaperSize - (Text.toString().length + document.getElementById(id).innerHTML.length);
    var pElement = document.getElementById(id);
    pElement.appendChild(document.createTextNode(FillGaps(Math.floor(SpacerSize/4)+2, "‌‌ ")));
    pElement.appendChild(document.createTextNode(Text));
}

function AppendCenter(id, Text) {
    SpacerSize = 0;
    if (Text.length < PaperSize) {
        var SpacerSize = (PaperSize - Text.length) / 2;
    }
    var pElement = document.getElementById(id);
    pElement.appendChild(document.createTextNode(FillGaps(Math.floor(SpacerSize/3), "‌‌ ")));
    pElement.appendChild(document.createTextNode(Text));
    pElement.appendChild(document.createTextNode(FillGaps(Math.floor(SpacerSize / 3), "‌‌ ")));
}

function AppendRight(id, Text) {
    var pElement = document.getElementById(id);
    pElement.appendChild(document.createTextNode(Text));
}

function AppendGap() {
    var pElement = document.getElementById("pGap");
    pElement.appendChild(document.createTextNode(".............................."));
}

//Add the infomation to the p element with separator.
function AppendSpacer(id, Text) {
    var SpacerSize = PaperSize - (Text.toString().length + document.getElementById(id).innerHTML.length);
    var pElement = document.getElementById(id);
    pElement.appendChild(document.createTextNode(FillGaps(SpacerSize, Spacer)));
    pElement.appendChild(document.createTextNode(Text));
}

function PrintGuide() {
    var SpacerLine = "";
    for (var i = 0; i < PaperSize; i++) {
        SpacerLine += Spacer;
    }
    return SpacerLine;
}