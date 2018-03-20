using Blog.Core.Models;
using System.Collections.Generic;

namespace Blog.Core.Repositories
{
    public interface IImageRepository
    {
        Image GetById(int id);
        void Save(Image image);
        IEnumerable<Image> Images();
    }
}