define(['knockout', 'postman'], (ko, postman) => {
    return function () {
        console.log(window.movieValue)
        window.value = "";

        let titleData = ko.observableArray([])
        let titleGenreData = ko.observableArray([])
        let titlePersonData = ko.observableArray([])
        let similarTitles = ko.observableArray([])
        let alternativeTitle = ko.observableArray([]);
        let bookmarkList = ko.observableArray([]);

        const url = 'http://localhost:5001/api/title/';
        let urlLists = 'http://localhost:5001/api/user/'+ window.userIdString +'/lists';

        const myHeaders = new Headers({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + window.tokenString
        });

        function gotoPeoplePage(){
            postman.publish("changeContent", "peoplePage");
        }
        
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
                    /*   ADD MOVIE TO BOOKMARK   */
                    $('.addToBookmark.info-text').on('click', function(){
                        let value = $(this).val();
                        if(window.movieValue.includes('tt') && value.includes('t')){
                            let trimVal = value.substring(1);
                            let data = {"titleId":window.movieValue, "listid":trimVal};
                            const json = JSON.stringify(data);
                            console.log("DATA: "+data)
                            $.ajax({
                                type: 'POST',
                                url: 'http://localhost:5001/api/tlist/'+trimVal+'/bookmark',
                                headers: {Authorization: 'Bearer '+window.tokenString},
                                dataType: 'json',
                                data: json,
                                contentType: 'application/json',
                                success: function (result) {
                                    if(result){
                                        alert("The movie has been added!")
                                    } else {
                                        alert("Movie already exists in list!")
                                    }
                                }
                            })
                        }
                        else {
                            alert("Cannot add to list or is a Person List")
                        }
                    })
                })
                .catch(function(error){
                    console.log("Error: "+error)
                });
        }
        getList();

   
        function getMovieData(){
            fetch(url + window.movieValue)
                .then((response) => {
                    return response.json()
                })
                .then((data) => {
                    titleData(data.titleDto)
                    titleGenreData(data.titleGenres)
                    titlePersonData(data.titlePersons)
                    similarTitles(data.limitedEpisodes)
                    alternativeTitle(data.titleAkases)
                    $(".updateRating").on('click', function(){
                        let titleid = window.movieValue;//$(this).val();
                        let rating = $(this).text();
                        $.ajax({
                            type: 'POST',
                            url: 'http://localhost:5001/api/title/'+titleid+'/RateMovie/'+window.userIdString+'/'+rating,
                            headers: {Authorization: 'Bearer '+window.tokenString},
                            success: function (result) {
                                if(result) {
                                    alert("You have rated this movie"+rating+"!")
                                    getMovieData();
                                } else {
                                    alert("Something went wrong!")
                                }
                            }
                        })
                    });
                    console.log(titleData())
                    console.log(url + window.movieValue)
                }).catch((err) => {
            })
        }
        getMovieData();
        
        $(document).on('click', '.goToPeoplePage', function() {
            window.value = $(this).val();
            console.log(movieValue)
            gotoPeoplePage()
        });

        $(document).on('click', '.goFromMovieToMovie', function() {
            window.movieValue = $(this).val();
            console.log(movieValue)
            fetch(url + window.movieValue)
                .then((response) => {
                    return response.json()
                })
                .then((data) => {
                    titleData(data.titleDto)
                    titleGenreData(data.titleGenres)
                    titlePersonData(data.titlePersons)
                    similarTitles(data.limitedEpisodes)
                    console.log(titleData())
                    console.log(url + window.movieValue)
                }).catch((err) => {
            })
        });
        
        return {
            titleData,
            titleGenreData,
            titlePersonData,
            similarTitles,
            postman,
            alternativeTitle,
            bookmarkList
        };
    }
});