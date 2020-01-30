using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// To make sure there won't be different instances of classes that implement IBL
    /// </summary>
    public class FactoryBL
    {
        static IBL bl = null;
        public static IBL GetBL() 
        {
            if (bl == null)
                bl = new BL_basic();
            return bl; 
        }
    }
}
