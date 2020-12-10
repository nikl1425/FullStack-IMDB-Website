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

        let getData = url => {
            dataservice.getMovies(url, data => {
                pageSizes(data.pageSizes);
                prev(data.prev || undefined);
                next(data.next || undefined);
                movieList(data.movieList);

            });
        }


        // INDSÆT GAMMEL FETCH

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

        document.getElementById("scrolltotop").addEventListener("click", function() {
            console.log("Clicked!");
            $('html,body').animate({ scrollTop: $('#scrolltothisdiv').offset().top }, 1000);
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
            enableNext,
            scrolltotop
            
        };
    }
});