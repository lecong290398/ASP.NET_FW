function error_handler(e) {
    if (e.errors) {
        var message = "Errors:\n";
        $.each(e.errors, function (key, value) {
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += this + "\n";
                });
            }
        });
        alert(message);
    }
}

function datePicker(args) {

    args.element.kendoDatePicker({
        format: "dd/MM/yyyy"
    });
}


var listFileId = [];
var listFileName = [];
var listRemoveFileUpload = [];
var idInformation = "";
var _token = "";
var _refreshToken = "";

var _indexPage = 0;
// Truyền thêm id để lưu tên file trên server là GUI_<tenFile>
function onUpload(e) {

    var fileIds = [];
    for (var i = 0; i < e.files.length; i++) {
        fileIds.push(e.files[i].uid);
    }

    //console.log("onUpload");
    //console.log(fileIds);

    e.data = { fileIds: fileIds };
}

// Truyền thêm id để xóa tên file trên server là GUI_<tenFile>
function onRemove(e) {

    var fileIds = [];
    for (var i = 0; i < e.files.length; i++) {
        fileIds.push(e.files[i].uid);
    }

    //console.log("onRemove");
    //console.log(fileIds);

    e.data = { fileIds: fileIds };
}

// Lưu lại tên file thêm/xóa thành công trên server là GUI_<tenFile>
// Dùng để truyền lên API copy file từ web qua

function onSuccess(e) {

    //console.log("onSuccess");
    //console.log(e.operation);


    if (e.operation === "upload") {
        for (var i = 0; i < e.files.length; i++) {
            listFileId.push(e.files[i].uid);
            listFileName.push(e.files[i].name);
        }
    }
    else if (e.operation === "remove") {
        for (var i = 0; i < e.files.length; i++) {

            for (var j = 0; j < listFileId.length; j++) {
                if (listFileId[j] === e.files[i].uid) {
                    listFileId.splice(j, 1);
                    listFileName.splice(j, 1);
                }
            }
        }
    }

    //console.log(listFileId);
    //console.log(listFileName);
}

function onEditInfromation(e) {
    e.preventDefault();
    
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var id = dataItem.Id;
    window.location.href = '/InfomationUsers/Edit?id=' + id;
}

$(document).ready(function (e) {

    var validator = $("#InformationFromCreate").kendoValidator().data("kendoValidator");
    var validatorEdit = $("#InformationFrom-Edit").kendoValidator().data("kendoValidator");

    //Load trang đầu tiên
    LoadListInfromation(_indexPage);

    $('.k-grid-button-create-popup').click(function (e) {
        $('#createPopup').modal({ backdrop: 'static' });
        listFileId = [];
        listFileName = [];
        $("#files").data("kendoUpload").clearAllFiles();
        LoadDataEditAndCreates(true);
    });

    //Create Information

    $("#CreateInfromation").click(function (event) {
        event.preventDefault();
        var fkAccountObject = $("#FkAccountObject").data("kendoComboBox").value();
        var type = $("#StypeAccountObject").data("kendoComboBox").value();
        var stype = $("#TypeAccountObject").data("kendoComboBox").value();
        var fistName = $("input#FistName").val();
        //var lastName = $("input#LastName").val();
        var lastName = $("#LastName").data("kendoEditor").value();
        var diemSo = $("input#DiemSo").val();
        var ngaySinh = $("#datepicker").data("kendoDatePicker").value();

        if (lastName == "" || lastName == null) {
            $.confirm({
                icon: 'fa fa-spinner fa-spin',
                title: 'Thông báo!',
                type: 'red',
                typeAnimated: true,
                content: 'Trường Tên rỗng !',
                buttons: {
                    tryAgain: {
                        text: 'Đóng',
                        btnClass: 'btn-red',
                        action: function () {

                        }
                    },
                }
            });

            return;
        }

        if (fkAccountObject == "" || fkAccountObject == null) {

            $.confirm({
                icon: 'fa fa-spinner fa-spin',
                title: 'Thông báo!',
                type: 'red',
                typeAnimated: true,
                content: 'Chưa chọn tài khoản !',
                buttons: {
                    tryAgain: {
                        text: 'Đóng',
                        btnClass: 'btn-red',
                        action: function () {

                        }
                    },
                }
            });

            return;

        }

        if (type == "" || type == null) {

            $.confirm({
                icon: 'fa fa-spinner fa-spin',
                title: 'Thông báo!',
                type: 'red',
                typeAnimated: true,
                content: 'Loại tài khoản rỗng!',
                buttons: {
                    tryAgain: {
                        text: 'Đóng',
                        btnClass: 'btn-red',
                        action: function () {

                        }
                    },
                }
            });

            return;

        }
        if (stype == "" || stype == null) {

            $.confirm({
                icon: 'fa fa-spinner fa-spin',
                title: 'Thông báo!',
                type: 'red',
                typeAnimated: true,
                content: 'Kiểu tài khoản rỗng!',
                buttons: {
                    tryAgain: {
                        text: 'Đóng',
                        btnClass: 'btn-red',
                        action: function () {

                        }
                    },
                }
            });

            return;

        }

        if (validator.validate()) {

            var dataInput = {
                "Fk_AccountObject": fkAccountObject,
                "FistName": fistName,
                "LastName": lastName,
                "Kieu": type,
                "Loai": stype,
                "DiemSo": diemSo,
                "NgaySinh": ngaySinh,
                "listFileIds": listFileId,
                "listFileNames": listFileName
            };

            if (dataInput != null) {


                CallApiAsynchronous(dataInput, "/api/InfomationUsersAPI/create-information-api", false, "POST", function (keyQuaTraVe) {

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
                                        var grid = $("#Grid-Information").data("kendoGrid");
                                        var datasource = grid.dataSource;
                                        datasource.insert(keyQuaTraVe);

                                        //Set lại dữ liệu trống choc các trường
                                        $("input#DiemSo").val(null);
                                        $("input#LastName").val(null);
                                        $("input#FistName").val(null);
                                        $("#TypeAccountObject").data("kendoComboBox").value(null);
                                        $("#StypeAccountObject").data("kendoComboBox").value(null);
                                        $("#FkAccountObject").data("kendoComboBox").value(null);

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

    $("#Edit-informtionUser").click(function (event) {
        event.preventDefault();

        var type = $("#StypeAccountObject-edit").data("kendoComboBox").value();
        var stype = $("#TypeAccountObject-edit").data("kendoComboBox").value();
        var fistName = $("input#FistName-edit").val();
        //var lastName = $("input#LastName-edit").val();
        var lastName = $("#LastName-edit").data("kendoEditor").value();
        var diemSo = $("input#DiemSo-edit").val();
        var ngaySinh = $("#datepicker-edit").data("kendoDatePicker").value();
        var isInactive = $("#switchIsInactive-edit").data("kendoSwitch").value();
        var Rowvesion = $("input#RowVesion-edit").val();
        var Id = $("input#id-edit").val();
        var Fk_Account = $("input#FkAccountObject-edit").val();

        if (lastName == "" || lastName == null) {
            $.confirm({
                icon: 'fa fa-spinner fa-spin',
                title: 'Thông báo!',
                type: 'red',
                typeAnimated: true,
                content: 'Trường Tên rỗng !',
                buttons: {
                    tryAgain: {
                        text: 'Đóng',
                        btnClass: 'btn-red',
                        action: function () {

                        }
                    },
                }
            });

            return;
        }


        if (type == "" || type == null) {

            $.confirm({
                icon: 'fa fa-spinner fa-spin',
                title: 'Thông báo!',
                type: 'red',
                typeAnimated: true,
                content: 'Loại tài khoản rỗng!',
                buttons: {
                    tryAgain: {
                        text: 'Đóng',
                        btnClass: 'btn-red',
                        action: function () {

                        }
                    },
                }
            });

            return;

        }
        if (stype == "" || stype == null) {

            $.confirm({
                icon: 'fa fa-spinner fa-spin',
                title: 'Thông báo!',
                type: 'red',
                typeAnimated: true,
                content: 'Kiểu tài khoản rỗng!',
                buttons: {
                    tryAgain: {
                        text: 'Đóng',
                        btnClass: 'btn-red',
                        action: function () {

                        }
                    },
                }
            });

            return;

        }

        if (validatorEdit.validate()) {
  
            var dataInput = {
                "FistName": fistName,
                "LastName": lastName,
                "Kieu": type,
                "Loai": stype,
                "DiemSo": diemSo,
                "NgaySinh": ngaySinh,
                "IsInactive": isInactive,
                "RowVesion": Rowvesion,
                "Id": Id,
                "Fk_AccountObject": Fk_Account,
                "ListFileRemove": listRemoveFileUpload,
                "listFileIds": listFileId,
                "listFileNames": listFileName
            };
            CallApiAsynchronous(dataInput, "/api/InfomationUsersAPI/edit-information-api", false, "POST", function (keyQuaTraVe) {
                if (keyQuaTraVe) {
                    $.confirm({
                        icon: 'fas fa-check-circle',
                        title: 'Thông báo',
                        content: 'Cập nhập thông tin User thành công !',
                        type: 'green',
                        typeAnimated: true,
                        buttons: {
                            OK: {
                                btnClass: 'btn-green',
                                action: function () {
                                    //load trang đầu tiên

                                    LoadListInfromation(_indexPage);
                                    $('#EditPopup').modal('hide');
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

//load danh sách Infromation
function LoadListInfromation(indexPage) {

    CallApiAsynchronous(null, "/api/InfomationUsersAPI/load-list-information/?indexPage=" + indexPage, true, "POST", function (keyQuaTraVe) {
        var grid = $("#Grid-Information").data("kendoGrid");
        grid.setDataSource(keyQuaTraVe);
    });
}

function LoadDataEditAndCreates(typeCreateOrEdit) {

    //Load cb AccountObject
    if (typeCreateOrEdit == true) {
        CallApiAsynchronous("null", "/api/InfomationUsersAPI/load-list-account-object", true, "POST", function (keyQuaTraVe) {

            var CbAccountObj = $("#FkAccountObject").data("kendoComboBox");
            CbAccountObj.setDataSource(keyQuaTraVe);

        });
    }
  
    //Load CB loại tài khoản
    CallApiAsynchronous("null", "/api/InfomationUsersAPI/load-list-type", true, "POST", function (keyQuaTraVe) {
        if (typeCreateOrEdit) {
            var CbType = $("#TypeAccountObject").data("kendoComboBox");
            CbType.setDataSource(keyQuaTraVe);
        }
    });


    //Load CB kiểu tài khoản
    CallApiAsynchronous("null", "/api/InfomationUsersAPI/load-list-stype", true, "POST", function (keyQuaTraVe) {

        if (typeCreateOrEdit) {
            var CbStype = $("#StypeAccountObject").data("kendoComboBox");
            CbStype.setDataSource(keyQuaTraVe);
        } 
    });
}

function onDeleteInfromation(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    console.log(dataItem.Id + "----" + dataItem.RowVesion);
    var id = dataItem.Id;
    var rowVesion = dataItem.RowVesion;

    $.confirm({
        title: 'Thông báo !',
        content: "Bạn chắc chắc muốn xóa thông tin tài khoản này !",
        buttons: {
            stop: {
                text: 'No!'
            },
            go: {
                text: 'Yes!', // With spaces and symbols
                action: function () {

                    var dataInput = {
                        Id: id,
                        RowVesion: rowVesion
                    };

                    CallApiAsynchronous(dataInput, "/api/InfomationUsersAPI/delete-informatio-api", false, "POST", function (keyQuaTraVe) {

                        if (keyQuaTraVe != null) {

                            $.alert('Xóa thông tin thành công !');
                            var grid = $("#Grid-Information").data("kendoGrid");
                            var tr = $(e.target).closest("tr");
                            grid.removeRow(tr);
                        }
                    });
                }
            }
        }
    });
}

function onPaging(arg) {

    _indexPage = arg.page - 1;
    console.log(_indexPage);
    LoadListInfromation(_indexPage);
}