
function ShowPreviewOfImage(imageUpload, preview){
    if (imageUpload.files && imageUpload.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(preview).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUpload.files[0]);
    }
}