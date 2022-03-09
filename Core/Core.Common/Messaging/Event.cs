using MediatR;
using System;

namespace Core.Common.Messaging
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }


        protected Event() =>        
            Timestamp = DateTime.Now;
        
    }
}
