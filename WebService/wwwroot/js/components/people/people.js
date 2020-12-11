define(['knockout'], (ko) => {
    return function () {
    console.log(window.value)
        
            let personData = ko.observableArray([])
            const url = 'http://localhost:5001/api/name/';
            
            fetch(url + window.value)
                .then((response) => {
                    return response.json()
                })
                .then((data) => {
                    personData(data.personDtos)
                    console.log(personData())
                }).catch((err) => {
            })
        
        
        return {
            personData
        };
    }
});