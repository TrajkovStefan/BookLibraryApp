let userFirstName = $("#firstName");
let userLastName = $("#lastName");
let username = $("#username");
let startDate = $("#startDate");
let endDate = $("#endDate");
let reservationPrice = $("#reservationPrice");
let paymentMethod;
let saveBtn = $("#saveBtn");

let firstName, lastName, userName;
let userId = localStorage.getItem("userId");
let idBookItem = localStorage.getItem("reserveBookId");
let titleBookItem = localStorage.getItem("reserveBookTitle");


$('document').ready(function() {
    firstName = localStorage.getItem("userfName");
    lastName = localStorage.getItem("userlName");
    userName = localStorage.getItem("userusername");

    userFirstName.attr("value", firstName);
    userLastName.attr("value", lastName);
    username.attr("value", userName);

    window.location.reload = "http://localhost:62683/templates/makeReservation.html";
});

startDate.on("change", (e) => {
    let reservationDate = new Date(e.target.value);
    let endReservationDate = new Date();
    if (reservationDate <= endReservationDate) {
        startDate.attr("value", );
        window.location.reload = "http://localhost:62683/templates/makeReservation.html";
        return alert("You can not select past days");
    }
    endReservationDate.setDate(reservationDate.getDate() + 14);
    endDate.attr("value", endReservationDate.toISOString().split('T')[0].replace("/", "-"));
    window.location.reload = "http://localhost:62683/templates/makeReservation.html";
})

$('input[name="flexRadioDefault"]').on("click", function() {
    paymentMethod = $('input[name="flexRadioDefault"]:checked').val();
})

let url = "http://localhost:62683/api/reservation/addReservation";

let addReservation = async() => {

    let token = localStorage.getItem("booksApiToken");
    console.log(paymentMethod);
    console.log("payment");
    let reservation = {
        StartDate: startDate.val(),
        PaymentMethod: parseInt(paymentMethod),
        UserId: parseInt(userId),
        BookId: parseInt(idBookItem)
    };

    let response = await fetch(url, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
            "Access-Control-Allow-Origin": "*",
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(reservation)
    })

    .then(function(response) {
            console.log(response);
            if (response.status != 201) {
                window.location.reload();
                return alert("The reservation cant be created! Try Again!");
            } else {
                window.location = "http://localhost:62683/templates/catalog.html";
            }
        })
        .catch(function(error) {
            console.log(error);
        })
}

saveBtn.click(addReservation);