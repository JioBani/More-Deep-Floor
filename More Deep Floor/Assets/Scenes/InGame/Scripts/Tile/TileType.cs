using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Tiles
{
    [Flags]
    public enum TileType
    {
        Blank = 1 << 0, //1
        WaitingRoom = 1 << 2, //4
        BattleField = 1 << 4, //16
        Road = 1 << 6, //64
    }

}

