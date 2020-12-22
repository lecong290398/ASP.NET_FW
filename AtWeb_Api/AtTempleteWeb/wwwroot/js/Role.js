

var modelEditRole = {};

$(document).ready(function (e) {

    LoadListRole(1);

    var validator_edit = $("#from-edit-role").kendoValidator().data("kendoValidator");
    var validator_create = $("#from-create-role").kendoValidator().data("kendoValidator");

    $('.k-grid-button-create-popup').click(function (e) {

        //Set lại dữ liệu trống choc các trường
        $("input#code-create").val(null);
        $("input#name-role-create").val(null);
        $("input#prioty-create").val(null);
        $('#create-role-popup').modal({ backdrop: 'static' });
    });

    $('#btn-create-role').click(function (e) {



        if (validator_create.validate()) {

            var code = $("input#code-create").val();
            var nameRole = $("input#name-role-create").val();
            var prioty = $("input#prioty-create").val();


            var dataInput = {
                "code": code,
                "roleName": nameRole,
                "prioty": prioty
            };

            if (dataInput != null) {

                CallApiAsynchronous(dataInput, "/api/Roles/them-moi-quyen", false, "POST", function (keyQuaTraVe) {

                    if (keyQuaTraVe) {

                        $.confirm({
                            icon: 'fas fa-check-circle',
                            title: 'Thông báo',
                            content: 'Thêm quyền thành công !',
                            type: 'green',
                            typeAnimated: true,
                            buttons: {
                                OK: {

                                    btnClass: 'btn-green',
                                    action: function () {
                                        // Insert data vào grid
                                        var grid = $("#Grid-Role").data("kendoGrid");
                                        var datasource = grid.dataSource;
                                        datasource.insert(keyQuaTraVe);

                                        $('#create-role-popup').modal('hide');
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

    $("#btn-edit-role").click(function (event) {

        if (validator_edit.validate()) {
            var roleName = $("input#name-role-edit").val();

            if (modelEditRole != null) {
                var dataInput = {
                    "id": modelEditRole.Id,
                    "roleName": roleName,
                    "atRowversion": modelEditRole.AtRowversion
                };

                if (dataInput != null) {

                    CallApiAsynchronous(dataInput, "/api/Roles/chinh-sua-quyen", false, "POST", function (keyQuaTraVe) {
                        if (keyQuaTraVe) {
                            $.confirm({
                                icon: 'fas fa-check-circle',
                                title: 'Thông báo',
                                content: 'Cập nhập quyền thành công !',
                                type: 'green',
                                typeAnimated: true,
                                buttons: {
                                    OK: {
                                        btnClass: 'btn-green',
                                        action: function () {
                                            LoadListRole();
                                            $('#edit-role-popup').modal('hide');
                                        }
                                    }
                                }
                            });
                        }
                    });

                }
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

                        }
                    },
                }
            });

        }
    });

});

function LoadListRole(pageNumber) {

    CallApiAsynchronous(null, "/api/Roles/danh-sach-quyen/?pageNumber=" + pageNumber, true, "GET", function (keyQuaTraVe, totalCount, pageSize) {

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

        var grid = $("#Grid-Role").data("kendoGrid");
        grid.setDataSource(dataSource);
    });

}

function onDeleteRole(e) {

    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    console.log(dataItem.Id + "----" + dataItem.RowVesion);
    var id = dataItem.Id;
    var rowVesion = dataItem.AtRowversion;

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

                    CallApiAsynchronous(dataInput, "/api/Roles/xoa-quyen", false, "POST", function (keyQuaTraVe) {

                        if (keyQuaTraVe != null) {
                            $.alert('Xóa thông tin thành công !');
                            var grid = $("#Grid-Role").data("kendoGrid");
                            var tr = $(e.target).closest("tr");
                            grid.removeRow(tr);
                        }
                    });
                }
            }
        }
    });
}

function onEditRole(e) {
    e.preventDefault();
    modelEditRole = null;

    $("input#name-role-edit").val(null);
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var id = dataItem.Id;
    var AtRowversion = dataItem.AtRowversion;

    modelEditRole = {
        "Id": id,
        "AtRowversion": AtRowversion
    };


    CallApiAsynchronous("null", "/api/Roles/du-lieu-chinhsua-quyen/?idRole=" + id, true, "POST", function (keyQuaTraVe) {
        // disabled username
        $("#tilte-edit-Role").html("Chỉnh sửa Role: <span style='color:red; font-size : 20px ; font-style:italic;>'> " + keyQuaTraVe.RoleName + "</span>");
        $("input#name-role-edit").val(keyQuaTraVe.RoleName);
        $('#edit-role-popup').modal({ backdrop: 'static' });
    });
}

function onPhanQuyen(e) {
    e.preventDefault();
    var token = window.localStorage.getItem('key_Token');
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var id = dataItem.Id;
    window.location = _url + "/menu-role/?idRole=" + id;

}

function onPage(e) {
    LoadListRole(e.page);
}

function onDataBound(e) {
    checkPermissionRow();
}