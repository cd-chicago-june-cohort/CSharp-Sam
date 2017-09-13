function makeSquare(title, id){
    var htmlString = "<div id='square" + id + "'><div class='left'><h4>" + title + "</h4><form class='descForm' id='" + id + "'><div class='desc' id='desc" + id + "'></div><div class='hold'><textarea class='descBox' name='content'></textarea></div></form></div><div class='right'><button class='delete' id='delete" + id + "'>Delete</button></div><hr></div>";
    return htmlString;
}

$("#addForm").submit(function(event){
    event.preventDefault();
    var titleData = $("#title").val();
    $.post("/grab", {title: titleData}, function(response){
        console.log(response);
        var title = response["title"];
        var id = response["id"];
        console.log(makeSquare(title, id));
        $("#noteContainer").append(makeSquare(title, id));
        $("#title").val("");
    });
});

$(document).on('click',".desc", function(){
    var myID = $(this).attr("id");
    console.log(myID);
    console.log("hello");
    var checkText = $(this).text();
    console.log("checking for text inside div");
    console.log(checkText);
    $(this).hide();
    if (checkText!=""){
        $(this).next().children('.descBox').val(checkText);
        $(this).next().show();
    } else {
        $(this).next().show();
    }

    $(this).next().children('.descBox').keydown(function (event) {
        if (event.which == 13) {
            var descData = $(this).val();
            console.log(descData);
            var passID = myID.slice(4,myID.length);
            $.post("/update", {content: descData, id: passID}, function(response){
                $("#desc" + passID).text(response);
            });
            $(this).parent().hide();
            $(this).parent().siblings('.desc').show();
        }
    });

});

$(document).on('click', ".delete", function(){
    var preID = $(this).attr("id");
    var id = preID.slice(6,preID.length);
    console.log(preID);
    $.post("/destroy", {id: id}, function(response){
        // console.log(response);
        $("#square" + id).hide();
    });
});



