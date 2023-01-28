using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfChatCustomer
{
    class MessageClass
    {
        public MessageClass(string message, string name)
        {
            Message = message;
            Name = name;
        }

        public string Message { get; set; }
        public string Name { get; set; }

    }
}
