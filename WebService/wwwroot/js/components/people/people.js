define(['knockout', 'postman'], (ko, postman) => {
    return function () {
        let people = ko.observableArray([]);


        fetch('http://localhost:5001/api/name')
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                people(data);
                console.log(people())
            })

        function goToPersonPage(){
            postman.publish("changeContent", "peoplePage");
        }

        $(document).on('click', '.fromPersonListToPage', function() {
            window.value = $(this).val();
            console.log(window.value)
            goToPersonPage()
        });
        
        document.getElementById("scrolltotop").addEventListener("click", function () {
            console.log("Clicked!");
            $('html,body').animate({scrollTop: $('#scrolltothisdiv').offset().top}, 1000);
        });

        document.getElementById("prevscrolltotop").addEventListener("click", function () {
            console.log("Clicked!");
            $('html,body').animate({scrollTop: $('#scrolltothisdiv').offset().top}, 1000);
        });

        return {
            people,
            postman

        };
    }
});