using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardRental.Models
{
    public class Longboard
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Motorized { get; set; }
        public string? Price { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Customer>? Customer{ get; set; }


    }
}
