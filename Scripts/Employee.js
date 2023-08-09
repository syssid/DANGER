$(document).ready(function () {

});

function BtnSubmit() {
    Event.preventDefault();
    var Name = $('#Name').val();
    var Email = $('#Email').val();
    var Phone = $('#Phone').val();
    var Salary = $('#Salary').val();

    if (Name == "") {
        alert("Please Enter Employee Name");
        return;
    }
    if (Email == "") {
        alert('Please Enter Email Address');
        return;
    }
    if (Phone == '') {
        alert('Please Enter Mobile Number');
        return;
    }
    if (Salary == '') {
        alert('Please Enter Salary');
        return;
    }
    $.ajax({
        url: '/Home/InsertData',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ 'Name': Name, 'Email': Email, 'Phone': Phone, 'Salary': Salary }),
        success: function (result) {
            alert("Data Saved Successfully");
            location.reload();

        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function Delete() {
    var ID = '';
    $("#Bdt tbody tr").each(function () {

        const currentRow = $(this);

        if (currentRow.find('.chkrow').is(':checked')) {
            var IDs = currentRow.find('.Sel').html();

            ID += ',' + IDs;
        }
    });

    ID = ID.slice(1);
    var con = confirm("Are You Sure Want To Delete?")
    if (con == false) {
        return;
    }
    $.ajax({
        url: '/Home/DeleteData',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ 'ID': ID }),
        success: function (result) {
            alert("Data Deleted Successfully");
            location.reload();

        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}