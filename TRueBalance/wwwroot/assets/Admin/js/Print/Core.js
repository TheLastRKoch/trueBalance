$(document).ready(function () {
    const SaleInfo = '{"invoiceID":1,"date":"25/05/2019","vendor":"Sergio","clientName":"Sergio","products":[{"name":"Acom - papas francesas","price":1000},{"name":"Acom - doraditas","price":700},{"name":"Jugo de naranja","price":600}],"paymentType":"Efectivo","totalToPay":2300,"cash":2500,"clientChange":0}';
    const BusinessInfo = '{"organizationName":"PLACE - Comidas rapidas","legalPerson":"SARA ZUNIGA MURILLO","iDLegalPerson":"1-1650-0540","phoneNumber":"TEL: 4034-0895","address":"Residencial hacienda del rey 48J","legalInformation":"Regimen Simplificado","codeInfo":"Codigo 552004","slogan":"Un lugar para ser feliz","sendoff":"- Muchas gracias por su compra -"}';

    function SetCurrentInvoiceID(value) {
        this.CurrentInvoiceID = value;
    }

    function RenderData(SaleInfo) {
        var SaleJson = SaleInfo;
        var BusinessJson = JSON.parse(BusinessInfo);

        RenderHeader(BusinessJson);
        RenderBody(SaleJson);
        RenderProductList(SaleJson);
        RenderPaymentInfo(SaleJson);
        RenderFooter(BusinessJson);
    }

    function core() {
        $.ajax({
            type: 'get',
            url: '/Invoice/GetJson/' + CurrentInvoiceID,
            async: false,
            success: function (response) {
                RenderData(response);
                window.print();
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    core();
});