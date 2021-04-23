"use strict";

var filterBtn = document.getElementById("filterBtn");
var showAllBtn = document.getElementById("allBtn");
var delBtn = document.getElementById("deleteBtn");
var idTexField = document.getElementById("filterText");
var resultContainer = document.getElementById("showPlaceholder");

showAllBtn.addEventListener("click", ajaxGetAll);
filterBtn.addEventListener("click", ajaxFilter);
delBtn.addEventListener("click", ajaxRemove);

event.preventDefault(); // Will this be nessecery???? I'm getting error in chrome for this!


// jQuery has to be loaded!!

function ajaxGetAll() {
    $.get("AJAX/AllPersons", function (data, status) {          /* goto  "Controller/Function"  */
        console.log("Data: " + data + "\nStatus: " + status);
        resultContainer.innerHTML = data;
    });

}




function ajaxFilter() {     // WARNING TAKE CARE OF AN EMPTY LIST

    $.post("AJAX/Filter", {Id: idTexField.value},function (data, status) {
            console.log("Data: " + data + "\nStatus: " + status);
            resultContainer.innerHTML = data;
        });

}




function ajaxRemove() {

    $.post("AJAX/Remove", { Id: idTexField.value},function (data, status) {
            console.log("Data: " + data + "\nStatus: " + status);
    });

    alert("Person removed from list");
}