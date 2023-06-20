using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyEvent.BLL.Models
{
    public class BLEventImages
    {
        public Guid ImgId { get; set; }

        public byte[]? ImgBody { get; set; }

        public string? ImgType { get; set; }

        public string? ImgName { get; set; }

        public Guid EventId { get; set; }

    }
}
