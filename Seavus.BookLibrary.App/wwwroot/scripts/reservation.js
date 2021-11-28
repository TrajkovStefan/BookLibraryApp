let cardsDiv = document.getElementById("cardsDiv");
let searchBtn = document.getElementById("searchBtn");
let searchInputByTitle = document.getElementById("searchInputBook");
let searchInputByGenre = document.getElementById("searchInputGenre");
let searchInputByAuthor = document.getElementById("searchInputAuthor");
let userUsername = localStorage.getItem("userusername");

$('document').ready(function() {
    let token = localStorage.getItem("booksApiToken");
    $.ajax({
        type: "GET",
        url: "http://localhost:62683/api/user/" + userUsername,
        headers: { 'Authorization': `Bearer ${token}` },
        success: function(data) {
            let firstName = localStorage.setItem("userfName", data.firstName);
            let lastName = localStorage.setItem("userlName", data.lastName);
            let userId = localStorage.setItem("userId", data.id);
        }
    })

    window.location.reload = "http://localhost:62683/templates/reservation.html";
});

let getBooks = async() => {
    let token = localStorage.getItem("booksApiToken");
    let response = await fetch(`http://localhost:62683/api/book/searchBookByInput?title=${searchInputByTitle.value}&genre=${searchInputByGenre.value}&author=${searchInputByAuthor.value}`, {
        method: 'GET',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
            "Access-Control-Allow-Origin": "*",
            'Authorization': `Bearer ${token}`
        }
    })
    cardsDiv.innerHTML = "";
    if (response.status != 200) {
        response.text()
            .then(function(text) {
                cardsDiv.innerHTML = "";
                searchInputByTitle.value = "";
                searchInputByGenre.value = "";
                searchInputByAuthor.value = "";
                return cardsDiv.innerHTML +=
                    `<h2>${text}</h2>`
            })
    }
    let data = await response.json()
        .then(function(data) {
            console.log(data);
            cardsDiv.innerHTML = "";
            console.log(data);
            for (i = 0; i < data.length; i++) {
                cardsDiv.innerHTML +=
                    `<div class="card" style="width: 18rem;">
            <div class="card-body">
              <h5 class="card-title">${data[i].title}</h5>
              <p class="card-text">${data[i].description}</p>
              <p class="card-text">${data[i].authors}</p>
              <p class="card-text">${data[i].genre}</p>
              <a class='btn btn-warning btn-sm' onclick=getBookById('${data[i].id}')>Reserve</a>
            </div>
          </div>`
            }
            searchInputByTitle.value = "";
            searchInputByGenre.value = "";
            searchInputByAuthor.value = "";
        })
        .catch(function(error) {
            console.log(error);
        })
}

//*******GET BOOK BY ID FOR UPDATE*******

function getBookById(id) {
    let token = localStorage.getItem("booksApiToken");
    $.ajax({
        type: "GET",
        url: "http://localhost:62683/api/book/" + id,
        headers: { 'Authorization': `Bearer ${token}` },
        success: function(data) {
            let bookId = localStorage.setItem("reserveBookId", data.id);
            let titleBook = localStorage.setItem("reserveBookTitle", data.title);
            window.location.href = "http://localhost:62683/templates/makeReservation.html"
        }
    })
}

searchBtn.addEventListener("click", getBooks);