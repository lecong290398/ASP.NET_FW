var modelPassReset = {};

$(document).ready(function (e) {

    var validator = $("#AccountObject").kendoValidator().data("kendoValidator");
    var validator_edit = $("#AccountObject-Edit").kendoValidator().data("kendoValidator");
    var validator_password = $("#from-password-reset").kendoValidator().data("kendoValidator");

    LoadListAccount(1);

    $('#save-password').click(function (e) {

        if (validator_password.validate()) {

            var password = $("#password-reset").val();


            if (modelPassReset != null) {
                var dataInput = {
                    "Id": modelPassReset.Id,
                    "AtRowVersion": modelPassReset.AtRowVersion,
                    "PassWord": password
                }
                CallApiAsynchronous(dataInput, "/api/AccountObjects/reset-password", false, "POST", function (keyQuaTraVe) {
                    if (keyQuaTraVe) {
                        $.confirm({
                            icon: 'fas fa-check-circle',
                            title: 'Thông báo',
                            content: 'Cập nhập Password thành công !',
                            type: 'green',
                            typeAnimated: true,
                            buttons: {
                                OK: {
                                    btnClass: 'btn-green',
                                    action: function () {
                                        $('#ResetPasswordPopup').modal('hide');

                                    }
                                }
                            }
                        });
                    }
                });
            }
            else {
                $.confirm({
                    icon: 'fa fa-spinner fa-spin',
                    title: 'Thông báo!',
                    type: 'red',
                    typeAnimated: true,
                    content: 'Không tìm thấy đối tượng thay đổi. Vui lòng thử lại !',
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
                            $('#ResetPasswordPopup').modal('hide');
                        }
                    },
                }
            });
        }
    });

    $('.k-grid-button-create-popup').click(function (e) {
        $('#createPopup').modal({ backdrop: 'static' });

        LoadDataCreateOrEdit(true);

        //Set lại dữ liệu trống choc các trường
        $("input#fullname").val(null);
        $("input#username").val(null);
        $("input#password").val(null);
        $("input#phone").val(null);
        $("input#email").val(null);
        $("#multiselect-vai-tro").data("kendoMultiSelect").value(null);
        $("#multiselect-phong-ban").data("kendoMultiSelect").value(null);

    });

    $("#save-account").click(function (event) {

        if (validator.validate()) {
            var accountObjectName = $("input#accountObjectName").val();
            var userName = $("input#username").val();
            var passWord = $("input#password").val();
            var phone = $("input#phone").val();
            var email = $("input#email").val();
            var vaitro = $("#multiselect-vai-tro").data("kendoMultiSelect").value();
            var phongban = $("#multiselect-phong-ban").data("kendoMultiSelect").value();
            var dataInput = {
                "accountObjectName": accountObjectName,
                "userName": userName,
                "passWord": passWord,
                "phone": phone,
                "email": email,
                "listIdRole": vaitro,
                "listIdDepartment": phongban,
            };

            if (dataInput != null) {

                CallApiAsynchronous(dataInput, "/api/AccountObjects/them-moi-tai-khoan", false, "POST", function (keyQuaTraVe) {

                    if (keyQuaTraVe) {

                        $.confirm({
                            icon: 'fas fa-check-circle',
                            title: 'Thông báo',
                            content: 'Thêm thông tin User thành công !',
                            type: 'green',
                            typeAnimated: true,
                            buttons: {
                                OK: {

                                    btnClass: 'btn-green',
                                    action: function () {

                                        // Insert data vào grid
                                        var grid = $("#Grid-AccountObject").data("kendoGrid");
                                        var datasource = grid.dataSource;
                                        datasource.insert(keyQuaTraVe);

                                        //Set lại dữ liệu trống choc các trường
                                        $("input#fullname").val(null);
                                        $("input#username").val(null);
                                        $("input#password").val(null);
                                        $("input#phone").val(null);
                                        $("input#email").val(null);
                                        $("#multiselect-vai-tro").data("kendoMultiSelect").value(null);
                                        $("#multiselect-phong-ban").data("kendoMultiSelect").value(null);

                                        $('#createPopup').modal('hide');
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

    $("#save-edit-account").click(function (event) {

        if (validator_edit.validate()) {
            var accountObjectName = $("input#accountObjectName-edit").val();
            var userName = $("input#username-edit").val();
            var phone = $("input#phone-edit").val();
            var id = $("input#IdAccount-edit").val();
            var email = $("input#email-edit").val();
            var vaitro = $("#multiselect-vai-tro-edit").data("kendoMultiSelect").value();
            var phongban = $("#multiselect-phong-ban-edit").data("kendoMultiSelect").value();
            var rowVersion = $("input#row-version-edit").val();
            var dataInput = {
                "Id": id,
                "accountObjectName": accountObjectName,
                "phone": phone,
                "email": email,
                "listIdRole": vaitro,
                "listIdDepartment": phongban,
                "atRowVersion": rowVersion,
            };

            if (dataInput != null) {

                CallApiAsynchronous(dataInput, "/api/AccountObjects/chinh-sua-tai-khoan", false, "POST", function (keyQuaTraVe) {
                    if (keyQuaTraVe) {
                        $.confirm({
                            icon: 'fas fa-check-circle',
                            title: 'Thông báo',
                            content: 'Cập nhập tài khoản thành công !',
                            type: 'green',
                            typeAnimated: true,
                            buttons: {
                                OK: {
                                    btnClass: 'btn-green',
                                    action: function () {

                                        //Set lại dữ liệu trống choc các trường
                                        $("input#fullname-edit").val(null);
                                        $("input#username-edit").val(null);
                                        $("input#phone-edit").val(null);
                                        $("input#email-edit").val(null);
                                        $("#multiselect-vai-tro-edit").data("kendoMultiSelect").value(null);
                                        $("#multiselect-phong-ban-edit").data("kendoMultiSelect").value(null);

                                        LoadListAccount(1);
                                        $('#editPopup').modal('hide');
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
});

function onDataBound(e) {
    checkPermissionRow();
}

function resetPassword(e) {

    e.preventDefault();
    modelPassReset = null;
    $("#password-reset").val(null);
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var Id = dataItem.Id;
    var AtRowVersion = dataItem.AtRowVersion;
    modelPassReset = {
        "Id": Id,
        "AtRowVersion": AtRowVersion
    };

    $("#exampleModalLongTitle-reset-password").html("Đổi mật khẩu tài khoản: <span style='color:red; font-size : 20px ; font-style:italic;>'> " + Id + "</span>");

    $('#ResetPasswordPopup').modal({ backdrop: 'static' });
    console.log(modelPassReset);
}

function onEditAccountObject(e) {

    e.preventDefault();

    //Set lại dữ liệu trống choc các trường
    $("input#fullname-edit").val(null);
    $("input#username-edit").val(null);
    $("input#phone-edit").val(null);
    $("input#email-edit").val(null);
    $("#multiselect-vai-tro-edit").data("kendoMultiSelect").value(null);
    $("#multiselect-phong-ban-edit").data("kendoMultiSelect").value(null);

    LoadDataCreateOrEdit(false);
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var id = dataItem.Id;


    console.log(id);

    CallApiAsynchronous("null", "/api/AccountObjects/tai-du-lieu-tai-khoan-detail/?idAccount=" + id, true, "GET", function (keyQuaTraVe) {

        // disabled username
        $("#exampleModalLongTitle-edit").html("Chỉnh sửa tài khoản: <span style='color:red; font-size : 20px ; font-style:italic;>'> " + id + "</span>");
        $("input#row-version-edit").val(keyQuaTraVe.AccountObject_Edit.AtRowVersion);
        $("input#IdAccount-edit").val(keyQuaTraVe.AccountObject_Edit.Id);
        $("input#accountObjectName-edit").val(keyQuaTraVe.AccountObject_Edit.AccountObjectName);
        $("input#username-edit").val(keyQuaTraVe.AccountObject_Edit.UserName);
        $("input#phone-edit").val(keyQuaTraVe.AccountObject_Edit.Phone);
        $("input#email-edit").val(keyQuaTraVe.AccountObject_Edit.Email);
        $("#multiselect-vai-tro-edit").data("kendoMultiSelect").value(keyQuaTraVe.ListIdRole);
        $("#multiselect-phong-ban-edit").data("kendoMultiSelect").value(keyQuaTraVe.ListIdPhongBan);
        $('#editPopup').modal({ backdrop: 'static' });
    });
}

//load danh sách Infromation
function LoadListAccount(pageNumber) {

    CallApiAsynchronous(null, "/api/AccountObjects/danh-sach-tai-khoan?pageNumber=" + pageNumber, true, "GET", function (keyQuaTraVe, totalCount, pageSize) {
        console.log(keyQuaTraVe);

        var dataSource = new kendo.data.DataSource({
            data: keyQuaTraVe,
            pageSize: pageSize, 
            serverPaging: true,
            page: pageNumber,
            schema: {
                total: function () {
                    return totalCount;
                }
            }
        });

        var grid = $("#Grid-AccountObject").data("kendoGrid");
        grid.setDataSource(dataSource);
    });
}

function onPage(e) {
    LoadListAccount(e.page);
}

function LoadDataCreateOrEdit(type) {

    // Load cb ROLE
    CallApiAsynchronous("null", "/api/Roles/load-cb-Role", true, "POST", function (keyQuaTraVe) {
        if (type) {
            var multiselect = $("#multiselect-vai-tro").data("kendoMultiSelect");
            multiselect.setDataSource(keyQuaTraVe);
        }
        else {
            var multiselect = $("#multiselect-vai-tro-edit").data("kendoMultiSelect");
            multiselect.setDataSource(keyQuaTraVe);
        }

    });

    // Load cb Phòng ban
    CallApiAsynchronous("null", "/api/Departments/load-cb-departments", true, "POST", function (keyQuaTraVe) {
        if (type) {
            var multiselect = $("#multiselect-phong-ban").data("kendoMultiSelect");
            multiselect.setDataSource(keyQuaTraVe);
        } else {
            var multiselect = $("#multiselect-phong-ban-edit").data("kendoMultiSelect");
            multiselect.setDataSource(keyQuaTraVe);
        }
    });
}

function onDeleteAccount(e) {

    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    console.log(dataItem.Id + "----" + dataItem.RowVesion);
    var id = dataItem.Id;
    var rowVesion = dataItem.AtRowVersion;

    $.confirm({
        title: 'Thông báo !',
        content: "Bạn chắc chắc muốn xóa tài khoản này !",
        buttons: {
            stop: {
                text: 'No!'
            },
            go: {
                text: 'Yes!', // With spaces and symbols
                action: function () {

                    var dataInput = {
                        Id: id,
                        AtRowVersion: rowVesion
                    };

                    CallApiAsynchronous(dataInput, "/api/AccountObjects/xoa-tai-khoan", false, "POST", function (keyQuaTraVe) {

                        if (keyQuaTraVe != null) {
                            $.alert('Xóa thông tin thành công !');
                            var grid = $("#Grid-AccountObject").data("kendoGrid");
                            var tr = $(e.target).closest("tr");
                            grid.removeRow(tr);
                        }
                    });
                }
            }
        }
    });
}

function onPhanQuyen(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var id = dataItem.Id;
    window.location = _url + "/menu-account/?idAccount=" + id;
}