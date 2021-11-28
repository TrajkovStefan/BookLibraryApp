let urlUser = "http://localhost:62683/api/author";
let firstNameUser = $("#firstNameUser");
let lastNameUser = $("#lastNameUser");
let username = $("#usernameUser");
let password = $("#passUser");
let updateButton = $("#updateBtn");

let userId, firstName, lastName, userName, pass;
userId = localStorage.getItem("idUser");

$('document').ready(function() {
    firstName = localStorage.getItem("fNameUser");
    lastName = localStorage.getItem("lNameUser");
    userName = localStorage.getItem("userName");
    pass = localStorage.getItem("pass");

    firstNameUser.attr("value", firstName);
    lastNameUser.attr("value", lastName);
    username.attr("value", userName);
    password.attr("value", pass);

    window.location.reload = "http://localhost:62683/templates/updateUser.html";
});

let url = "http://localhost:62683/api/user/updateUser";

let updateUser = async() => {

    let token = localStorage.getItem("booksApiToken");

    let user = {
        Id: parseInt(userId),
        FirstName: firstNameUser.val(),
        LastName: lastNameUser.val(),
        Username: username.val(),
        Password: password.val(),
    };

    let response = await fetch(url, {
        method: 'PUT',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
            "Access-Control-Allow-Origin": "*",
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(user)
    })

    .then(function(response) {
            console.log(response);
            if (response.status != 202) {
                window.location.reload();
                response.text()
                    .then(function(text) {
                        return alert(text);
                    })
            } else {
                window.location = "http://localhost:62683/templates/catalog.html";
            }
        })
        .catch(function(error) {
            console.log(error);
        })
}

updateButton.click(updateUser);