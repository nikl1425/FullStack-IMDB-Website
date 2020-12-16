define(['knockout', 'postman'], (ko, postman) => {
    return function () {
        self = this;
        let userRatingList = ko.observableArray([]);
        let bookmarkList = ko.observableArray([]);
        let user = ko.observableArray([]);
        window.listValue = "";
        window.movieValue = "";
        window.value = "";


        let urlRating = 'http://localhost:5001/api/user/'+ window.userIdString +'/ratings';
        let urlLists = 'http://localhost:5001/api/user/'+ window.userIdString +'/lists';
        let urlUser = 'http://localhost:5001/api/user/'+ window.userIdString;
        let baseUrl = 'http://localhost:5001/api/'
        let loggedIn = ko.observableArray([]);

        const myHeaders = new Headers({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + window.tokenString
        });

        window.testidvariable = urlUser;

        let newTListUrl = 'http://localhost:5001/api/user/'+window.userIdString+'/tlist/create'
        let newPListUrl = 'http://localhost:5001/api/user/'+window.userIdString+'/plist/create'

        
        /*  CREATE NEW BOOKMARK LIST  */
        const serialize_form_list = form => JSON.stringify(
            Array.from(new FormData(form).entries())
                .reduce((m,[key,value]) => Object.assign(m,{[key]: value}),{})
        );

        $(".new_list_form").submit(function (e) {
            e.preventDefault();
            const json = serialize_form_list(this);
            console.log();
            let s = json.toString();
            console.log("STRING: "+s)
            if(s.includes('tlist')) {
                console.log("tlist")
                $.ajax({
                    type: 'POST',
                    url: newTListUrl,
                    headers: {Authorization: 'Bearer '+window.tokenString},
                    dataType: 'json',
                    data: json,
                    contentType: 'application/json',
                    success: function(x) {
                        getList()
                        $('.modal').modal('hide');
                        $('.modal-backdrop').remove();
                        console.log("we r sending.... plist"+x)
                        alert("Your title list has been created!")
                    }
                });
            } else
            if(s.includes('plist')) {
                console.log("plist")
                $.ajax({
                    type: 'POST',
                    url: newPListUrl,
                    headers: {Authorization: 'Bearer '+window.tokenString},
                    dataType: 'json',
                    data: json,
                    contentType: 'application/json',
                    success: function (data) {
                        getList()
                        $('.modal').modal('hide');
                        $('.modal-backdrop').remove();
                        console.log("we r sending.... plist")
                        if(data) {
                            alert("Your person list has been created!")
                        }
                    }
                });
            } else {
                alert("Please select a type of list.")
            }
        });






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
                            url: baseUrl+'title/'+titleid+'/RateMovie/'+window.userIdString+'/'+rating,
                            headers: {Authorization: 'Bearer '+window.tokenString},
                            success: function (result) {
                                if(result) {
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
                    });
                    $(".deleteRating.btn.btn-warning").on('click', function(){
                        let titleid = $(this).val();
                        $.ajax({
                            type: 'DELETE',
                            url: baseUrl+'title/'+titleid+'/RateMovie/'+window.userIdString+'/Delete',
                            headers: {Authorization: 'Bearer '+window.tokenString},
                            success: function (result) {
                                if(result) {
                                    alert("Your rating has been deleted!")
                                    getRating();
                                } else {
                                    alert("Something went wrong!")
                                }
                            }
                        })
                    });
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
                                    url: 'http://localhost:5001/api/tlist/' + window.userIdString + '/' +trimValTitle+'/delete',
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
                                    url: 'http://localhost:5001/api/plist/' + window.userIdString + '/' +trimVal+'/delete',
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
                loggedIn.push("true")
            }).catch(error => {
                console.error(error);
            });
        }

        getUserInfo()


        $('#changeContentToLogin').click(function(){
            goToLogin()
        })

        $('#changeContentToUpdate').click(function(){
            goToUpdate()
        })


        $('#logOutButton').click(function(){
            window.tokenString = "";
            window.userIdString = "";
            goToHome()
            alert("Successfully logged out")
        })


        let changeContentToLogin = () => {
            goToLogin();
            console.log("Test")
        }
        function goToHome(){
            postman.publish("changeContent", "home");
        }

        function goToLogin(){
            postman.publish("changeContent", "Login");
        }

        function goToUpdate(){
            postman.publish("changeContent", "Updateuser");
        }

        $('#myModal').on('shown.bs.modal', function () {
            $('#myInput').trigger('focus')
        })



        return {
            userRatingList,
            bookmarkList,
            user,
            loggedIn,
            changeContentToLogin

        };
    }

});