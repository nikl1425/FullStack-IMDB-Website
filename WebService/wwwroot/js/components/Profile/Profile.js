define(['knockout'], (ko) => {
    return function () {
        let userRatingList = ko.observableArray([]);
        let bookmarkList = ko.observableArray([]);
        let user = ko.observableArray([]);
        //TODO : incooporate session user smth
        let id = 10;
        let testId = 28;
        //let url = 'api/user/'+id+'/ratings/';
        let urlRating = 'http://localhost:5001/api/user/'+id+'/ratings';
        let urlLists = 'http://localhost:5001/api/user/'+id+'/lists';
        let urlUser = 'http://localhost:5001/api/user/'+id;
        let urlUpdate = 'http://localhost:5001/api/user/'+id+'/update'
        let urlUpdatePW = 'http://localhost:5001/api/user/'+testId+'/changepassword'
        let urlDelete = 'http://localhost:5001/api/user/'+testId+'/delete'

        /*  FETCH RATING FROM USER  */
        fetch(urlRating)
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                userRatingList(data);
                console.log(data);
            })
            .then(function (setRating){
                alert("test")
                $.ajax({
                    type: "POST",
                    url: setRating.updateUrl+'/'
                });
            })
            .catch(function (error) {
                console.log("Error: " + error)
            });

        /*  FETCH USER'S BOOKMARK LISTS  */
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
        
        /*  FETCH USER INFO  */
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

        /*  UPDATE PROFILE  */
        const serialize_form = form => JSON.stringify(
            Array.from(new FormData(form).entries())
                .reduce((m,[key,value]) => Object.assign(m,{[key]: value}),{})
        );
        $(".updateProfile").submit(function (e) {
            e.preventDefault();
            const json = serialize_form(this);
            console.log(json);
            $.ajax({
                type: 'POST',
                url: urlUpdate,
                dataType: 'json',
                data: json,
                contentType: 'application/json',
                success: function (data) {
                    if(data) {
                        alert("Your account has been updated!")
                    } else {
                        alert("Either email is in use or smth went wrong!")
                    }
                }
            });
        });

        /*  UPDATE PASSWORD  */
        const serialize_form_PW = form => JSON.stringify(
            Array.from(new FormData(form).entries())
                .reduce((m,[key,value]) => Object.assign(m,{[key]: value}),{})
        );
        $(".updatePWForm").submit(function (e) {
            e.preventDefault();
            const json = serialize_form_PW(this);
            console.log(json);
            $.ajax({
                type: 'POST',
                url: urlUpdatePW,
                dataType: 'json',
                data: json,
                contentType: 'application/json',
                success: function (data) {
                    if(data) {
                        alert("Your password has been updated!");
                        $('#updateBtn').modal('hide');
                        $('.modal-backdrop').remove();
                    } else {
                        alert("Either email is in use or smth went wrong!")
                    }
                }
            });
        });

        /*  UPDATE RATING  */
        $("#updateRating").click(function(e) {
            alert("test");
            e.preventDefault();
            $.ajax({
                type: 'GET',
                url: $(this).attr("href"),
                success: function (result) {
                    if(result) {
                        alert("You have updated the rating!")
                    } else {
                        alert("Something went wrong!")
                    }
                }
            });
        });

        /*  DELETE PROFILE  */
        $("#confirmDelete").on('click', function() {      
            $.ajax({
                type: 'DELETE',
                url: urlDelete,
                success: function (result) {
                    if(result) {
                        alert("Your account has been deleted!")
                        location.replace("http://localhost:5001")
                    } else {
                        alert("Something went wrong!")
                    }
                }
            });
        });
        
        /*  DELETE BOOKMARKS LIST   */
        $("#deleteBookmarkList").on('click', function() {
            console.log(bookmarkList.Id);
            $.ajax({
                type: 'DELETE',
                url: 'http://localhost:5001/api/tlist/17/delete',
                success: function (result) {
                    if(result) {
                        alert("Your list has been deleted!")
                    } else {
                        alert("Something went wrong!")
                    }
                }
            });
            
        });

        return {
            userRatingList,
            bookmarkList,
            user
        };
    }
    
});
