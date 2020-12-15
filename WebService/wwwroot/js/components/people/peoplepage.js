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
                })
                .catch(function(error){
                    console.log("Error: "+error)
                });
        }
        getList();
        /*   ADD PERSON TO BOOKMARK   */
        $(document).on('click', '.addToBookmark', function(){
            let value = $(this).val();
            if(window.movieValue.includes('nm') && value.includes('p')){
                let trimVal = value.substring(1);
                let data = {"titleId":window.movieValue, "listid":trimVal};
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
                            alert("Cannot add to title list")
                        }
                    }
                })
            }
            else {
                alert("Cannot add to title list")
            }
        })        
        
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