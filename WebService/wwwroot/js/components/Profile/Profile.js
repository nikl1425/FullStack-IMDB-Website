define(['knockout', 'postman'], (ko, postman) => {
    return function () {
        self = this;
        let userRatingList = ko.observableArray([]);
        let bookmarkList = ko.observableArray([]);
        let user = ko.observableArray([]);
        window.listValue = "";
        
        //TODO : incooporate session user smth
        let userId = 6;
        let testId = 2;
        //let url = 'api/user/'+id+'/ratings/';
        let urlRating = 'http://localhost:5001/api/user/'+ window.userIdString +'/ratings';
        let urlLists = 'http://localhost:5001/api/user/'+ window.userIdString +'/lists';
        let urlUser = 'http://localhost:5001/api/user/'+ window.userIdString;
        let urlUpdate = 'http://localhost:5001/api/user/'+ window.userIdString +'/update'
        let urlUpdatePW = 'http://localhost:5001/api/user/'+ window.userIdString +'/changepassword'
        let urlDelete = 'http://localhost:5001/api/user/'+testId+'/delete'
        let baseUrl = 'http://localhost:5001/api/';
        let loggedIn = ko.observableArray([]);
        

        let urlDelete = 'http://localhost:5001/api/user/'+ window.userIdString +'/delete'
        let baseUrl = 'http://localhost:5001/api/'
        
        const myHeaders = new Headers({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + window.tokenString
        });
        
        window.testidvariable = urlUser;
        console.log(window.testidvariable)
        console.log(window.tokenString)
        /*  FETCH RATING FROM USER  */
        function getRating(){
            fetch(urlRating, {
                method: 'GET',
                headers: myHeaders
            })
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                userRatingList(data);
                console.log("rating: "+data);
                $(".updateRating").on('click', function(){
                    let titleid = $(this).val();
                    let rating = $(this).text();
                    $.ajax({
                        type: 'POST',
                        url: baseUrl+'title/'+titleid+'/RateMovie/'+userId+'/'+rating, 
                        headers: {Authorization: 'Bearer '+window.tokenString},
                        success: function (result) {
                           if(result) {
                               //alert("Your list has been deleted!")
                               getRating();
                           } else {
                               alert("Something went wrong!")
                           }
                       }
                   })
                });
                $(".gotomoviepage").on('click', function(){
                    window.movieValue = $(this).val();
                    console.log($(this).val());
                    goToMoviePage();
                })
            })
            .catch(function (error) {
                console.log("Error: " + error)
            });
        }
        function goToMoviePage(){
            postman.publish("changeContent", "moviePage");
        }
        getRating();
        let goToList = () => {
            postman.publish("changeContent", "listpage");
        }
        let goToNewList = () => {
            postman.publish("changeContent", "createlist");
        }
        
        $('#gotonewlist').on('click', function(){
            console.log("Has focus")
            window.value = $(this).val();
            goToNewList()
        })
        
        
        /*  FETCH USER'S BOOKMARK LISTS  */
        function getList() {
        fetch(urlLists, {
            method: 'GET',
            headers: myHeaders
           })
            .then(function (response){
                return response.json();
            })
            .then(function (data){
                bookmarkList(data);
                console.log(data);
                $(".deleteBookmarkList").on('click', function() {
                    if(confirm("Are you sure you want to delete?")){
                        let value = $(this).val();
                        console.log("VALUE: "+value);
                        console.log("TRIM: "+value.substring(1))
                        if(value.indexOf('t')>-1){
                            let trimValTitle = value.substring(1);
                            $.ajax({
                                type: 'DELETE',
                                url: 'http://localhost:5001/api/tlist/'+trimValTitle+'/delete',
                                headers: {Authorization: 'Bearer '+window.tokenString},
                                success: function (result) {
                                    if(result) {
                                        //alert("Your list has been deleted!")                                    
                                        getList();
                                        $('#deleteList').modal('hide');
                                        $('.modal-backdrop').remove();
                                    } else {
                                        alert("Something went wrong!")
                                    }
                                }
                            })
                        }
                        //value.indexOf('p')>-1
                        else{
                            let trimVal = value.substring(1);
                            $.ajax({
                                type: 'DELETE',
                                url: 'http://localhost:5001/api/plist/'+trimVal+'/delete',
                                headers: {Authorization: 'Bearer '+window.tokenString},
                                success: function (result) {
                                    if(result) {
                                        //alert("Your list has been deleted!")                                    
                                        getList();
                                        $('#deleteList').modal('hide');
                                        $('.modal-backdrop').remove();
                                    } else {
                                        alert("Something went wrong!")
                                    }
                                }
                            })
                        }
                    } else {
                    }
                });
                $('.gotolist').focus(function(){
                    console.log("Has focus " + $(this).val())
                    window.listValue = $(this).val();
                    goToList()
                })
            })
            .catch(function(error){
                console.log("Error: "+error)
            });
        }
        getList();
        
       
        function getUserInfo() {
            fetch(urlUser, {
                method: 'GET',
                headers: myHeaders
                })
                .then(response => {
                    return response.json()
                }).then(function (data) {
                   user(data)
                    console.log(user(data));
                 }).catch(error => {
                    console.error(error);
                });
            }
            
            getUserInfo()
        
        /*
        beforeSend: function(request) {
                    request.setRequestHeader("Bearer ", window.tokenString);
                },
         */

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
                headers: {Authorization: 'Bearer '+window.tokenString},
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
                headers: {Authorization: 'Bearer '+window.tokenString},
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
                headers: {Authorization: 'Bearer '+window.tokenString},
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
                headers: {Authorization: 'Bearer '+window.tokenString},
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
        
        
        
        let changeContentToLogin = () => {
                goToLogin();
        }
        
        
        function goToLogin(){
            postman.publish("changeContent", "Login");
        }


       

        return {
            userRatingList,
            bookmarkList,
            user,
            loggedIn,
            changeContentToLogin
            
        };
    }
    
});
