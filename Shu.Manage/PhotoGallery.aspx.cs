using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Shu.Manage
{
    public partial class PhotoGallery : System.Web.UI.Page
    {
        protected List<string> imagename;
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> images = new List<string>();
            string pth = "/Images/Desktop/";
            string[] paths = Directory.GetFiles(HttpContext.Current.Server.MapPath(pth));
            foreach (string path in paths)
            {
                images.Add(Path.Combine(pth, Path.GetFileName(path)));
            }
            imagename = images;
        }
    }
}