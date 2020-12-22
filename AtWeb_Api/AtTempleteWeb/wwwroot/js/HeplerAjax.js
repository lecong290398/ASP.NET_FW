
function RefreshToken(KQrefreshToken) {

    console.log("RefreshToken Now");

    $.ajax({
        url: _url + "/RefreshToken",
        type: "POST",
        contentType: "application/json",
        success: function (data) {
            // Truong hop ok co du lieu
            if (data != null) {

                ////xóa các key liên quan token
                //window.localStorage.removeItem('key_Token');
                //window.localStorage.removeItem('refreshToken');
                //// tạo key mới
                //localStorage.setItem("key_Token", data.accessToken);
                //localStorage.setItem("refreshToken", data.refreshToken);

                console.log("Xác thực đăng nhập thành công !");

                return KQrefreshToken(true);
            } else {
                console.log(data.Error);
                if (data.Error == 16) {
                    window.location = _url + "/Home/PageErros/?statusCode=" + 16;
                }else if (data.Error == 2) {
                    window.location = _url + "/Home/PageErros/?statusCode=" + 2;
                }
                else {
                    window.location = _url + "/dang-nhap";
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log(textStatus + "\n" + errorThrown);

            ////xóa các key liên quan token
            //window.localStorage.removeItem('key_Token');
            //window.localStorage.removeItem('refreshToken');
            window.location = _url + "/Home/PageErros/?statusCode=" + 19;
        }
    });
}


function CallApiAsynchronous(inputData, linkApi, typeCall, method, hamTraVeKQ) {

    var token = window.localStorage.getItem('key_Token');
    var refreshToken = window.localStorage.getItem('refreshToken');

    if (typeCall == true) {

        $.ajax({
            url: _urlApi + linkApi,
            type: method,
            contentType: "application/json",
            success: function (data) {
                // Truong hop ok co du lieu
                if (data.IsOk === true) {
                    if (data.PayLoad != null) {

                        hamTraVeKQ(data.PayLoad, data.TotalCount, data.PageSize);
                    }
                }
                else {

                    ShowMessageWithDataErros(data);

                    //if (data.Error === 18) {
                    //    window.location = _url + "/Home/PageErros/?statusCode=" + data.Error;
                    //}
                    //else {
                    //    processApiDataNG(data);
                    //}

                }
            }, beforeSend: function (xhr, settings) { xhr.setRequestHeader('Authorization', 'Bearer ' + token); },
            error: function (xhr, textStatus, errorThrown) {

                console.log(textStatus + "\n" + errorThrown);

                if (xhr.status == "401") {


                    RefreshToken(function ketQuaTraVe() {
                        CallApiAsynchronous(inputData, linkApi, typeCall, method, hamTraVeKQ);
                    })

                }
                else {
                    console.log(textStatus + "\n" + errorThrown);
                    processApiError(xhr, textStatus, errorThrown);
                }
            }
        });
    }
    else if (typeCall == false) {

        $.ajax({
            url: _urlApi + linkApi,
            type: method,
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(inputData),
            success: function (data) {
                // Truong hop ok co du lieu
                if (data.IsOk === true) {
                    if (data.PayLoad != null) {

                        hamTraVeKQ(data.PayLoad, data.TotalCount);
                    }
                }
                else {

                    ShowMessageWithDataErros(data);

                    //if (data.Error === 18) {
                    //    window.location = _url + "/Home/PageErros/?statusCode=" + data.Error;
                    //}
                    //else {
                    //    processApiDataNG(data);
                    //}

                }
            }, beforeSend: function (xhr, settings) { xhr.setRequestHeader('Authorization', 'Bearer ' + token); },
            error: function (xhr, textStatus, errorThrown) {



                console.log(textStatus + "\n" + errorThrown);

                if (xhr.status == "401") {

                    RefreshToken(function (KqTraVe) {
                        CallApiAsynchronous(inputData, linkApi, typeCall, method, hamTraVeKQ);
                    });
                }
                else if (xhr.status == "500") {
                    console.log(textStatus + "\n" + errorThrown);
                    processApiError(xhr, textStatus, errorThrown);
                }
                else {

                    var readJson = JSON.parse(xhr.responseText);
                    console.log(readJson);

                    if (readJson.Errors != null) {
                        var stErros = "";
                        for (var i = 0; i < readJson.Errors.length; i++) {
                            console.log(readJson.Errors[i]);
                            stErros += ' <label class="control-label" style="color:red; margin-bottom: .5rem; font-weight: 500; line-height: 1.2; font-style: italic;">' + " _ " + readJson.Errors[i] + " !" + '</label> <br>';
                        }
                        $.confirm({
                            title: 'Thông báo ?',
                            content: '' + stErros,
                            autoClose: 'close|10000',
                            buttons: {
                                close: {
                                    btnClass: 'btn-warning',
                                    text: 'Đóng',
                                    action: function () {

                                    }
                                }
                            }
                        });
                    }
                }
            }
        });
    }
}

