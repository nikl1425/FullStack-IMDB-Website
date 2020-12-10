
define(['knockout'], (ko) => {
    return function () {
        let poster = ko.observableArray([]);
        self.getPosters = function () {
            ko.mapping.fromJS(data.poster, {}, self.poster)
        }

        fetch('http://localhost:5001/api/title/topposter')
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                poster(data);
            })
        return {
            poster
        };
    }
});


define(['postman'], (postman) => {
    return function () {

        let gotoMovie = () => {
            postman.publish("changeContent", "movie");
        }
        return {
            gotoMovie
        };
    }
});
