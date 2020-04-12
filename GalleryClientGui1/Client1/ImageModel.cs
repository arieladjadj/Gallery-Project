using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ImageModel
    {
        public String _id { get; set; }
        public String Name {get; set;}
        public String CreationDate { get; set; }
        public String AlbumId{get; set;}
        public String Location { get; set; }
        public String Text { get; set; }

        public void setText(string text)
        {
            this.Text = text;
        }

        public String getCreationDate()
        {
            return this.CreationDate;
        }
        public String getName()
        {
            return this.Name;
        }
    }
}
