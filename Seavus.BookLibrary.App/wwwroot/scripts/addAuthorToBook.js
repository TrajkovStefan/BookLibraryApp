let urlAllAuthors = "http://localhost:62683/api/author";
let authors = document.getElementById("authors");
let saveBtn = document.getElementById("saveBtn");
let h1 = document.getElementById("header");
let bookId = localStorage.getItem("id");
let bookIdTitle = localStorage.getItem("title");

let getAllAuthors = async() => {
    let token = localStorage.getItem("booksApiToken");

    let response = await fetch(urlAllAuthors, {
        method: 'GET',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
            "Access-Control-Allow-Origin": "*",
            'Authorization': `Bearer ${token}`
        }
    });
    let data = await response.json();

    h1.innerHTML += `Add Author To Book ${bookIdTitle}`;
    for (i = 0; i < data.length; i++) {
        authors.innerHTML +=
            `<option value="${data[i].id}">${data[i].firstName} ${data[i].lastName}</option>`
    }
}

getAllAuthors();

let urlAuthorBook = "http://localhost:62683/api/authorbook/addAuthorToBook";

let addAuthorToBook = async() => {

    let token = localStorage.getItem("booksApiToken");

    let authorBook = {
        BookId: parseInt(bookId),
        AuthorId: parseInt(authors.value)
    };

    let response = await fetch(urlAuthorBook, {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(authorBook)
        })
        .then(function(response) {
            console.log(response);
            if (response.status != 201) {
                window.location.reload();
                return alert("The author cant be added to book. Try Again!");
            } else {
                window.location = "http://localhost:62683/templates/catalog.html";
            }
        })
        .catch(function(error) {
            console.log(error);
        })
}

saveBtn.addEventListener("click", addAuthorToBook);