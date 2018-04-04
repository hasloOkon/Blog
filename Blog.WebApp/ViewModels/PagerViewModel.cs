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

        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
}
