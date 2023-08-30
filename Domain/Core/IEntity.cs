using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
   public interface IEntity<key>
    {
        public key Id { get; set; }
    }
}
