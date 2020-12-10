define(['knockout'], (ko) => {
    return function () {
        let userRatingList = ko.observableArray([]);
        let bookmarkList = ko.observableArray([]);
        let user = ko.observableArray([]);
        //TODO : incooporate session user smth
        let id = 1;
        //let url = 'api/user/'+id+'/ratings/';
        let urlRating = 'http://localhost:5001/api/user/'+id+'/ratings';
        let urlLists = 'http://localhost:5001/api/user/'+id+'/lists';
        let urlUser = 'http://localhost:5001/api/user/'+id;

        fetch(urlRating)
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                userRatingList(data);
                console.log(data);
            })
            .catch(function (error) {
                console.log("Error: " + error)
            });
        
        fetch(urlLists)
            .then(function (response){
                return response.json();
            })
            .then(function (data){
                bookmarkList(data);
                console.log(data);
            })
            .catch(function(error){
                console.log("Error: "+error)
            });
        
        fetch(urlUser)
            .then(function (response){
                return response.json();
            })
            .then(function (data){
                user(data);
                console.log(data);
            })
            .catch(function(error){
                console.log("Error: "+error)
            });

        return {
            userRatingList,
            bookmarkList,
            user
        };
    }
    
});
