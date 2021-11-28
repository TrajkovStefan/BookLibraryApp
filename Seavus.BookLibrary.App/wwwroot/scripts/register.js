let regBtn = document.getElementById("regBtn");
let firstName = document.getElementById("firstName");
let lastName = document.getElementById("lastName");
let username = document.getElementById("username");
let password = document.getElementById("password");

let register = async() => {

    let url = "http://localhost:62683/api/user/registerUser";
    let user = {
        FirstName: firstName.value,
        LastName: lastName.value,
        Username: username.value,
        Password: password.value
    };

    let response = await fetch(url, {
            method: 'POST',
            mode: "cors",
            headers: {
                'Content-Type': 'application/json',
                "Access-Control-Allow-Origin": "*",
            },
            body: JSON.stringify(user)
        })
        .then(function(response) {
            response.text()
                .then(function(text) {
                    if (response.status != 201) {
                        window.location.reload();
                        return alert(text);
                    } else {
                        window.location.href = "http://localhost:62683/templates/index.html"
                    }
                })

        })
        .catch(function(error) {
            console.log(error);
        });
}
regBtn.addEventListener("click", register);