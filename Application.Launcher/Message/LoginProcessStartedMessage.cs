using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Launcher.Message
{
    public class LoginProcessStartedMessage : ValueChangedMessage<bool>
    {
        public LoginProcessStartedMessage(bool value) : base(value)
        {
        }
    }
}
