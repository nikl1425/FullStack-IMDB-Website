
define(['knockout', 'postman'], (ko, postman) => {
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
//let genreData = ko.observableArray([])
let titleData = ko.observableArray([])
//let professionData = ko.observableArray([])
 

        
        function goToPersonPage(){
            postman.publish("changeContent", "peoplePage");
        }
        function goToMoviePage(){
            postman.publish("changeContent", "moviePage");
        }
        
window.value = "";
        
$(document).on("keyup", "input", function(e){
    const inputVal = $(this).val();
    let inputValLength = $(this).val().length;
    const url = 'http://localhost:5001/api/search/';
        fetch(url + inputVal)
            .then((response) => {
                return response.json()
            })
            .then((data) => {
                personData(data.newSearchPersonDTO)
                titleData(data.newSearchTitleDTO)
                console.log(inputValLength);
                $('.gotopage').focus(function(){
                    console.log("Has focus")
                    window.value = $(this).val();
                    goToPersonPage()
                })
                $('.goToMoviePage').focus(function(){
                    window.movieValue = $(this).val();
                    //console.log(movieValue)
                    goToMoviePage()
                })
                
            }).catch((err) => {
        })
    
    if (inputValLength <= 1) {
        $('.dropdown-content').hide();
    }
    else {
        $('.dropdown-content').show();
        }


    $(document).ready(function(){
        $('#searchbar').focus(function(){
            console.log("Has focus")
        }).focusout(function(){
            //$('.dropdown-content').hide();
        });
    });

    console.log(inputValLength);

        if (e.keyCode === 13){
        //console.log("Enter");
    }
});

        return {
            poster,
            personData,
            titleData,

        };
    }
});








