define(['knockout', 'postman'], (ko, postman) => {
    return function () {
    console.log(window.value)
    window.personToMoviePage = "";

            var personData = ko.observableArray([])
            var professionData = ko.observableArray([])
            var knownForTitlesData = ko.observableArray([])
            const url = 'http://localhost:5001/api/name/';
        
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
            postman
        };
    }
});