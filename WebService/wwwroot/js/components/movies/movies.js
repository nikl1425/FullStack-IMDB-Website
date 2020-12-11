define(['knockout', 'dataservice'], (ko, dataservice) => {
    return function () {
        let movies = ko.observableArray([]);
        let movieList = ko.observableArray([])
        let genres = ko.observableArray([]);
        let pageSizes = ko.observableArray();
        let prev = ko.observableArray();
        let next = ko.observableArray();
        let selectedPage = ko.observableArray([10]);
        self.selectedType = ko.observable();
        let objGenre = ko.observable();
        let movieGenres = ko.observable();
        let types = ko.observableArray([]);
       

        self.selectedType.subscribe(() => {
            objGenre = selectedType().name;
            console.log(objGenre)
            });
        
        let ChangeMovies = () => {
            change = function () {
                fetch('http://localhost:5001/api/title/type/'+objGenre)
                    .then(function (response) {
                        return response.json();
                    })
                    .then(function (data) {
                        movieList(data.movieList)
                        console.log(movieList)
                    })
            };
            change();
        }

       
        

        self.getMovies = function () {
            ko.mapping.fromJS(data.movies, {}, self.movies)
        }

        let getMovies = url => {
            dataservice.getMovies(url, data => {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                movieList(data.movieList);
                movieGenres(movieList.genre)
            });
        }
        
        let getGenres = function () {
            fetch('http://localhost:5001/api/genre')
                .then(function (response) {
                    return response.json();
                })
                .then(function (data) {
                    genres(data);
                    console.log(genres());
                })
        };

        let getTypes = function () {
            fetch('http://localhost:5001/api/type')
                .then(function (response) {
                    return response.json();
                })
                .then(function (data) {
                    types(data);
                    console.log(types());
                  
                })
        };


        // INDSÆT GAMMEL FETCH

        let showPrev = movie => {
            console.log(prev());
            getMovies(prev());
        }

        let enablePrev = ko.computed(() => prev() !== undefined);

        let showNext = product => {
            console.log(next());
            getMovies(next());
        }

        let enableNext = ko.computed(() => next() !== undefined);

        selectedPage.subscribe(() => {
            var size = selectedPage()[0];
            getMovies(dataservice.getMoviesUrlWithPageSize(size));
        });


        document.getElementById("scrolltotop").addEventListener("click", function () {
            console.log("Clicked!");
            $('html,body').animate({scrollTop: $('#scrolltothisdiv').offset().top}, 1000);
        });

        document.getElementById("prevscrolltotop").addEventListener("click", function () {
            console.log("Clicked!");
            $('html,body').animate({scrollTop: $('#scrolltothisdiv').offset().top}, 1000);
        });

        getMovies();
        getGenres();
        getTypes();

        self.optionsAfterRender = function (option, view) {
            if (view.defaultView) {
                option.className = 'defaultViewHighlight';
            }
        };


        return {
            pageSizes,
            selectedPage,
            movies,
            movieList,
            showPrev,
            enablePrev,
            showNext,
            enableNext,
            genres,
            optionsAfterRender,
            selectedType,
            types,
            ChangeMovies
            
            

        };
    }
});