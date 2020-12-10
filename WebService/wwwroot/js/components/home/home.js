
define(['knockout'], (ko) => {
    return function () {
        let poster = ko.observableArray([]);
        self.getPosters = function () {
            ko.mapping.fromJS(data.poster, {}, self.poster)
        }

        fetch('http://localhost:5001/api/title/topposter')
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                poster(data);
            })
        return {
            poster
        };
    }
});

function search(searchVariable){
        var url = 'http://localhost:5001/api/search/';

    fetch(url + searchVariable) // Call the fetch function passing the url of the API as a parameter
        .then((response) => {
            return response.json()
        })
        .then((data) => {
            console.log(data);
        }).catch((err) => {
        alert("Error!");
    })
}


$(document).on("keypress", "input", function(e){
    if(e.which == 13){
        var inputVal = $(this).val();
        alert("You've searched: " + inputVal);
        search(inputVal);
    }
});



define(['postman'], (postman) => {
    return function () {

        let gotoMovie = () => {
            postman.publish("changeContent", "movie");
        }
        return {
            gotoMovie
        };
    }
});
