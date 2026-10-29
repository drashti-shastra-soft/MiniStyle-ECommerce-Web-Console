$(document).ready(function () {
    $('#WearablePart').change(toggleFields);
    toggleFields(); 

    $('.btn-submit').on('click', function (e) {
        e.preventDefault();

        if (fnValidation()) {
            fnSaveSizeChart();
        }
    });

    const config = window.notificationConfig;

    if (config.type != null && config.type != undefined && config.type != '') {
        if (config.message != null && config.message != undefined && config.message != '') {
            showToastr(config.type, config.message)
        }
    }
});

function fnSaveSizeChart() {
    const mSizeChart = {
        ProductId: $('#ProductId').val(),
        ProductAgeGroupId: $('#ProductAgeGroupId').val(),
        SleeveLength: $('#SleeveLength').val(),
        Number: $('#Number').val(),
        WearablePart: $('#WearablePart').val(),
        UnitType: $('#UnitType').val(),
        Shoulder: $('#Shoulder').val(),
        Length: $('#Length').val(),
        Chest: $('#Chest').val(),
        Waist: $('#Waist').val(),
    };

    fnHandleAjaxRequest({
        url: `/Product/UpsertProductSizeChart`,
        method: 'POST',
        contentType: 'application/json',
        dataType: 'html',
        data: JSON.stringify(mSizeChart),
        onSuccess: function (response) {
            var extractResponse = JSON.parse(response);
            if (extractResponse) {
                showToastr(extractResponse.type, extractResponse.message);
                $('.modal').modal('hide');
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                var $activeTab = $('.size-chart-nav-tab.active');
                if ($activeTab.length) {
                    var productId = parseInt($activeTab.data('id'));
                    var unitType = $activeTab.data('unit-type');
                    var tabId = $activeTab.data('tab');
                    fnGetSizeChartRows(productId, unitType, tabId);
                }
            }
        },
        onError: function (xhr, status, error) {
            showToastr('error', error);
            $('.modal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
    });
}

function fnValidation() {
    var isValid = true;
    var wearablePart = $.trim($("#WearablePart").val());
    var shoulder = $.trim($("#Shoulder").val());
    var chest = $.trim($("#Chest").val());
    var sleeveLength = $.trim($("#SleeveLength").val());
    var unitType = $.trim($("#UnitType").val());
    var productAgeGroupId = $.trim($("#ProductAgeGroupId").val());
    var number = $.trim($("#Number").val());
    var length = $.trim($("#Length").val());
    var waist = $.trim($("#Waist").val());

   
    //Wearable Part
    if (wearablePart == null || wearablePart == "" || wearablePart == undefined || wearablePart == "Select WearablePart") {
        $("#spnWearablePart").text("Wearable Part is required!");
        isValid = false;
    } else {
        $("#spnWearablePart").text("");
    }
    if (wearablePart === "Top") {
        //Shoulder
        if (shoulder == null || shoulder == "" || shoulder == undefined) {
            $("#Shoulder").next("span").text("Shoulder is required!");
            isValid = false;
        } else {
            $("#Shoulder").next("span").text("");
        }

        //Chest
        if (chest == null || chest == "" || chest == undefined) {
            $("#Chest").next("span").text("Chest is required!");
            isValid = false;
        } else {
            $("#Chest").next("span").text("");
        }

        //Sleeve Length
        if (sleeveLength == null || sleeveLength == "" || sleeveLength == undefined) {
            $("#SleeveLength").next("span").text("Sleeve Length is required!");
            isValid = false;
        } else {
            $("#SleeveLength").next("span").text("");
        }

        //Unit
        if (unitType == null || unitType == "" || unitType == undefined || unitType == "Select Unit") {
            $("#UnitType").next("span").text("Unit Type is required!");
            isValid = false;
        } else {
            $("#UnitType").next("span").text("");
        }

        //Age Group
        if (productAgeGroupId == null || productAgeGroupId == "" || productAgeGroupId == undefined || productAgeGroupId == "Select AgeGroup") {
            $("#spnAgeGroup").text("Age Group is required!");
            isValid = false;
        } else {
            $("#spnAgeGroup").text("");
        }

        //Number
        if (number == null || number == "" || number == undefined) {
            $("#Number").next("span").text("Number is required!");
            isValid = false;
        } else {
            $("#Number").next("span").text("");
        }

        //Weist
        if (waist == null || waist == "" || waist == undefined) {
            $("#Waist").next("span").text("Waist is required!");
            isValid = false;
        } else {
            $("#Waist").next("span").text("");
        }

        //Lenght
        if (length == null || length == "" || length == undefined) {
            $("#Length").next("span").text("Length is required!");
            isValid = false;
        } else {
            $("#Length").next("span").text("");
        }
    }

    if (wearablePart === "Bottom") {
        //Unit Type
        if (unitType == null || unitType == "" || unitType == undefined || unitType == "Select Unit") {
            $("#UnitType").next("span").text("Unit Type is required!");
            isValid = false;
        } else {
            $("#UnitType").next("span").text("");
        }

        //Age Group
        if (productAgeGroupId == null || productAgeGroupId == "" || productAgeGroupId == undefined || productAgeGroupId == "Select AgeGroup") {
            $("#spnAgeGroup").text("Age Group is required!");
            isValid = false;
        } else {
            $("#spnAgeGroup").text("");
        }

        if (length == null || length == "" || length == undefined) {
            $("#Length").next("span").text("Length is required!");
            isValid = false;
        } else {
            $("#Length").next("span").text("");
        }

        //Weist
        if (waist == null || waist == "" || waist == undefined) {
            $("#Waist").next("span").text("Waist is required!");
            isValid = false;
        } else {
            $("#Waist").next("span").text("");
        }

        //Number
        if (number == null || number == "" || number == undefined) {
            $("#Number").next("span").text("Number is required!");
            isValid = false;
        } else {
            $("#Number").next("span").text("");
        }
    }

    return isValid;
}

function toggleFields() {
    const part = $('#WearablePart').val();
    if (part === 'Top') {
        $('#topExtraFields').show();
    } else {
        $('#topExtraFields').hide();
    }
}