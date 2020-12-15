define(['knockout', 'postman'], (ko, postman) => {
    return function () {
        self = this;
        let userRatingList = ko.observableArray([]);
        let bookmarkList = ko.observableArray([]);
        let user = ko.observableArray([]);
        window.listValue = "";
    
        let urlUser = 'http://localhost:5001/api/user/'+ window.userIdString;
        let urlUpdate = 'http://localhost:5001/api/user/'+ window.userIdString +'/update'
        let urlUpdatePW = 'http://localhost:5001/api/user/'+ window.userIdString +'/changepassword'
        let loggedIn = ko.observableArray([]);
        let urlDelete = 'http://localhost:5001/api/user/'+ window.userIdString +'/delete'

        const myHeaders = new Headers({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + window.tokenString
        });

        window.testidvariable = urlUser;
        console.log(window.testidvariable)
        console.log(window.tokenString)

/*  UPDATE PASSWORD  */
const serialize_form_PW = form => JSON.stringify(
    Array.from(new FormData(form).entries())
        .reduce((m,[key,value]) => Object.assign(m,{[key]: value}),{})
);
$(".updatePWForm").submit(function (e) {
    e.preventDefault();
    const json = serialize_form_PW(this);
    console.log(json);
    $.ajax({
        type: 'POST',
        url: urlUpdatePW,
        dataType: 'json',
        data: json,
        contentType: 'application/json',
        headers: {Authorization: 'Bearer '+window.tokenString},
        success: function (data) {
            if(data) {
                alert("Your password has been updated!");
                $('#updateBtn').modal('hide');
                $('.modal-backdrop').remove();
            } else {
                alert("Either email is in use or smth went wrong!")
            }
        }
    });
});

/*  UPDATE PROFILE  */
const serialize_form = form => JSON.stringify(
    Array.from(new FormData(form).entries())
        .reduce((m,[key,value]) => Object.assign(m,{[key]: value}),{})
);
$(".updateProfile").submit(function (e) {
    e.preventDefault();
    const json = serialize_form(this);
    console.log(json);
    $.ajax({
        type: 'POST',
        url: urlUpdate,
        headers: {Authorization: 'Bearer '+window.tokenString},
        dataType: 'json',
        data: json,
        contentType: 'application/json',
        success: function (data) {
            if(data) {
                alert("Your account has been updated!")
            } else {
                alert("Either email is in use or smth went wrong!")
            }
        }
    });
});


/*  DELETE PROFILE  */
$("#confirmDelete").on('click', function() {
    $.ajax({
        type: 'DELETE',
        url: urlDelete,
        headers: {Authorization: 'Bearer '+window.tokenString},
        success: function (result) {
            if(result) {
                alert("Your account has been deleted!")
                location.replace("http://localhost:5001")
            } else {
                alert("Something went wrong!")
            }
        }
    });
});

return {
    userRatingList,
    bookmarkList,
    user,
    loggedIn,

};
}

});

        