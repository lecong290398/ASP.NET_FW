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

var listFileId = [];
var listFileName = [];
var listRemoveFileUpload = [];
var idInformation = "";
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
    LoadDataEditAndCreates();

    //Khởi tạo lại dữ liệu
    listFileId = [];
    listFileName = [];
    idInformation = "";
    listRemoveFileUpload = [];
    $("#files_edit").data("kendoUpload").clearAllFiles();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var id = dataItem.Id;
    $.ajax({
        url: _url + "/api/InfomationUsersAPI/get-information-with-id/?idInformation=" + id,
        type: "GET",
        contentType: "application/json",
        success: function (data) {
            if (data != null) {
                // Truong hop ok co du lieu
                if (data.IsOk === true) {


                    var type = $("#StypeAccountObject-edit").data("kendoComboBox").value(data.PayLoad.Loai);
                    var stype = $("#TypeAccountObject-edit").data("kendoComboBox").value(data.PayLoad.Kieu);
                    var fistName = $("input#FistName-edit").val(data.PayLoad.FistName);
                    var lastName = $("input#LastName-edit").val(data.PayLoad.LastName);
                    var lastName = $("input#AccountName-edit").val(data.PayLoad.Fk_AccountObject);
                    var diemSo = $("input#DiemSo-edit").val(data.PayLoad.DiemSo);
                    var Id = $("input#id-edit").val(data.PayLoad.Id);
                    var Fk_Account = $("input#FkAccountObject-edit").val(data.PayLoad.Id);
                    var Rowvesion = $("input#RowVesion-edit").val(data.PayLoad.RowVesion);
                    var ngaySinh = $("#datepicker-edit").data("kendoDatePicker").value(data.PayLoad.NgaySinh);

                    //gán id
                    idInformation = Id;
                    //gen list file đính kèm
                    $(".file-upload").remove();
                    for (var f = 0; f < data.PayLoad.ListFileAttchment.length; f++) {

                        $("#list-file-upload").append(
                            "<div class='row file-upload' id ='"
                            + data.PayLoad.ListFileAttchment[f].AttachmentID
                            + "'><div class='col-sm-9'><label class='control-label'>"
                            + data.PayLoad.ListFileAttchment[f].FileName
                            + " </label></div ><div class='col-sm-3'><button  onclick='dowloadfile(this.id)' class='btn btn-brown' id='"
                            + data.PayLoad.ListFileAttchment[f].AttachmentID
                            + "' ><i class='fas fa-ban mr-1'></i>Tải file</button><button  onclick='deleteFileUpload(this.id)' class='btn btn-brown' id='"
                            + data.PayLoad.ListFileAttchment[f].AttachmentID
                            + "' ><i class='fas fa-ban mr-1'></i>Xóa file</button></div></div>"
                        );
                    }
                    if (data.PayLoad.ListFileAttchment.length == 0) {
                        $("#list-file-upload").append("");
                    }

                    $('#EditPopup').modal({ backdrop: 'static' });

                }
                else {
                    if (data.Error == 0) {
                        $.alert('Tải dữ liệu thất bại !. Vui lòng thử lại !');
                    }
                }
            } else {
                $.alert('Lỗi đường truyền ! Vui lòng kiểm tra lại');
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log(textStatus + "\n" + errorThrown);
        }

    });
}

function deleteFileUpload(clicked_id) {
    listRemoveFileUpload.push(clicked_id);
    $("#" + clicked_id).remove();
}

function dowloadfile(clicked_id) {
    window.open(_url + "/api/InfomationUsersAPI/tai-file-dinh-kem/?idFile=" + clicked_id);
}

$(document).ready(function (e) {

    //load danh sách Infromation
    $.ajax({
        url: _url + "/api/InfomationUsersAPI/load-list-information",
        type: "GET",
        contentType: "application/json",
        success: function (data) {
            // Truong hop ok co du lieu
            if (data.IsOk === true) {
                if (data.PayLoad != null) {
                    var grid = $("#Grid-Information").data("kendoGrid");
                    grid.setDataSource(data.PayLoad);
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log(textStatus + "\n" + errorThrown);
        }
    });

    $('.k-grid-button-create-popup').click(function (e) {
        $('#createPopup').modal({ backdrop: 'static' });
        LoadDataEditAndCreates();
        listFileId = [];
        listFileName = [];
        $("#files").data("kendoUpload").clearAllFiles();
    });

    //Create Information
    $('#create-informtionUser').click(function (e) {

        LoadDataEditAndCreates();

        var fkAccountObject = $("#FkAccountObject").data("kendoComboBox").value();
        var type = $("#StypeAccountObject").data("kendoComboBox").value();
        var stype = $("#TypeAccountObject").data("kendoComboBox").value();
        var fistName = $("input#FistName").val();
        var lastName = $("input#LastName").val();
        var diemSo = $("input#DiemSo").val();
        var ngaySinh = $("#datepicker").data("kendoDatePicker").value();

        $.ajax({
            url: _url + "/api/InfomationUsersAPI/create-information-api",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                Fk_AccountObject: fkAccountObject,
                FistName: fistName,
                LastName: lastName,
                Kieu: type,
                Loai: stype,
                DiemSo: diemSo,
                NgaySinh: ngaySinh,
                listFileIds: listFileId,
                listFileNames: listFileName
            }),
            success: function (data) {
                // Truong hop ok co du lieu
                if (data.IsOk === true) {
                    if (data.PayLoad != null) {
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
                                        datasource.insert(data.PayLoad);

                                        //Set lại dữ liệu trống choc các trường
                                        $("input#DiemSo").val(null);
                                        $("input#LastName").val(null);
                                        $("input#FistName").val(null);
                                        $("#TypeAccountObject").data("kendoComboBox").value(null);
                                        $("#StypeAccountObject").data("kendoComboBox").value(null);
                                        $("#FkAccountObject").data("kendoComboBox").value(null);

                                    }
                                }
                            }
                        });
                    }
                    else {
                        $.confirm({
                            icon: 'fas fa-exclamation-triangle',
                            title: 'Thông báo',
                            content: 'Thêm thông tin tài khoản thất bại!',
                            type: 'red',
                            typeAnimated: true,
                            buttons: {
                                OK: {
                                    btnClass: 'btn-red',
                                    action: function () {

                                    }
                                }
                            }
                        })
                    }
                }
                else if (data.Error == 1) {
                    $.confirm({
                        icon: 'fas fa-exclamation-triangle',
                        title: 'Thông báo',
                        content: 'Thêm thông tin tài khoản thất bại!',
                        type: 'red',
                        typeAnimated: true,
                        buttons: {
                            OK: {
                                btnClass: 'btn-red',
                                action: function () {

                                }
                            }
                        }
                    })
                }
                $('#createPopup').modal('hide');
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log(textStatus + "\n" + errorThrown);
            }
        });
    });

    $('#Edit-informtionUser').click(function (e) {
        var type = $("#StypeAccountObject-edit").data("kendoComboBox").value();
        var stype = $("#TypeAccountObject-edit").data("kendoComboBox").value();
        var fistName = $("input#FistName-edit").val();
        var lastName = $("input#LastName-edit").val();
        var diemSo = $("input#DiemSo-edit").val();
        var ngaySinh = $("#datepicker-edit").data("kendoDatePicker").value();
        var isInactive = $("#switchIsInactive-edit").data("kendoSwitch").value();
        var Rowvesion = $("input#RowVesion-edit").val();
        var Id = $("input#id-edit").val();
        var Fk_Account = $("input#FkAccountObject-edit").val();


        $.ajax({
            url: _url + "/api/InfomationUsersAPI/edit-information-api",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                FistName: fistName,
                LastName: lastName,
                Kieu: type,
                Loai: stype,
                DiemSo: diemSo,
                NgaySinh: ngaySinh,
                IsInactive: isInactive,
                RowVesion: Rowvesion,
                Id: Id,
                Fk_AccountObject: Fk_Account,
                ListFileRemove: listRemoveFileUpload,
                listFileIds: listFileId,
                listFileNames: listFileName
            }),
            success: function (data) {
                // Truong hop ok co du lieu
                if (data.IsOk === true) {
                    if (data.PayLoad != null) {
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
                                        // Load data 
                                        var grid = $("#Grid-Information").data("kendoGrid");
                                        grid.setDataSource(data.PayLoad);
                                    }
                                }
                            }
                        });
                    }
                } else {
                    if (data.Error == 2) {
                        $.alert('Phiên giao xử lý hết hạn. Vui lòng thử lại !');
                    }
                    else if (data.Error == 0) {
                        $.alert('Cập nhập thông tin tài khoản thất bại. Vui lòng thử lại !');
                    }

                }

                $('#EditPopup').modal('hide');
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log(textStatus + "\n" + errorThrown);
            }
        });

    });

});

function LoadDataEditAndCreates() {
    //Load cb AccountObject
    $.ajax({
        url: _url + "/api/InfomationUsersAPI/load-list-account-object",
        type: "POST",
        contentType: "application/json",
        success: function (data) {
            // Truong hop ok co du lieu
            if (data.IsOk === true) {
                if (data.PayLoad != null) {
                    var CbAccountObj = $("#FkAccountObject").data("kendoComboBox");
                    CbAccountObj.setDataSource(data.PayLoad);
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log(textStatus + "\n" + errorThrown);
        }
    });


    //Load CB loại tài khoản
    $.ajax({
        url: _url + "/api/InfomationUsersAPI/load-list-type",
        type: "POST",
        contentType: "application/json",
        success: function (data) {
            // Truong hop ok co du lieu
            if (data.IsOk === true) {
                if (data.PayLoad != null) {
                    var CbType = $("#TypeAccountObject").data("kendoComboBox");
                    CbType.setDataSource(data.PayLoad);
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log(textStatus + "\n" + errorThrown);
        }
    });

    //Load CB kiểu tài khoản
    $.ajax({
        url: _url + "/api/InfomationUsersAPI/load-list-stype",
        type: "POST",
        contentType: "application/json",
        success: function (data) {
            // Truong hop ok co du lieu
            if (data.IsOk === true) {
                if (data.PayLoad != null) {
                    var CbStype = $("#StypeAccountObject").data("kendoComboBox");
                    CbStype.setDataSource(data.PayLoad);
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log(textStatus + "\n" + errorThrown);
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

                    $.ajax({
                        url: _url + "/api/InfomationUsersAPI/delete-informatio-api",
                        type: "POST",
                        contentType: "application/json",
                        data: JSON.stringify({
                            Id: id,
                            RowVesion: rowVesion
                        }),
                        success: function (data) {
                            if (data != null) {
                                // Truong hop ok co du lieu
                                if (data.IsOk === true) {

                                    var grid = $("#Grid-Information").data("kendoGrid");
                                    var tr = $(e.target).closest("tr");
                                    grid.removeRow(tr);


                                    $.alert('Xóa thông tin thành công !');
                                }
                                else {

                                    if (data.Error == 2) {
                                        $.alert('Phiên giao xử lý hết hạn. Vui lòng thử lại !');
                                    }
                                    else if (data.Error == 0) {
                                        $.alert('Xóa thông tin tài khoản thất bại. Vui lòng thử lại !');

                                    }
                                }


                            } else {
                                $.alert('Lỗi đường truyền ! Vui lòng kiểm tra lại');
                            }
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            console.log(textStatus + "\n" + errorThrown);
                        }

                    });



                }
            }
        }
    });
}