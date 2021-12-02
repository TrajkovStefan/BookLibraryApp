let titleBook = document.getElementById("titleBook");
let numberOfCopies = document.getElementById("numOfCopies");
let genreValue = document.getElementById("genreBook");
let statusValue = document.getElementById("statusBook");
let description = document.getElementById("description");
let fullNameAuthor = document.getElementById("fullNameAuthor");
let addButton = document.getElementById("addBtn");
let fullName;
let firstNameAuthor;
let lastNameAuthor;

$(document).ready(function() {
    let authorsArray = [];
    let token = localStorage.getItem("booksApiToken");
    $.ajax({
        url: "http://localhost:62683/api/author",
        async: true,
        dataType: 'json',
        headers: { 'Authorization': `Bearer ${token}` },
        success: function(data) {
            for (let i = 0; i < data.length; i++) {
                var id = (data[i].id).toString();
                authorsArray.push({ 'value': data[i].firstName + " " + data[i].lastName, 'data': id })
            }
            loadAuthors(authorsArray);
        }
    });

    function loadAuthors(options) {
        $("#fullNameAuthor").autocomplete({
            lookup: options,
            onSelect: function(suggestion) {
                fullName = suggestion.value.split(" ");
                firstNameAuthor = fullName[0];
                lastNameAuthor = fullName[1];
            }
        });
    }
});

fullNameAuthor.addEventListener('change', (e) => {
    const select = e.target;
    const value = select.value;
    fullName = value.split(" ");
    firstNameAuthor = fullName[0];
    lastNameAuthor = fullName[1];
    console.log(firstNameAuthor);
    console.log(lastNameAuthor);
})

let urlBook = "http://localhost:62683/api/book/addBook";

let addBook = async() => {
    let token = localStorage.getItem("booksApiToken");
    let book = {
        Title: titleBook.value,
        Genre: parseInt(genreValue.value),
        Description: description.value,
        Status: statusValue.value,
        NumOfCopies: parseInt(numberOfCopies.value),
        FirstName: firstNameAuthor,
        LastName: lastNameAuthor,
    };
    let response = await fetch(urlBook, {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json',
                "Access-Control-Allow-Origin": "*",
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(book)
        })
        .then(function(response) {
            response.text()
                .then(function(text) {
                    if (response.status != 201) {
                        window.location.reload();
                        return alert(text);
                    } else {
                        window.location = "http://localhost:62683/templates/catalog.html";
                    }
                })
        })
        .catch(function(error) {
            console.log(error);
        })
}

addButton.addEventListener("click", addBook);