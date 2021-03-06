﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCorner : Tile
{
    private static int[][,] occupied = new int[][,] { new int[,] { { 0, 0 }, { 1, 0 },
                                                                   { 0, 1 }, },
                                                      new int[,] { { 0, 0 }, { 1, 0 },
                                                                             { 1, 1 } },
                                                      new int[,] {           { 1, 0 },
                                                                   { 0, 1 }, { 1, 1 } },
                                                      new int[,] { { 0, 0 },
                                                                   { 0, 1 }, { 1, 1 } } };

    public TileCorner(int x = 0, int y = 0, int rot = 0)
    {
        walls = new Wall[4][] { new Wall[2], new Wall[2], new Wall[2], new Wall[2] };
        Place(x, y, rot);
        RandomizeWalls();
    }

    /// <summary>
    /// Returns an array of tilemap coordinates {x,y} occupied by this Tile. 
    /// </summary>
    /// <returns>Array of Int[] containing {x, y}</returns>
    public override int[,] GetRelativeSpacesOccupied()
    {
        return occupied[Rotation / 90];
    }
}
