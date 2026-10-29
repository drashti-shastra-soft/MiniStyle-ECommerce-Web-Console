function toggleLoading(show) {
    $('#loading-element').toggle(show);
}

function resetPagination() {
    $('#Pagination_Skip').val(0);
    $('#Pagination_TotalRecord').val(0);
}

function fnHandleAjaxRequest({ url, method = 'GET', dataType = 'html', contentType = 'application/json', data = null, onSuccess, onError }) {
    //toggleLoading(true);
    $.ajax({
        url: url,
        method: method,
        contentType: contentType,
        dataType: dataType,
        data: data,
        beforeSend: function () {
            $('#loading-element').show();
        },
        success: function (response) {
            toggleLoading(false);
            if (typeof onSuccess === 'function') {
                onSuccess(response);
            }
        },
        error: function () {
            toggleLoading(false);
            if (typeof onError === 'function') {
                onError();
            }
        },
        complete: function () {
            $('#loading-element').hide();
        }
    });
}
