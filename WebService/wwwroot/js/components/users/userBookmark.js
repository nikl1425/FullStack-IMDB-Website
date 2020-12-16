define(['knockout', 'postman'], (ko, postman) => {
    return function () {
        console.log(window.listValue);
        //window.movieValue = "";
                
        let tListData = ko.observableArray([])
        let tBookmarks = ko.observableArray([])
        let user = ko.observableArray([]);
        let pListData = ko.observableArray([])
        let pBookmarks = ko.observableArray([])
        
        let userid = 6;
        let urlUser = 'http://localhost:5001/api/user/'+window.userIdString;
        const tlistUrl = 'http://localhost:5001/api/tlist/';
        const plistUrl = 'http://localhost:5001/api/plist/';
        

        const myHeaders = new Headers({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + window.tokenString
        });
        console.log(window.tokenString);

        /*  FETCH USER INFO  */
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
        
        function getListData(){
            if(window.listValue.indexOf('t')>-1) {
                let listId = window.listValue.substring(1);
                console.log("listid: "+listId);
                console.log(tlistUrl+listId);
                fetch(tlistUrl + listId, {
                    method: 'GET',
                    headers: myHeaders
                  })
                    .then((response) => {
                        return response.json()
                    })
                    .then((data) => {
                        tListData(data.titleList)
                        tBookmarks(data.tbookmarkDtos)
                        // DELETE BOOKMARK FROM LIST
                        $('.deleteBookmarkTitle.btn.btn-warning').on('click', function() {
                            if (window.listValue.indexOf('t') > -1) {
                                let listId = window.listValue.substring(1);
                                let bookmarkId = $(this).val();
                                console.log("bookmarkID: " + bookmarkId);
                                console.log("window val: " + window.listValue);
                                console.log("URL: " + 'http://localhost:5001/api/tlist/' + listId + '/' + bookmarkId)
                                $.ajax({
                                    type: 'DELETE',
                                    url: 'http://localhost:5001/api/tlist/' + window.userIdString + '/' + listId + '/' + bookmarkId,
                                    headers: {Authorization: 'Bearer ' + window.tokenString},
                                    success: function (result) {
                                        if (result) {
                                            alert("Bookmark has been removed")
                                            getListData()
                                        } else {
                                            alert("Something went wrong!")
                                        }
                                    }
                                })
                            }
                        });
                    }).catch((err) => {
                })
            }
            if(window.listValue.indexOf('p')>-1) {
                let listId = window.listValue.substring(1);
                console.log("listid: "+listId);
                console.log(plistUrl+listId);
                fetch(plistUrl + listId, {
                    method: 'GET',
                    headers: myHeaders
                })
                    .then((response) => {
                        return response.json()
                    })
                    .then((data) => {
                        pListData(data.plistDto)
                        pBookmarks(data.pbookmarkDtos)
                        // DELETE BOOKMARK FROM LIST
                        $('.deleteBookmarkPerson.btn.btn-warning').on('click', function(){
                            if(window.listValue.indexOf('p')>-1) {
                                let listId = window.listValue.substring(1);
                                let bookmarkId = $(this).val();
                                console.log("bookmarkID: " + bookmarkId);
                                console.log("window val: " + window.listValue);
                                console.log("URL: " + 'http://localhost:5001/api/plist/'+ window.userIdString + '/' + listId + '/' + bookmarkId)
                                $.ajax({
                                    type: 'DELETE',
                                    url: 'http://localhost:5001/api/plist/'+ window.userIdString + '/' + listId + '/' + bookmarkId,
                                    headers: {Authorization: 'Bearer ' + window.tokenString},
                                    success: function (result) {
                                        if (result) {
                                            alert("Bookmark has been removed")
                                            getListData()
                                        } else {
                                            alert("Something went wrong!")
                                        }
                                    }
                                })
                            }
                        });
                    }).catch((err) => {
                })
            }
        }
        getListData()

        function gotoPeoplePage(){
            postman.publish("changeContent", "peoplePage");
        }
        function goToMoviePage(){
            postman.publish("changeContent", "moviePage");
        }
        let goToNewList = () => {
            postman.publish("changeContent", "createlist");
        }
   
        $('#gotonewlist').on('click', function(){
            console.log("Has focus")
            window.value = $(this).val();
            goToNewList()
        })
        
        $(document).on('click', '.goToPeoplePage', function() {
            window.value = $(this).val();
            console.log(window.value)
            gotoPeoplePage()
        });
        $(document).on('click', '.gotomoviepage', function() {
            window.movieValue = $(this).val();
            console.log($(this).val());
            goToMoviePage();
        });
        

        return {
            pListData,
            pBookmarks,
            tListData,
            tBookmarks,
            postman,
            user
        };
    }
});