using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core.Base
{
    public class BaseEntity<TKey> : IEntity<TKey>
    {
        [Key]
        public TKey ID { get; set; }
    }
}
