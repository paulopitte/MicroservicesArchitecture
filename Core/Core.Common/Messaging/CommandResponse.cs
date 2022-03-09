namespace Core.Common.Messaging
{
    /// <summary>
    /// Representa as Respostas de um commando executado.
    /// </summary>
    public class CommandResponse
    {
        public static CommandResponse Ok = new CommandResponse { Success = true };
        public static CommandResponse Fail = new CommandResponse { Success = false };

        public CommandResponse(bool success = false) => Success = success;

        public bool Success { get; private set; }
    }
}
