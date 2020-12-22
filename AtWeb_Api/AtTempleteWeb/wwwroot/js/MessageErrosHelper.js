function ShowMessageWithDataErros(data) {

    switch (data.Error) {
        case 0:
            ShowconfirmErros("Không tìm thấy !");
            break;
        case 1:
            ShowconfirmErros("Phiên giao dịch hết hạn !");
            break;

        case 5:
            ShowconfirmErros("Mã đã tồn tại. Vui lòng thay đổi và thử lại !");
            break;

        case 6:
            ShowconfirmErros("Cập nhập dữ liệu thất bại. Vui lòng thử lại !");
            break;

        case 8:
            ShowconfirmErros("Tải dữ liệu thất bại. Vui lòng thử lại !");
            break;

        case 11:
            ShowconfirmErros("Xóa dữ liệu thất bại. Vui lòng thử lại !");
            break;

        case 12:
            ShowconfirmErros("Upload file thất bại. Vui lòng thử lại !");
            break;

        case 15:
            ShowconfirmErros("Remove file thất bại. Vui lòng thử lại !");
            break;

        case 16:
            GotoPageErros(data);
            break;

        case 18:
            GotoPageErros(data);
            break;

        case 19:
            GotoPageErros(data);
            break;

        default:
            processApiDataNG(data);
            break;
    }

}

function ShowconfirmErros(strMessageError) {
    $.confirm({
        title: 'Thông báo lỗi xử lý !',
        content: '' + strMessageError,
        type: 'red',
        typeAnimated: true,
        buttons: {
            tryAgain: {
                text: 'Đóng',
                btnClass: 'btn-red',
                action: function () {

                }
            }
        }
    });
}

function GotoPageErros(data) {
    window.location = _url + "/Home/PageErros/?statusCode=" + data.Error;
}