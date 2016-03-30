
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public interface IMobileRepository
    {
        IEnumerable<MobilePhone> MobilePhones { get;  }
    }
}
