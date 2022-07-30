namespace LiteNinja.Systems
{
    public interface IFixedTickable
    {
        public float FixedTickDelay { get; set; }
        public void FixedTick(float fixedDeltaTime);
    }
}