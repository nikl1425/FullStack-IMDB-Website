
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
/*
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
*/

       

let personData = ko.observableArray([])
let genreData = ko.observableArray([])
let titleData = ko.observableArray([])
let professionData = ko.observableArray([])

        
$(document).on("keydown", "input", function(e){
    const inputVal = $(this).val();
    const url = 'http://localhost:5001/api/search/';
        fetch(url + inputVal)
            .then((response) => {
                return response.json()
            })
            .then((data) => {
                personData(data.newSearchPersonDTO)
                console.log(personData())
                if (data.newSearchGenreDTO.isNull){
                    
                }else{
                    genreData(data.newSearchGenreDTO)
                }
                console.log(genreData())
                titleData(data.newSearchTitleDTO)
                console.log(titleData())
                professionData(data.newSearchProfessionDTO)
                console.log(professionData())
            }).catch((err) => {
        })


    if (e.keyCode === 13){
        //console.log("Enter");
    }
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








