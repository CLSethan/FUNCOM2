
public interface IUpgradeableWeapon
{
    int currentUpgradeLevel { get; }
    int upgradeLevelMax { get; }

    void Upgrade();
    void Evolve();
}