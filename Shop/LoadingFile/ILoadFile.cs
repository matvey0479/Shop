using Shop.XmlModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.LoadingFile
{
    public interface ILoadFile
    {
        public List<XmlDataModel> Load(string path);
    }
}
