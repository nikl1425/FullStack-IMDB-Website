define(['knockout'], (ko) => {
    return function () {
        //console.log(window.movieValue)
        console.log(window.personToMoviePage)

        let titleData = ko.observableArray([])
        const url = 'http://localhost:5001/api/title/';

        fetch(url + window.movieValue)
            .then((response) => {
                return response.json()
            })
            .then((data) => {
                titleData(data.titleDto)
                console.log(titleData())
            }).catch((err) => {
        })
        
       

        
        
      
        
        return {
            titleData
        };
    }
});