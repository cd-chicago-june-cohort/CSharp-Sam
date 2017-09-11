$(document).ready(function(){
    $("#generate").click(function(){
        $.get("/generate", function(response){
            $(".passcode").text(response);
        });
        $.get("/count", function(response){
            $("#count").text(response);
        });
    });
});