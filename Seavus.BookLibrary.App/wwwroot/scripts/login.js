let usernameInput = document.getElementById("username");
let passwordInput = document.getElementById("password");
let loginBtn = document.getElementById("loginBtn");
let butttons = document.getElementsByClassName("buttons");

let login = async() => {
    let admin = {
        Username: usernameInput.value,
        Password: passwordInput.value
    };

    let response = await fetch("http://localhost:62683/api/admin/loginAdmin", {
            method: 'POST',
            mode: "cors",
            headers: {
                'Content-type': 'application/json',
                "Access-Control-Allow-Origin": "*",
            },
            body: JSON.stringify(admin)
        })
        .then(function(response) {
            console.log(response);
            if (response.status != 200) {
                window.location.reload();
                response.text()
                    .then(function(text) {
                        return alert(text);
                    })
            }
            response.text()
                .then(function(text) {
                    localStorage.setItem("booksApiToken", text);
                    localStorage.setItem("userusername", usernameInput.value);
                    window.location.href = "http://localhost:62683/templates/index.html"
                })
        })
        .catch(function(err) {
            console.log(err);
        })
}

loginBtn.addEventListener("click", login);