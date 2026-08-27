using GamePrototype.Units;

namespace GamePrototype.Utils
{
    public interface IUnitFactory
    {
        Unit CreatePlayer(string name);
        Unit CreateEnemy();
    }
}
