using Shop.XmlModel;
using System.Xml.Linq;

namespace Shop.LoadingFile
{
    public class XmlLoadFile : ILoadFile
    {
        public XmlLoadFile()
        {

        }
        public List<XmlDataModel> Load(string path)
        {
            XDocument xdoc = XDocument.Load(path);
            XmlDataModel model = new XmlDataModel();
            var queri = xdoc.Element("orders")?
           .Elements("order")
           .Select(x => new XmlDataModel
           {
               NumberOrder = x.Element("no")?.Value,
               OrderDate = x.Element("reg_date")?.Value,
               Products = x.Descendants("product")
                                .Select(prod => new XmlProductModel
                                {
                                    Quantity = prod.Element("quantity")?.Value,
                                    ProductName = prod.Element("name")?.Value,
                                    Price = prod.Element("price")?.Value.Replace(".", ","),
                                }),
               Users = x.Descendants("user")
                            .Select(user => new XmlUserModel
                            {
                                Fio = user.Element("fio")?.Value,
                                Email = user.Element("email")?.Value,
                            })

           });
            var orders = queri.ToList();
            return orders;
        }
    }
}
