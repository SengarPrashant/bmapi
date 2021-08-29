using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Common
{
    public class PageLinks
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string DisplayText { get; set; }
        public string RelatedLink { get; set; }
        public Boolean NewTab { get; set; }
        public Boolean IsActive { get; set; }
        public string CreatedDate { get; set; }
    }
}
