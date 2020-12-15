define(['knockout', 'postman'], (ko, postman) => {
    return function () {
        console.log(window.value)
        window.personToMoviePage = "";
    
        var personData = ko.observableArray([])
        var professionData = ko.observableArray([])
        var knownForTitlesData = ko.observableArray([])
        let bookmarkList = ko.observableArray([]);
        const url = 'http://localhost:5001/api/name/';
        let urlLists = 'http://localhost:5001/api/user/'+ window.userIdString +'/lists';
    
        const myHeaders = new Headers({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + window.tokenString
        });

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
                    /*   ADD PERSON TO BOOKMARK   */
                    $('.addPersonToBookmark').on('click', function(){
                        let value = $(this).val();
                        if(window.value.includes('nm') && value.includes('p')){
                            let trimVal = value.substring(1);
                            let data = {"person_Id":window.value, "list_Id":trimVal};
                            const json = JSON.stringify(data)
                            console.log("DATA: "+data)
                            $.ajax({
                                type: 'POST',
                                url: 'http://localhost:5001/api/plist/'+trimVal+'/bookmark',
                                headers: {Authorization: 'Bearer '+window.tokenString},
                                dataType: 'json',
                                data: json,
                                contentType: 'application/json',
                                success: function (result) {
                                    if(result) {
                                        alert("The person has been added!")
                                    } else {
                                        alert("Already exists!")
                                    }
                                }
                            })
                        }
                        else {
                            alert("Cannot add to list or is a Title List")
                        }
                    })
                })
                .catch(function(error){
                    console.log("Error: "+error)
                });
        }
        getList();
           
        
    function goToMoviePage(){
            postman.publish("changeContent", "moviePage");
        }
       
            fetch(url + window.value)
                .then((response) => {
                    return response.json()
                })
                .then((data) => {
                    personData(data.personDtos)
                    professionData(data.professionDtos)
                    knownForTitlesData(data.personKnownTitleDtos)
                    
                }).catch((err) => {
            })

        $(document).on('click', '.fromPeopleToMovie', function() {
            window.movieValue = $(this).val();
            console.log(movieValue)
            goToMoviePage()
        });
    
        return {
            personData,
            professionData,
            knownForTitlesData,
            postman,
            bookmarkList
        };
    }
});