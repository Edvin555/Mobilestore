using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class MobilePhone
    {
        [Key]
        public int MobilePhoneId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string ImgUrl { get; set; }
       
        
    }

     public class UpdateDate
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}

