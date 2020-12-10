
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
            alert(data)
        }
    });
});
