define(['knockout'], (ko) => {
    return function () {
        let people = ko.observableArray([]);

        self.getMovies = function (){
            ko.mapping.fromJS(data.people,{}, self.people)
        }

        fetch('http://localhost:5001/api/name')
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                people(data);
                console.log(people())
            })

        return {
            people

        };
    }
});