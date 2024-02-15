﻿function deleteEmployee(i) 
{
    $.ajax({
        url: 'Home/Delete',
        type: 'POST',
        data: {
            id: i
        },
        success: function() {
            window.location.reload();
        }
    });
}

function populateForm(i) {

    $.ajax({
        url: 'Home/PopulateForm',
        type: 'GET',
        data: {
            id: i
        },
        dataType: 'json',
        success: function (response) {
            $("#Employee_Name").val(response.name);
            $("#Employee_Description").val(response.description);
            $("#Employee_Id").val(response.id);
            $("#form-button-badge").text("Edit");
            $("#form-button").text("Update");
            $("#form-action").attr("action", "/Home/Update");
        }
    });
}