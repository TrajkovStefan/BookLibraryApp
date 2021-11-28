let titleBookUpdate = $("#titleBook");
let numberOfCopiesUpdate = $("#numOfCopies");
let genreValueUpdate = $("#genreBook");
let statusValueUpdate = $("#statusBook");
let updateBtn = $("#updateBtn");

let idBookItem, titleItem, numOfCopiesItem, genreItem, statusItem;
idBookItem = localStorage.getItem("id");

$('document').ready(function() {
    titleItem = localStorage.getItem("title");
    numOfCopiesItem = localStorage.getItem("copies");
    genreItem = localStorage.getItem("genre");
    statusItem = localStorage.getItem("status");

    titleBookUpdate.attr("value", titleItem);
    numberOfCopiesUpdate.attr("value", numOfCopiesItem);
    if (genreItem == "1") {
        $("select:first option:nth-child(1)").text("Action").attr('selected', 'selected');
        genreValueUpdate.attr("value", genreItem);
    }
    if (genreItem == "2") {
        $("select:first option:nth-child(2)").text("Comedy").attr('selected', 'selected');
        genreValueUpdate.attr("value", genreItem);
    }
    if (genreItem == "3") {
        $("select:first option:nth-child(3)").text("Horror").attr('selected', 'selected');
        genreValueUpdate.attr("value", genreItem);
    }
    if (genreItem == "4") {
        $("select:first option:nth-child(4)").text("Romantic").attr('selected', 'selected');
        genreValueUpdate.attr("value", genreItem);
    }

    if (statusItem.toLowerCase() == "true") {
        $("select:last option:nth-child(1)").text("True").attr('selected', 'selected');
        statusValueUpdate.attr("value", statusItem);
    }
    if (statusItem.toLowerCase() == "false") {
        $("select:last option:nth-child(2)").text("False").attr('selected', 'selected');
        statusValueUpdate.attr("value", statusItem);
    }

    window.location.reload = "http://localhost:62683/templates/updateBook.html";
});


let urlforUpdate = "http://localhost:62683/api/book/updateBook";

let updateBook = async() => {

    let token = localStorage.getItem("booksApiToken");

    let book = {
        Id: parseInt(idBookItem),
        Title: titleBookUpdate.val(),
        Genre: parseInt(genreValueUpdate.val()),
        Status: statusValueUpdate.val(),
        NumOfCopies: parseInt(numberOfCopiesUpdate.val())
    };

    let response = await fetch(urlforUpdate, {
        method: 'PUT',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
            "Access-Control-Allow-Origin": "*",
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(book)
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

updateBtn.click(updateBook);