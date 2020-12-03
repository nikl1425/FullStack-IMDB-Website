require.config({
    baseUrl: "js",
    paths: {
        ko: "lib/node_modules/knockout/build/output/knockout-latest"
    }
});

require(['ko', 'title'], function(ko, title) {
    ko.applyBindings(title);
});