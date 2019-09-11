using UnityEngine;
using System.Collections;

public enum AreaType
{
    Entrance = 1,
    Exit = 2,
    Normal = 3,
    Empty = 4
}

public enum CellType
{
    Empty = 0,
    Room = 1,
    Path = 2,
    Monster = 3,
    Reserved = 11,
}