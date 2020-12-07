fetch('http://localhost:5001/api/name/')
    .then(function (response) {
        return response.json();
    })
    .then(function (data) {
        appendData(data);
    })
    .catch(function (err) {
        console.log('error: ' + err);
    });

function appendData(data) {
    var mainContainer = document.getElementById("myData");
    for (var i = 0; i < data.length; i++) {
        var div = document.createElement("div");
        div.innerHTML = '<a href="' + data[i].url + '">' + 'Name: ' + data[i].name + '</a>';
        mainContainer.appendChild(div);
        
        
    }
}