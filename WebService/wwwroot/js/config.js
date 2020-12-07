require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/node_modules/knockout/build/output/knockout-latest",
        text: "lib/node_modules/require-text/index",
        postman: "services/postman"
    }
});


require(['knockout', 'text'], (ko) => {
    ko.components.register('Home', {
        viewModel: { require: "WebService/wwwroot/js/components/home/home.js" },
        template: { require: "text!WebService/wwwroot/js/components/home/home.html" }
    });
});

require(['knockout', 'text'], (ko) => {
    ko.components.register('Movie', {
        viewModel: { require: "WebService/wwwroot/js/components/movies/movies.js" },
        template: { require: "text!WebService/wwwroot/js/components/movies/movies.html" }
    });
});

require(['knockout', 'viewModel'], function(ko, vm) {
    ko.applyBindings(vm);
});