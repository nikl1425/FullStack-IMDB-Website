require.config({
    baseUrl: "js",
    paths: {
        ko: "lib/node_modules/knockout/build/output/knockout-latest"
    }
});

require(['ko', 'userBookmark'], 
    function (ko, userBookmark){
    ko.applyBindings(userBookmark);
});


