using System;

namespace Blog.Core.Models
{
    public class Image
    {
        public virtual int Id { get; set; }
        public virtual DateTime UploadTime { get; set; }
        public virtual byte[] Data { get; set; }
    }
}