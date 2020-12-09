define(['knockout'], (ko) => {
  return function () {
      names = [];
      let obj = [{}];

    fetch('http://example.com/movies.json')
        .then(function (response) {
          return response.json();
        })
        .then(function (data) {
          names(data)
        })
        .then(data => obj = data);

    return {

    };
  }
});