
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
                    $("#firstTab").html(response.html);
                    refreshAfterAddAlbum($(form).attr('data-restUrl'), true);
                    $.notify(response.message, "success");
                    if (typeof activateTable != 'undefined' && $.isFunction(activateTable)) {
                        activateTable();
                    }
                }
                else {
                    $.notify(response.message, "error");
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
            $('#secondTabHeader').html('Add New');
            if (showViewTab)
                $('#firstTabHeader').tab('show');
        }
    });
}

function Edit(editUrl) {
    $.ajax({
        type: 'GET',
        url: editUrl,
        success: function (response) {
            $("#secondTab").html(response);
            $('#secondTabHeader').html('Edit');
            $('#secondTabHeader').tab('show');
        }

    });
}

function Delete(url) {
    
    if (confirm('Are you Sure to delete this record ? ') == true) {
        $.ajax({
            type: 'POST',
            url: url,
            success: function (response) {
                if (response.success) {
                    $("#firstTab").html(response.html);
                    $.notify(response.message, "warn");
                    if (typeof activateTable != 'undefined' && $.isFunction(activateTable)) {
                        activateTable();
                    }
                }
                else {
                    $.notify(response.message, "error");
                }
            }
        });
    }
}