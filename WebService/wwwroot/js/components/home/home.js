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