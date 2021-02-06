using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeUtility
{
    public class PageListUtility<T> where T : class
    {

        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int Skip { get; set; }

        public List<T> Result { get; set; }

        public PageListUtility(List<T> items, int count,int totalPage, int currentPage, int pageSize, int skip)
        {
            Result = items;
            TotalCount = count;
            TotalPage = totalPage;
            CurrentPage = currentPage;
            PageSize = pageSize;
            Skip = skip;
        }

        /// <summary>
        /// Phân trang theo tiện ích Update 10/01/2019
        /// </summary>
        /// <param name="source">Danh sách truyền vào</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng dòng trên một trang</param>
        /// <returns> Một đối tượng PageListUtility theo kiểu data truyền vào </returns>
        public static PageListUtility<T> PageList(List<T> source, int currentPage, int pageSize)
        {
            var totalCount = source.Count();
            int totalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
            currentPage = currentPage < 1 ? 1 : (currentPage > totalPage ? totalPage : currentPage); ;
            int skip = (currentPage - 1) * pageSize;
            var items = source.Skip(skip).Take(pageSize).ToList();
            return new PageListUtility<T>(items, totalCount,totalPage, currentPage, pageSize, skip);

        }
    }

}
