using System.Collections.Generic;
using Blog.Core.Models;

namespace Blog.Core.Repositories
{
    public interface IImageRepository : IRepository<Image>
    {
        IList<Image> FetchData(IEnumerable<Image> images);
    }
}