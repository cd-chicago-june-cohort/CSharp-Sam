// Write your Javascript code.
function makeOptions(num){
    var htmlStr = "<option></option>";
    for(var i = 1; i <= num; i += 1){
        htmlStr = htmlStr + "<option value=" + i + ">" + i + "</option>";
    }
    return htmlStr;
}

$(document).ready(function(){

    
    
    $(document).on("click", "#product", function(){
        $(document).on("change", "#product", function(){
            var test = $('#product').val();
            var num = $("#" + test).text();
            $("#quantity").html(makeOptions(num));
        });
    });

})

