
$(document).on('click',"#search", function(){
    $("#search").keydown(function (event) {
        if (event.which == 13) {
            var search = $("#search").val();
            event.preventDefault();
            var grabData = "https://api.themoviedb.org/3/search/movie?api_key=6b3eecb513da5abb385683b020537ea4&query=" + search;
            $.get(grabData, function(response){
                console.log(response);
                var myMovie = response.results[0];
                console.log(myMovie);
                console.log(myMovie["release_date"]);
                $("#search").val("");
                $.post("/search", {movie: myMovie}, function(response){
                    var html = "<tr><td>";
                    html += response["myTitle"];
                    html+= "</td><td>Rating: ";
                    html+= response["myRating"];
                    html+= "</td><td>Released on ";
                    html += response["myDate"];
                    html+= "</td></tr>";
                    $("#movieData").prepend(html);
                });
            });
        }
    });
});
