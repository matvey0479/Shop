using Shop.Domain.Entites;


namespace Shop.XmlModel
{
    public class XmlDataModel
    {
        public XmlDataModel() { }
        public string numberOrder { get; set; }
        public string OrderDate { get; set; }
        public string quantity { get; set; }
        public string productName { get; set; }
        public string price { get; set; }
        public string fio { get; set; }
        public string email { get; set; }
        public IEnumerable<XmlProductModel> Products { get; set; }
        public IEnumerable<XmlUserModel> Users { get; set; }
    }
}
