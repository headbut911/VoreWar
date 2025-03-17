using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;

class TownHall : ConstructibleBuilding
{
    public BuildingUpgrade standardUpgrade;
    public BuildingUpgrade prefabUpgrade;
    public BuildingUpgrade manaStoneUpgrade;
    
    public TownHall(Vec2i location) : base(location)
    {
        Name = "Town Hall";
        Desc = "The town hall is used to construct a new village.";
        spriteID = 88;
        buildingType = ConstructibleType.TownHall;

        ApplyConfigStats(Config.BuildCon.CasterTower);
        standardUpgrade = AddUpgrade(standardUpgrade, Config.BuildCon.TownHallManualUpgrade);
        prefabUpgrade = AddUpgrade(prefabUpgrade, Config.BuildCon.TownHallPrefabUpgrade);
        manaStoneUpgrade = AddUpgrade(manaStoneUpgrade, Config.BuildCon.TownHallManaStoneUpgrade);
    }
    internal override void RunBuildingFunction()
    {
        if ((standardUpgrade.built || prefabUpgrade.built || manaStoneUpgrade.built) && active)
        {
            var contstruct = State.World.Constructibles.ToList();
            contstruct.Remove(this);
            State.World.Constructibles = contstruct.ToArray();
            Owner.Buildings.Remove(this);

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (j != 0 || i != 0)
                    {
                        State.World.Tiles[Position.x + i, Position.y + j] = StrategicTileType.field;
                    }
                    //DestroyVillagesAtTile(new Vec2i(x + i, y + j));
                }
            }
            State.GameManager.StrategyMode.RedrawTiles();

            var villages = State.World.Villages.ToList();
            villages.Add(new Village("New Village", Position, 1, Owner.Race, false));
            State.World.Villages = villages.ToArray();
        }
    }
}

