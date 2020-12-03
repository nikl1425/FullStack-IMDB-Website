define(['ko'], function hej(ko){
  
  let genres = ko.observableArray([]);
 
  
  
  fetch('api/genre')
    .then(function (response){
      return response.json();
    })
    .then(function (data){
      genres(data);
    }).catch(function(error){
      console.log("Server is down")
    })
    

  


    return {
      genres
    };
  
  });
