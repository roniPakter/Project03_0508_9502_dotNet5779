using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
     public class Factory
    {
       public static  IDL GetDL(string type)
        {
            switch (type)
            {
                case "lists":
                    return DLObject.getInstance();

                default:
                    throw new Exception("No such type of DL object implementation exists (factory exception...)");
            }
        }
    }
}
