$(document).ready(function (e) {

    LoadDanhSachMenuFuction(1);

    var validator_create = $("#from-create-menufunction").kendoValidator().data("kendoValidator");
    var validator_edit = $("#from-edit-menufunction").kendoValidator().data("kendoValidator");

    $('.k-grid-button-create-popup').click(function (e) {

        //Set lại dữ liệu trống choc các trường
        $("input#tilte-create").val(null);
        $("input#controller-name-create").val(null);
        $("input#acction-name-create").val(null);
        $("input#sort-index-create").val(null);
        $("input#controller-name-view-create").val(null);
        $("input#acction-name-viewcode-create").val(null);
        $("#note-create").val(null);
        $("#MenuFunctionSub-create").data("kendoMultiColumnComboBox").value(null);
        $('#IsMenu-create').prop('checked', false);
        $('#IsPublic-create').prop('checked', false);
        LoadComboxboxSubMenu(true);
        $('#create-menufunction-popup').modal({ backdrop: 'static' });
    });


    $('#btn-create-menufunction').click(function (e) {
        e.preventDefault();
        if (validator_create.validate()) {

            var Tile = $("input#tilte-create").val();
            var ControllerName = $("input#controller-name-create").val();
            var AcctionName = $("input#acction-name-create").val();
            var SortIndex = $("input#sort-index-create").val();
            var ControllerNameView = $("input#controller-name-view-create").val();
            var AcctionNameView = $("input#acction-name-viewcode-create").val();
            var Note = $("#note-create").val();
            var Submenu = $("#MenuFunctionSub-create").data("kendoMultiColumnComboBox").value();
            var isMenu = $('#IsMenu-create').is(":checked");
            var isPublic = $('#IsPublic-create').is(":checked");


            var dataInput = {
                "title": Tile,
                "controllerName": ControllerName,
                "acctionName": AcctionName,
                "controllerNameView": ControllerNameView,
                "acctionNameView": AcctionNameView,
                "sortIndex": SortIndex,
                "isMenu": isMenu,
                "isPublic": isPublic,
                "fK_MenuSubGroup": Submenu,
                "note": Note
            };

            if (dataInput != null) {

                CallApiAsynchronous(dataInput, "/api/MenuFunctions/them-moi-menu-function", false, "POST", function (keyQuaTraVe) {

                    if (keyQuaTraVe) {

                        $.confirm({
                            icon: 'fas fa-check-circle',
                            title: 'Thông báo',
                            content: 'Thêm chức năng thành công !',
                            type: 'green',
                            typeAnimated: true,
                            buttons: {
                                OK: {

                                    btnClass: 'btn-green',
                                    action: function () {
                                        // Insert data vào grid
                                        var grid = $("#Grid-MenuFunction").data("kendoGrid");
                                        var datasource = grid.dataSource;
                                        datasource.insert(keyQuaTraVe);

                                        $('#create-menufunction-popup').modal('hide');
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

    $("#btn-edit-menufunction").click(function (event) {

        if (validator_edit.validate()) {

            var Id = $("input#id-edit").val();
            var Tile = $("input#tilte-edit").val();
            var ControllerName = $("input#controller-name-edit").val();
            var AcctionName = $("input#acction-name-edit").val();
            var SortIndex = $("input#sort-index-edit").val();
            var ControllerNameView = $("input#controller-name-view-edit").val();
            var AcctionNameView = $("input#acction-name-viewcode-edit").val();
            var Note = $("#note-edit").val();
            var Submenu = $("#MenuFunctionSub-edit").data("kendoMultiColumnComboBox").value();
            var isMenu = $('#IsMenu-edit').is(":checked");
            var isPublic = $('#IsPublic-edit').is(":checked");

            var dataInput = {
                "id": Id,
                "title": Tile,
                "controllerName": ControllerName,
                "acctionName": AcctionName,
                "controllerNameView": ControllerNameView,
                "acctionNameView": AcctionNameView,
                "sortIndex": SortIndex,
                "isMenu": isMenu,
                "isPublic": isPublic,
                "fK_MenuSubGroup": Submenu,
                "Note": Note
            };

            if (dataInput != null) {

                CallApiAsynchronous(dataInput, "/api/MenuFunctions/chinh-sua-menu-function", false, "POST", function (keyQuaTraVe) {
                    if (keyQuaTraVe) {
                        $.confirm({
                            icon: 'fas fa-check-circle',
                            title: 'Thông báo',
                            content: 'Cập nhập chức năng thành công !',
                            type: 'green',
                            typeAnimated: true,
                            buttons: {
                                OK: {
                                    btnClass: 'btn-green',
                                    action: function () {
                                        LoadDanhSachMenuFuction();
                                        $('#edit-menufunction-popup').modal('hide');
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

function LoadDanhSachMenuFuction(pageNumber) {
    CallApiAsynchronous(null, "/api/MenuFunctions/danh-sach-menu-function/?pageNumber=" + pageNumber, true, "GET", function (keyQuaTraVe, totalCount, pageSize) {

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

        var grid = $("#Grid-MenuFunction").data("kendoGrid");
        grid.setDataSource(dataSource);

    });
}

function onPage(e) {
    LoadDanhSachMenuFuction(e.page);
}

function onDataBound(e) {
    checkPermissionRow();
}

function LoadComboxboxSubMenu(type) {
    CallApiAsynchronous(null, "/api/MenuFunctionSubGroups/danh-sach-menu-function", true, "GET", function (keyQuaTraVe) {

        if (type) {
            var multicolumncombobox = $("#MenuFunctionSub-create").data("kendoMultiColumnComboBox");
            multicolumncombobox.setDataSource(keyQuaTraVe);
        }
        else {
            var multicolumncombobox = $("#MenuFunctionSub-edit").data("kendoMultiColumnComboBox");
            multicolumncombobox.setDataSource(keyQuaTraVe);
        }

    });
}

function onEditMenuFunction(e) {
    e.preventDefault();

    //Set lại dữ liệu trống choc các trường
    $("input#tilte-edit").val(null);
    $("input#id-edit").val(null);
    $("input#controller-name-edit").val(null);
    $("input#acction-name-edit").val(null);
    $("input#sort-index-edit").val(null);
    $("input#controller-name-view-edit").val(null);
    $("input#acction-name-viewcode-edit").val(null);
    $("#note-edit").val(null);
    $("#MenuFunctionSub-edit").data("kendoMultiColumnComboBox").value(null);
    $('#IsMenu-edit').prop('checked', false);
    $('#IsPublic-edit').prop('checked', false);

    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var id = dataItem.Id;
    CallApiAsynchronous("null", "/api/MenuFunctions/get-menu-function-edit/?idMenuFunction=" + id, true, "GET", function (keyQuaTraVe) {
        LoadComboxboxSubMenu(false);
        // disabled username
        $("#tilte-edit").html("Chỉnh sửa chức năng: <span style='color:red; font-size : 20px ; font-style:italic;>'> " + id + "</span>");
        $("input#tilte-edit").val(keyQuaTraVe.Title);
        $("input#id-edit").val(keyQuaTraVe.Id);
        $("input#controller-name-edit").val(keyQuaTraVe.ControllerName);
        $("input#acction-name-edit").val(keyQuaTraVe.AcctionName);
        $("input#sort-index-edit").val(keyQuaTraVe.SortIndex);
        $("input#controller-name-view-edit").val(keyQuaTraVe.ControllerNameView);
        $("input#acction-name-viewcode-edit").val(keyQuaTraVe.AcctionNameView);
        $("#note-edit").val(keyQuaTraVe.Note);
        $("#MenuFunctionSub-edit").data("kendoMultiColumnComboBox").value(keyQuaTraVe.FK_MenuSubGroup);
        $('#IsMenu-edit').prop('checked', keyQuaTraVe.IsMenu);
        $('#IsPublic-edit').prop('checked', keyQuaTraVe.IsPublic);

        $('#edit-menufunction-popup').modal({ backdrop: 'static' });
    });


}

function onDeleteMenuFunction(e) {

    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    console.log(dataItem.Id + "----" + dataItem.RowVesion);
    var id = dataItem.Id;

    $.confirm({
        title: 'Thông báo !',
        content: "Bạn chắc chắc muốn xóa chức năng này !",
        buttons: {
            stop: {
                text: 'No!'
            },
            go: {
                text: 'Yes!', // With spaces and symbols
                action: function () {



                    CallApiAsynchronous(null, "/api/MenuFunctions/delete-menu-function/?idMenuFunction=" + id, true, "POST", function (keyQuaTraVe) {

                        if (keyQuaTraVe != null) {
                            $.alert('Xóa chức năng thành công !');
                            var grid = $("#Grid-MenuFunction").data("kendoGrid");
                            var tr = $(e.target).closest("tr");
                            grid.removeRow(tr);
                        }
                    });
                }
            }
        }
    });
}

function DanhSachRole(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    console.log(dataItem.Id + "----" + dataItem.RowVesion);
    var id = dataItem.Id;
     
    CallApiAsynchronous(null, "/api/MenuFunctions/danh-sach-role/?idMenuFunction=" + id, true, "GET", function (keyQuaTraVe) {
        $("#exampleModalLongTitle-edit").html("Danh sách quyền chức năng : <span style='color:red; font-size : 20px ; font-style:italic;>'> " + id + "</span>");
        var grid = $("#Grid-List-Role").data("kendoGrid");
        grid.setDataSource(keyQuaTraVe);
        $('#list-role-popup').modal({ backdrop: 'static' });
    });

}

function DanhSachTaiKhoan(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    console.log(dataItem.Id + "----" + dataItem.RowVesion);
    var id = dataItem.Id;

    CallApiAsynchronous(null, "/api/MenuFunctions/danh-sach-tai-khoan/?idMenuFunction=" + id, true, "GET", function (keyQuaTraVe) {
        $("#exampleModalLongTitle-edit").html("Danh sách tài khoản chức năng : <span style='color:red; font-size : 20px ; font-style:italic;>'> " + id + "</span>");
        var grid = $("#Grid-List-Account").data("kendoGrid");
        grid.setDataSource(keyQuaTraVe);
        $('#list-account-popup').modal({ backdrop: 'static' });
    });
}