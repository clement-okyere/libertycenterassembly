// Dashboard 1 Morris-chart
$(function () {
    "use strict";

    var isCustomClicked = false;
    $("#custom").click(function () {

        $(this).removeClass("btn-default");
        $(this).addClass("btn-info");
        $("#last7").removeClass("btn-info");
        $("#last7").addClass("btn-default");
        $("#last30").removeClass("btn-info");
        $("#last30").addClass("btn-default");
        $("#Everything").removeClass("btn-info");
        $("#Everything").addClass("btn-default");
        $(this).addClass("btn-info");

        if (isCustomClicked == false) {
            $("#dateFilter").css("display", "flex").fadeIn();
            // $("#dateFilter").removeProp('hidden');
            isCustomClicked = true;

        }
        else {
            //$("#dateFilter").prop('hidden', 'hidden');
            $("#dateFilter").css("display", "none").fadeOut();
            isCustomClicked = false;
        }

    });


    $("#last7").click(function () {
        if (isCustomClicked == true) {
            $("#dateFilter").css("display", "none").fadeOut();
            isCustomClicked = false;
        }

        $(this).removeClass("btn-default");
        $(this).addClass("btn-info");
        $("#last7").removeClass("btn-info");
        $("#last7").addClass("btn-default");
        $("#last30").removeClass("btn-info");
        $("#last30").addClass("btn-default");
        $("#Everything").removeClass("btn-info");
        $("#Everything").addClass("btn-default");
        $("#custom").removeClass("btn-info");
        $("#custom").addClass("btn-default");
        $(this).addClass("btn-info");

        var curDateT = $("#curDate").text();
        var endDate = new Date(curDateT).toISOString().split('T')[0];

        var startDateT = new Date(curDateT);
        startDateT.setDate(new Date(curDateT).getDate() - 7);
        var startDate = startDateT.toISOString().split('T')[0];
        $("#StartDate").val(startDate);
        $("#EndDate").val(endDate);

        getStateStatsCountWithDates(startDate, endDate);
        getOverDueCasesCountDateTime(startDate, endDate);
        getCaseStateDisplayStats(startDate, endDate);
        getTopicGroupStats(startDate, endDate);
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChartwithDate);


    });


    $("#last30").click(function () {
        if (isCustomClicked == true) {
            $("#dateFilter").css("display", "none").fadeOut();
            isCustomClicked = false;
        }


        $(this).removeClass("btn-default");
        $(this).addClass("btn-info");
        $("#last7").removeClass("btn-info");
        $("#last7").addClass("btn-default");
        //$("#last30").removeClass("btn-info");
        //$("#last30").addClass("btn-default");
        $("#Everything").removeClass("btn-info");
        $("#Everything").addClass("btn-default");
        $("#custom").removeClass("btn-info");
        $("#custom").addClass("btn-default");
        $(this).addClass("btn-info");


        var curDateT = $("#curDate").text();
        var endDate = new Date(curDateT).toISOString().split('T')[0];

        var startDateT = new Date(curDateT);
        startDateT.setDate(new Date(curDateT).getDate() - 30);
        var startDate = startDateT.toISOString().split('T')[0];

        $("#StartDate").val(startDate);
        $("#EndDate").val(endDate);

        getStateStatsCountWithDates(startDate, endDate);
        getOverDueCasesCountDateTime(startDate, endDate);
        getCaseStateDisplayStats(startDate, endDate);
        getTopicGroupStats(startDate, endDate);
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChartwithDate);
        //drawChartwithDate();

    });


    $("#Everything").click(function () {
        if (isCustomClicked == true) {
            $("#dateFilter").css("display", "none").fadeOut();
            isCustomClicked = false;
        }

        $(this).removeClass("btn-default");
        $(this).addClass("btn-info");
        $("#last7").removeClass("btn-info");
        $("#last7").addClass("btn-default");
        $("#last30").removeClass("btn-info");
        $("#last30").addClass("btn-default");
        $("#custom").removeClass("btn-info");
        $("#custom").addClass("btn-default");
        //$("#Everything").removeClass("btn-info");
        //$("#Everything").addClass("btn-default");
        $(this).addClass("btn-info");

        
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChartwithDate);

        getOverDueCasesCount();
        getStateStatsCount();
        getTopicGroupStats("null","null");
        getCaseStateDisplayStats("null", "null");
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);
       // drawChartwithDate();



    });

    $("#FormCaseStats").submit(function(e) {
       // alert("graph load clicked");
        e.preventDefault();
    });


    $("#TestSubmit").click(function (e) {
        var startDate = $('#FormCaseStats').find('input[name="StartDate"]').val();
        var endDate = $('#FormCaseStats').find('input[name="EndDate"]').val();
       // console.log("startdate " + startDate);
       // console.log("enddate " + endDate);
        getStateStatsCountWithDates(startDate, endDate);
		getOverDueCasesCountDateTime(startDate,endDate); 
		getCaseStateDisplayStats(startDate,endDate);
		getTopicGroupStats(startDate,endDate);
	    google.charts.load('current', {'packages':['corechart']});
        google.charts.setOnLoadCallback(drawChartwithDate);
			
      
        e.preventDefault();
    });
	
	//new code starts here
	
	  function GenerateColor() {
        r = Math.floor(Math.random() * 200);
        g = Math.floor(Math.random() * 200);
        b = Math.floor(Math.random() * 200);
        color = 'rgb(' + r + ', ' + g + ', ' + b + ')';
        return color;
    }
	
	
	    function getCaseStateDisplayStats(startDate,endDate){
        $.ajax({
            
            url: `/Agent/_CaseStateStatsDisplay?StartDate=${startDate}&EndDate=${endDate}`,
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
                //console.log(xhr.responseText);
               // alert("Oooops! something went wrong!!")
            },
			async:false,
            success: function (response) {
               // console.log(response);
              $("#CaseStateStatsDisplay").html(response);
            }
        });
    }
	
	function getTopicGroupStats(startDate,endDate){
        $.ajax({
            
            url: `/Agent/_TopicGroupDisplay?StartDate=${startDate}&EndDate=${endDate}`,
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
               // console.log(xhr.responseText);
               // alert("Oooops! something went wrong!!")
            },
			async:false,
            success: function (response) {
               // console.log(response);
              $("#TopicGroupStats").html(response);
            }
        });
    }
	
	
    
    function getOverDueCasesCount(){
        $.ajax({
            //url: '/Setup/_GetStaffIDGenerator/area=Administrator',
            url: '/Agent/GetOverDueCasesCount',
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
                //console.log(xhr.responseText);
               // alert("Oooops! something went wrong!!")
            },
			async:false,
            success: function (response) {
               // console.log(response);
                var responseCount = JSON.parse(response).length;
                if (responseCount == 0) {
                    $("#noOverDue").css("display", "block");
                }
                else {
                    $("#noOverDue").css("display", "none");
                }
                //console.log("over due cases reponse count -" + responseCount);
                var label = [];
                var data = [];
                $.each(JSON.parse(response), function (i, value) {
                    
                    data.push(value.overDueCases);
                    label.push(value.shortName);
                });

				$("#chart2").empty();
                overDueCasesChart(label, data);
               // console.log(label);
               // console.log(data);
            }
        });
    }
	
	
	 function getOverDueCasesCountDateTime(startDate,endDate){
        $.ajax({
            //url: '/Setup/_GetStaffIDGenerator/area=Administrator',
            url: `/Agent/GetOverDueCasesCount?StartDate=${startDate}&EndDate=${endDate}`,
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
               // console.log(xhr.responseText);
                alert("Oooops! something went wrong!!")
            },
			async:false,
            success: function (response) {
               // console.log(response);
                var responseCount = JSON.parse(response).length;
              //  console.log("over due cases reponse count -" + responseCount);
                if (responseCount == 0) {
                    $("#noOverDue").css("display", "block");
                }
                else {
                    $("#noOverDue").css("display", "none");
                }

                var label = [];
                var data = [];
                $.each(JSON.parse(response), function (i, value) {
                    
                    data.push(value.overDueCases);
                    label.push(value.shortName);
                });

				$("#chart2").empty();
                overDueCasesChart(label, data);
               // console.log(label);
               // console.log(data);
            }
        });
    }


//Overdue Cases Chart
    function overDueCasesChart(labels,data) {
        var ctx2 = document.getElementById("chart2").getContext("2d");
        var data2 = {
            labels: labels,
            datasets: [
                {
                    label: "My First dataset",
                    fillColor: "#009efb",
                    strokeColor: "#009efb",
                    highlightFill: "#009efb",
                    highlightStroke: "#009efb",
                    data: data
                }
            ]
        };

        var chart2 = new Chart(ctx2).Bar(data2, {
            scaleBeginAtZero: true,
            scaleShowGridLines: true,
            scaleGridLineColor: "rgba(0,0,0,.005)",
            scaleGridLineWidth: 0,
            scaleShowHorizontalLines: true,
            scaleShowVerticalLines: true,
            barShowStroke: true,
            barStrokeWidth: 0,
            tooltipCornerRadius: 2,
            barDatasetSpacing: 3,
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].fillColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            responsive: true
        });
    }

	
	    getOverDueCasesCount();
	
	
	
	//new code ends here
  


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
                //console.log(xhr.responseText);
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

               // console.log(firstItem);

                var keys = Object.keys(firstItem);
                var IsSuccess = $.inArray("status", keys);
              //  console.log("IsSuccess", IsSuccess);
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
                   // console.log(data);
                   // console.log(data);

                    //loop and populate colours
                    var coloursList = parsedResponse[size - 1];
                    //console.log(coloursList);

                    $.each(coloursList, function (i, value) {

                        colors.push(value);

                    });
                    $("#morris-area-chart").empty();
                    AreaChart(yvals, data, colors);
                }
                else {

				 $("#morris-area-chart").empty();
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
            url: `/Agent/GetStateStats?StartDate=${startDate}&EndDate=${endDate}`,
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
                //console.log(xhr.responseText);
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

               // console.log(firstItem);

                var keys = Object.keys(firstItem);
                var IsSuccess = $.inArray("status", keys);
              //  console.log("IsSuccess", IsSuccess);
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
                  //  console.log(data);
                  //  console.log(data);

                    //loop and populate colours
                    var coloursList = parsedResponse[size - 1];
                   // console.log(coloursList);

                    $.each(coloursList, function (i, value) {

                        colors.push(value);

                    });

                    $("#morris-area-chart").empty();
                    AreaChart(yvals, data, colors);
                }
                else {

				 $("#morris-area-chart").empty();
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

	
	
	   function getEntityCasesCount(handleData) {
       $.ajax({
           //url: '/Setup/_GetStaffIDGenerator/area=Administrator',
           url: '/Agent/GetEntityCaseCount',
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
              // console.log(xhr.status);
              // console.log(xhr.responseText);
               alert("Oooops! something went wrong!!")
           },
		   async:false,
           success: function (response) {
               //console.log(response);
               var data = [
			    ['Task', 'Hours per Day']
			   ];
                
               $.each(JSON.parse(response), function (i, value) {

                   var content = [
				   value.organizationName,
				   value.organizationCaseCount
				   ];
				 //  console.log(content);
                   data.push(content);                  
               });
			  // console.log(data);
			   handleData(data);

           }
       });	   
    }

	getStateStatsCount();
	
		
	   function getEntityCasesCountWithDates(handleData) {
	   var startDate = $('#FormCaseStats').find('input[name="StartDate"]').val();
       var endDate = $('#FormCaseStats').find('input[name="EndDate"]').val();	 
		  
       $.ajax({    
		
           url: `/Agent/GetEntityCaseCount?StartDate=${startDate}&EndDate=${endDate}`,
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
              // console.log(xhr.status);
              // console.log(xhr.responseText);
               //alert("Oooops! something went wrong!!")
           },
		   async:false,
           success: function (response) {
               //console.log(response);
               var data = [
			    ['Task', 'Hours per Day']
			   ];
                
               $.each(JSON.parse(response), function (i, value) {

                   var content = [
				   value.organizationName,
				   value.organizationCaseCount
				   ];
				  // console.log(content);
                   data.push(content);                  
               });
			  // console.log(data);
			   handleData(data);

           }
       });	   
    }




      
	  
	  google.charts.load('current', {'packages':['corechart']});
      google.charts.setOnLoadCallback(drawChart);
	  
	  

      function drawChart(data) {
		  
		  var test;
		  getEntityCasesCount(function(output){
          test = Array.from(output);
        });

        var data = google.visualization.arrayToDataTable(
		test
		);

        var options = {
          title: 'My Daily Activities',
		  is3D: true,
		  chartArea: {width: 600, height: 300}
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechart'));

        chart.draw(data, options);
      }
	  
	  
	   function drawChartwithDate(data) {  
		  var test;
		  
		  getEntityCasesCountWithDates(function(output){
          test = Array.from(output);
        });

        var data = google.visualization.arrayToDataTable(
		test
		);

        var options = {
          title: 'My Daily Activities',
		  is3D: true,
		  chartArea: {width: 600, height: 300}
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechart'));

        chart.draw(data, options);
      }
	  
	   


   

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







 });    