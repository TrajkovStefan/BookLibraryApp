let urlAuthor = "http://localhost:62683/api/author";
let firstNameAuthor = $("#firstNameAuthor");
let lastNameAuthor = $("#lastNameAuthor");
let updateButton = $("#updateBtn");

let authorId, firstName, lastName;
authorId = localStorage.getItem("idAuthor");

$('document').ready(function() {
    firstName = localStorage.getItem("fName");
    lastName = localStorage.getItem("lName");

    firstNameAuthor.attr("value", firstName);
    lastNameAuthor.attr("value", lastName);

    window.location.reload = "http://localhost:62683/templates/updateAuthor.html";
});

let url = "http://localhost:62683/api/author/updateAuthor";

let updateAuthor = async() => {

    let token = localStorage.getItem("booksApiToken");

    let author = {
        Id: parseInt(authorId),
        FirstName: firstNameAuthor.val(),
        LastName: lastNameAuthor.val()
    };

    let response = await fetch(url, {
        method: 'PUT',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
            "Access-Control-Allow-Origin": "*",
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(author)
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

updateButton.click(updateAuthor);