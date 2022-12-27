namespace SoloDevApp.Service.Constants
{
    public class MessageConstants
    {
        public static string MESSAGE_ERROR_400 = "Yêu cầu không hợp lệ!";
        public static string MESSAGE_ERROR_404 = "Không tìm thấy tài nguyên!";
        public static string MESSAGE_ERROR_500 = "Xảy ra lỗi chưa xử lý!";

        public static string INSERT_SUCCESS = "Thêm mới thành công!";
        public static string INSERT_ERROR = "Thêm mới thất bại!";

        public static string UPDATE_SUCCESS = "Cập nhật thành công!";
        public static string UPDATE_ERROR = "Cập nhật thất bại!";

        public static string DELETE_SUCCESS = "Xóa thành công!";
        public static string DELETE_ERROR = "Xóa thất bại!";

        public static string SIGNUP_SUCCESS = "Đăng ký tài khoản thành công!";
        public static string SIGNUP_ERROR = "Đăng ký tài khoản thất bại!";
        public static string EMAIL_EXITST = "Email hoặc số điện thoại đã được sử dụng!";
        public static string INFO_EXITST = "Email hoặc facebook đã được sử dụng!";

        public static string SIGNIN_WRONG = "Sai email hoặc mật khẩu!";
        public static string SIGNIN_SUCCESS = "Đăng nhập thành công!";
        public static string SIGNIN_ERROR = "Đăng nhập thất bại!";

        public static string TOKEN_GENERATE_ERROR = "Không thể tạo token!";

        public static string UPLOAD_SUCCESS = "Upload thành công!";
        public static string UPLOAD_ERROR = "Upload thất bại!";

        public static string USER_NOT_FOUND = "Không tìm thấy người dùng!";
        public static string LO_TRINH_NOT_FOUND = "Không tìm thấy lộ trình!";
        public static string KHOA_HOC_NOT_FOUND = "Không tìm thấy khoá học!";
        public static string KHOA_HOC_NOT_VALID = "Khoá học không hợp lệ!";
        public static string KHOA_HOC_IS_EXISTED = "Khoá học đã tồn tại!";

        public static string EMAIL_IS_USED = "Email đã được sử dụng!";
        public static string PHONE_IS_USED = "Số điện thoại đã được sử dụng!";

        public static string PHONE_AND_EMAIL_IS_VALID = "Email và số điện thoại có thể sử dụng!";
        
        public static string QUESTION_NOT_FOUND = "Không tìm thấy câu hỏi trong ngân hàng dữ liệu!";
        public static string QUESTION_IS_EXISTED = "Nội dung câu hỏi đã tồn tại trong ngân hàng câu hỏi!";
        public static string CREATE_QUESTION_ERROR = "Xảy ra lỗi trong quá trình tạo câu hỏi trắc nghiệm!";
        public static string CREATE_QUESTION_SUCCESS = "Tạo câu hỏi trắc nghiệm thành công!";
        
        public static string ERROR_WHILE_FETCHING_DATA = "Có lỗi xảy ra trong quá trình truy xuất dữ liệu!";
        public static string FETCHING_DATA_SUCCESS = "Truy cập thông tin thành công!";
        
        public static string TIENTRINH_NOT_FOUND = "Không tìm thấy tiến trình học của người dùng!";
        public static string BAIHOC_IS_COMPLETED = "Bài học đã được hoàn thành!";
        public static string BAIHOC_ADDED_SUCCESS = "Hoàn thành bài học thành công!";
        public static string BAIHOC_IS_NOT_BELONG_TO_KHOAHOC = "Bài học không thuộc danh sách của khoá học!";
        
        public static string USER_DO_NOT_AUTHORIZED = "Người dùng không có quyền truy cập!";
    }
}