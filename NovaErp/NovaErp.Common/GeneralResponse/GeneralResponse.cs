using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Common.GeneralResponse
{
    public class GeneralResponse<T> : BaseResponseModel
    {
        public T Object { get; set; }
    }

    public class GeneralResponse : BaseResponseModel
    {
    }
}
