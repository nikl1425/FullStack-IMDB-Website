define(['knockout', 'dataservice'], (ko, dataservice) => {
    return function () {
        let movies = ko.observableArray([]);
        let movieList = ko.observableArray([])
        let pageSizes = ko.observableArray();
        let prev = ko.observableArray();
        let next = ko.observableArray();
        let selectedPage = ko.observableArray([10]);

        self.getMovies = function () {
            ko.mapping.fromJS(data.movies, {}, self.movies)
        }

        let getData = function () {
            fetch('http://localhost:5001/api/title')
                .then(function (response) {
                    return response.json();
                })
                .then(function (data) {
                    movies(data);
                    movieList(data.movieList);
                    pageSizes(data.pageSizes);
                    prev(data.prev || undefined);
                    next(data.next || undefined);
                    console.log(movieList());
                })
        };


        let showPrev = movie => {
            console.log(prev());
            getData(prev());
        }

        let enablePrev = ko.computed(() => prev() !== undefined);

        let showNext = product => {
            console.log(next());
            getData(next());
        }

        let enableNext = ko.computed(() => next() !== undefined);

        selectedPage.subscribe(() => {
            var size = selectedPage()[0];
            getData(dataservice.getMoviesUrlWithPageSize(size));
        });
        
        
        
        getData();


        return {
            pageSizes,
            selectedPage,
            movies,
            movieList,
            showPrev,
            enablePrev,
            showNext,
            enableNext

        };
    }
});