using System;
using System.Collections.Generic;
using System.Text;

namespace NovaErp.Common.GeneralResponse
{
    public abstract class BaseResponseModel
    {
        private string _message;
        public bool Success { get; set; }
        public string Message { get { return string.IsNullOrWhiteSpace(_message) ? string.Join("<br/>", Messages) : _message; } set { _message = value; } }
        public List<string> Messages { get; set; }
        protected BaseResponseModel()
        {
            this.Messages = new List<string>();
        }
    }
}
