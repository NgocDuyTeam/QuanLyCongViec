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

function GetlstPage(pageTotal, pageIndex, functionLoad) {
    var str = '';
    if (pageTotal > 1) {
        // Button previous
        if (pageIndex > 1)
            str = str + '<li class="paginate_button previous" data-ng-click="' + functionLoad + '(1)"><a> << </a></li>';
        else
            str = str + '<li class="paginate_button previous disabled"><a > << </a></li>';
        // Các Button giữa
        if (pageTotal <= 9) {
            for (var i = 1; i < pageTotal + 1; i++) {
                if (pageIndex == i) {
                    str = str + '<li class="paginate_button active"><a >' + i + '</a></li>';
                }
                else {
                    str = str + '<li class="paginate_button " data-ng-click="' + functionLoad + '(' + i + ')"><a >' + i + '</a></li>'
                }
            }
        }
        else // Neu co nhieu hon 9 page
        {
            if (pageIndex > 1 && pageIndex < pageTotal) {
                if (pageIndex - 3 > 1) str = str + '<li class="paginate_button "><a>...</a></li>';
                for (var i = pageIndex - 3; i < pageIndex + 4; i++) {
                    if (i > 0 && i <= pageTotal) {
                        if (i == pageIndex)
                            str = str + '<li class="paginate_button active"><a >' + pageIndex + '</a></li>';
                        else
                            str = str + '<li class="paginate_button " data-ng-click="' + functionLoad + '(' + i + ')"><a>' + i + '</a></li>';
                    }
                }
                if (pageIndex + 4 < pageTotal) str = str + '<li class="paginate_button "><a>...</a></li>';
            }
            else if (pageIndex == 1) {
                str = str + '<li class="paginate_button active"><a >' + pageIndex + '</a></li>';
                for (var i = pageIndex + 1; i < pageIndex + 6; i++) {
                    if (i <= pageTotal) {
                        str = str + '<li class="paginate_button " data-ng-click="' + functionLoad + '(' + i + ')"><a >' + i + '</a></li>';
                    }
                }
                if (pageIndex + 5 < pageTotal) str = str + '<li ><a>...</a></li>';
            }
            else if (pageIndex == pageTotal) {
                if (pageIndex - 5 > 1) str = str + '<li class="paginate_button "><a>...</a></li>';
                for (var i = pageIndex - 5; i < pageIndex; i++) {
                    if (i > 0) {
                        str = str + '<li class="paginate_button " data-ng-click="' + functionLoad + '(' + i + ')"><a>' + i + '</a></li>';
                    }
                }
                str = str + '<li class="paginate_button active"><a>' + pageIndex + '</a></li>';
            }
        }

        if (pageIndex < pageTotal)
            str = str + '<li class="paginate_button next" data-ng-click="' + functionLoad + '(' + pageTotal + ')"><a> >> </a></li>';
        else
            str = str + '<li class="paginate_button next disabled"><a> >> </a></li>';
    }
    return str;
};