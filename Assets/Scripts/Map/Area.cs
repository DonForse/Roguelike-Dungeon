using UnityEngine;
using System.Collections;

public class Area
{
    public AreaType AreaType { get; set; }
    public int CellCountCol { get; set; }
    public int CellCountRow { get; set; }

    public int MapPositionCol { get; set; }
    public int MapPositionRow { get; set; }

    public int SurfaceArea { get { return CellCountRow * CellCountCol; } }
    public string Name { get; set; }

    public Vector2 CoordTopLeft { get { return new Vector2(MapPositionCol, MapPositionRow); } }
    public Vector2 CoordTopRight { get { return new Vector2(MapPositionCol + CellCountCol, MapPositionRow); } }
    public Vector2 CoordBotLeft { get { return new Vector2(MapPositionCol, MapPositionRow + CellCountRow); } }
    public Vector2 CoordBotRight { get { return new Vector2(MapPositionCol + CellCountCol, MapPositionRow + CellCountRow); } }
}

