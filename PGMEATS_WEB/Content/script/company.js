
$(document).ready(function () {
    loadData();
});

//Load Data function
function loadData() {
    $.ajax({
        url: "/Factory/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.FactoryCode + '</td>';
                html += '<td>' + item.FactoryName + '</td>';
                html += '<td>' + item.UpdateDate + '</td>';
                html += '<td>' + item.UpdateBy + '</td>';
                html += '<td><a href="#" onclick="return getFactory(\'' + item.FactoryCode + '\')">Edit</a> | <a href="#" onclick="Delete(\'' + item.FactoryCode + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Data Function 
function Add() {
    //var res = validate();
    //if (res == false) {
    //    return false;
    //}
    var empObj = {
        FactoryCode: $('#FactoryCode').val(),
        FactoryName: $('#FactoryName').val(),
        UpdateDate: $('#UpdateDate').val(),
        UpdateBy: $('#UpdateBy').val()
    };
    $.ajax({
        url: "/Factory/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based
function getFactory(FactoryCode) {
    $('#FactoryName').css('border-color', 'lightgrey');
    $('#RegisterDate').css('border-color', 'lightgrey');
    $('#UpdateBy').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Factory/getFactory/" + FactoryCode,
        async: false,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        datatype: 'json',
        data: { FactoryCode: FactoryCode },
        success: function (result) {
            $('#FactoryCode').val(result.FactoryCode).prop('disabled', true);
            $('#FactoryName').val(result.FactoryName);
            $('#UpdateDate').val(result.UpdateDate);
            $('#UpdateBy').val(result.UpdateBy);
        
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#myModalLabelEdit').show();
            $('#btnAdd').hide();
            $('#myModalLabel').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating
function Update() {
    //var res = validate();
    //if (res == false) {
    //    return false;
    //}
    var empObj = {
        FactoryCode: $('#FactoryCode').val(),
        FactoryName: $('#FactoryName').val(),
        UpdateDate: $('#UpdateDate').val(),
        UpdateBy: $('#UpdateBy').val(),
    };
    $.ajax({
        url: "/Factory/Update",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#FactoryCode').val("");
            $('#FactoryName').val("");
            $('#UpdateDate').val("");
            $('#UpdateBy').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting 
function Delete(FactoryCode) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Factory/Delete/" + FactoryCode,
            async: true,
            cache: false,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            data: JSON.stringify({ FactoryCode: FactoryCode }),
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes
function clearTextBox() {
    $('#FactoryCode').val("");
    $('#FactoryName').val("");
    $('#UpdateDate').val("");
    $('#UpdateBy').val("");
    $('#FactoryName').css('border-color', 'lightgrey');
    $('#UpdateDate').css('border-color', 'lightgrey');
    $('#UpdateBy').css('border-color', 'lightgrey');
    $('#btnUpdate').hide();
    $('#myModalLabelEdit').hide();
    $('#btnAdd').show();
    $('#myModalLabel').show();
}
//Valdidation using jquery
function validate() {
    var isValid = true;
    if ($('#FactoryCode').val().trim() == "") {
        $('#FactoryCode').css('border-color', 'Red');
        //$('#FactoryCode').val('Required');
        return false;
    }
    else {
        $('#FactoryName').css('border-color', 'lightgrey');
    }
    if ($('#FactoryName').val().trim() == "") {
        $('#FactoryName').css('border-color', 'Red');
        //$('#FactoryName').val('Required');
        return false;
    }
    else {
        $('#FactoryCode').css('border-color', 'lightgrey');
    }         
    return Add();
}

function OnSuccess() {
    if ($.validator) {
        $.validator.unobtrusive.parse("form");
    }

    $("#modalDialog").html(response);
}