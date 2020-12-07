require.config({
    baseUrl: "js",
    paths: {
        ko: "lib/node_modules/knockout/build/output/knockout-latest"
    }
});

require(['ko', 'userRating'],
    function (ko, userRating){
        ko.applyBindings(userRating);
    });


