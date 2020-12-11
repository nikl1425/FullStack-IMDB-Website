
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

let personData = ko.observableArray([])
let genreData = ko.observableArray([])
let titleData = ko.observableArray([])
let professionData = ko.observableArray([])
        
$(document).on("keypress", "input", function(e){
    const inputVal = $(this).val();
    const url = 'http://localhost:5001/api/search/';
        fetch(url + inputVal)
            .then((response) => {
                return response.json()
            })
            .then((data) => {
                personData(data.personSearch)
                genreData(data.genreSearch)
                titleData(data.titleSearch)
                professionData(data.professionSearch)
            }).catch((err) => {
        })

    
});

        return {
            poster,
            personData,
            genreData,
            titleData,
            professionData
        };
    }
});








