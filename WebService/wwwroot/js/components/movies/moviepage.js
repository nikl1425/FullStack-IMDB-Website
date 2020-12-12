define(['knockout'], (ko) => {
    return function () {
        console.log(window.movieValue)

        let titleData = ko.observableArray([])
        let titleGenreData = ko.observableArray([])
        const url = 'http://localhost:5001/api/title/';

        fetch(url + window.movieValue)
            .then((response) => {
                return response.json()
            })
            .then((data) => {
                titleData(data.titleDto)
                titleGenreData(data.titleGenres)
                console.log(titleData())
                console.log(url + window.movieValue)
            }).catch((err) => {
        })
        
        
        return {
            titleData,
            titleGenreData
        };
    }
});