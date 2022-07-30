namespace LiteNinja.Systems
{
    public interface ITickable
    {
        public float TickDelay { get; set; }
        public void Tick(float deltaTime);
    }
}