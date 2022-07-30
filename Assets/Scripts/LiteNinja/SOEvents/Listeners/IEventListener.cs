namespace LiteNinja.SOEvents
{
    public interface IEventListener<in T>
    {
        void OnEventRaised(T data);

        /// <summary>
        /// if event has parameter, raise it again with the same data
        /// </summary>
        void RaiseAgain();
    }
}