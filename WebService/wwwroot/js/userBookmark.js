define(['ko'], function userBookmarks(ko){
    
    let bookmarkList = ko.observableArray([]);
    //TODO : incooporate session user smth
    let url = 'api/user/1/lists';   
    
    fetch(url)
        .then(function (response){
            return response.json();
        })
        .then(function (data){
            bookmarkList(data);
        })
        .catch(function(error){
            console.log("Error: "+error)
        });
    
    return {
        bookmarkList
    };
    
});