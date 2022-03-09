 
 namespace Core.Common.Messaging
{
    using FluentValidation.Results;
    using MediatR;
    using System;
    public abstract class Command : Message, IRequest<ValidationResult>, IBaseRequest
    {
        public Command() { }
        


        public DateTime Timestamp { get; }
        public ValidationResult ValidationResult { get; set; }

        public abstract  bool IsValid();
    }
}
