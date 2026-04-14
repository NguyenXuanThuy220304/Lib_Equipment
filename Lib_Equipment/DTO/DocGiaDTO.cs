using System;

namespace Lib_Equipment.DTO
{
    public class DocGiaDTO
    {
        public string ReaderID { get; set; }
        public string FullName { get; set; }
        public string ReaderType { get; set; } // "Sinh viên", "Giảng viên", "Giáo sư", "Tiến sĩ"...
        public int Status { get; set; }
        public decimal Balance { get; set; }
    }
}