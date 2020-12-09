define(['knockout'], (ko) => {
  return function () {

    fetch('http://example.com/movies.json')
        .then((response) => {
          return response.json();
        })
        .then((myJson) => {
          console.log(myJson);
        });

    return {

    };
  }
});