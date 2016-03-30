using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WebUI.Models
{
    public class Parser
    {
        public static CultureInfo lvCulture = new CultureInfo("lv-LV");
        public NumberFormatInfo dbNumberFormat = lvCulture.NumberFormat;

        public List<MobilePhone> Parse1Alv()
        {
            string url = "http://www.1a.lv/telefoni_plansetdatori/mobilie_telefoni/mobile_phones";

            var web = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8,
            };
            var html1 = web.Load(url);
            var headRoot = html1.DocumentNode;

            string pattern = @"(\d+)";

            // Instantiate the regular expression object.
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            var t = headRoot.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("paging-text")).First()
                   .Descendants("strong").First().NextSibling;
            Match m = r.Match(t.InnerText);
            int total = int.Parse(m.Value);

            var displayCount = headRoot.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("paging-text")).First()
                   .Descendants("strong").First().InnerText.ToString();
            displayCount = displayCount.Substring(displayCount.Length - 2, 2);

            int dCount = int.Parse(displayCount);

            int pageCount = (int)Math.Ceiling((double)(total / dCount));
            if ((pageCount * dCount < total)) pageCount++;

            var list = new List<MobilePhone>();
            
            for (int i = 1; i < (pageCount + 1); i++)
            {
                url = "http://www.1a.lv/telefoni_plansetdatori/mobilie_telefoni/mobile_phones";
                url = url + "/" + i.ToString();

                var web1 = new HtmlWeb
                {
                    AutoDetectEncoding = false,
                    OverrideEncoding = Encoding.UTF8,
                };
                var html = web1.Load(url);
                var root = html.DocumentNode;

                var parents = root.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("area")).ToArray();

                foreach (var parent in parents)
                {
                    var phone = new MobilePhone();
                    var brand = parent.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("p-content")).First().Descendants("a").First();
                    //var href = brand.GetAttributeValue("href", "");
                    var phoneImg = parent.Descendants("img").First();
                    string imgUrl = phoneImg.GetAttributeValue("src", "");
                    imgUrl = "http://www.1a.lv" + imgUrl;

                    var price = parent.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("p-info")).First().Descendants("span").First();
                    //href = "http://www.1a.lv" + href;

                    phone.Price = decimal.Parse(price.InnerText.ToString().Replace(".", ","), dbNumberFormat);
                    phone.Name = HtmlEntity.DeEntitize(brand.InnerText.ToString());
                    phone.ImgUrl = imgUrl.ToString();
                    
                    list.Add(phone);

                }

            }
            return list;

        }
    }
}