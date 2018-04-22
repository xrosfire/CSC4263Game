﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardMap{
    private int boardSizeX, boardSizeY;
    public boardTile[,] boardTiles;
    public List<placedTile> board{ get; private set; }
    int center;

    public boardMap() {
        boardSizeX = boardSizeY = 1001;
        center = boardSizeX / 2;
        boardTiles = new boardTile[boardSizeY, boardSizeX];
        board = new List<placedTile>();
    }

    boardTile tempTile;
    public bool placeTile(int x, int y, boardTile newTile, bool firstTile=false) {
        if (checkTileSpot(x, y, newTile) || firstTile) {
            

            for (int i = 0; i < newTile.yLen; i++) {
                for (int j = 0; j < newTile.xLen; j++) {
                    Debug.Log(x + "  " + y + "   " + j + "    " + i);
                    board.Add(new placedTile(x+j, -y+i, newTile.squares[i, j]));
                    tempTile = new boardTile(1, 1);
                    tempTile.setSquare(0, 0, new boardTile.Wall[] { newTile.squares[i, j].getTop(), newTile.squares[i, j].getRight(), newTile.squares[i, j].getBottom(), newTile.squares[i, j].getLeft() });
                    boardTiles[y + center + i, x + center + j] = tempTile;
                }
            }
            return true;
        }
        return false;
    }
    /*
     * Check a tile to the board location
     * x,y location of top-left corner of tile
     * check the location to ensure there are not rooms where tile will place rooms
     */
    public bool checkTileSpot(int x, int y, boardTile newTile) {
        if (checkLocation(x+center, y+center, newTile) && checkForOneDoor(x+center,y+center,newTile)){
            return true;
        } else {
            return false;
        }
    }

    private bool checkForOneDoor(int x, int y, boardTile newTile) {

        List<boardTile.Door> doors = newTile.getAllDoors();
        bool doorCheck = false;
        foreach (boardTile.Door door in doors) {
            switch (door.facing) {
                case boardTile.edgeLoc.top:
                    if (boardTiles[door.y + y + 1, door.x + x] != null) {
                        if (boardTiles[y + 1 - door.y, x + door.x].squares[0, 0].bottomIsDoor()) {
                            doorCheck = true;
                        }
                    }
                    break;
                case boardTile.edgeLoc.right:
                    if (boardTiles[door.y + y,door.x + x + 1] != null) {
                        if (boardTiles[y - door.y, x + 1 + door.x].squares[0, 0].leftIsDoor()) {
                            doorCheck = true;
                        }
                    }
                    break;
                case boardTile.edgeLoc.bottom:
                    if (boardTiles[y - 1 - door.y,door.x + x] != null) {
                        if (boardTiles[y - 1 - door.y, x+door.x].squares[0, 0].topIsDoor()) {
                            doorCheck = true;
                        }
                    }
                    break;
                case boardTile.edgeLoc.left:
                    if (boardTiles[y - door.y, door.x + x - 1] != null) {
                        if (boardTiles[y - door.y, x - 1 + door.x].squares[0, 0].rightIsDoor()) {
                            doorCheck = true;
                        }
                    }
                    break;
            }
        }
        return doorCheck;
    }

    private bool checkLocation(int x, int y, boardTile newTile) {
        int xLen = newTile.xLen;
        int yLen = newTile.yLen;
        Debug.Log(xLen + " " + yLen);
        bool clear = true;
        for (int i = 0; i < yLen; i++) {
            for (int j = 0; j < xLen; j++) {
                if (boardTiles[y + i, x + j] != null && !newTile.squares[i, j].isEmpty()) {
                    clear = false;
                }
            }
        }
        Debug.Log(clear);
        return clear;
    }
}
