define(['knockout'], (ko) => {
    return function () {
    console.log(window.value)
        
            var personData = ko.observableArray([])
            var professionData = ko.observableArray([])
            var knownForTitlesData = ko.observableArray([])
            const url = 'http://localhost:5001/api/name/';

       
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

        console.log(personData())
        console.log(professionData())
        console.log(knownForTitlesData())
        console.log("hej");
        
        return {
            personData,
            professionData,
            knownForTitlesData
        };
    }
});