
$('#folowNature').on('show.bs.collapse', function () {
    $('#icon-up-down-nature').addClass('fa-chevron-up');
    $('#icon-up-down-nature').removeClass('fa-chevron-down');
});

$('#folowNature').on('hide.bs.collapse', function () {
    $('#icon-up-down-nature').removeClass('fa-chevron-up');
    $('#icon-up-down-nature').addClass('fa-chevron-down');
});
//Get the button
var mybutton = document.getElementById("myBtnScrollTop");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        mybutton.style.display = "block";
    } else {
        mybutton.style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}


$(document).ready(function () {
    var validator_edit = $("#hoSoCaNhan-Edit").data("kendoValidator");
    var validator_password = $("#from-password-edit").data("kendoValidator");

    $('#open-hosocanhan').click(function (e) {
        e.preventDefault();

        console.log(_userId);
        CallApiAsynchronous("null", "/api/AccountObjects/load-du-lieu-tai-khoan-ca-nhan/?idAccount=" + _userId, true, "GET", function (keyQuaTraVe) {

            // disabled username
            $("#detail-fullname").text(keyQuaTraVe.AccountObject_Edit.AccountObjectName);
            $("#detail-username").text(keyQuaTraVe.AccountObject_Edit.UserName);
            $("#detail-sdt").text(keyQuaTraVe.AccountObject_Edit.Phone);
            $("#detail-email").text(keyQuaTraVe.AccountObject_Edit.Email);
            $('#ho-so-ca-nhan').modal({ backdrop: 'static' });
        });

    });


    $('#edit-account').click(function (e) {
        e.preventDefault();
        $('#ho-so-ca-nhan').modal('hide');

        $('#edit-fullname').val($("#detail-fullname").text());
        $('#edit-phone').val($("#detail-sdt").text());
        $('#edit-email').val($("#detail-email").text());

        $('#capnhattaikhoan-model').modal({ backdrop: 'static' });

    });

    //lưu thông tin hồ sơ
    $('#save-edit-ho-so').click(function (e) {
        e.preventDefault();
        if (validator_edit.validate()) {
            var newFullName = $('#edit-fullname').val();
            var newSDT = $('#edit-phone').val();
            var newEmail = $('#edit-email').val();

            var dataInput = {
                "AccountObjectName": newFullName,
                "Phone": newSDT,
                "Email": newEmail,
            };
            if (dataInput != null) {
                CallApiAsynchronous(dataInput, "/api/AccountObjects/chinh-sua-thong-tin-ca-nhan", false, "POST", function (keyQuaTraVe) {
                    if (keyQuaTraVe) {
                        $.confirm({
                            icon: 'fas fa-check-circle',
                            title: 'Thông báo',
                            content: 'Cập nhập thông tin cá nhân thành công !',
                            type: 'green',
                            typeAnimated: true,
                            buttons: {
                                OK: {
                                    btnClass: 'btn-green',
                                    action: function () {
                                        $('#capnhattaikhoan-model').modal('hide');
                                    }
                                }
                            }
                        });
                    }
                });

            }
        } else {
            $.confirm({
                icon: 'fa fa-spinner fa-spin',
                title: 'Thông báo!',
                type: 'red',
                typeAnimated: true,
                content: 'Các trường bắt buộc rỗng.  Vui lòng thử lại ',
                buttons: {
                    tryAgain: {
                        text: 'Đóng',
                        btnClass: 'btn-red',
                        action: function () {

                        }
                    },
                }
            });
        }
    });

    //lưu mật khẩu
    $('#save-edit-password').click(function (e) {
        e.preventDefault();
        if (validator_password.validate()) {

            var oldPass = $('#PasswordOld').val();
            var newPass = $('#PasswordNew').val();
            var reNewPass = $('#rePasswordNew').val();

            if (newPass !== reNewPass) {
                $.confirm({
                    icon: 'fa fa-spinner fa-spin',
                    title: 'Thông báo!',
                    type: 'red',
                    typeAnimated: true,
                    content: 'Hai mật khẩu không giống nhau.  Vui lòng thử lại ',
                    buttons: {
                        tryAgain: {
                            text: 'Đóng',
                            btnClass: 'btn-red',
                            action: function () {

                            }
                        },
                    }
                });
            }
            else {
                var dataInput = {
                    "oldPass": oldPass,
                    "newPass": newPass,
                };
                if (dataInput != null) {
                    CallApiAsynchronous(dataInput, "/api/AccountObjects/doi-mat-khau-ca-nhan", false, "POST", function (keyQuaTraVe) {
                        if (keyQuaTraVe) {
                            $.confirm({
                                icon: 'fas fa-check-circle',
                                title: 'Thông báo',
                                content: 'Cập nhập mật khẩu cá nhân thành công !',
                                type: 'green',
                                typeAnimated: true,
                                buttons: {
                                    OK: {
                                        btnClass: 'btn-green',
                                        action: function () {
                                            dangXuat();
                                        }
                                    }
                                }
                            });
                        }
                    });

                }
            }
        } else {
            $.confirm({
                icon: 'fa fa-spinner fa-spin',
                title: 'Thông báo!',
                type: 'red',
                typeAnimated: true,
                content: 'Các trường bắt buộc rỗng.  Vui lòng thử lại ',
                buttons: {
                    tryAgain: {
                        text: 'Đóng',
                        btnClass: 'btn-red',
                        action: function () {

                        }
                    },
                }
            });
        }

    });

    $('#btn-logout').click(function (e) {
        dangXuat();
    });

    $("#order-type").click(function () {
        let data = $('#icon-collapse-order-type').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-order-type").addClass('fa-chevron-circle-down');
            $("#icon-collapse-order-type").removeClass('fa-chevron-circle-up');
            $("#content-order-type").removeClass('d-none');
        } else {
            $("#content-order-type").addClass('d-none');
            $("#icon-collapse-order-type").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-order-type").addClass('fa-chevron-circle-up');
        }
    });

    $("#category-pro").click(function () {
        let data = $('#icon-collapse-category-pro').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-category-pro").addClass('fa-chevron-circle-down');
            $("#icon-collapse-category-pro").removeClass('fa-chevron-circle-up');
            $("#content-category-pro").removeClass('d-none');
        } else {
            $("#content-category-pro").addClass('d-none');
            $("#icon-collapse-category-pro").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-category-pro").addClass('fa-chevron-circle-up');
        }
    });

    $("#group-pro").click(function () {
        let data = $('#icon-collapse-group-pro').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-group-pro").addClass('fa-chevron-circle-down');
            $("#icon-collapse-group-pro").removeClass('fa-chevron-circle-up');
            $("#content-group-pro").removeClass('d-none');
        } else {
            $("#content-group-pro").addClass('d-none');
            $("#icon-collapse-group-pro").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-group-pro").addClass('fa-chevron-circle-up');
        }
    });

    $("#inventory-pro").click(function () {
        let data = $('#icon-collapse-inventory-pro').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-inventory-pro").addClass('fa-chevron-circle-down');
            $("#icon-collapse-inventory-pro").removeClass('fa-chevron-circle-up');
            $("#content-inventory-pro").removeClass('d-none');
        } else {
            $("#content-inventory-pro").addClass('d-none');
            $("#icon-collapse-inventory-pro").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-inventory-pro").addClass('fa-chevron-circle-up');
        }
    });
    // script handle colappse for sidebar responsive
    $("#order-type-res").click(function () {
        let data = $('#icon-collapse-order-type-res').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-order-type-res").addClass('fa-chevron-circle-down');
            $("#icon-collapse-order-type-res").removeClass('fa-chevron-circle-up');
            $("#content-order-type-res").removeClass('d-none');
        } else {
            $("#content-order-type-res").addClass('d-none');
            $("#icon-collapse-order-type-res").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-order-type-res").addClass('fa-chevron-circle-up');
        }
    });

    $("#category-pro-res").click(function () {
        let data = $('#icon-collapse-category-pro-res').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-category-pro-res").addClass('fa-chevron-circle-down');
            $("#icon-collapse-category-pro-res").removeClass('fa-chevron-circle-up');
            $("#content-category-pro-res").removeClass('d-none');
        } else {
            $("#content-category-pro-res").addClass('d-none');
            $("#icon-collapse-category-pro-res").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-category-pro-res").addClass('fa-chevron-circle-up');
        }
    });

    $("#group-pro-res").click(function () {
        let data = $('#icon-collapse-group-pro-res').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-group-pro-res").addClass('fa-chevron-circle-down');
            $("#icon-collapse-group-pro-res").removeClass('fa-chevron-circle-up');
            $("#content-group-pro-res").removeClass('d-none');
        } else {
            $("#content-group-pro-res").addClass('d-none');
            $("#icon-collapse-group-pro-res").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-group-pro-res").addClass('fa-chevron-circle-up');
        }
    });

    $("#inventory-pro-res").click(function () {
        let data = $('#icon-collapse-inventory-pro-res').attr('class');
        let isCollapse = data.includes("fa-chevron-circle-up");
        // console.log(data.includes("fa-chevron-circle-up"))
        if (isCollapse) {
            $("#icon-collapse-inventory-pro-res").addClass('fa-chevron-circle-down');
            $("#icon-collapse-inventory-pro-res").removeClass('fa-chevron-circle-up');
            $("#content-inventory-pro-res").removeClass('d-none');
        } else {
            $("#content-inventory-pro-res").addClass('d-none');
            $("#icon-collapse-inventory-pro-res").removeClass('fa-chevron-circle-down');
            $("#icon-collapse-inventory-pro-res").addClass('fa-chevron-circle-up');
        }
    });

});
/* Set the width of the side navigation to 250px and the left margin of the page content to 250px and add a black background color to body */
function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
    document.getElementById("mySidenav").style.backgroundColor = "#fff";
    document.getElementById("main").style.marginLeft = "250px";
    document.body.style.backgroundColor = "rgba(0,0,0,0.4)";
}

function dangXuat() {
    $.ajax({
        url: _url + "/dang-xuat",
        type: "POST",
        contentType: "application/json",
        success: function (data) {
            if (data.IsOk === true) {
                if (data.PayLoad != null) {
                    //xóa các key liên quan token
                    //window.localStorage.removeItem('key_Token');
                    //window.localStorage.removeItem('refreshToken');
                    window.location = _url + "/login";
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log(textStatus + "\n" + errorThrown);
        }
    });
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("main").style.marginLeft = "0";
    document.body.style.backgroundColor = "white";
}

function openViewChangePass() {
    $('#ho-so-ca-nhan').modal('hide');
    $('#doimatkhau-model').modal({ backdrop: 'static' });
}
function openViewEditProfile() {
    $('#ho-so-ca-nhan').modal('hide');
    $('#capnhattaikhoan-model').modal({ backdrop: 'static' });
}
function customFunction(input) {
    if (input.attr('name') === "multiselect-vai-tro" && input.val() == null) {
        return false;
    }
    return true;
}

function goback() {
    $('#doimatkhau-model').modal('hide');
    $('#capnhattaikhoan-model').modal('hide');
    $('#ho-so-ca-nhan').modal({ backdrop: 'static' });
}
function customFunction(input) {
    if (input.attr('name') === "multiselect-vai-tro" && input.val() == null) {
        return false;
    }
    return true;
}
