using Shop.XmlModel;
using System.Xml.Linq;

namespace Shop
{
    public class XmlLoad
    {
        public string path;
        public XmlLoad(string path)
        {
            this.path = path;
        }
        public List<XmlDataModel> Load()
        {
            XDocument xdoc = XDocument.Load(path);
            XmlDataModel model = new XmlDataModel();
            var queri = xdoc.Element("orders")?
           .Elements("order")
           .Select(x=>new XmlDataModel 
           {
               numberOrder = x.Element("no")?.Value,
               OrderDate = x.Element("reg_date")?.Value,
               Products = x.Descendants("product")
                                .Select(prod => new XmlProductModel
                                {
                                    quantity = prod.Element("quantity")?.Value,
                                    productName = prod.Element("name")?.Value,
                                    price = prod.Element("price")?.Value.Replace(".",","),
                                }),
               Users = x.Descendants("user")
                            .Select(user => new XmlUserModel
                            {
                                fio = user.Element("fio")?.Value,
                                email = user.Element("email")?.Value,
                            })

           });
            var orders = queri.ToList();
            return orders;
        }
    }
}
