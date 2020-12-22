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

function onEditInfromation(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    var id = dataItem.Id;
    $.ajax({
        url: _url + "/Get-Data-Edit/?idInformation=" + id,
        type: "POST",
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
                        url: _url + "/DeleteInformationAjax",
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

$(document).ready(function (e) {


    $('.k-grid-button-create-popup').click(function (e) {
        $('#createPopup').modal({ backdrop: 'static' });
    });

    $('.k-grid-button-create-basic').click(function (e) {
        var url = '@Url.Action( "CreateInformation_Basic","InformationUserBasic")';
        window.location.href = url;
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
            url: _url + "/Edit-Information",
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
                Fk_AccountObject: Fk_Account
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



    //Create Information
    $('#create-informtionUser').click(function (e) {
        var fkAccountObject = $("#FkAccountObject").data("kendoComboBox").value();
        var type = $("#StypeAccountObject").data("kendoComboBox").value();
        var stype = $("#TypeAccountObject").data("kendoComboBox").value();
        var fistName = $("input#FistName").val();
        var lastName = $("input#LastName").val();
        var diemSo = $("input#DiemSo").val();
        var ngaySinh = $("#datepicker").data("kendoDatePicker").value();

        $.ajax({
            url: _url + "/tao-moi-thongtin-user",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                Fk_AccountObject: fkAccountObject,
                FistName: fistName,
                LastName: lastName,
                Kieu: type,
                Loai: stype,
                DiemSo: diemSo,
                NgaySinh: ngaySinh
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
});