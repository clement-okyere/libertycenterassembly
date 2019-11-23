$(document).ready(function(){
   
  // console.log("notifications appear here");
    var isNotificationsClicked = false;
    
    $("#notifications").click(function () {
        var username = $(this).data('username');
        markRead(username);
    });


});

function markRead(username) {
    $.ajax({

        url: `/Agent/NotificationsMarkRead?username=${username}`,
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
        async: false,
        success: function (response) {
            // console.log(response);
            var responseJson = JSON.parse(response);
            if (responseJson.key && responseJson.value == "Notifications Marked") {
                $("#notificationsEffect").html('<span></span>');
            }
        }
    });
}