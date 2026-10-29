$(document).ready(function () {
    $(".btn-submit").click(function () {
        if (fnValidation()) {
            $('#frmCreate').submit();
        }
    });

    $('#FullName').on('input', function () {
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
    });

    var notificationType = $.trim($('#NotificationType').val());
    var notificationMsg = $.trim($('#NotificationMsg').val());

    if (notificationType != null && notificationType != undefined && notificationType != '') {
        if (notificationMsg != null && notificationMsg != undefined && notificationMsg != '') {
            showToastr(notificationType, notificationMsg);
            $('.modal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#frmList').submit();
        }
    }

});

function fnValidation() {
    var isValid = true;
    var fullName = $.trim($("#FullName").val());
    var line1 = $.trim($("#Line1").val());
    var landmark = $.trim($("#Landmark").val());
    var zipCode = $.trim($("#ZipCode").val());
    var city = $.trim($("#City").val());
    var state = $.trim($("#State").val());
    var totalAmount = $.trim($("#TotalAmount").val());
    var paymentStatus = $.trim($("#PaymentStatus").val());
    var shippingStatus = $.trim($("#ShippingStatus").val());

    // FullName
    if (fullName == null || fullName == "" || fullName == undefined) {
        $("#FullName").next("span").text("Full Name is required!");
        isValid = false;
    } else {
        $("#FullName").next("span").text("");
    }

    //Line 1
    if (line1 == null || line1 == "" || line1 == undefined) {
        $("#Line1").next("span").text("Line1 is required!");
        isValid = false;
    } else {
        $("#Line1").next("span").text("");
    }

    //Landmark
    if (landmark == null || landmark == "" || landmark == undefined) {
        $("#Landmark").next("span").text("Landmark is required!");
        isValid = false;
    } else {
        $("#Landmark").next("span").text("");
    }

    //ZipCode
    if (zipCode == null || zipCode == "" || zipCode == undefined) {
        $("#ZipCode").next("span").text("Zip Code is required!");
        isValid = false;
    } else {
        $("#ZipCode").next("span").text("");
    }

    //City
    if (city == null || city == "" || city == undefined) {
        $("#City").next("span").text("City is required!");
        isValid = false;
    } else {
        $("#City").next("span").text("");
    }

    //State
    if (state == null || state == "" || state == undefined) {
        $("#State").next("span").text("State is required!");
        isValid = false;
    } else {
        $("#State").next("span").text("");
    }

    //Total Amount
    if (totalAmount == null || totalAmount == "" || totalAmount == undefined) {
        $("#TotalAmount").next("span").text("Total Amount is required!");
        isValid = false;
    } else {
        $("#TotalAmount").next("span").text("");
    }

    //Payment Status
    if (paymentStatus == null || paymentStatus == "" || paymentStatus == undefined) {
        $("#PaymentStatus").next("span").text("Payment Status is required!");
        isValid = false;
    } else {
        $("#PaymentStatus").next("span").text("");
    }

    //ShippingStatus
    if (shippingStatus == null || shippingStatus == "" || shippingStatus == undefined) {
        $("#ShippingStatus").next("span").text("Shipping Status is required!");
        isValid = false;
    } else {
        $("#ShippingStatus").next("span").text("");
    }


    return isValid;
}