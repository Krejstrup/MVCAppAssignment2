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


// ==== Event Click part of the JavaScript: ================



/// ajaxGetAll fetches the complete partial view for all persons.
function ajaxGetAll(event) {
    if (event != null) event.preventDefault();

    $.get("AJAX/AllPersons", function (data, status) {          /* goto  "Controller=AJAX/Function=AllPersons"  */
        resultContainer.innerHTML = data;
    });

}

/// The ajaxFilter is called from an event from the button in Index view.
/// The function posts the Id, fetched from the text-input, to the Controller.
/// The response is checked for success and puts the resulting partial view 
/// in the placeholder or signals the fails.
function ajaxFilter(event) { 
    if (event != null) event.preventDefault();

    $.post("AJAX/Filter", {Id: idTexField.value},function (data, status) {
        if (status == "success") {
            resultContainer.innerHTML = data;
        }

    }).fail(function (jqXHR, textStatus, errorThrown) {

        if (jqXHR.status == 404) {
            alert("Filter failed!");

        } else {
            alert("Status: " + jqXHR.status);
        }
    });

}

/// this function is called from an event from the button in Index view.
/// ajaxRemove Posts the Id, fetched from the text-input, to the Controller
/// The function checks for any success or fails.
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

        });
}



// ==== OnClick part of the JavaScript: ========================


/// About is called from the button in the end action end of the person listing.
/// Sends the person Id to be shown to this function and pass it on to the Controller. 
/// THe controller looks up a new partial view and returns it here.
/// The partial person view is replacing the standard person partial view.
// anropas via:  <button onclick="Edit(@Model.Id , event)" id="@("ab"+@Model.Id)">Edit</button> = "ab18" tex
function Edit(id, event) {

    $.post("People/LangEdit", { Id: id }, function (data, status) { // Go to controller People and do this!

/*        $("#per" + id).replaceWith(data);*/
    });

}

function EditPerson(person, event) {

    $.post("AJAX/EditPerson", { aPerson: person }, function (data, status) {
        $("#edit" + id).replaceWith(data);
    });

}

/// Just closes the about info partial by the same principal as for About.
///
function CloseEdit(id, event) {     // NEW FUNCTION - JUST CLOSE THE PARTIAL VIEW

    $.post("AJAX/CloseEdit", { Id: id }, function (data, status) {
        $("#edit" + id).replaceWith(data);
    });
}

/// DeletePerson is called from the button in the end of the Person listing
/// And then send the person data to remove to backend.
///
function DeletePerson(PersonId, event) {

    $.post("AJAX/Remove", { Id: PersonId }, function (data, status) {

        if (status == "success") {
            $("#per" + data).remove();       /* the object to be removed: id =@{ "per" + Model.Id }*/
        }
    }).fail(function () {
        alert("Delete failed!");
    });;
}


function LangAdd(LangId,event) {
    if (event != null) event.preventDefault();


}

function LangRem(LangId, event) {
    if (event != null) event.preventDefault();


}

function FNameChange(PersonId, thePerson) {

    $.post("AJAX/EditFname", { Id: id, changePerson: thePerson }, function (data, status) {
        $("#edit" + id).replaceWith(data);
    });
}

function LNameChange(PersonId, thePerson) {

    $.post("AJAX/EditLname", { Id: id }, function (data, status) {
        $("#edit" + id).replaceWith(data);
    });
}

///====== All other bits and bytes: ===========================

//== Load up the person-list in the view:
ajaxGetAll();   
