// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ajaxSuccess(function () {
    hideLoading();
});



$.ajaxSetup({
    beforeSend: function (xhr) {
        showLoading();
        xhr.setRequestHeader("AtUserToken", _atUserToken);
    }
});

function KhongCoQuyen() {
    $.confirm({
        title: 'Thông báo!',
        content: 'Bạn không có quyền truy cập!',
        type: 'red',
        typeAnimated: true,
        buttons: {
            close: {
                text: 'Đóng',
                btnClass: 'btn-red',
                action: function () {
                    window.history.back();
                }
            }
        }
    });
}

function processApiError(xhr, textStatus, errorThrown) {
    hideLoading();

    $.confirm({
        title: 'Thông báo!',
        content: 'Kết nối với máy chủ có lỗi về đường truyền.',
        type: 'red',
        typeAnimated: true,
        buttons: {
            close: {
                text: 'Đóng',
                btnClass: 'btn-red'
            }
        }
    });
}




function processApiDataNG(data) {
    hideLoading();

    $.confirm({
        title: 'Thông báo!',
        content: 'Truy vấn máy chủ trả về kết quả không thành công.',
        type: 'red',
        typeAnimated: true,
        buttons: {
            close: {
                text: 'Đóng',
                btnClass: 'btn-red'
            }
        }
    });
}

function getFriendlyByte(bytes) {
    if (bytes === 0) { return "0.00 B"; }
    var e = Math.floor(Math.log(bytes) / Math.log(1024));
    return (bytes / Math.pow(1024, e)).toFixed(2) + ' ' + ' KMGTP'.charAt(e) + 'B';
}

function kendoFastReDrawRow(grid, row) {
    var dataItem = grid.dataItem(row);

    var rowChildren = $(row).children('td[role="gridcell"]');

    for (var i = 0; i < grid.columns.length; i++) {

        var column = grid.columns[i];
        var template = column.template;
        var cell = rowChildren.eq(i);

        if (template !== undefined) {
            var kendoTemplate = kendo.template(template);

            // Render using template
            cell.html(kendoTemplate(dataItem));
        } else {
            var fieldValue = dataItem[column.field];

            var format = column.format;
            var values = column.values;

            if (values !== undefined && values != null) {
                // use the text value mappings (for enums)
                for (var j = 0; j < values.length; j++) {
                    var value = values[j];
                    if (value.value == fieldValue) {
                        cell.html(value.text);
                        break;
                    }
                }
            } else if (format !== undefined) {
                // use the format
                cell.html(kendo.format(format, fieldValue));
            } else {
                // Just dump the plain old value
                cell.html(fieldValue);
            }
        }
    }
}

function showLoading() {
    $("#div-overlay").show();
}
function hideLoading() {
    $("#div-overlay").hide();
}


function checkPermissionRow() {
    console.log("Check All Permission In Site.js");
    var listItem = $(".atpermissioncheckrow");
    console.log(listItem);
    if (listItem.length > 0) {
        // Nếu có item thì mình mới tiến hành gọi API để check permission
        $.ajax({
            url: _urlApi + "/*get-permission*",
            type: "GET",
            dataType: 'json',
            success: function (data) {
                if (data.IsOk === true) {
                    // Lặp danh sách Item trên layout để kiểm tra quyền được phép sử dụng
                    //console.log("ajax OK");
                    for (var i = 0; i < listItem.length; i++) {
                        var itemLayout = listItem[i];
                        var controllerLayout = itemLayout.getAttribute('atpermisscontroller');
                        var acctionLayout = itemLayout.getAttribute('atpermissaction');
                        var atGridId = itemLayout.getAttribute('atgridid');
                        var atGridColIndex = itemLayout.getAttribute('atgridcolindex');
                        //console.log(controllerLayout);
                        //console.log(acctionLayout);
                        var havePermission = false;
                        for (var j = 0; j < data.PayLoad.length; j++) {
                            var itemData = data.PayLoad[j];
                            if (itemData.ControllerNameView == controllerLayout && itemData.AcctionNameView == acctionLayout) {
                                // Người dùng có quyền
                                havePermission = true;
                                break;
                            }
                        }
                        //console.log("Check Result");
                        //console.log(havePermission);
                        if (havePermission == false) {
                            if (atGridId !== null && atGridId !== undefined) {
                                var grid = $("#" + atGridId).data("kendoGrid");
                                grid.hideColumn(Number.parseInt(atGridColIndex));
                                $(".k-grid-button-create-popup").css("display", "none");
                            }
                            else {
                                // cho display:none
                          
                                $(itemLayout).css("display", "none");
                            }
                        }

                    }

                }
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log(textStatus + "\n" + errorThrown);
            }
        });
    }
}