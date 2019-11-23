// Dashboard 1 Morris-chart
$(function () {
    "use strict";

    $("#FormCaseStats").submit(function(e) {
       // alert("graph load clicked");
        e.preventDefault();
    });


    $("#TestSubmit").click(function (e) {
        var startDate = $('#FormCaseStats').find('input[name="StartDate"]').val();
        var endDate = $('#FormCaseStats').find('input[name="EndDate"]').val();
        console.log("startdate " + startDate);
        console.log("enddate " + endDate);
        getStateStatsCountWithDates(startDate, endDate);
      
        e.preventDefault();
    });
  


    function getStateStatsCount() {
        $.ajax({
            //url: '/Setup/_GetStaffIDGenerator/area=Administrator',
            url: '/Agent/GetStateStats',
            type: 'GET',
            dataType: 'html',
            //data: $('#hostelAddForm').serialize(),
            beforeSend: function () {
                //Show the "busy" Gif:
                //d.style.display = "block";
                //checkTop = false;
            },
            complete: function () {
                // d.style.display = "none";
            },
            error: function (xhr) {
                //console.log(xhr.status);
                console.log(xhr.responseText);
                //alert("Oooops! something went wrong!!")
            },
            async:false,
            success: function (response) {
               /// console.log("Case State Stats Response");
                //console.log(JSON.parse(response));
                var yvals = [];
                var data = [];
                var colors = [];
                

                var parsedResponse = JSON.parse(response);
                var firstItem = parsedResponse[0];

                console.log(firstItem);

                var keys = Object.keys(firstItem);
                var IsSuccess = $.inArray("status", keys);
                console.log("IsSuccess", IsSuccess);
                if (IsSuccess == -1) {

                    var i;
                    for (i = 1; i < keys.length; i++) {
                        yvals.push(keys[i]);
                    }


                    var size = parsedResponse.length;
                    $.each(parsedResponse, function (i, value) {
                        if (i != (size - 1)) {
                            data.push(value);
                        }

                    });
                    console.log(data);
                    console.log(data);

                    //loop and populate colours
                    var coloursList = parsedResponse[size - 1];
                    console.log(coloursList);

                    $.each(coloursList, function (i, value) {

                        colors.push(value);

                    });
                    $("#morris-area-chart").empty();
                    AreaChart(yvals, data, colors);
                }
                else {

                    swal({
                        title: "",
                        text: "Range Does not contain any Data",
                        html: true,
                        type: status,
                        showCancelButton: false,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK",
                        closeOnConfirm: true
                    });
                }
            }
        });
    }


    function getStateStatsCountWithDates(startDate,endDate) {
        $.ajax({
            //url: '/Setup/_GetStaffIDGenerator/area=Administrator',
            url: `/Agent/GetStateStats?StartDate= ${startDate}&EndDate=${endDate}`,
            type: 'GET',
            dataType: 'html',
            //data: $('#hostelAddForm').serialize(),
            beforeSend: function () {
                //Show the "busy" Gif:
                //d.style.display = "block";
                //checkTop = false;
            },
            complete: function () {
                // d.style.display = "none";
            },
            error: function (xhr) {
                //console.log(xhr.status);
                console.log(xhr.responseText);
                //alert("Oooops! something went wrong!!")
            },
            async: false,
            success: function (response) {
                /// console.log("Case State Stats Response");
                //console.log(JSON.parse(response));

                var IsSuccess;
                var yvals = [];
                var data = [];
                var colors = [];


                var parsedResponse = JSON.parse(response);
                var firstItem = parsedResponse[0];

                console.log(firstItem);

                var keys = Object.keys(firstItem);
                var IsSuccess = $.inArray("status", keys);
                console.log("IsSuccess", IsSuccess);
                if (IsSuccess == -1) {

                    var i;
                    for (i = 1; i < keys.length; i++) {
                        yvals.push(keys[i]);
                    }


                    var size = parsedResponse.length;
                    $.each(parsedResponse, function (i, value) {
                        if (i != (size - 1)) {
                            data.push(value);
                        }

                    });
                    console.log(data);
                    console.log(data);

                    //loop and populate colours
                    var coloursList = parsedResponse[size - 1];
                    console.log(coloursList);

                    $.each(coloursList, function (i, value) {

                        colors.push(value);

                    });

                    $("#morris-area-chart").empty();
                    AreaChart(yvals, data, colors);
                }
                else {

                    swal({
                        title: "",
                        text: "Range Does not contain any Data",
                        html: true,
                        type: status,
                        showCancelButton: false,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "OK",
                        closeOnConfirm: true
                    });
                }
            }
        });
    }


    getStateStatsCount();

    function AreaChart(yValues,data,colors) {

        Morris.Area({
            element: 'morris-area-chart',
            data: data,
            xkey: 'period',
            ykeys: yValues,
            labels: yValues,
            pointSize: 3,
            fillOpacity: 0,
            pointStrokeColors: ['#55ce63', '#009efb', '#2f3d4a'],
            behaveLikeLine: true,
            gridLineColor: '#e0e0e0',
            lineWidth: 3,
            hideHover: 'auto',
            lineColors: colors,
            resize: true

        });
    }

//Morris.Area({
//        element: 'morris-area-chart2',
//        data: [{
//            period: '2010',
//            SiteA: 0,
//            SiteB: 0,
            
//        }, {
//            period: '2011',
//            SiteA: 130,
//            SiteB: 100,
            
//        }, {
//            period: '2012',
//            SiteA: 80,
//            SiteB: 60,
            
//        }, {
//            period: '2013',
//            SiteA: 70,
//            SiteB: 200,
            
//        }, {
//            period: '2014',
//            SiteA: 180,
//            SiteB: 150,
            
//        }, {
//            period: '2015',
//            SiteA: 105,
//            SiteB: 90,
            
//        },
//         {
//            period: '2016',
//            SiteA: 250,
//            SiteB: 150,
           
//        }],
//        xkey: 'period',
//        ykeys: ['SiteA', 'SiteB'],
//        labels: ['Site A', 'Site B'],
//        pointSize: 0,
//        fillOpacity: 0.4,
//        pointStrokeColors:['#b4becb', '#009efb'],
//        behaveLikeLine: true,
//        gridLineColor: '#e0e0e0',
//        lineWidth: 0,
//        smooth: false,
//        hideHover: 'auto',
//        lineColors: ['#b4becb', '#009efb'],
//        resize: true
        
//    });


//// LINE CHART
//        var line = new Morris.Line({
//          element: 'morris-line-chart',
//          resize: true,
//          data: [
//            {y: '2011 Q1', item1: 2666},
//            {y: '2011 Q2', item1: 2778},
//            {y: '2011 Q3', item1: 4912},
//            {y: '2011 Q4', item1: 3767},
//            {y: '2012 Q1', item1: 6810},
//            {y: '2012 Q2', item1: 5670},
//            {y: '2012 Q3', item1: 4820},
//            {y: '2012 Q4', item1: 15073},
//            {y: '2013 Q1', item1: 10687},
//            {y: '2013 Q2', item1: 8432}
//          ],
//          xkey: 'y',
//          ykeys: ['item1'],
//          labels: ['Item 1'],
//          gridLineColor: '#eef0f2',
//          lineColors: ['#009efb'],
//          lineWidth: 1,
//          hideHover: 'auto'
//        });
// // Morris donut chart
        
//    Morris.Donut({
//        element: 'morris-donut-chart',
//        data: [{
//            label: "Download Sales",
//            value: 12,

//        }, {
//            label: "In-Store Sales",
//            value: 30
//        }, {
//            label: "Mail-Order Sales",
//            value: 20
//        }],
//        resize: true,
//        colors:['#009efb', '#55ce63', '#2f3d4a']
//    });

//// Morris bar chart
//    Morris.Bar({
//        element: 'morris-bar-chart',
//        data: [{
//            y: '2006',
//            a: 100,
//            b: 90,
//            c: 60
//        }, {
//            y: '2007',
//            a: 75,
//            b: 65,
//            c: 40
//        }, {
//            y: '2008',
//            a: 50,
//            b: 40,
//            c: 30
//        }, {
//            y: '2009',
//            a: 75,
//            b: 65,
//            c: 40
//        }, {
//            y: '2010',
//            a: 50,
//            b: 40,
//            c: 30
//        }, {
//            y: '2011',
//            a: 75,
//            b: 65,
//            c: 40
//        }, {
//            y: '2012',
//            a: 100,
//            b: 90,
//            c: 40
//        }],
//        xkey: 'y',
//        ykeys: ['a', 'b', 'c'],
//        labels: ['A', 'B', 'C'],
//        barColors:['#55ce63', '#2f3d4a', '#009efb'],
//        hideHover: 'auto',
//        gridLineColor: '#eef0f2',
//        resize: true
//    });
//// Extra chart
// Morris.Area({
//        element: 'extra-area-chart',
//        data: [{
//                    period: '2010',
//                    iphone: 0,
//                    ipad: 0,
//                    itouch: 0
//                }, {
//                    period: '2011',
//                    iphone: 50,
//                    ipad: 15,
//                    itouch: 5
//                }, {
//                    period: '2012',
//                    iphone: 20,
//                    ipad: 50,
//                    itouch: 65
//                }, {
//                    period: '2013',
//                    iphone: 60,
//                    ipad: 12,
//                    itouch: 7
//                }, {
//                    period: '2014',
//                    iphone: 30,
//                    ipad: 20,
//                    itouch: 120
//                }, {
//                    period: '2015',
//                    iphone: 25,
//                    ipad: 80,
//                    itouch: 40
//                }, {
//                    period: '2016',
//                    iphone: 10,
//                    ipad: 10,
//                    itouch: 10
//                }


//                ],
//                lineColors: ['#55ce63', '#2f3d4a', '#009efb'],
//                xkey: 'period',
//                ykeys: ['iphone', 'ipad', 'itouch'],
//                labels: ['Site A', 'Site B', 'Site C'],
//                pointSize: 0,
//                lineWidth: 0,
//                resize:true,
//                fillOpacity: 0.8,
//                behaveLikeLine: true,
//                gridLineColor: '#e0e0e0',
//                hideHover: 'auto'
        
//    });
 });    