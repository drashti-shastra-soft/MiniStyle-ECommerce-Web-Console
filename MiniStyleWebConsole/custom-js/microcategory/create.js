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
    var subcategoryId = $.trim($("#SubCategoryId").val());

    //Name

    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $("#Name").next("span").text("");
    }


    //SubCategory

    if (subcategoryId == null || subcategoryId == "" || subcategoryId == undefined || subcategoryId == "Select SubCategory") {
        $("#SubCategoryId").next("span").text("Sub Category is required!");
        isValid = false;
    } else {
        $("#SubCategoryId").next("span").text("");
    }

    return isValid;
}