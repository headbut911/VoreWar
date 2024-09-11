using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StrategicTileDictionary : MonoBehaviour
{
    internal Tile[] WaterFloat;
    internal Tile[] GrassFloat;
    internal Tile[] DesertFloat;
    internal Tile[] SnowFloat;
    internal Tile[] AshenFloat;
    internal Tile[] VolcanicFloat;
    internal Tile[] SwampFloat;
    internal Tile[] DrySwampFloat;
    internal Tile[] PurpleBogFloat;
    internal Tile[] SavannahFloat;
    internal Tile[] RainforestFloat;
    internal Tile[] SmallIslandsFloat;
    internal Tile[] IceOverSnow;
    internal Tile[] DeepWaterOverWater;

    internal Tile[] Objects;

    public Sprite[] WaterFloatSprites;
    public Sprite[] GrassFloatSprites;
    public Sprite[] DesertFloatSprites;
    public Sprite[] SnowFloatSprites;
    public Sprite[] AshenFloatSprites;
    public Sprite[] VolcanicFloatSprites;
    public Sprite[] SwampFloatSprites;
    public Sprite[] DrySwampFloatSprites;
    public Sprite[] PurpleBogFloatSprites;
    public Sprite[] SavannahFloatSprites;
    public Sprite[] RainforestFloatSprites;
    public Sprite[] SmallIslandsFloatSprites;

    public Sprite[] IceOverSnowSprites;
    public Sprite[] DeepWaterOverWaterSprites;
    public Sprite[] ObjectSprites;

    void Start()
    {

        IceOverSnow = CreateTiles(IceOverSnowSprites);
        DeepWaterOverWater = CreateTiles(DeepWaterOverWaterSprites);
        WaterFloat = CreateTiles(WaterFloatSprites);
        GrassFloat = CreateTiles(GrassFloatSprites);
        DesertFloat = CreateTiles(DesertFloatSprites);
        SnowFloat = CreateTiles(SnowFloatSprites);
        AshenFloat = CreateTiles(AshenFloatSprites);
        VolcanicFloat = CreateTiles(VolcanicFloatSprites);
        SwampFloat = CreateTiles(SwampFloatSprites);
        DrySwampFloat = CreateTiles(DrySwampFloatSprites);
        PurpleBogFloat = CreateTiles(PurpleBogFloatSprites);
        SavannahFloat = CreateTiles(SavannahFloatSprites);
        RainforestFloat = CreateTiles(RainforestFloatSprites);
        SmallIslandsFloat = CreateTiles(SmallIslandsFloatSprites);
        Objects = CreateTiles(ObjectSprites);



        Tile[] CreateTiles(Sprite[] sprites)
        {
            Tile[] temptiles = new Tile[sprites.Count()];
            for (int i = 0; i < sprites.Count(); i++)
            {
                temptiles[i] = ScriptableObject.CreateInstance<Tile>();
                temptiles[i].sprite = sprites[i];
            }
            return temptiles;
        }
    }
}

