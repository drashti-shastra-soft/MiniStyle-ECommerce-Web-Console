$(document).off('click', '.icon-delete').on('click', '.icon-delete', function (e) {
    e.preventDefault();
    const id = $(this).data('id');
    if (id) fnDelete(id);
});


function fnDelete(id) {
    fnHandleAjaxRequest({
        url: `/ProductSizeChart/Delete/${id}`,
        dataType: 'json',
        onSuccess: function (response) {
            if (response) {
                showToastr(response.NotificationType, response.NotificationMsg);
                $('#frmList').submit();
            }
        },
        onError: function () {
            showToastr("Failed to delete user.");
        }
    });
}