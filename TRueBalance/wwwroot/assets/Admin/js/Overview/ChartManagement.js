﻿/*
 * @author: RKoch3196 : github.com/rkoch3196
 */

$(document).ready(function () {

    function GetProductCategoryNames() {

        var CategoryNames = [];

        $.ajax({
            type: "Get",
            url: "/Overview/GetProductCategoryNames",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (result) {
                CategoryNames = result;
            }
        });

        return CategoryNames;
    }

    function GetCountPerCategry() {

        var CountPerCategry = [];

        $.ajax({
            type: "Get",
            url: "/Overview/GetCountPerCategry",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (result) {
                console.log(result);
                CountPerCategry = result;
            }
        });

        return CountPerCategry;
    }

    function GetPaymentTypeCategoryNames() {

        var CategoryNames = [];

        $.ajax({
            type: "Get",
            url: "/Overview/GetPaymentTypeCategoryNames",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (result) {
                CategoryNames = result;
            }
        });

        return CategoryNames;
    }

    function GetCountPerPaymentType() {

        var CountPerCategry = [];

        $.ajax({
            type: "Get",
            url: "/Overview/GetCountPerPaymentType",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (result) {
                console.log(result);
                CountPerCategry = result;
            }
        });

        return CountPerCategry;
    }

    /*
     * Simple function to generate the colors for the chats in a dynamic way 
     */
    function GenerateColors(ColorsRGB, Array) {
        var ColorsArray = [];
        var i = 0;
        while (i < Array.length / 2) {
            for (var j = 0; j < ColorsRGB.length; j++) {
                ColorsArray.push(ColorsRGB[j]);
            }
            i++;
        }
        console.log(ColorsArray);
        return ColorsArray;
    }

    /*
     *  Make a genereric implementation and initialization of the library charts JS
     */
    function InitializeChart(CanvasID, Type, Metrics, Names, Values) {
        let myChart = document.getElementById(CanvasID).getContext('2d');

        //Configuration
        myChart.canvas.width = Metrics[0];
        myChart.canvas.height = Metrics[1];

        // Global Options
        Chart.defaults.global.defaultFontColor = '#777';
        var LegendStatus = false;

        if (Type !== 'bar') {
            LegendStatus = true;
        }

        let massPopChart = new Chart(myChart, {
            type: Type,
            data: {
                labels: Names,
                datasets: [{
                    label: '',
                    data: Values,
                    backgroundColor: GenerateColors(['#1BB99A', '#3DB9DC'], Names),
                    borderWidth: 3,
                    borderColor: '#F8F8F8',
                    hoverBorderWidth: 3,
                    hoverhoverBorderColor: '#F8F8F8'
                }]
            },
            options: {
                legend: {
                    display: LegendStatus,
                    position: 'bottom'
                }

            }
        });
    }

    function Constructor() {

        InitializeChart('ChartStasticsSells', 'bar', [250, 117], GetProductCategoryNames(), GetCountPerCategry());
        InitializeChart('ChartTypeOfSells', 'doughnut', [50, 50], GetPaymentTypeCategoryNames(), GetCountPerPaymentType());
    }

    Constructor();
});