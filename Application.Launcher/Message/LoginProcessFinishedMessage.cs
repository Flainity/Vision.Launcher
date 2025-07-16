using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Launcher.Type;

namespace Application.Launcher.Message
{
    public class LoginProcessFinishedMessage : ValueChangedMessage<ServerResponseType>
    {
        public LoginProcessFinishedMessage(ServerResponseType value) : base(value)
        {
        }
    }
}
