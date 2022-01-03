using BusinessLogic.Management;
using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL.View.Utility
{
    public class Global
    {
        public static CanBoModel TaiKhoan_Login
        {
            get
            {
                if (!HttpContext.Current.Request.IsAuthenticated)
                {
                    return new CanBoModel();
                }
                var json = HttpContext.Current.User.Identity.Name;
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<CanBoModel>(json);
                return obj;
            }
        }

        //Khi nào cần lấy thêm thông tin tài khoản login ngoài thông tin cơ bản trong DangNhapModel thì bổ sung vào hàm này
        public static CanBoModel ThongTinTaiKhoan_Login
        {
            get
            {
                var currentAcc = TaiKhoan_Login;
                if (currentAcc != null && currentAcc.Id != Guid.Empty)
                {
                    var TaiKhoan = CanBoManager.Instance.SelectById(currentAcc.Id);
                    if (TaiKhoan != null)
                    {
                        var cb = new CanBoModel
                        {
                            Id = TaiKhoan.Id,
                            UserName = TaiKhoan.UserName,
                            Role = TaiKhoan.Role,
                            IdKhoa = TaiKhoan.IdKhoa,
                            HoVaTen = TaiKhoan.HoVaTen,
                            SoDienThoai = TaiKhoan.SoDienThoai,

                        };
                        if (TaiKhoan.IdKhoa.HasValue)
                        {
                            var khoa = KhoaPhongManager.Instance.SelectById(TaiKhoan.IdKhoa.Value);
                            cb.TenKhoa = khoa.Ten;
                        }
                        return cb;
                    }
                }
                return null;
            }
        }
    }
}