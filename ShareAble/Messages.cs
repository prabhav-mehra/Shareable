using System;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ShareAble
{
    public class MyMessage : ValueChangedMessage<string>
    {
        public MyMessage(string value) : base(value)
        {

        }
    }
}

