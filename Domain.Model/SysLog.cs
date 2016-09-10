using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Core.Base;

namespace Domain.Model
{
 public   class SysLog:BaseEntity<int>
    {
        public DateTime? DATES { get; set; }
        public string LEVELS { get; set; }

        public string LOGGER { get; set; }

        public string CLIENTUSER { get; set; }

        public string CLIENTIP { get; set; }

        public string REQUESTURL { get; set; }

        public string ACTION { get; set; }

        public string MESSAGE { get; set; }

        public string EXCEPTION { get; set; }

    }
}
