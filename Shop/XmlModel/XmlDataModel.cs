using Shop.Domain.Entites;


namespace Shop.XmlModel
{
    public class XmlDataModel
    {
        public XmlDataModel() { }
        public string NumberOrder { get; set; }
        public string OrderDate { get; set; }
        public IEnumerable<XmlProductModel> Products { get; set; }
        public IEnumerable<XmlUserModel> Users { get; set; }
    }
}
