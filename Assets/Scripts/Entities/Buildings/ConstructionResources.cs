using OdinSerializer;

public enum ConstructionresourceType
{
    wood,
    stone,
    ores,
    naturalmaterials,
    prefabs,
    manastones
}
public class ConstructionResources
{
    [OdinSerialize]
    public int Wood = 0;
    [OdinSerialize]
    public int Stone = 0;
    [OdinSerialize]
    public int Ores = 0;
    [OdinSerialize]
    public int NaturalMaterials = 0;
    [OdinSerialize]
    public int Prefabs = 0;
    [OdinSerialize]
    public int ManaStones = 0;

    public void AddResource(ConstructionresourceType type, int amount)
    {
        switch (type)
        {
            case ConstructionresourceType.wood:
                Wood += amount; break;
            case ConstructionresourceType.stone:
                Stone += amount; break;
            case ConstructionresourceType.ores:
                Ores += amount; break;
            case ConstructionresourceType.naturalmaterials:
                NaturalMaterials += amount; break;
            case ConstructionresourceType.prefabs:
                Prefabs += amount; break;
            case ConstructionresourceType.manastones:
                ManaStones += amount; break;
            default:
                break;
        }
    }

    public void SpendResource(ConstructionresourceType type, int amount)
    {
        switch (type)
        {
            case ConstructionresourceType.wood:
                Wood -= amount; break;
            case ConstructionresourceType.stone:
                Stone -= amount; break;
            case ConstructionresourceType.ores:
                Ores -= amount; break;
            case ConstructionresourceType.naturalmaterials:
                NaturalMaterials -= amount; break;
            case ConstructionresourceType.prefabs:
                Prefabs += amount; break;
            case ConstructionresourceType.manastones:
                ManaStones -= amount; break;
            default:
                break;
        }
    }

    public bool HasNeededResource(ConstructionresourceType type, int amount)
    {
        switch (type)
        {
            case ConstructionresourceType.wood:
                return Wood >= amount;
            case ConstructionresourceType.stone:
                return Stone >= amount;
            case ConstructionresourceType.ores:
                return Ores >= amount;
            case ConstructionresourceType.naturalmaterials:
                return NaturalMaterials >= amount;
            case ConstructionresourceType.prefabs:
                return Prefabs >= amount;
            case ConstructionresourceType.manastones:
                return ManaStones >= amount;
            default:
                break;
        }
        return false;
    }

    public void SetResources(int wood, int stones, int ores, int nm, int prefabs, int ms)
    {
        Wood = wood;
        Stone = stones;
        Ores = ores;
        NaturalMaterials = nm;
        Prefabs = prefabs;
        ManaStones = ms;
    }
}

