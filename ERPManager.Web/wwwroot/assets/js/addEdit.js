jQuery.validator.setDefaults({
    debug: true,
    success: "valid"
});

function CreateOrEditTableItem(title, getUrl) {
    $("#createOrEditFormModalTitle").html(title);

    $.ajax({
        method: "GET",
        url: getUrl,
    }).done(function (data) {
        $("#createOrEditFormBody").html(data)
        $("#createOrEditFormModal").modal('show');
    });
}

function InsertTableItem(d, postUrl) {
    var $form = $(d).parents('form:first');
    var formData = new FormData($form[0]);
    $.validator.unobtrusive.parse($form);
    $form.validate();
    if ($form.valid()) {
        $.ajax({
            method: "POST",
            async: false,
            url: postUrl,
            data: formData,
            cache: false,
            contentType: false,
            processData: false
        })
            .done(function (data) {
                if (data.isSuccess) {
                    ShowSuccessMessage(data.successMessage);
                    EntityUpdatedOrDeleted();
                    //window.location = window.location.href;
                }
                else {
                    ShowErrorMessage(data.errors[0]);
                }

            });
    }
}

//function InsertTableItem(d, postUrl) {
//    var form = $(d).parents('form:first');
//    var formData = new FormData(form[0]);
//    $(form).validate({
//        submitHandler: function (form) {
//            $.ajax({
//                method: "POST",
//                async: false,
//                url: postUrl,
//                data: formData,
//                cache: false,
//                contentType: false,
//                processData: false
//            })
//                .done(function (data) {
//                    if (data.isSuccess) {
//                        ShowSuccessMessage(data.successMessage);
//                        EntityUpdatedOrDeleted();
//                        //window.location = window.location.href;
//                    }
//                    else {
//                        ShowErrorMessage(data.errors[0]);
//                    }

//                })
//        }
//    });
//}

function DeleteTableItem(message, deleteUrl, yes, no) {
    alertify
        .okBtn(yes)
        .cancelBtn(no)
        .confirm(message, function () {

            $.ajax({
                method: "GET",
                url: deleteUrl,
            }).done(function (data) {
                EntityUpdatedOrDeleted();
            });
        }, function () {
        });
}

function EntityUpdatedOrDeleted()
{
    $("#createOrEditFormModal").modal("hide");
    $(".dataTable").DataTable().ajax.reload();
}
