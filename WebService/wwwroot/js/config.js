require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/knockout/knockout-latest.debug",
        text: "lib/require-text/text.min",
        jquery: "lib/jquery/jquery.min",
        bootstrap: "../css/lib/twitter-bootstrap/js/bootstrap.bundle.min",
        postman: "services/postman",
        slick: "slick/slick"
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

    ko.components.register('persons', {
        viewModel: { require: "components/persons/person" },
        template: { require: "text!components/persons/persons.html" }
    });

    ko.components.register('create user', {
        viewModel: { require: "components/users/userConfig.js" },
        template: { require: "text!components/movies/movies.html" }
    });

    ko.components.register('profile', {
        viewModel: { require: "components/Profile/Profile" },
        template: { require: "text!components/Profile/Profile.html" }
    });

    ko.components.register('about', {
        viewModel: { require: "components/About/About" },
        template: { require: "text!components/About/About.html" }
    });
});

require(['knockout', 'viewModel',  'bootstrap'], function(ko, vm) {
    ko.applyBindings(vm);
});
