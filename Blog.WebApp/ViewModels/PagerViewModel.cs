using System;

namespace Blog.WebApp.ViewModels
{
    public class PagerViewModel
    {
        public PagerViewModel(int totalItems, int pageNumber, int pageSize)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            var currentPage = pageNumber;
            var startPage = Math.Max(currentPage - 3, 1);
            var endPage = Math.Min(currentPage + 3, totalPages);

            CurrentPage = currentPage;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int StartPage { get; }
        public int EndPage { get; }
    }
}
