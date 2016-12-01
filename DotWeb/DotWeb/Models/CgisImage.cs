using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace DotWeb.Models
{
    public class CgisImage
    {
        public string ImageName { get; set; }
        public Bitmap CgisImageWithAnnot { get; set; }
        public byte[] CgisByteImageWithAnnot { get; set; }
    }
}
