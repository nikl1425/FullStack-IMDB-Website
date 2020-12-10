define(['knockout'], (ko) => {
  return function () {
      let movies = ko.observableArray([]);
      
      self.getMovies = function (){
          ko.mapping.fromJS(data.movies,{}, self.movies)
      }

    fetch('http://localhost:5001/api/title/movies')
        .then(function (response) {
          return response.json();
        })
        .then(function (data) {
          movies(data);
          console.log(movies())
        })
       
        
    
    
    
    


    return {
          movies

    };
  }
});