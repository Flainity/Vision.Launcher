using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veritas.Services.Launcher.Type;

namespace Veritas.Services.Launcher.Message
{
    public class LoginProcessFinishedMessage : ValueChangedMessage<ServerResponseType>
    {
        public LoginProcessFinishedMessage(ServerResponseType value) : base(value)
        {
        }
    }
}
