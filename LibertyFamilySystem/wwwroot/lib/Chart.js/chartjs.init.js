$(document).ready(function () {


    function GenerateColor() {
        r = Math.floor(Math.random() * 200);
        g = Math.floor(Math.random() * 200);
        b = Math.floor(Math.random() * 200);
        color = 'rgb(' + r + ', ' + g + ', ' + b + ')';
        return color;
    }

    function getOverDueCasesCount() {
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
                console.log(xhr.responseText);
                alert("Oooops! something went wrong!!")
            },
            success: function (response) {
                // console.log(response);
                var label = [];
                var data = [];
                $.each(JSON.parse(response), function (i, value) {

                    data.push(value.overDueCases);
                    label.push(value.shortName);
                });

                overDueCasesChart(label, data);
                // console.log(label);
                // console.log(data);
            }
        });
    }



    getOverDueCasesCount();
    //getEntityCasesCount();





    //Overdue Cases Chart
    function overDueCasesChart(labels, data) {
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


});