using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kr4.Services.Interface
{
    public interface  IMessageService
    {
        void SendMessage(string message);
        void SendMessageError(string message);
    }
}
