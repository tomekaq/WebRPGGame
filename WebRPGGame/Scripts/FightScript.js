$("body").on("click", "input", function () {
    var th = $(this).context.id;
    console.log("cos kliknieto", th);

    blockButton(th);

    $.ajax({
        url: '/Fight/CallToFight',
        type: 'POST',
        data: { ID: th, MyID: 3 },
        success: function (response) {

            // console.log(response.Hero1);
            redirecting(th, 3);
        },
        error: function (xhr) {
            console.log("Error with get users list.");
        }
    })
});

function blockButton(th) {

    var buttonID = "#" + th;
    $('#myTable :input').prop("disabled", true);
}

function redirecting(ID, MyID) {
    window.location = "../Fight/Index";
    //testwindow = window.open("../Fight/Index.html", "_self");
    //testwindow.addEventListener('load', function () {
    //    testwindow.
    fight(ID, MyID);
};

function fight(ID1, MyID1) {
    $(document).ready(function () {

        $.ajax({
            url: "/Fight/Fight",
            data: { ID: ID1, MyID: MyID1 },
            type: 'POST',
            success: function (data) {
                console.log("ok");
                wypisz(data);
            },
            error: function (xhr) {
                console.log(xhr.status);
            }
        });
    });
};

function wypisz(data) {
    var row = '<br>' + data.message + '</br>'
    $('#frame').append(row);
    console.log(data.message);
}
