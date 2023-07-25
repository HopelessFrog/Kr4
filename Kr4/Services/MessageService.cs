using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Kr4.Services.Interface;

namespace Kr4.Services
{
    public class MessageService : IMessageService
    {
        public void SendMessage(string message)
        {
            Xceed.Wpf.Toolkit.MessageBox.Show(message, "",
                MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        public void SendMessageError(string message)
        {
            Xceed.Wpf.Toolkit.MessageBox.Show( message, "",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
