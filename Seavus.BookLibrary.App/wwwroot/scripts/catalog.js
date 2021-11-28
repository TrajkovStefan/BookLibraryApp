//*******DATATABLE WITH BOOKS******* 

let url = "http://localhost:62683/api/book";
let tab = document.getElementById("theadTable");
let bookHeader = document.getElementById("bookHeader");
let addBookSpan = document.getElementById("addNewBook");
let dataTableBook = $('#bookTable');

$(document).ready(function() {
    let username = localStorage.getItem("userusername");
    if (username != "petko123") {
        $("#getBtn").hide();
        $("#getBtnAuthors").hide();
        $("#getBtnReservations").hide();
        $("#myUsers").hide();
    } else {
        $("#getBtn").show();
        $("#getBtnAuthors").show();
        $("#getBtnReservations").show();
        $("#myUsers").show();
    }
});

$('#getBtn').click(function() {
    addBookSpan.innerHTML = `<a href="../templates/addBook.html"><button class="btn btn-success">Add Book</button></a>`;
    bookHeader.innerHTML = "All Books";
    tab.innerHTML = `<tr>
    <th scope="col">Title</th>
    <th scope="col">Authors</th>
    <th scope="col">Genre</th>
    <th scope="col">Description</th>
    <th scope="col">Options</th>
    </tr>`

    let token = localStorage.getItem("booksApiToken");
    dataTableBook.DataTable({
        ajax: {
            url: url,
            dataSrc: "",
            headers: { 'Authorization': `Bearer ${token}` },
        },
        columns: [{
                data: "title"
            },
            {
                data: "authors"
            },
            {
                data: "genre"
            },
            {
                data: "description"
            },
            {
                data: "id",
                "render": function(data) {
                    return `<a class='btn btn-warning btn-sm' style'margin-left:5px' onclick=getBookById('${data}')>Edit</a> <a class='btn btn-danger btn-sm' style'margin-left:5px' onclick=DeleteBook('${data}')>Delete</a>`
                }
            }
        ]
    });
});

//*******GET BOOK BY ID FOR UPDATE*******

function getBookById(id) {
    let token = localStorage.getItem("booksApiToken");
    $.ajax({
        type: "GET",
        async: true,
        url: "http://localhost:62683/api/book/" + id,
        headers: { 'Authorization': `Bearer ${token}` },
        success: function(data) {
            let id = localStorage.setItem("id", data.id);
            let title = localStorage.setItem("title", data.title);
            let numOfCopies = localStorage.setItem("copies", data.numOfCopies);
            let genre = localStorage.setItem("genre", data.genre);
            let status = localStorage.setItem("status", data.status);
            window.location.href = "http://localhost:62683/templates/updateBook.html"
        }
    })
}

//*******DELETE BOOK*******

function DeleteBook(id) {
    let token = localStorage.getItem("booksApiToken");
    if (confirm('Are you sure to delete this book?')) {
        $.ajax({
            type: "DELETE",
            url: "http://localhost:62683/api/book/" + id,
            headers: { 'Authorization': `Bearer ${token}` },
            success: function() {
                dataTableBook.DataTable().ajax.reload();
            }
        })
    }
}

//*******DATATABLE WITH AUTHORS*******

let urlAuthor = "http://localhost:62683/api/author";
let tabAuthor = document.getElementById("theadTableAuthor");
let authorHeader = document.getElementById("authorHeader");
let addAuhtorSpan = document.getElementById("addNewAuthor");
let dataTableAuthor = $('#bookTableAuthor');


$("#getBtnAuthors").click(function() {
    addAuhtorSpan.innerHTML = `<a href="../templates/addAuthor.html"><button class="btn btn-success">Add Author</button></a>`;
    authorHeader.innerHTML = "All Authors";
    tabAuthor.innerHTML = `<tr>
          <th scope="col">FirstName</th>
          <th scope="col">LastName</th>
          <th scope="col">Books</th>
          <th scope="col">Options</th>
          </tr>`

    let token = localStorage.getItem("booksApiToken");
    dataTableAuthor.DataTable({
        ajax: {
            url: urlAuthor,
            dataSrc: "",
            headers: { 'Authorization': `Bearer ${token}` },
        },
        columns: [{
                data: "firstName"
            },
            {
                data: "lastName"
            },
            {
                data: "books"
            },
            {
                data: "id",
                "render": function(data) {
                    return `<a class='btn btn-warning btn-sm' style'margin-left:5px' onclick=getAuthorById('${data}')>Edit</a>  <a class='btn btn-danger btn-sm' style'margin-left:5px' onclick=DeleteAuthor('${data}')><i class='fa fa-trash'></i>Delete</a>`
                }
            }
        ]
    });
});

//*******DELETE AUTHOR*******

function DeleteAuthor(id) {
    let token = localStorage.getItem("booksApiToken");
    if (confirm('Are you sure to delete this author?')) {
        $.ajax({
            type: "DELETE",
            url: "http://localhost:62683/api/author/" + id,
            headers: { 'Authorization': `Bearer ${token}` },
            success: function() {
                dataTableAuthor.DataTable().ajax.reload();
            }
        })
    }
}

//*******GET AUTHOR BY ID FOR UPDATE*******

function getAuthorById(id) {
    let token = localStorage.getItem("booksApiToken");
    $.ajax({
        type: "GET",
        url: "http://localhost:62683/api/author/" + id,
        headers: { 'Authorization': `Bearer ${token}` },
        success: function(data) {
            let id = localStorage.setItem("idAuthor", data.id);
            let firstName = localStorage.setItem("fName", data.firstName);
            let lastName = localStorage.setItem("lName", data.lastName);
            window.location.href = "http://localhost:62683/templates/updateAuthor.html"
        }
    })
}

//*******DATATABLE WITH ALL RESERVATIONS*******

let urlReservation = "http://localhost:62683/api/reservation";
let tabReservation = document.getElementById("theadTableReservation");
let reservationHeader = document.getElementById("reservationHeader");
let dataTableReservation = $('#reservationTable');

$("#getBtnReservations").click(function() {
    reservationHeader.innerHTML = "All Reservations";
    tabReservation.innerHTML = `<tr>
          <th scope="col">User First Name</th>
          <th scope="col">User Last Name</th>
          <th scope="col">Book</th>
          <th scope="col">Start Date</th>
          <th scope="col">End Date</th>
          <th scope="col">Payment Method</th>
          </tr>`

    let token = localStorage.getItem("booksApiToken");
    dataTableReservation.DataTable({
        ajax: {
            url: urlReservation,
            dataSrc: "",
            headers: { 'Authorization': `Bearer ${token}` },
        },
        columns: [{
                data: "userFirstName"
            },
            {
                data: "userLastName"
            },
            {
                data: "bookTitle"
            },
            {
                data: "startDate"
            },
            {
                data: "endDate"
            },
            {
                data: "paymentMethod"
            }
        ]
    });
});


//*******DATATABLE WITH LOGGED USER RESERVATIONS*******

let urluserReservations = "http://localhost:62683/api/reservation/";
let userReservationHeader = document.getElementById("userReservationsHeader");
let userDataTableReservation = $('#userReservationTable');
let userTabReservation = document.getElementById("userTheadTableReservation");
let h3 = document.getElementById("header3");


$("#myReservations").click(function() {
    let username = localStorage.getItem("userusername");
    userReservationHeader.innerHTML = "My Reservations";
    userTabReservation.innerHTML = `<tr>
          <th scope="col">Book</th>
          <th scope="col">Start Date</th>
          <th scope="col">End Date</th>
          <th scope="col">Payment Method</th>
          <th scope="col">Options</th>
          </tr>`
    let token = localStorage.getItem("booksApiToken");
    userDataTableReservation.DataTable({
        ajax: {
            url: urluserReservations + username,
            dataSrc: "",
            headers: { 'Authorization': `Bearer ${token}` },
        },
        columns: [{
                data: "bookTitle"
            },
            {
                data: "startDate"
            },
            {
                data: "endDate"
            },
            {
                data: "paymentMethod"
            },
            {
                data: "id",
                "render": function(data) {
                    return `<a class='btn btn-warning btn-sm' onclick=returnBook('${data}')>Return Book</a>`
                }
            }
        ]
    });
});

//*******RETURN BOOK*******

function returnBook(id) {
    let token = localStorage.getItem("booksApiToken");
    if (confirm('Are you sure to return this book?')) {
        $.ajax({
            type: "DELETE",
            url: "http://localhost:62683/api/reservation/" + id,
            headers: { 'Authorization': `Bearer ${token}` },
            success: function() {
                userDataTableReservation.DataTable().ajax.reload();
            }
        })
    }
}


//*******DATATABLE WITH ALL USERS*******

let urlAllUsers = "http://localhost:62683/api/user";
let tabUsers = document.getElementById("usersThead");
let userHeader = document.getElementById("headerAllUsers");
let dataTableAllUsers = $('#usersTable');

$("#myUsers").click(function() {
    userHeader.innerHTML = "All Users";
    tabUsers.innerHTML = `<tr>
          <th scope="col">First Name</th>
          <th scope="col">Last Name</th>
          <th scope="col">Username</th>
          <th scope="col">Options</th>
          </tr>`

    let token = localStorage.getItem("booksApiToken");
    dataTableAllUsers.DataTable({
        ajax: {
            url: urlAllUsers,
            dataSrc: "",
            headers: { 'Authorization': `Bearer ${token}` },
        },
        columns: [{
                data: "firstName"
            },
            {
                data: "lastName"
            },
            {
                data: "username"
            },
            {
                data: "id",
                "render": function(data) {
                    return `<a class='btn btn-warning btn-sm' style'margin-left:5px' onclick=getUserById('${data}')>Edit</a>  <a class='btn btn-danger btn-sm' style'margin-left:5px' onclick=DeleteUser('${data}')><i class='fa fa-trash'></i>Delete</a>`
                }
            }
        ]
    });
});


//*******DELETE USER*******

function DeleteUser(id) {
    let token = localStorage.getItem("booksApiToken");
    if (confirm('Are you sure to delete this user?')) {
        $.ajax({
            type: "DELETE",
            url: "http://localhost:62683/api/user/" + id,
            headers: { 'Authorization': `Bearer ${token}` },
            statusCode: {
                400: function() {
                    return alert("User has reservations. Cannot be deleted!");
                }
            },
            success: function() {
                dataTableAllUsers.DataTable().ajax.reload();
            }
        })

    }
}

//*******GET USER BY ID FOR UPDATE*******

function getUserById(id) {
    let token = localStorage.getItem("booksApiToken");
    $.ajax({
        type: "GET",
        url: "http://localhost:62683/api/user/userId/" + id,
        headers: { 'Authorization': `Bearer ${token}` },
        success: function(data) {
            localStorage.setItem("idUser", data.id);
            localStorage.setItem("fNameUser", data.firstName);
            localStorage.setItem("lNameUser", data.lastName);
            localStorage.setItem("userName", data.username);
            localStorage.setItem("pass", data.password);
            window.location.href = "http://localhost:62683/templates/updateUser.html"
        },
        error: function(err) {
            console.log(err);
        }
    })
}