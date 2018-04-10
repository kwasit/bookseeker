using System;

namespace BookSeeker.Providers.Common.Helpers
{
    public static class IsbnHelper
    {
        public static string ConvertToIsbn13(string isbn)
        {
            var isbn10 = "978" + isbn.Substring(0, 9);
            var isbn10_1 = Convert.ToInt32(isbn10.Substring(0, 1));
            var isbn10_2 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(1, 1)) * 3);
            var isbn10_3 = Convert.ToInt32(isbn10.Substring(2, 1));
            var isbn10_4 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(3, 1)) * 3);
            var isbn10_5 = Convert.ToInt32(isbn10.Substring(4, 1));
            var isbn10_6 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(5, 1)) * 3);
            var isbn10_7 = Convert.ToInt32(isbn10.Substring(6, 1));
            var isbn10_8 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(7, 1)) * 3);
            var isbn10_9 = Convert.ToInt32(isbn10.Substring(8, 1));
            var isbn10_10 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(9, 1)) * 3);
            var isbn10_11 = Convert.ToInt32(isbn10.Substring(10, 1));
            var isbn10_12 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(11, 1)) * 3);
            var k = (isbn10_1 + isbn10_2 + isbn10_3 + isbn10_4 + isbn10_5 + isbn10_6 + isbn10_7 + isbn10_8 + isbn10_9 + isbn10_10 + isbn10_11 + isbn10_12);
            var checkdigit = 10 - (isbn10_1 + isbn10_2 + isbn10_3 + isbn10_4 + isbn10_5 + isbn10_6 + isbn10_7 + isbn10_8 + isbn10_9 + isbn10_10 + isbn10_11 + isbn10_12) % 10;
            if (checkdigit == 10)
            {
                checkdigit = 0;
            }
            return isbn10 + checkdigit;
        }

        //static string IsbnToEan13(string isbn)
        //{
        //    isbn = NormalizeIsbn(isbn);
        //    string code = "978" + isbn.Substring(0, 9);
        //    code += (char)('0' + Ean13.CalculateChecksum(code));
        //    return code;
        //}

        public static bool CheckIsbn(string isbn)
        {
            if (isbn == null)
            {
                return false;
            }

            isbn = NormalizeIsbn(isbn);
            if (isbn.Length != 10)
            {
                return false;
            }

            for (var i = 0; i < 9; i++)
            {
                if (!int.TryParse(isbn[i].ToString(), out _))
                {
                    return false;
                }

            }

            var sum = 0;
            for (var i = 0; i < 9; i++)
            {
                sum += (i + 1) * int.Parse(isbn[i].ToString());
            }

            var r = sum % 11;
            if (r == 10)
            {
                return isbn[9] == 'X';
            }

            return isbn[9] == (char)('0' + r);
        }

        private static string NormalizeIsbn(string isbn) => isbn.Replace("-", "").Replace(" ", "");
    }
}