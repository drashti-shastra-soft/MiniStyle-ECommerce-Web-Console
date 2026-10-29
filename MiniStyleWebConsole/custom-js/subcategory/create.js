$(document).ready(function () {

    $(".btn-submit").click(function () {
        if (fnValidation()) {
            $('#frmCreate').submit();
        }
    });

    $('#Name').on('input', function () {
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
    });

    var notificationType = $.trim($('#NotificationType').val());
    var notificationMsg = $.trim($('#NotificationMsg').val());

    if (notificationType != null && notificationType != undefined && notificationType != '') {
        if (notificationMsg != null && notificationMsg != undefined && notificationMsg != '') {

            showToastr(notificationType, notificationMsg)
            $('.modal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#frmList').submit();
        }
    }
});

function fnValidation() {
    var isValid = true;

    var name = $.trim($("#Name").val());

    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $("#Name").next("span").text("");
    }

    return isValid;
}

function fnValidation() {
    var isValid = true;
    var name = $.trim($("#Name").val());
    var categoryId = $.trim($("#CategoryId").val());
    var dateRegex = /^\d{4}-\d{2}-\d{2}$/;

    // Name
    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $("#Name").next("span").text("");
    }

    //Category
    if (categoryId == null || categoryId == "" || categoryId == undefined || categoryId == "Select Category") {
        $("#CategoryId").next("span").text("Category is required!");
        isValid = false;
    } else {
        $("#CategoryId").next("span").text("");
    }


    return isValid;
}

