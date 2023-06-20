using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HTSTakvim.Models
{
    public class Class1
    {
        public class WebServiceProxy
        {
            private string _webServiceUrl;


            public WebServiceProxy()
            {
                _webServiceUrl = ConfigurationManager.ConnectionStrings["WebServiceConnection"].ConnectionString;

            }

            private string _property;

            public string GetData(string v)
            {
                return _property;
            }
        }
    }
}