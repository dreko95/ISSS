using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSS.Models
{
    public class DocumentModel
    {
        public DocumentModel(int id, string name, string content)
        {
            Id = id;
            Name = name;
            Content = content;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
