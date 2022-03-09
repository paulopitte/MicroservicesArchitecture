namespace Core.Common.App
{
    public interface IApplicationEvents
    {
        /// <summary>
        /// </summary>
        void OnStarted();

        /// <summary>
        /// </summary>
        void OnStopping();

        /// <summary>
        /// </summary>
        void OnStopped();
    }
}
