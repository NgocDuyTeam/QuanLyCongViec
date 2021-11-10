var _baseUrl = $("#config_ApiUrl").val();
var _tenKhoa = $("#login_TenKhoa").val();
var _idCanBo = $("#login_IdCanBo").val();
var _idKhoa = $("#login_IdKhoa").val();

//setTimeout(function () {
//    $.gritter.add({
//        title: 'Thông báo',
//        text: 'Không lấy được thông tin config.',
//        time: 2000
//    });
//}, 0);

$(".search_date_VN").datepicker({
    //buttonImage: "/content/ui/calendar-ico.png",
    buttonImageOnly: true,
    changeMonth: true,
    changeYear: true,
    yearRange: '1950:2050',
    dateFormat: 'dd/mm/yy',
});