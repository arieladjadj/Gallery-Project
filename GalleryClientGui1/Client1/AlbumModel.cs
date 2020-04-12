using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class AlbumModel
    {
        /// <summary>
        /// {"_id":"5e8ce0ecd90de9ce03e80de8","name":"First album","creationDate":"20/20/20","userId":"5e8c563b4378dd2b8a923d90"}
        /// </summary>
        public String _id { get; set; }
        public String Name { get; set; }
        public String CreationDate { get; set; }
        public String UserId { get; set; }
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
