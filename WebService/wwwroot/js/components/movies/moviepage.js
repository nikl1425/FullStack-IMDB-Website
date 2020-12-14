define(['knockout', 'postman'], (ko, postman) => {
    return function () {
        console.log(window.movieValue)

        let titleData = ko.observableArray([])
        let titleGenreData = ko.observableArray([])
        let titlePersonData = ko.observableArray([])
        let similarTitles = ko.observableArray([])
        let alternativeTitle = ko.observableArray([]);
        const url = 'http://localhost:5001/api/title/';

        function gotoPeoplePage(){
            postman.publish("changeContent", "peoplePage");
        }

   

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
                console.log(titleData())
                console.log(url + window.movieValue)
            }).catch((err) => {
        })


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
            alternativeTitle
        };
    }
});