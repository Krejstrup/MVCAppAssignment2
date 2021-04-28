"use strict";

var filterBtn = document.getElementById("filterBtn");
var showAllBtn = document.getElementById("allBtn");
var delBtn = document.getElementById("deleteBtn");
var idTexField = document.getElementById("filterText");
var resultContainer = document.getElementById("showPlaceholder");

showAllBtn.addEventListener("click", ajaxGetAll);
filterBtn.addEventListener("click", ajaxFilter);
deleteBtn.addEventListener("click", ajaxRemove);



// jQuery has to be loaded!!


// ==== Event Click part of the JavaScript ================

function ajaxGetAll(event) {
    if (event != null) event.preventDefault();

    $.get("AJAX/AllPersons", function (data, status) {          /* goto  "Controller/Function"  */
        resultContainer.innerHTML = data;
    });

}

function ajaxFilter(event) { 
    if (event != null) event.preventDefault();

    $.post("AJAX/Filter", {Id: idTexField.value},function (data, status) {
            resultContainer.innerHTML = data;
     });

}

function ajaxRemove(element, event) {
    if (event != null) event.preventDefault();

    console.log("Deleting element: {0}", element);

    $.post("AJAX/Remove", { Id: idTexField.value }, function (data, status) {

        if (status == "success") {
            alert("Person Id" + data + " was removed from list");
            $("#per" + data).remove();       /* the object to be removed: id =@{ "per" + Model.Id }*/
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {

        if (jqXHR.status == 404) {
            alert("Delete failed!");

        } else {
            alert("Status: " + jqXHR.status);
        }

        });;
}





// ==== OnClick part of the JavaScript ========================


function About(id, event) {     // Ask for more details from person view
    if (event != null) event.preventDefault();

    $.post("AJAX/About", { Id: id }, function (data, status) {

        //resultContainer.innerHTML = data;
        $("#per" + id).replaceWith(data);
    });

}

function CloseAbout(id, event) {     // Just closes the about info partial
    if (event != null) event.preventDefault();

    $.post("AJAX/NotAbout", { Id: id }, function (data, status) {
        $("#cab" + id).replaceWith(data);
    });

    //$("#cab" + id).remove();
}

/// DeletePerson is called from the button in the end of the Person listing
/// And then send the person data to remove to backend
/// How do I put that Id into a person data model to the backend?
function DeletePerson(PersonId, event) {
    if (event != null) console.log("Delete person");

    $.post("AJAX/Remove", { Id: PersonId }, function (data, status) {

        if (status == "success") {
            $("#per" + data).remove();       /* the object to be removed: id =@{ "per" + Model.Id }*/
        }
    }).fail(function () {
        alert("Delete failed!");
    });;
}


ajaxGetAll();   // Load up the person-list in the view
