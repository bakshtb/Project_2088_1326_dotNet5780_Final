using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    /// <summary>
    /// returns the class that implements interface IDAL (because there is two implementations of Dal)
    /// </summary>
    public class FactoryDal
    {
        public static IDAL GetDal()
        {
            return new Dal_XML_imp();
        }
    }
}
