using MediatR;

namespace Core.Common.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        /// <summary>
        /// Representa uma lista de Notificações contendo erros 
        /// </summary>
        public DomainNotificationHandler() => _notifications = new List<DomainNotification>();


        /// <summary>
        /// Obtem a lista de erros
        /// </summary>
        /// <returns></returns>
        public virtual List<DomainNotification> GetNotifications() => _notifications;

        /// <summary>
        /// Captura as notificações geradas pelos cliente
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ops!!!: {message.Key} - {message.Value}");

            return Task.CompletedTask;
        }
        /// <summary>
        /// Retorna um boleano true caso contenha erros persistidos.
        /// </summary>
        /// <returns></returns>
        public virtual bool HasNotifications() => _notifications.Any();
        public void Dispose() => _notifications = new List<DomainNotification>();



    }
}
