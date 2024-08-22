using Shop.XmlModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DbActions
{
    public interface IDbAction
    {
        public void ExecuteInsert(List<XmlDataModel> orders);
        public void ShowResultInsert();
        public void ShowSales();

    }
}
