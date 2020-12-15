require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/knockout/knockout-latest.debug",
        text: "lib/require-text/text.min",
        jquery: "lib/jquery/jquery.min",
        bootstrap: "../css/lib/twitter-bootstrap/js/bootstrap.bundle.min",
        postman: "services/postman",
        slick: "slick/slick",
        dataservice: "components/DataService/DataService"
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

    ko.components.register('people', {
        viewModel: { require: "components/people/people" },
        template: { require: "text!components/people/people.html" }
    });

    ko.components.register('login', {
        viewModel: { require: "components/users/userConfig" },
        template: { require: "text!components/users/login.html" }
    });
    
    ko.components.register('register', {
        viewModel: { require: "components/users/userConfig" },
        template: { require: "text!components/users/register.html" }
    });

    ko.components.register('profile', {
        viewModel: { require: "components/profile/profile" },
        template: { require: "text!components/profile/Profile.html" }
    });

    ko.components.register('about', {
        viewModel: { require: "components/about/about" },
        template: { require: "text!components/about/about.html" }
    });

    ko.components.register('peoplepage', {
        viewModel: { require: "components/people/peoplepage" },
        template: { require: "text!components/people/peoplepage.html" }
    });
    
    ko.components.register('moviepage', {
        viewModel: { require: "components/movies/moviepage" },
        template: { require: "text!components/movies/moviepage.html" }
    });

    ko.components.register('listpage', {
        viewModel: { require: "components/users/userBookmark" },
        template: { require: "text!components/users/listpage.html" }
    });

    ko.components.register('createlist', {
        viewModel: { require: "components/users/userBookmark" },
        template: { require: "text!components/users/createlist.html" }
    });

    ko.components.register('updateuser', {
        viewModel: { require: "components/users/updateuser" },
        template: { require: "text!components/users/updateuser.html" }
    });
});

require(['knockout', 'viewModel',  'bootstrap'], function(ko, vm) {
    ko.applyBindings(vm);
});