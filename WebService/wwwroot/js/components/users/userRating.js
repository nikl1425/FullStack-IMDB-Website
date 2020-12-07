define(['ko'], function userRating(ko){

    let userRatingList = ko.observableArray([]);
    //TODO : incooporate session user smth
    let id = 1;
    let url = 'api/user/'+id+'/ratings/';

    fetch(url)
        .then(function (response){
            return response.json();
        })
        .then(function (data){
            userRatingList(data);
            console.log(data);
        })
        .catch(function(error){
            console.log("Error: "+error)
        });

    return {
        userRatingList
    };

});