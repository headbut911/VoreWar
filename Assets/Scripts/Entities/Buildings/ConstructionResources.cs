using OdinSerializer;
using System.Collections.Generic;

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

    public ConstructionResources()
    {
        Wood = 0;
        Stone = 0;
        Ores = 0;
        NaturalMaterials = 0;
        Prefabs = 0;
        ManaStones = 0;
    }
    public ConstructionResources(int wood = 0, int stones = 0, int nm = 0, int ores = 0, int prefabs = 0, int ms = 0)
    {
        Wood = wood;
        Stone = stones;
        Ores = ores;
        NaturalMaterials = nm;
        Prefabs = prefabs;
        ManaStones = ms;
    }
    public void Reset()
    {
        Wood = 0;
        Stone = 0;
        Ores = 0;
        NaturalMaterials = 0;
        Prefabs = 0;
        ManaStones = 0;
    }

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

    /// <summary>
    /// Checks if caller has resources needed of required.
    /// </summary>
    /// <returns></returns>
    public bool CanBuildWithCurrentResources(ConstructionResources required)
    {
        if (!HasNeededResource(ConstructionresourceType.wood, required.Wood)) return false;
        if (!HasNeededResource(ConstructionresourceType.stone, required.Stone)) return false;
        if (!HasNeededResource(ConstructionresourceType.ores, required.Ores)) return false;
        if (!HasNeededResource(ConstructionresourceType.naturalmaterials, required.NaturalMaterials)) return false;
        if (!HasNeededResource(ConstructionresourceType.prefabs, required.Prefabs)) return false;
        if (!HasNeededResource(ConstructionresourceType.manastones, required.ManaStones)) return false;
        return true;
    }
    public void SpendProvidedResources(ConstructionResources required)
    {
        Wood -= required.Wood;
        Stone -= required.Stone;
        Ores -= required.Ores;
        NaturalMaterials -= required.NaturalMaterials;
        Prefabs -= required.Prefabs;
        ManaStones -= required.ManaStones;
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

    public void SetResources(int wood, int stones, int nm, int ores, int prefabs, int ms)
    {
        Wood = wood;
        Stone = stones;
        Ores = ores;
        NaturalMaterials = nm;
        Prefabs = prefabs;
        ManaStones = ms;
    }

    public Dictionary<ConstructionresourceType, int> NeededResources(ConstructionResources required)
    {
        Dictionary<ConstructionresourceType, int> req = new Dictionary<ConstructionresourceType, int>();
        if (!HasNeededResource(ConstructionresourceType.wood, required.Wood))
        {
            req.Add(ConstructionresourceType.wood, Wood - required.Wood);
        }
        if (!HasNeededResource(ConstructionresourceType.stone, required.Stone))
        {
            req.Add(ConstructionresourceType.stone, Stone - required.Stone);
        }
        if (!HasNeededResource(ConstructionresourceType.ores, required.Ores))
        {
            req.Add(ConstructionresourceType.ores, Ores - required.Ores);
        }
        if (!HasNeededResource(ConstructionresourceType.naturalmaterials, required.NaturalMaterials))
        {
            req.Add(ConstructionresourceType.naturalmaterials, NaturalMaterials - required.NaturalMaterials);
        }
        if (!HasNeededResource(ConstructionresourceType.prefabs, required.Prefabs))
        {
            req.Add(ConstructionresourceType.prefabs, Prefabs - required.Prefabs);
        }
        if (!HasNeededResource(ConstructionresourceType.manastones, required.ManaStones))
        {
            req.Add(ConstructionresourceType.manastones, ManaStones - required.ManaStones);
        }
        return req;
    }
}

