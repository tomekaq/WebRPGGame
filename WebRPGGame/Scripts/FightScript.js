
$("body").on("click", "input", function () {
    var th = $(this).context.id;
    console.log("cos kliknieto", th);

    blockButton(th);

    $.ajax({
        url: '/Fight/CallToFight',
        type: 'POST',
        data: { ID: th, MyID: 7 },
        success: function (response) {
            // console.log(response.Hero1);
            redirecting(th, 7);
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
    window.location = "../Fight/Index?ID="+ID+"&MyID="+MyID;
    //fight(ID, MyID);
};

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function fight(ID1, MyID1) {
    $(document).ready(function () {
        $.ajax({
            url: "/Fight/Fight",
            data: { ID: ID1, MyID: MyID1 },
            type: 'POST',
            success: function (data) {
                console.log("ok");
                startListening(ID1, MyID1);
            },
            error: function (e) {
                console.error(e);
            }
        });
    });
};

function startListening(id, myid) {
    window.setInterval(function () {
        listen(id, myid);
    }, 1000);   
}

function listen(id, myid) {
  
        $.ajax({
            url: "/Fight/GetFight",
            data: { ID: id, MyID: myid },
            type: 'GET',
            success: function (data) {
                var message = data.message;
                var messages = message.replace('\n', '<br/>');
                console.log('response from listen');
                var last = $("#frame").html();
                var row = "<br>" + messages + "</br>";
                $("#frame").html(last + row);
            },
            error: function (e) {
                console.error(e);
            }
        });
    
    }