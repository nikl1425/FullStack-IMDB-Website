define(['knockout', 'postman'], (ko, postman) => {
    return function () {

        let goToProfilePage = () => {
            postman.publish("changeContent", "profile");
        }
        let goToRegisterPage = () => {
            postman.publish("changeContent", "register");
        }
        let goToLoginPage = () => {
            postman.publish("changeContent", "login");
        }
        $("#gotoregisterpage").on('click', function(){
            goToRegisterPage();
        })
        $("#gotoforgotpw").on('click',function (){
            alert("TBA - contact the admin")
        })
        $("#gotologinpage").on('click', function(){
            goToLoginPage();
        })
        
window.tokenString = "";

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
            console.log($(this).val());
            $.ajax({
                type: 'POST',
                url: 'http://localhost:5001/api/user/login',
                dataType: 'json',
                data: json,
                contentType: 'application/json',
                success: function (data) {
                    if(data) {
                        alert("You are now logged in!")
                        console.log(data.tokenStr)
                        window.tokenString = data.tokenStr;
                        goToProfilePage();
                        //location.replace("http://localhost:5001")
                    } else {
                        alert("Username or password is incorrect!")
                    }
                }
            });
        });


        return {

        };
    }
});