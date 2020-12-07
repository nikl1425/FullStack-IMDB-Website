require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/knockout/knockout-latest.debug",
        text: "lib/require-text/text.min",
        jquery: "lib/jquery/jquery.min",
        bootstrap: "../css/lib/twitter-bootstrap/js/bootstrap.bundle.min",
        postman: "services/postman"
    },
    shim: {
        bootstrap: ['jquery']
    }
});



require(['knockout', 'text'], (ko) => {
    ko.components.register('home', {
        viewModel: { require: "components/home/home" },
        template: { require: "text!components/home/home.html" }
    });

    ko.components.register('movie', {
        viewModel: { require: "components/movies/movies" },
        template: { require: "text!components/movies/movies.html" }
    });
});

require(['knockout', 'viewModel',  'bootstrap'], function(ko, vm) {
    ko.applyBindings(vm);
});
