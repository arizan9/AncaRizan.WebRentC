using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anca.Rizan.WebRentC.Core.Models
{
    public abstract class BaseEntity
    {
      

        [NotMapped]
        public DateTimeOffset CreatedAt { get; set; }

        public BaseEntity()
        {
            this.CreatedAt = DateTime.Now;
        }
    }
}
