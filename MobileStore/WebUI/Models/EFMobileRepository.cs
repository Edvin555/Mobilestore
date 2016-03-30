
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class EFMobileRepository : IMobileRepository 
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<MobilePhone> MobilePhones
        {
            get { return context.MobilePhones; }
           
        }
    }
}
