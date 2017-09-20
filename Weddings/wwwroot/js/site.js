// Write your Javascript code.
$(document).ready(function(){
    var a = new Date($.now());
    var b = a.toISOString();
    var min = b.slice(0,10)
    $("#wedDate").click(function(){
        console.log(min);
        $(this).attr("min", min);
    })
})