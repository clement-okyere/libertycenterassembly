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
               console.log(xhr.status);
               console.log(xhr.responseText);
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
				   console.log(content);
                   data.push(content);                  
               });
			  // console.log(data);
			   handleData(data);

           }
       });
	   
	   
    }

      
     // getEntityCasesCount();
	  
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
