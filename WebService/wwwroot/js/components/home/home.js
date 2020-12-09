define(['postman'], (postman) => {
    return function () {

        let gotoMovie = () => {
            postman.publish("changeContent", "movie");
        }

        var jsonObj;

        fetch('http://localhost:5001/api/title/topposter')
            .then(function (response) {
                return response.json();
            })
            .then(function (data) {
                const myObjStr = JSON.stringify(eval(data));
                jsonObj = JSON.parse(myObjStr);
                console.log(jsonObj);
                return jsonObj;
            })
            .then(data => obj = data);


        return {
            gotoMovie
        };
    }
});