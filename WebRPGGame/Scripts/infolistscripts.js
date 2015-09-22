
//$(document).on('click', '#button1', function () {
//    $.ajax({
//        url: '/Account/GamerList/SendGamerList',
//        type: 'GET',
//        dataType: "json",
//        success: function (xhr) {
//            console.log(xhr.type);
//        },
//        error: function (xhr) {
//            console.log("Error with remove ");
//        }
//    });
//});

//$(document).ready(function () {
//    $("#button1").click(function () {
//        $.getJSON("/GamerList/GetGamerList", function (result) {
//            $.each(result, function (i, field) {
//                $("div").append(field + " ");
//            });
//        });
//    });
//});

//$(document).on('click', '#button2', function () {

//    console.log(" cos dzial");
//});

//$(".GamerList").ready(function () {
//    window.hwnd = window.setInterval(
//        function () {
//            console.info('ok');
//        }, 10000);

//});

$(document).on('click', '#button2', function () {

    var ref1 = $("#textbox1").val();
    var ref2 = parseInt($('#textbox2').val());

    $.ajax({
        url: '/Fight/GetInfoHero',
        type: 'POST',
        data: JSON.stringify({refinput:ref1}),
        contentType: 'application/json',
        dataType: "json",
        success: function (data) {
            console.log("cos dziala");
            console.log(data.success);
        },
        error: function (xhr) {
            console.log("cos nie dziala");
            console.log("Error with remove ");
        }
    });
});
