using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Common.GeneralResponse
{
    public class GeneralResponseList<T> : BaseResponseModel
    {
        public List<T> Items { get; set; }
    }
}
