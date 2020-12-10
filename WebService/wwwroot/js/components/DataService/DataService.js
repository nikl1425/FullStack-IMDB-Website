define([], () => {
    const movieApiUrl = "api/title";
    

    let getJson = (url, callback) => {
        fetch(url).then(response => response.json()).then(callback);
    };

    let getMovies = (url, callback) => {
        if (url === undefined) {
            url = movieApiUrl;
        }
        getJson(url, callback);
    };

    let getMoviesUrlWithPageSize = size => movieApiUrl + "?pageSize=" + size;


    return {
        getMovies,
        getMovie: getJson,
        getMoviesUrlWithPageSize
    };
});