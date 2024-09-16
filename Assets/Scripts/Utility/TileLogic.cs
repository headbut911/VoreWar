using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using System.Linq;


enum Neighbor
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest
}

enum Wanted
{
    Yes,
    No,
    DontCare
}

struct DirectionalInfo
{
    internal Wanted north;
    internal Wanted northeast;
    internal Wanted east;
    internal Wanted southeast;
    internal Wanted south;
    internal Wanted southwest;
    internal Wanted west;
    internal Wanted northwest;

    public DirectionalInfo(Wanted north = Wanted.DontCare, Wanted northeast = Wanted.DontCare, Wanted east = Wanted.DontCare, Wanted southeast = Wanted.DontCare, Wanted south = Wanted.DontCare, Wanted southwest = Wanted.DontCare, Wanted west = Wanted.DontCare, Wanted northwest = Wanted.DontCare)
    {
        this.north = north;
        this.northeast = northeast;
        this.east = east;
        this.southeast = southeast;
        this.south = south;
        this.southwest = southwest;
        this.west = west;
        this.northwest = northwest;
    }
}

class TacticalTileLogic
{
    TacticalTileType[,] tiles;
    TacticalTileType[,] newtiles;

    internal TacticalTileType[,] ApplyLogic(TacticalTileType[,] tiles)
    {
        this.tiles = tiles;
        newtiles = new TacticalTileType[tiles.GetLength(0), tiles.GetLength(1)];
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                switch (tiles[x, y])
                {
                    case TacticalTileType.RockOverSand:
                        {
                            int type = DetermineType(new Vec2(x, y), TacticalTileType.RockOverSand);
                            newtiles[x, y] = (TacticalTileType)(2000 + type);
                            break;
                        }
                    case TacticalTileType.RockOverTar:
                        {
                            int type = DetermineType(new Vec2(x, y), TacticalTileType.RockOverTar);
                            newtiles[x, y] = (TacticalTileType)(2100 + type);
                            break;
                        }
                    case TacticalTileType.GrassOverWater:
                        {
                            int type = DetermineType(new Vec2(x, y), TacticalTileType.GrassOverWater);
                            newtiles[x, y] = (TacticalTileType)(2200 + type);
                            break;
                        }
                    case TacticalTileType.VolcanicOverGravel:
                        {
                            int type = DetermineType(new Vec2(x, y), TacticalTileType.VolcanicOverGravel);
                            newtiles[x, y] = (TacticalTileType)(2300 + type);
                            break;
                        }
                    case TacticalTileType.VolcanicOverLava:
                        {
                            int type = DetermineType(new Vec2(x, y), TacticalTileType.VolcanicOverLava);
                            newtiles[x, y] = (TacticalTileType)(2400 + type);
                            break;
                        }
                    default:
                        newtiles[x, y] = tiles[x, y];
                        continue;

                }
            }
        }
        return newtiles;
    }

    internal int DetermineType(Vec2 pos, TacticalTileType type)
    {
        Wanted yes = Wanted.Yes;
        Wanted no = Wanted.No;
        Wanted any = Wanted.DontCare;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, any, no, any))) return 0;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, yes, yes, any))) return 1;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, yes, yes, any))) return 2;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, any, no, any, no, any))) return 3;
        if (AreaCheck(pos, type, new DirectionalInfo(north: no, west: yes, south: no, east: yes))) return 4;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, yes, any))) return 5;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, yes, yes, yes))) return 6;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, no, yes, yes))) return 7;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, any, no, any))) return 8;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, yes, yes, yes))) return 9;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, yes, yes, yes))) return 10;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, no, yes, no))) return 11;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, any, no, any))) return 12;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, no, any))) return 13;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, yes, yes, yes))) return 14;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, yes, yes, no))) return 15;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, no, any))) return 16;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, yes, yes))) return 17;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, yes, yes))) return 18;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, no, any))) return 19;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, any, no, any))) return 20;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, any, no, any))) return 21;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, no, yes, any))) return 22;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, no, yes, yes))) return 23;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, yes, yes, any))) return 24;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, no, yes, any))) return 25;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, any, no, any))) return 26;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, no, yes, yes))) return 27;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, no, any))) return 28;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, no, any))) return 29;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, yes, no))) return 30;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, yes, yes, no))) return 31;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, yes, yes))) return 32;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, yes, no))) return 33;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, any, no, any))) return 34;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, yes, yes, no))) return 35;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, any, no, any))) return 36;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, no, yes, any))) return 37;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, yes, no))) return 38;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, no, yes, no))) return 39;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, no, yes, no))) return 40;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, yes, yes, yes))) return 41;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, no, yes, yes))) return 42;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, yes, yes, no))) return 43;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, no, yes, no))) return 44;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, no, yes, yes))) return 45;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, no, yes, no))) return 46;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, yes, yes, no))) return 47;
        UnityEngine.Debug.Log("fallthrough");
        return 9;

    }


    internal bool AreaCheck(Vec2 pos, TacticalTileType type, DirectionalInfo info)
    {

        if (Fails(info.north, Neighbor.North))
            return false;
        if (Fails(info.northeast, Neighbor.NorthEast))
            return false;
        if (Fails(info.east, Neighbor.East))
            return false;
        if (Fails(info.southeast, Neighbor.SouthEast))
            return false;
        if (Fails(info.south, Neighbor.South))
            return false;
        if (Fails(info.southwest, Neighbor.SouthWest))
            return false;
        if (Fails(info.west, Neighbor.West))
            return false;
        if (Fails(info.northwest, Neighbor.NorthWest))
            return false;

        return true;

        bool Fails(Wanted direction, Neighbor neighbor)
        {
            if (direction == Wanted.DontCare)
                return false;
            if (direction == Wanted.Yes && IsTileType(GetPos(pos, neighbor), type) == false)
                return true;
            if (direction == Wanted.No && IsTileType(GetPos(pos, neighbor), type))
                return true;
            return false;
        }
    }

    internal Vec2 GetPos(Vec2 startingPos, Neighbor direction)
    {
        switch (direction)
        {
            case Neighbor.North:
                return new Vec2(startingPos.x, startingPos.y + 1);
            case Neighbor.NorthEast:
                return new Vec2(startingPos.x + 1, startingPos.y + 1);
            case Neighbor.East:
                return new Vec2(startingPos.x + 1, startingPos.y);
            case Neighbor.SouthEast:
                return new Vec2(startingPos.x + 1, startingPos.y - 1);
            case Neighbor.South:
                return new Vec2(startingPos.x, startingPos.y - 1);
            case Neighbor.SouthWest:
                return new Vec2(startingPos.x - 1, startingPos.y - 1);
            case Neighbor.West:
                return new Vec2(startingPos.x - 1, startingPos.y);
            case Neighbor.NorthWest:
                return new Vec2(startingPos.x - 1, startingPos.y + 1);
            default:
                return startingPos;
        }
    }

    internal bool IsTileType(Vec2 pos, TacticalTileType type)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x > tiles.GetUpperBound(0) || pos.y > tiles.GetUpperBound(1))
            return false;
        return tiles[pos.x, pos.y] == type;
    }
}

class StrategicTileLogic
{
    StrategicTileType[,] tiles;
    StrategicTileType[,] newtiles;

    internal StrategicTileType[,] ApplyLogic(StrategicTileType[,] originalTiles, out StrategicTileType[,] overTiles, out StrategicTileType[,] underTiles)
    {
        tiles = new StrategicTileType[originalTiles.GetLength(0), originalTiles.GetLength(1)];
        StrategicTileType[,] temptiles = new StrategicTileType[originalTiles.GetLength(0), originalTiles.GetLength(1)];
        underTiles = new StrategicTileType[tiles.GetLength(0), tiles.GetLength(1)];
        for (int x = 0; x < originalTiles.GetLength(0); x++)
        {
            for (int y = 0; y < originalTiles.GetLength(1); y++)
            {
                temptiles[x, y] = originalTiles[x, y];
                tiles[x, y] = originalTiles[x, y];
                underTiles[x, y] = (StrategicTileType)99;
            }
        }
        newtiles = new StrategicTileType[tiles.GetLength(0), tiles.GetLength(1)];
        overTiles = new StrategicTileType[tiles.GetLength(0), tiles.GetLength(1)];

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (tiles[x, y] == StrategicTileType.water)
                {
                    int type = DetermineType(new Vec2(x, y), StrategicTileType.water);
                    overTiles[x, y] = (StrategicTileType)(2000 + type);
                    if (type == 19)
                        overTiles[x, y] = StrategicTileType.water;

                    int sand = DirectBorderTiles(x, y, StrategicTileType.desert);
                    int snow = DirectBorderTiles(x, y, StrategicTileType.snow);
                    int grass = DirectBorderTiles(x, y, StrategicTileType.grass);
                    if (grass >= sand && grass >= snow)
                        temptiles[x, y] = StrategicTileType.grass;
                    else if (sand >= grass && sand >= snow)
                        temptiles[x, y] = StrategicTileType.desert;
                    else if (snow >= sand && snow >= grass)
                        temptiles[x, y] = StrategicTileType.snow;
                    else
                        temptiles[x, y] = StrategicTileType.grass;


                    continue;
                }

            }
        }
        tiles = temptiles;

        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (overTiles[x, y] != 0)
                {
                    underTiles[x, y] = tiles[x, y];
                    int type = DetermineType(new Vec2(x, y), tiles[x, y]);
                    newtiles[x, y] = (StrategicTileType)(2100 + type);
                    //newtiles[x, y] = tiles[x, y];
                    continue;
                }
                else if (Config.HardLava)
                {
                    if (tiles[x, y] == StrategicTileType.lava)
                        overTiles[x, y] = StrategicTileType.lava;
                    if (tiles[x, y] == StrategicTileType.volcanic)
                        overTiles[x, y] = StrategicTileType.volcanic;

                }
                switch (tiles[x, y])
                {
                    case StrategicTileType.ice:
                        {
                            int type = DetermineType(new Vec2(x, y), StrategicTileType.ice);
                            underTiles[x, y] = (StrategicTileType)(2200 + type);
                            //underTiles[x, y] = StrategicTileType.ice;
                            type = DetermineType(new Vec2(x, y), StrategicTileType.snow);
                            newtiles[x, y] = (StrategicTileType)(2100 + type);
                            break;
                        }
                    default:
                        {
                            int type = DetermineType(new Vec2(x, y), tiles[x, y]);
                            newtiles[x, y] = (StrategicTileType)(2100 + type);
                            break;
                        }

                        //newtiles[x, y] = tiles[x, y];
                        //continue;
                }
            }
        }
        return newtiles;

        int DirectBorderTiles(int x, int y, StrategicTileType type)
        {
            int count = 0;
            count += IsTileType(new Vec2(x, y + 1), type) ? 3 : 0;
            count += IsTileType(new Vec2(x, y - 1), type) ? 3 : 0;
            count += IsTileType(new Vec2(x - 1, y), type) ? 3 : 0;
            count += IsTileType(new Vec2(x + 1, y), type) ? 3 : 0;
            count += IsTileType(new Vec2(x + 1, y + 1), type) ? 1 : 0;
            count += IsTileType(new Vec2(x + 1, y - 1), type) ? 1 : 0;
            count += IsTileType(new Vec2(x - 1, y + 1), type) ? 1 : 0;
            count += IsTileType(new Vec2(x - 1, y - 1), type) ? 1 : 0;
            return count;
        }
    }

    internal int DetermineType(Vec2 pos, StrategicTileType type, bool inverted = false)
    {
        Wanted yes;
        Wanted no;
        if (inverted)
        {
            yes = Wanted.No;
            no = Wanted.Yes;
        }
        else
        {
            yes = Wanted.Yes;
            no = Wanted.No;
        }


        Wanted any = Wanted.DontCare;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, any, no, any))) return 0;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, yes, yes, any))) return 1;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, yes, yes, any))) return 2;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, any, no, any, no, any))) return 3;
        if (AreaCheck(pos, type, new DirectionalInfo(north: no, west: yes, south: no, east: yes))) return 4;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, yes, any))) return 5;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, yes, yes, yes))) return 6;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, no, yes, yes))) return 7;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, any, no, any))) return 8;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, yes, yes, yes))) return 9;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, yes, yes, yes))) return 10;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, no, yes, no))) return 11;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, any, no, any))) return 12;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, no, any))) return 13;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, yes, yes, yes))) return 14;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, yes, yes, no))) return 15;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, no, any))) return 16;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, yes, yes))) return 17;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, yes, yes))) return 18;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, no, any, no, any))) return 19;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, any, no, any))) return 20;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, any, no, any))) return 21;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, no, any, yes, no, yes, any))) return 22;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, no, yes, yes))) return 23;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, yes, yes, any))) return 24;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, yes, yes, no, yes, any))) return 25;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, any, no, any))) return 26;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, no, yes, yes))) return 27;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, no, any))) return 28;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, no, any))) return 29;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, no, any, yes, no))) return 30;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, yes, yes, no))) return 31;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, yes, yes))) return 32;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, any, no, any, yes, no))) return 33;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, any, no, any))) return 34;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, yes, yes, no))) return 35;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, any, no, any))) return 36;
        if (AreaCheck(pos, type, new DirectionalInfo(no, any, yes, no, yes, no, yes, any))) return 37;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, any, no, any, yes, no))) return 38;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, any, no, any, yes, no, yes, no))) return 39;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, yes, yes, no, yes, no))) return 40;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, yes, yes, yes))) return 41;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, no, yes, yes))) return 42;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, yes, yes, no))) return 43;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, yes, yes, no, yes, no, yes, no))) return 44;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, no, yes, yes))) return 45;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, yes, yes, no, yes, no))) return 46;
        if (AreaCheck(pos, type, new DirectionalInfo(yes, no, yes, no, yes, yes, yes, no))) return 47;
        UnityEngine.Debug.Log("fallthrough");
        return 9;

    }


    internal bool AreaCheck(Vec2 pos, StrategicTileType type, DirectionalInfo info)
    {

        if (Fails(info.north, Neighbor.North))
            return false;
        if (Fails(info.northeast, Neighbor.NorthEast))
            return false;
        if (Fails(info.east, Neighbor.East))
            return false;
        if (Fails(info.southeast, Neighbor.SouthEast))
            return false;
        if (Fails(info.south, Neighbor.South))
            return false;
        if (Fails(info.southwest, Neighbor.SouthWest))
            return false;
        if (Fails(info.west, Neighbor.West))
            return false;
        if (Fails(info.northwest, Neighbor.NorthWest))
            return false;

        return true;

        bool Fails(Wanted direction, Neighbor neighbor)
        {
            if (direction == Wanted.DontCare)
                return false;
            if (direction == Wanted.Yes && IsTileType(GetPos(pos, neighbor), type) == false)
                return true;
            if (direction == Wanted.No && IsTileType(GetPos(pos, neighbor), type))
                return true;
            return false;
        }
    }

    internal Vec2 GetPos(Vec2 startingPos, Neighbor direction)
    {
        switch (direction)
        {
            case Neighbor.North:
                return new Vec2(startingPos.x, startingPos.y + 1);
            case Neighbor.NorthEast:
                return new Vec2(startingPos.x + 1, startingPos.y + 1);
            case Neighbor.East:
                return new Vec2(startingPos.x + 1, startingPos.y);
            case Neighbor.SouthEast:
                return new Vec2(startingPos.x + 1, startingPos.y - 1);
            case Neighbor.South:
                return new Vec2(startingPos.x, startingPos.y - 1);
            case Neighbor.SouthWest:
                return new Vec2(startingPos.x - 1, startingPos.y - 1);
            case Neighbor.West:
                return new Vec2(startingPos.x - 1, startingPos.y);
            case Neighbor.NorthWest:
                return new Vec2(startingPos.x - 1, startingPos.y + 1);
            default:
                return startingPos;
        }
    }

    internal bool IsTileType(Vec2 pos, StrategicTileType type)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x > tiles.GetUpperBound(0) || pos.y > tiles.GetUpperBound(1))
            return true;
        if (type == StrategicTileType.ice)
            return tiles[pos.x, pos.y] == type;
        if (StrategicTileInfo.SandFamily.Contains(type))
        {
            return StrategicTileInfo.SandFamily.Contains(tiles[pos.x, pos.y]);
        }
        if (StrategicTileInfo.GrassFamily.Contains(type))
        {
            return StrategicTileInfo.GrassFamily.Contains(tiles[pos.x, pos.y]);
        }
        if (StrategicTileInfo.SnowFamily.Contains(type))
        {
            return StrategicTileInfo.SnowFamily.Contains(tiles[pos.x, pos.y]);
        }
        if (StrategicTileInfo.AshenFamily.Contains(type))
        {
            return StrategicTileInfo.AshenFamily.Contains(tiles[pos.x, pos.y]);
        }
        if (StrategicTileInfo.ShallowWaterFamily.Contains(type))
        {
            return StrategicTileInfo.ShallowWaterFamily.Contains(tiles[pos.x, pos.y]);
        }
        if (StrategicTileInfo.SavannahFamily.Contains(type))
        {
            return StrategicTileInfo.SavannahFamily.Contains(tiles[pos.x, pos.y]);
        }
        //if (StrategicTileInfo.WaterFamily.Contains(type))
        //{
        //    return StrategicTileInfo.WaterFamily.Contains(tiles[pos.x, pos.y]);
        //}
        return tiles[pos.x, pos.y] == type;
    }

    internal bool AreTypesSame(Vec2 pos1, Vec2 pos2)
    {
        StrategicTileType postion1 = State.World.Tiles[pos1.x, pos1.y];
        StrategicTileType postion2 = State.World.Tiles[pos2.x, pos2.y];



        if ((StrategicTileInfo.SandFamily.Contains(postion1) && StrategicTileInfo.SandFamily.Contains(postion2)) ||
            (StrategicTileInfo.GrassFamily.Contains(postion1) && StrategicTileInfo.GrassFamily.Contains(postion2)) ||
            (StrategicTileInfo.SnowFamily.Contains(postion1) && StrategicTileInfo.SnowFamily.Contains(postion2)) ||
            (StrategicTileInfo.AshenFamily.Contains(postion1) && StrategicTileInfo.AshenFamily.Contains(postion2)) ||
            (StrategicTileInfo.ShallowWaterFamily.Contains(postion1) && StrategicTileInfo.ShallowWaterFamily.Contains(postion2)) ||
            (StrategicTileInfo.SavannahFamily.Contains(postion1) && StrategicTileInfo.SavannahFamily.Contains(postion2))
            )
        {
            return true;
        }

        return postion1 == postion2;
    }

    internal IEnumerable<KeyValuePair<int, StrategicTileType>> DetermineOverlay(int x, int y)
    {
        Vec2 pos = new Vec2(x, y);
        int dir = DetermineType(pos, State.World.Tiles[x,y]);
        List<StrategicTileType> adj_types = new List<StrategicTileType>();



        var temp_Dict = from entry in GetOverlayTypes(dir) orderby entry.Value ascending select entry;

        Vec2 temp_vec;
        Vec2 comp_vec;

        List<int> needs_NW_corner = new List<int> {0, 1, 3, 8, 12, 13, 21};
        List<int> needs_NE_corner = new List<int> {2, 1, 5, 10, 12, 13, 22};
        List<int> needs_SW_corner = new List<int> {3, 8, 13, 17, 16, 28, 29};
        List<int> needs_SE_corner = new List<int> {5, 10, 13,17, 18, 28, 30};

        IEnumerable<KeyValuePair<int, StrategicTileType>> conat_list = temp_Dict;

        if (needs_NW_corner.Contains(dir))
        {
            if (!((dir == 1 || dir == 8) && !(pos.x > 1 && pos.y < State.World.Tiles.GetUpperBound(1) - 1)))
            {
                if (!AreTypesSame(GetPos(pos, Neighbor.NorthWest), GetPos(pos, Neighbor.North)) && !AreTypesSame(GetPos(pos, Neighbor.NorthWest), GetPos(pos, Neighbor.West)) && (AreTypesSame(GetPos(pos, Neighbor.North), GetPos(pos, Neighbor.West)) || (dir == 1 || dir == 8)))
                {
                    temp_vec = GetPos(pos, Neighbor.NorthWest);
                    comp_vec = GetPos(pos, dir == 8 ? Neighbor.West : Neighbor.North);
                    if (State.World.Tiles[temp_vec.x, temp_vec.y] > State.World.Tiles[comp_vec.x, comp_vec.y])
                    {
                        KeyValuePair<int, StrategicTileType> corner = new KeyValuePair<int, StrategicTileType>(15, State.World.Tiles[temp_vec.x, temp_vec.y]);
                        conat_list = temp_Dict.Append(corner);
                    }
                }
            }

        }
        if (needs_NE_corner.Contains(dir))
        {
            if (!((dir == 1 || dir == 10) && !(pos.x < State.World.Tiles.GetUpperBound(0) - 1 && pos.y < State.World.Tiles.GetUpperBound(1) - 1)))
            {
                if (!AreTypesSame(GetPos(pos, Neighbor.NorthEast), GetPos(pos, Neighbor.North)) && !AreTypesSame(GetPos(pos, Neighbor.NorthEast), GetPos(pos, Neighbor.East)) && (AreTypesSame(GetPos(pos, Neighbor.North), GetPos(pos, Neighbor.East)) || (dir == 1 || dir == 10)))
                {
                    temp_vec = GetPos(pos, Neighbor.NorthEast);
                    comp_vec = GetPos(pos, dir == 10 ? Neighbor.East : Neighbor.North);
                    if (State.World.Tiles[temp_vec.x, temp_vec.y] > State.World.Tiles[comp_vec.x, comp_vec.y])
                    {
                        KeyValuePair<int, StrategicTileType> corner = new KeyValuePair<int, StrategicTileType>(14, State.World.Tiles[temp_vec.x, temp_vec.y]);
                        conat_list = conat_list.Append(corner);
                    }

                }
            }           
        }
        if (needs_SW_corner.Contains(dir))
        {
            if (!((dir == 17 || dir == 8) && !(pos.x > 1 && pos.y > 1)))
            {
                if (!AreTypesSame(GetPos(pos, Neighbor.SouthWest), GetPos(pos, Neighbor.South)) && !AreTypesSame(GetPos(pos, Neighbor.SouthWest), GetPos(pos, Neighbor.West)) && (AreTypesSame(GetPos(pos, Neighbor.South), GetPos(pos, Neighbor.West)) || (dir == 17 || dir == 8)))
                {
                    temp_vec = GetPos(pos, Neighbor.SouthWest);
                    comp_vec = GetPos(pos, dir == 8 ? Neighbor.West : Neighbor.South);
                    if (State.World.Tiles[temp_vec.x, temp_vec.y] > State.World.Tiles[comp_vec.x, comp_vec.y])
                    {
                        KeyValuePair<int, StrategicTileType> corner = new KeyValuePair<int, StrategicTileType>(7, State.World.Tiles[temp_vec.x, temp_vec.y]);
                        conat_list = conat_list.Append(corner);
                    }
                }
            }
        }
        if (needs_SE_corner.Contains(dir))
        {
            if (!((dir == 17 || dir == 10) && !(pos.x < State.World.Tiles.GetUpperBound(0) - 1 && pos.y > 1)))
            {
                if (!AreTypesSame(GetPos(pos, Neighbor.SouthEast), GetPos(pos, Neighbor.East)) && !AreTypesSame(GetPos(pos, Neighbor.SouthEast), GetPos(pos, Neighbor.South)) && (AreTypesSame(GetPos(pos, Neighbor.East), GetPos(pos, Neighbor.South)) || (dir == 17 || dir == 10)))
                {
                    temp_vec = GetPos(pos, Neighbor.SouthEast);
                    comp_vec = GetPos(pos, dir == 10 ? Neighbor.East : Neighbor.South);
                    if (State.World.Tiles[temp_vec.x, temp_vec.y] > State.World.Tiles[comp_vec.x, comp_vec.y])
                    {
                        KeyValuePair<int, StrategicTileType> corner = new KeyValuePair<int, StrategicTileType>(6, State.World.Tiles[temp_vec.x, temp_vec.y]);
                        conat_list = conat_list.Append(corner);
                    }

                }
            }


        }

        return conat_list;

        Dictionary<int, StrategicTileType> GetOverlayTypes(int direction)
        {
            Dictionary<int, StrategicTileType> true_types = new Dictionary<int, StrategicTileType>();

            Vec2 temp_vec1;
            Vec2 temp_vec2;
            Vec2 temp_vec3;
            Vec2 temp_vec4;

            switch (direction)
            {
                case 0:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.West);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(0, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(8, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 1:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    break;
                case 2:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(2, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 3:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.West);
                    temp_vec3 = GetPos(pos, Neighbor.South);
                    bool t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    bool t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    bool t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(3, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(0, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(17, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(4, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(8, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(16, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(8, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(17, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 4:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.South);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(4, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(17, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 5:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    temp_vec3 = GetPos(pos, Neighbor.South);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(5, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(2, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(17, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(4, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(18, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(17, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 6:
                    temp_vec1 = GetPos(pos, Neighbor.SouthEast);
                    true_types.Add(6, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    break;
                case 7:
                    temp_vec1 = GetPos(pos, Neighbor.SouthWest);
                    true_types.Add(7, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    break;
                case 8:
                    temp_vec1 = GetPos(pos, Neighbor.West);
                    true_types.Add(8, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    break;
                case 9:
                    //true_types.Add(State.World.Tiles[x,y]); // This is an empty space on the sprite sheet
                    break;
                case 10:
                    temp_vec1 = GetPos(pos, Neighbor.East);
                    true_types.Add(10, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    break;
                case 11:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec2 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    temp_vec4 = GetPos(pos, Neighbor.SouthWest);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    bool t1_t4_same = AreTypesSame(temp_vec1, temp_vec4);
                    bool t2_t4_same = AreTypesSame(temp_vec2, temp_vec4);
                    bool t3_t4_same = AreTypesSame(temp_vec3, temp_vec4);
                    if (t1_t2_same && t1_t3_same && t1_t4_same) //all same
                    {
                        true_types.Add(11, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else if (t1_t2_same && t1_t3_same) // t4 not same
                    {
                        true_types.Add(47, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(7, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t1_t3_same && t1_t4_same) // t2 not same
                    {
                        true_types.Add(45, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(15, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t1_t2_same && t1_t4_same) // t3 not same
                    {
                        true_types.Add(46, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(6, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t2_t3_same && t2_t4_same)
                    {
                        true_types.Add(44, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(14, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t1_t2_same && t3_t4_same)
                    {
                        true_types.Add(43, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(42, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t1_t3_same && t2_t4_same)
                    {
                        true_types.Add(41, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(40, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t1_t4_same && t2_t3_same)
                    {
                        true_types.Add(23, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(31, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    else if (t1_t2_same)
                    {
                        true_types.Add(43, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        true_types.Add(7, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t1_t3_same)
                    {
                        true_types.Add(42, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(15, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        true_types.Add(17, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t2_t3_same)
                    {
                        true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(31, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        true_types.Add(6, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t1_t4_same)
                    {
                        true_types.Add(23, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(15, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                    }
                    else if (t2_t4_same)
                    {
                        true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        true_types.Add(41, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t3_t4_same)
                    {
                        true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(14, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        true_types.Add(42, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else // none same
                    {
                        true_types.Add(14, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(15, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        true_types.Add(7, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    break;
                case 12:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    temp_vec3 = GetPos(pos, Neighbor.West);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(12, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(2, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(8, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(20, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(20, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(8, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 13:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    temp_vec3 = GetPos(pos, Neighbor.South);
                    temp_vec4 = GetPos(pos, Neighbor.West);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    t1_t4_same = AreTypesSame(temp_vec1, temp_vec4);
                    t2_t4_same = AreTypesSame(temp_vec2, temp_vec4);
                    t3_t4_same = AreTypesSame(temp_vec3, temp_vec4);
                    if (t1_t2_same && t1_t3_same && t1_t4_same) //all same
                    {
                        true_types.Add(13, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else if (t1_t2_same && t1_t3_same) // t4 not same
                    {
                        true_types.Add(5, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(8, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t1_t3_same && t1_t4_same) // t2 not same
                    {
                        true_types.Add(3, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    else if (t1_t2_same && t1_t4_same) // t3 not same
                    {
                        true_types.Add(12, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(17, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                    }
                    else if (t2_t3_same && t2_t4_same)
                    {
                        true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(28, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t1_t2_same && t3_t4_same)
                    {
                        true_types.Add(2, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(16, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t1_t3_same && t2_t4_same)
                    {
                        true_types.Add(4, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(20, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t1_t4_same && t2_t3_same)
                    {
                        true_types.Add(0, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(18, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    else if (t1_t2_same)
                    {
                        true_types.Add(2, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(17, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        true_types.Add(8, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t1_t3_same)
                    {
                        true_types.Add(4, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        true_types.Add(8, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t2_t3_same)
                    {
                        true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(18, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        true_types.Add(8, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t1_t4_same)
                    {
                        true_types.Add(0, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        true_types.Add(17, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                    }
                    else if (t2_t4_same)
                    {
                        true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(17, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        true_types.Add(20, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else if (t3_t4_same)
                    {
                        true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        true_types.Add(16, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    else // none same
                    {
                        true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        true_types.Add(17, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        true_types.Add(8, State.World.Tiles[temp_vec4.x, temp_vec4.y]);
                    }
                    break;
                case 14:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    true_types.Add(14, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    break;
                case 15:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    break;
                case 16:
                    temp_vec1 = GetPos(pos, Neighbor.West);
                    temp_vec2 = GetPos(pos, Neighbor.South);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(16, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(8, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(17, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 17:
                    temp_vec1 = GetPos(pos, Neighbor.South);
                    true_types.Add(17, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    break;
                case 18:
                    temp_vec1 = GetPos(pos, Neighbor.South);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(18, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(17, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 19:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    true_types.Add(19, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    break; // Full tile, no clue which way
                case 20:
                    temp_vec1 = GetPos(pos, Neighbor.West);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(20, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(8, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 21:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.West);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(21, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(0, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(24, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(8, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(26, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(8, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 22:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.East);
                    temp_vec3 = GetPos(pos, Neighbor.SouthWest);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(22, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(2, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(7, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(25, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(27, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(10, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(7, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 23:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(23, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(14, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(7, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 24:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.SouthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(24, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(6, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 25:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(25, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(7, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 26:
                    temp_vec1 = GetPos(pos, Neighbor.West);
                    temp_vec2 = GetPos(pos, Neighbor.SouthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(26, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(8, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(6, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 27:
                    temp_vec1 = GetPos(pos, Neighbor.East);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(27, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(10, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(7, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 28:
                    temp_vec1 = GetPos(pos, Neighbor.East);
                    temp_vec2 = GetPos(pos, Neighbor.South);
                    temp_vec3 = GetPos(pos, Neighbor.West);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(28, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(18, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(8, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(20, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(32, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(10, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(16, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(10, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(17, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(8, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 29:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec2 = GetPos(pos, Neighbor.South);
                    temp_vec3 = GetPos(pos, Neighbor.West);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(29, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(32, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(8, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(34, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(17, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(14, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(16, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(14, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(17, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(8, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 30:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec2 = GetPos(pos, Neighbor.South);
                    temp_vec3 = GetPos(pos, Neighbor.East);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(30, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(33, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(10, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(35, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(17, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(18, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(17, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(10, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 31:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec2 = GetPos(pos, Neighbor.SouthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(31, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(6, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 32:
                    temp_vec1 = GetPos(pos, Neighbor.South);
                    temp_vec2 = GetPos(pos, Neighbor.NorthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(32, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(17, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(14, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 33:
                    temp_vec1 = GetPos(pos, Neighbor.South);
                    temp_vec2 = GetPos(pos, Neighbor.NorthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(33, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(17, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(15, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 34:
                    temp_vec1 = GetPos(pos, Neighbor.West);
                    temp_vec2 = GetPos(pos, Neighbor.NorthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(34, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(8, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(14, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 35:
                    temp_vec1 = GetPos(pos, Neighbor.East);
                    temp_vec2 = GetPos(pos, Neighbor.NorthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(35, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(10, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(15, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 36:
                    temp_vec1 = GetPos(pos, Neighbor.West);
                    temp_vec2 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(36, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(33, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(26, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(14, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(8, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(41, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(8, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(14, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 37:
                    temp_vec1 = GetPos(pos, Neighbor.North);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(37, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(25, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(24, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(7, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(42, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(1, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(7, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 38:
                    temp_vec1 = GetPos(pos, Neighbor.South);
                    temp_vec2 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec3 = GetPos(pos, Neighbor.NorthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(38, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(33, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(14, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(32, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(15, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(17, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(43, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(17, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(15, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(14, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 39:
                    temp_vec1 = GetPos(pos, Neighbor.East);
                    temp_vec2 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec3 = GetPos(pos, Neighbor.SouthWest);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(39, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(35, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(7, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(27, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(15, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(10, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(39, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(10, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(15, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(7, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 40:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(40, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(7, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 41:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec2 = GetPos(pos, Neighbor.SouthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(41, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(14, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(6, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 42:
                    temp_vec1 = GetPos(pos, Neighbor.SouthWest);
                    temp_vec2 = GetPos(pos, Neighbor.SouthEast);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(42, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(7, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(6, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 43:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec2 = GetPos(pos, Neighbor.NorthWest);
                    if (AreTypesSame(temp_vec1, temp_vec2))
                    {
                        true_types.Add(43, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                    }
                    else
                    {
                        true_types.Add(14, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        true_types.Add(15, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                    }
                    break;
                case 44:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(44, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(40, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(31, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(7, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(42, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(7, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 45:
                    temp_vec1 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec2 = GetPos(pos, Neighbor.SouthWest);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(45, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(23, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(41, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(7, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(14, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(42, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(14, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(7, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 46:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec2 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec3 = GetPos(pos, Neighbor.SouthWest);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(46, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(43, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(7, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(40, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(6, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(23, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(14, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(7, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                case 47:
                    temp_vec1 = GetPos(pos, Neighbor.NorthWest);
                    temp_vec2 = GetPos(pos, Neighbor.NorthEast);
                    temp_vec3 = GetPos(pos, Neighbor.SouthEast);
                    t1_t2_same = AreTypesSame(temp_vec1, temp_vec2);
                    t1_t3_same = AreTypesSame(temp_vec1, temp_vec3);
                    t2_t3_same = AreTypesSame(temp_vec2, temp_vec3);
                    if (t1_t2_same)
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(47, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                        }
                        else
                        {
                            true_types.Add(43, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    else
                    {
                        if (t1_t3_same)
                        {
                            true_types.Add(31, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(14, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else if (t2_t3_same)
                        {
                            true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(41, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                        }
                        else
                        {
                            true_types.Add(15, State.World.Tiles[temp_vec1.x, temp_vec1.y]);
                            true_types.Add(14, State.World.Tiles[temp_vec2.x, temp_vec2.y]);
                            true_types.Add(6, State.World.Tiles[temp_vec3.x, temp_vec3.y]);
                        }
                    }
                    break;
                default:
                    Debug.Log("Tile Logic Fell Through");
                    break;
            }

            return true_types;
        }

    }
}

