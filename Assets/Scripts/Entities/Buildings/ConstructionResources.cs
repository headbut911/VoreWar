using OdinSerializer;

public enum ConstructionresourceType
{
    wood,
    stone,
    ores,
    naturalmaterials,
    manastones
}
public class ConstructionResources
{
    [OdinSerialize]
    public int Wood;
    [OdinSerialize]
    public int Stone;
    [OdinSerialize]
    public int Ores;
    [OdinSerialize]
    public int NaturalMaterials;
    [OdinSerialize]
    public int ManaStones;

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
            case ConstructionresourceType.manastones:
                return ManaStones >= amount;
            default:
                break;
        }
        return false;
    }

}

