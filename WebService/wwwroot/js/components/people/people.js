define(['knockout'], (ko) => {
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

        document.getElementById("scrolltotop").addEventListener("click", function () {
            console.log("Clicked!");
            $('html,body').animate({scrollTop: $('#scrolltothisdiv').offset().top}, 1000);
        });

        document.getElementById("prevscrolltotop").addEventListener("click", function () {
            console.log("Clicked!");
            $('html,body').animate({scrollTop: $('#scrolltothisdiv').offset().top}, 1000);
        });

        return {
            people

        };
    }
});