
$(document).ready(function (e) {

    LoadListSetting(1);
    var validator = $("#from-setting").kendoValidator().data("kendoValidator");
    var validator_edit = $("#from-setting-edit").kendoValidator().data("kendoValidator");

    $('.k-grid-button-create-popup').click(function (e) {

        ////Set lại dữ liệu trống choc các trường

        $("input#code").val(null);
        $("input#value").val(null);
        $("inputdescription").val(null);
        $("#description").val(null);
        $("#multiselect-parent").data("kendoComboBox").value(null);


        LoadComboboxNhom(true);

        $('#setting-popup').modal({ backdrop: 'static' });
    });

    $('#btn-setting').click(function (e) {



        if (validator.validate()) {

            var id = $("input#code").val();
            var value = $("input#value").val();
            var description = $("#description").val();
            var idParent = $("#multiselect-parent").data("kendoComboBox").value();


            var dataInput = {
                "id": id,
                "value": value,
                "description": description,
                "idParent": idParent
            }

            if (dataInput != null) {

                CallApiAsynchronous(dataInput, "/api/Settings/them-moi-setting", false, "POST", function (keyQuaTraVe) {

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
                                        var grid = $("#Grid-Setting").data("kendoGrid");
                                        var datasource = grid.dataSource;
                                        datasource.insert(keyQuaTraVe);

                                        $('#setting-popup').modal('hide');
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


    $('#btn-setting-edit').click(function (e) {


        if (validator_edit.validate()) {

            var AtRowversion = $("input#AtRowversion-edit").val();
            var id = $("input#code-edit").val();
            var value = $("input#value-edit").val();
            var description = $("#description-edit").val();
            var idParent = $("#multiselect-parent-edit").data("kendoComboBox").value();


            var dataInput = {
                "id": id,
                "value": value,
                "description": description,
                "idParent": idParent,
                "rowVersion": AtRowversion
            }

            if (dataInput != null) {

                CallApiAsynchronous(dataInput, "/api/Settings/chinh-sua-setting", false, "POST", function (keyQuaTraVe) {

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
                                       
                                        $('#setting-popup-edit').modal('hide');
                                        LoadListSetting();
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

function LoadListSetting(pageNumber) {

    CallApiAsynchronous(null, "/api/Settings/danh-sach-setting/?pageNumber=" + pageNumber, true, "GET", function (keyQuaTraVe, totalCount, pageSize) {

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

        var grid = $("#Grid-Setting").data("kendoGrid");
        grid.setDataSource(dataSource);
    });

}

function onPage(e) {
    LoadListSetting(e.page);
}

function onDataBound(e) {
    checkPermissionRow();
}

function onDeleteSetting(e) {

    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    console.log(dataItem.Id + "----" + dataItem.RowVesion);
    var id = dataItem.Id;
    var rowVesion = dataItem.RowVersion;

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
                        "id": id,
                        "rowVersion": rowVesion
                    }

                    CallApiAsynchronous(dataInput, "/api/Settings/xoa-setting", false, "POST", function (keyQuaTraVe) {

                        if (keyQuaTraVe != null) {
                            $.alert('Xóa cấu hình thành công !');
                            var grid = $("#Grid-Setting").data("kendoGrid");
                            var tr = $(e.target).closest("tr");
                            grid.removeRow(tr);
                        }
                    });
                }
            }
        }
    });
}

function onEditSetting(e) {
    e.preventDefault();

    $("input#AtRowversion-edit").val(null);
    $("input#code-edit").val(null);
    $("input#value-edit").val(null);
    $("#description-edit").val(null);
    $("#multiselect-parent-edit").data("kendoComboBox").value(null);
 
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var id = dataItem.Id;

    CallApiAsynchronous("null", "/api/Settings/get-setting-edit/?idSetting=" + id, true, "GET", function (keyQuaTraVe) {
       
        LoadComboboxNhom(false);
        $("input#AtRowversion-edit").val(keyQuaTraVe.RowVersion);
        $("#tilte-setting").html("Chỉnh sửa cấu hình: <span style='color:red; font-size : 20px ; font-style:italic;>'> " + id + "</span>");
        $("input#code-edit").val(keyQuaTraVe.Id);
        $("input#value-edit").val(keyQuaTraVe.Value);
        $("#description-edit").val(keyQuaTraVe.Description);
        $("#multiselect-parent-edit").data("kendoComboBox").value(keyQuaTraVe.IdParent);
        $('#setting-popup-edit').modal({ backdrop: 'static' });
    });
}

function LoadComboboxNhom(mode) {
    CallApiAsynchronous("null", "/api/Settings/get-list-combobox-setting-parent", true, "POST", function (keyQuaTraVe) {

        if (mode) {
            var multiselect = $("#multiselect-parent").data("kendoComboBox");
            multiselect.setDataSource(keyQuaTraVe);
        }
        else {
            var multiselect = $("#multiselect-parent-edit").data("kendoComboBox");
            multiselect.setDataSource(keyQuaTraVe);
        }

    });
}