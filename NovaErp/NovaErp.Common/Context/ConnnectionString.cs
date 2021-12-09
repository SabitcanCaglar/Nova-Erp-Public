using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Common.Context
{
    public class ConnnectionString
    {
        public static string Get
        {
            get
            {
                return "Server=NovaErpServer;Host=localhost;Port=5432;Database=NovaErp;User Id=postgres;Password=sbtcan9406;";
            }
        }
    }
}