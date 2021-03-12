
function ShowPreviewOfImage(imageUploader, preview) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(preview).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}

function ajaxPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid) {
        var ajaxConfiguration = {
            type: "POST",
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html)
                    refreshAfterAddAlbum($(form).attr('data-restUrl'), true);
                }
                else {

                }
            }
        }
        if ($(form).attr('enctype') == 'multipart/form-data') {
            ajaxConfiguration['contentType'] = false;
            ajaxConfiguration['processData'] = false;
        }

        $.ajax(ajaxConfiguration);
    }
    return false;
}

function refreshAfterAddAlbum(resetUrl, showViewTab) {
    $.ajax({
        type: 'GET',
        url: resetUrl,
        success: function (response) {
            $("#secondTab").html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Add New');
            if (showViewTab)
                $('ul.nav.nav-tabs a:eq(0)').tab('show');
        }
    })
}