let firstNameAuthor = document.getElementById("firstNameAuthor");
let lastNameAuthor = document.getElementById("lastNameAuthor");
let addButton = document.getElementById("addBtn");


let url = "http://localhost:62683/api/author/addAuthor";

let addAuthor = async() => {

    let token = localStorage.getItem("booksApiToken");

    let author = {
        FirstName: firstNameAuthor.value,
        LastName: lastNameAuthor.value
    };

    let response = await fetch(url, {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(author)
        })
        .then(function(response) {
            response.text()
                .then(function(text) {
                    if (response.status != 201) {
                        window.location.reload();
                        return alert(text);
                    } else {
                        window.location.href = "http://localhost:62683/templates/catalog.html";
                    }
                })
        })
        .catch(function(error) {
            console.log(error);
        })
}

addButton.addEventListener("click", addAuthor);