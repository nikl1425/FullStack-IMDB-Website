define(['knockout', 'postman'], (ko, postman) => {
    return function () {

        let goToProfilePage = () => {
            postman.publish("changeContent", "profile");
        }


        const serialize_form = form => JSON.stringify(
            Array.from(new FormData(form).entries())
                .reduce((m,[key,value]) => Object.assign(m,{[key]: value}),{})
        );
        $(".api_register_form").submit(function (e) {
            e.preventDefault();
            const json = serialize_form(this);
            console.log(json);
            $.ajax({
                type: 'POST',
                url: 'http://localhost:5001/api/user/register',
                dataType: 'json',
                data: json,
                contentType: 'application/json',
                success: function (data) {
                    if(data) {
                        alert("Your account has been created!")
                        goToProfilePage();
                        //location.replace("http://localhost:5001")
                    } else {
                        alert("Either username is taken or email is in use!")
                    }
                }
            });
        });

        const serialize_form_login = form => JSON.stringify(
            Array.from(new FormData(form).entries())
                .reduce((m,[key,value]) => Object.assign(m,{[key]: value}),{})
        );
        $(".api_login_form").submit(function (e) {
            e.preventDefault();
            const json = serialize_form_login(this);
            console.log(json);
            $.ajax({
                type: 'POST',
                url: 'http://localhost:5001/api/login',
                dataType: 'json',
                data: json,
                contentType: 'application/json',
                success: function (data) {
                    if(data) {
                        alert("Your account has been created!")
                        goToProfilePage();
                        //location.replace("http://localhost:5001")
                    } else {
                        alert("Either username is taken or email is in use!")
                    }
                }
            });
        });


        return {

        };
    }
});