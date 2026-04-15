using Lib_Equipment.Database;
using System;
using System.Data.SqlClient;

namespace Lib_Equipment.Helpers
{
    public static class LocationHelper
    {
        public static string GenerateNewBookLocation(string categoryId, string title)
        {
            // 1. Tìm cuốn đứng TRƯỚC (Sử dụng bộ tham số riêng)
            string queryPrev = @"SELECT TOP 1 CabinetLocation FROM Book b JOIN BookCategory c ON b.CategoryID = c.CategoryID 
                                 WHERE b.IsDeleted = 0 AND (c.CategoryName < (SELECT CategoryName FROM BookCategory WHERE CategoryID = @cat) 
                                 OR (b.CategoryID = @cat AND b.Title < @title)) ORDER BY c.CategoryName DESC, b.Title DESC";

            SqlParameter[] paramPrev = {
                new SqlParameter("@cat", categoryId),
                new SqlParameter("@title", title)
            };
            string prevLoc = DataProvider.Instance.ExecuteScalar(queryPrev, paramPrev)?.ToString();

            // 2. Tìm cuốn đứng SAU (Sử dụng bộ tham số mới hoàn toàn)
            string queryNext = @"SELECT TOP 1 CabinetLocation FROM Book b JOIN BookCategory c ON b.CategoryID = c.CategoryID 
                                 WHERE b.IsDeleted = 0 AND (c.CategoryName > (SELECT CategoryName FROM BookCategory WHERE CategoryID = @cat) 
                                 OR (b.CategoryID = @cat AND b.Title > @title)) ORDER BY c.CategoryName ASC, b.Title ASC";

            SqlParameter[] paramNext = {
                new SqlParameter("@cat", categoryId),
                new SqlParameter("@title", title)
            };
            string nextLoc = DataProvider.Instance.ExecuteScalar(queryNext, paramNext)?.ToString();

            // --- LOGIC XỬ LÝ VỊ TRÍ ---
            if (string.IsNullOrEmpty(prevLoc) && string.IsNullOrEmpty(nextLoc)) return "A1-001";

            if (string.IsNullOrEmpty(prevLoc) && !string.IsNullOrEmpty(nextLoc))
            {
                int dash = nextLoc.LastIndexOf('-');
                return nextLoc.Substring(0, dash + 1) + "000.9";
            }

            int lastDash = prevLoc.LastIndexOf('-');
            string prefix = prevLoc.Substring(0, lastDash + 1);
            string sttPart = prevLoc.Substring(lastDash + 1);

            return prefix + (sttPart.Contains(".") ? sttPart + "1" : sttPart + ".1");
        }
    }
}