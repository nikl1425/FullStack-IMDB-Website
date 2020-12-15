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
        let newTListUrl = 'http://localhost:5001/api/user/'+window.userIdString+'/tlist/create'
        let newPListUrl = 'http://localhost:5001/api/user/'+window.userIdString+'/plist/create'

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
                    //console.log("TLISTDATA: "+data.titleList)
                    //console.log("TBOOKMARKS: "+data.tbookmarkDtos)
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
                }).catch((err) => {
            })
        }

        function gotoPeoplePage(){
            postman.publish("changeContent", "peoplePage");
        }
        function goToMoviePage(){
            postman.publish("changeContent", "moviePage");
        }
        let goToNewList = () => {
            postman.publish("changeContent", "createlist");
        }
        let goToProfilePage = () => {
            postman.publish("changeContent", "profile");
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
                        console.log("we r sending.... plist"+x)
                        alert("Your title list has been created!")
                        goToProfilePage();
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
                        console.log("we r sending.... plist")
                        if(data) {
                            alert("Your person list has been created!")
                            goToProfilePage();
                        }
                    }
                });
            } else {
                alert("Please select a type of list.")
            }
        });
        
        $(".deleteBookmark").on('click', function(){
            let bookmarkId = $(this).val();
            $.ajax({
                type: 'DELETE',
                url: 'http://localhost:5001/api/tlist/'+window.value+'/'+bookmarkId,
                headers: {Authorization: 'Bearer '+window.tokenString},
                success: function (result) {
                    if(result) {
                        alert("Bookmark has been removed")
                        getUserInfo()                        
                    } else {
                        alert("Something went wrong!")
                    }
                }
            })
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