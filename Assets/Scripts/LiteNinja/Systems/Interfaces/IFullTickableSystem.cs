namespace LiteNinja.Systems
{
    public interface IFullTickableSystem : ISystem, IPausable, ITickable, IFixedTickable
    {
    }
}