using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapManager : MonoBehaviour
{
    public int cellCountCol = 30;
    public int cellCountRow = 30;
    public int MapSize = 3;
    public CellType[,] Map { get; set; }
    public List<Area> sections = new List<Area>();
    public List<Room> rooms = new List<Room>();

    public GameObject Floor;
    public GameObject Roof;
    public GameObject FloorWallN;
    public GameObject FloorWallS;
    public GameObject FloorWallW;
    public GameObject FloorWallE;
    public GameObject WallE;
    public GameObject WallW;
    public GameObject WallN;
    public GameObject WallS;


    void Awake()
    {
        Floor = Resources.Load<GameObject>("Map/Floor");
        FloorWallE = Resources.Load<GameObject>("Map/Floor Wall E");
        FloorWallS = Resources.Load<GameObject>("Map/Floor Wall S");
        FloorWallN = Resources.Load<GameObject>("Map/Floor Wall N");
        FloorWallW = Resources.Load<GameObject>("Map/Floor Wall W");

        WallE = Resources.Load<GameObject>("Map/Wall E");
        WallW = Resources.Load<GameObject>("Map/Wall W");
        WallS = Resources.Load<GameObject>("Map/Wall S");
        WallN = Resources.Load<GameObject>("Map/Wall N");
        Roof = Resources.Load<GameObject>("Map/Floor");

        Map = new CellType[cellCountRow, cellCountCol];
        LocateAreas();

        CreatePaths();
        InstantiateMap();
    }

    // Use this for initialization
    void Start()
    {
        var personaje = GameObject.Find("Personaje");
        personaje.transform.position = new Vector3(rooms[0].Area.MapPositionCol * MapSize, 3, rooms[0].Area.MapPositionRow * MapSize);

        var monstruo = GameObject.Find("Monstruo");
        monstruo.transform.position = new Vector3(rooms[1].Area.MapPositionCol * MapSize, 3, rooms[1].Area.MapPositionRow * MapSize);
    }

    private Mesh mesh;

    private void InstantiateMap()
    {
        //GenerateMesh();
        for (int i = 0; i < cellCountCol; i++)
        {
            for (int j = 0; j < cellCountRow; j++)
            {
                switch (Map[i, j])
                {
                    case CellType.Empty:
                        break;
                    case CellType.Path:
                    case CellType.Room:
                        GameObject go = Floor;
                        IList<GameObject> walls = new List<GameObject>();;
                        //Vertices[i + (j * cellCountCol)] = new Vector3(i, 0,j);

                        /*Faltan Esquinas*/
                        if (i == 0 || Map[i - 1, j] == CellType.Empty)
                        {
                            walls.Add(FloorWallE);
                            walls.Add(WallE);
                        }
                        if (i == cellCountCol - 1 || Map[i + 1, j] == CellType.Empty)
                        {
                            walls.Add(FloorWallW);
                            walls.Add(WallW);
                        }
                        if (j == cellCountRow - 1 || Map[i, j + 1] == CellType.Empty)
                        {
                            walls.Add(FloorWallS);
                            walls.Add(WallS);
                        }
                        if (j == 0 || Map[i, j - 1] == CellType.Empty)
                        {
                            walls.Add(FloorWallN);
                            walls.Add(WallN);
                        }

                        var tileGo = Instantiate<GameObject>(go);
                        foreach (var wall in walls)
                        {
                            var wallGO = Instantiate<GameObject>(wall);
                            wallGO.transform.SetParent(tileGo.gameObject.transform);
                            wallGO.transform.localPosition = new Vector3(0, 0, 0);
                            wallGO.tag = Tags.Wall;
                        }
                        tileGo.transform.position = new Vector3(i * MapSize, 0, j * MapSize);
                        if (Map[i, j] == CellType.Room)
                        {
                            var room = rooms.FirstOrDefault(r
                                => i >= r.Area.MapPositionCol
                                && j >= r.Area.MapPositionRow
                                && i <= (r.Area.MapPositionCol + r.Area.CellCountCol)
                                && j <= r.Area.MapPositionRow + r.Area.CellCountRow);

                            tileGo.transform.SetParent(room.gameObject.transform);

                            var roofGo = Instantiate<GameObject>(Roof);
                            roofGo.name = Tags.Roof;
                            roofGo.transform.SetParent(tileGo.gameObject.transform);
                            roofGo.transform.localPosition = new Vector3(0, 0, -3.5f);
                            roofGo.tag = Tags.Roof;
                        }
                        break;
                    case CellType.Monster:
                        break;
                    case CellType.Reserved:
                        break;
                    default:
                        break;
                }
            }
        }

    }

    private void GenerateMesh()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";

        Vector3[] vertices = new Vector3[(cellCountCol + 1) * (cellCountRow + 1) * 2];
        var cellcount = 0;
        var wallCount = 0;
        for (int i = 0; i < cellCountCol; i++)
        {
            for (int j = 0; j < cellCountRow; j++)
            {
                vertices[i + (j * cellCountCol)] = new Vector3(i, 0, j);
                vertices[(cellCountCol * cellCountRow) + (i + (j * cellCountCol))] = new Vector3(i, 2, j);
                if (Map[i, j] != CellType.Empty)
                    cellcount++;

                if (i == 0 || Map[i - 1, j] == CellType.Empty)
                    wallCount++;
                if (i == cellCountCol - 1 || Map[i + 1, j] == CellType.Empty)
                    wallCount++;
                if (j == cellCountRow - 1 || Map[i, j + 1] == CellType.Empty)
                    wallCount++;
                if (j == 0 || Map[i, j - 1] == CellType.Empty)
                    wallCount++;
            }
        }

        int[] triangles = new int[(cellcount * 2 + wallCount) * 6];
        var triangleIndex = 0;
        for (int x = 0; x < cellCountCol; x++)
        {
            for (int y = 0; y < cellCountRow; y++)
            {
                if (Map[x, y] != CellType.Empty)
                {
                    triangles[triangleIndex] = x * cellCountCol + y;
                    triangles[triangleIndex + 3] = triangles[triangleIndex + 2] = x * cellCountCol + y + 1;
                    triangles[triangleIndex + 4] = triangles[triangleIndex + 1] = (x + 1) * cellCountCol + y;
                    triangles[triangleIndex + 5] = (x + 1) * cellCountCol + y + 1;
                    triangleIndex += 6;

                    if (x == 0 || Map[x - 1, y] == CellType.Empty)
                    {
                        Debug.Log("Left");
                        triangles[cellcount + triangleIndex] = x * cellCountCol + y;
                        triangles[cellcount + triangleIndex + 3] = triangles[cellcount + triangleIndex + 2] = cellCountCol * cellCountRow + x * cellCountCol + y;
                        triangles[cellcount + triangleIndex + 4] = triangles[cellcount + triangleIndex + 1] = (x + 1) * cellCountCol + y;
                        triangles[cellcount + triangleIndex + 5] = cellCountCol * cellCountRow + (x + 1) * cellCountCol + y;
                    }
                    if (x == cellCountCol - 1 || Map[x + 1, y] == CellType.Empty)
                    {
                        Debug.Log("Right");
                        triangles[cellcount + triangleIndex] = cellCountCol * cellCountRow + (x + 1) * cellCountCol + y + 1;
                        triangles[cellcount + triangleIndex + 3] = triangles[cellcount + triangleIndex + 2] = x * cellCountCol + y + 1;
                        triangles[cellcount + triangleIndex + 4] = triangles[cellcount + triangleIndex + 1] = cellCountCol * cellCountRow + x * cellCountCol + y;
                        triangles[cellcount + triangleIndex + 5] = (x + 1) * cellCountCol + y + 1;
                    }
                    if (y == cellCountRow - 1 || Map[x, y + 1] == CellType.Empty)
                    {
                        Debug.Log("Bottom");
                        triangles[cellcount + triangleIndex] = x * cellCountCol + y;
                        triangles[cellcount + triangleIndex + 3] = triangles[cellcount + triangleIndex + 2] = x * cellCountCol + y + 1;
                        triangles[cellcount + triangleIndex + 4] = triangles[cellcount + triangleIndex + 1] = cellCountCol * cellCountRow + x * cellCountCol + y;
                        triangles[cellcount + triangleIndex + 5] = cellCountCol * cellCountRow + x * cellCountCol + y + 1;
                    }
                    if (y == 0 || Map[x, y - 1] == CellType.Empty)
                    {
                        Debug.Log("Top");
                        triangles[cellcount + triangleIndex] = cellCountCol * cellCountRow + x * cellCountCol + y;
                        triangles[cellcount + triangleIndex + 3] = triangles[cellcount + triangleIndex + 2] = cellCountCol * cellCountRow + x * cellCountCol + y + 1;
                        triangles[cellcount + triangleIndex + 4] = triangles[cellcount + triangleIndex + 1] = (x + 1) * cellCountCol + y;
                        triangles[cellcount + triangleIndex + 5] = (x + 1) * cellCountCol + y + 1;
                    }
                }
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Area> AddAreas()
    {
        var r = new List<Area>();
        r.Add(new Area { AreaType = AreaType.Entrance, CellCountCol = 5, CellCountRow = 8 });
        r.Add(new Area { AreaType = AreaType.Exit, CellCountCol = 3, CellCountRow = 5 });
        r.Add(new Area { AreaType = AreaType.Normal, CellCountCol = 18, CellCountRow = 12 });
        r.Add(new Area { AreaType = AreaType.Normal, CellCountCol = 15, CellCountRow = 5 });
        r.Add(new Area { AreaType = AreaType.Normal, CellCountCol = 15, CellCountRow = 20 });
        r.Add(new Area { AreaType = AreaType.Empty, CellCountCol = 8, CellCountRow = 12 });
        r.Add(new Area { AreaType = AreaType.Empty, CellCountCol = 4, CellCountRow = 15 });
        r.Add(new Area { AreaType = AreaType.Normal, CellCountCol = 7, CellCountRow = 5 });
        r.Add(new Area { AreaType = AreaType.Empty, CellCountCol = 3, CellCountRow = 5 });
        return r;
    }

    public void CreatePaths()
    {
        int[,] AstarMap = new int[cellCountRow, cellCountCol];
        for (int i = 0; i < cellCountRow; i++)
        {
            for (int j = 0; j < cellCountCol; j++)
            {
                if (Map[i, j] == CellType.Room)
                    AstarMap[i, j] = 1;
                else
                    AstarMap[i, j] = 3;
            }
        }
        for (int i = 0; i < rooms.Count - 1; i++)
        {
            AStarNode startpoint = new AStarNode(Random.Range(rooms[i].Area.MapPositionCol, rooms[i].Area.MapPositionCol + rooms[i].Area.CellCountCol), Random.Range(rooms[i].Area.MapPositionRow, rooms[i].Area.MapPositionRow + rooms[i].Area.CellCountRow), 0);

            AStarNode nextPoint = new AStarNode(Random.Range(rooms[i + 1].Area.MapPositionCol, rooms[i + 1].Area.MapPositionCol + rooms[i + 1].Area.CellCountCol), Random.Range(rooms[i + 1].Area.MapPositionRow, rooms[i + 1].Area.MapPositionRow + rooms[i + 1].Area.CellCountRow), 0);


            var openList = new List<AStarNode> { startpoint };
            var closedList = new List<AStarNode>();
            for (int j = 0; j < openList.Count; j++)
            {
                var currentNode = openList[j];
                openList.RemoveAt(j);
                var successors = new List<AStarNode>();
                if (currentNode.Position.x > 0)
                    successors.Add(new AStarNode(currentNode.Position.x - 1, currentNode.Position.y, AstarMap[(int)currentNode.Position.x - 1, (int)currentNode.Position.y]));
                if (currentNode.Position.x < cellCountCol - 1)
                    successors.Add(new AStarNode(currentNode.Position.x + 1, currentNode.Position.y, AstarMap[(int)currentNode.Position.x + 1, (int)currentNode.Position.y]));
                if (currentNode.Position.y > 0)
                    successors.Add(new AStarNode(currentNode.Position.x, currentNode.Position.y - 1, AstarMap[(int)currentNode.Position.x, (int)currentNode.Position.y - 1]));
                if (currentNode.Position.y < cellCountRow - 1)
                    successors.Add(new AStarNode(currentNode.Position.x, currentNode.Position.y + 1, AstarMap[(int)currentNode.Position.x, (int)currentNode.Position.y + 1]));

                bool nodeFound = false;
                foreach (var successor in successors)
                {
                    successor.PreviousNode = currentNode;
                    if (successor.Position == nextPoint.Position)
                    {
                        nodeFound = true;
                        break;
                        //end.break;
                    }
                    successor.g = successor.PreviousNode.g + successor.Cost;

                    successor.h = (Mathf.Abs(nextPoint.Position.x - successor.Position.x) + Mathf.Abs(nextPoint.Position.y - successor.Position.y)) * 8;//Mathf.Sqrt(Mathf.Pow(nextPoint.Position.x - successor.Position.x, 2f) + Mathf.Pow(nextPoint.Position.y - successor.Position.y, 2f)) * 5;

                    successor.f = successor.g + successor.h;
                    if (openList.Any(n => successor.Position == n.Position && n.f <= successor.f))
                        continue;
                    if (closedList.Any(n => successor.Position == n.Position && n.f <= successor.f))
                        continue;
                    openList.Add(successor);
                }
                if (nodeFound)
                {
                    AstarMap = AddPaths(currentNode, startpoint, nextPoint, AstarMap);
                    break;
                }

                openList = openList.OrderBy(s => s.f).ToList();
                closedList.Add(currentNode);
                j--;
            }
        }
    }

    public int[,] AddPaths(AStarNode currentNode, AStarNode startPoint, AStarNode endPoint, int[,] aStarMap)
    {
        while (!(currentNode.Position == startPoint.Position))
        {
            if (Map[(int)currentNode.Position.x, (int)currentNode.Position.y] != CellType.Room)
            {
                aStarMap[(int)currentNode.Position.x, (int)currentNode.Position.y] = 1;
                Map[(int)currentNode.Position.x, (int)currentNode.Position.y] = CellType.Path;
            }
            currentNode = currentNode.PreviousNode;
        }
        //aStarMap[endPoint.position.x, endPoint.position.y] = 1;
        //Map[endPoint.position.x, endPoint.position.y] = CellType.Path;
        return aStarMap;
    }

    public void LocateAreas()
    {
        IList<Area> areas = AddAreas();
        //Add a section = to full map (should be empty map).
        sections.Add(new Area() { CellCountCol = cellCountCol, CellCountRow = cellCountRow, MapPositionCol = 0, MapPositionRow = 0 });
        foreach (var room in areas)
        {
            try
            {
                //bool canCreateArea = false;
                int posCol = 0;
                int posRow = 0;

                var section = SelectAvailableSection(room);

                posCol = Random.Range(1, (section.CellCountCol - room.CellCountCol) - 2); //Add a 1 space cell for reserved area (so it doesn't block future path or collapse the rooms)
                posRow = Random.Range(1, (section.CellCountRow - room.CellCountRow) - 2); //Add a 1 space cell for reserved area (so it doesn't block future path or collapse the rooms)

                DivideInNewSections(room, posCol, posRow, section);

                sections = sections.OrderByDescending(s => s.SurfaceArea).ToList();
                //unite quadrants

                UniteSections();
                room.MapPositionRow = section.MapPositionRow + posRow;
                room.MapPositionCol = section.MapPositionCol + posCol;

                for (int i = room.MapPositionCol; i < (room.MapPositionCol + room.CellCountCol); i++)
                {
                    for (int j = room.MapPositionRow; j < (room.MapPositionRow + room.CellCountRow); j++)
                    {
                        Map[i, j] = CellType.Room;
                    }
                }
                var roomGo = new GameObject("Room");
                var roomScript = roomGo.AddComponent<Room>();
                var boxCollider = roomGo.AddComponent<BoxCollider>();
                roomGo.transform.position = new Vector3(room.MapPositionCol * MapSize, 0, room.MapPositionRow * MapSize);
                boxCollider.size = new Vector3(room.CellCountCol * MapSize + 1, 15f, room.CellCountRow * MapSize + 1);
                boxCollider.center = new Vector3(room.CellCountCol * MapSize / 2 - 1, 6.75f, (room.CellCountRow * MapSize) / 2);
                boxCollider.isTrigger = true;
                roomScript.Area = room;
                rooms.Add(roomScript);
            }
            catch
            {
                break;
            }
        }


    }

    private void UniteSections()
    {
        for (int i = 0; i < sections.Count; i++)
        {
            bool canFuse = true;
            while (canFuse)
            {
                //union top
                canFuse = false;
                for (int j = i + 1; j < sections.Count; j++)
                {
                    //TODO: Fix equality comparer;

                    //top
                    if (sections[i].CoordTopRight == sections[j].CoordBotRight && sections[i].CoordTopLeft == sections[j].CoordBotLeft)
                    {
                        canFuse = true;
                        sections[i].MapPositionRow = sections[j].MapPositionRow;
                        sections[i].CellCountRow += sections[j].CellCountRow;
                        sections.RemoveAt(j);
                        break;
                    }
                    //left
                    if (sections[i].CoordTopLeft == sections[j].CoordTopRight && sections[i].CoordBotLeft == sections[j].CoordBotRight)
                    {
                        canFuse = true;
                        sections[i].MapPositionCol = sections[j].MapPositionCol;
                        sections[i].CellCountCol += sections[j].CellCountCol;
                        sections.RemoveAt(j);
                        break;
                    }
                    //bot
                    if (sections[i].CoordBotLeft == sections[j].CoordTopLeft && sections[i].CoordBotRight == sections[j].CoordTopRight)
                    {
                        canFuse = true;
                        sections[i].CellCountRow += sections[j].CellCountRow;
                        sections.RemoveAt(j);
                        break;
                    }
                    //right
                    if (sections[i].CoordTopRight == sections[j].CoordTopLeft && sections[i].CoordBotRight == sections[j].CoordBotLeft)
                    {
                        canFuse = true;
                        sections[i].CellCountCol += sections[j].CellCountCol;
                        sections.RemoveAt(j);
                        break;
                    }

                }
            }
        }
    }

    private void DivideInNewSections(Area room, int posCol, int posRow, Area section)
    {
        //divide section in 8 quadrants,
        var topLeftSection = new Area()
        {
            MapPositionCol = section.MapPositionCol,
            MapPositionRow = section.MapPositionRow,
            CellCountCol = posCol,
            CellCountRow = posRow,
            Name = "TL"
        };
        var topCenterSection = new Area()
        {
            MapPositionCol = section.MapPositionCol + topLeftSection.CellCountCol,
            MapPositionRow = section.MapPositionRow,
            CellCountCol = room.CellCountCol,
            CellCountRow = posRow,
            Name = "TC"
        };

        var topRightSection = new Area()
        {
            MapPositionCol = section.MapPositionCol + room.CellCountCol + posCol,
            MapPositionRow = section.MapPositionRow,
            CellCountCol = section.CellCountCol - (posCol + room.CellCountCol),
            CellCountRow = posRow,
            Name = "TR"
        };

        var middleLeftSection = new Area()
        {
            MapPositionCol = section.MapPositionCol,
            MapPositionRow = section.MapPositionRow + posRow,
            CellCountCol = posCol,
            CellCountRow = room.CellCountRow,
            Name = "ML"
        };

        var middleRightSection = new Area()
        {
            MapPositionCol = section.MapPositionCol + room.CellCountCol + posCol,
            MapPositionRow = section.MapPositionRow + posRow,
            CellCountCol = section.CellCountCol - (posCol + room.CellCountCol),
            CellCountRow = room.CellCountRow,
            Name = "MR"
        };

        //divide section in 8 quadrants,
        var bottomLeftSection = new Area()
        {
            MapPositionCol = section.MapPositionCol,
            MapPositionRow = section.MapPositionRow + room.CellCountRow + posRow,
            CellCountCol = posCol,
            CellCountRow = section.CellCountRow - (room.CellCountRow + posRow),
            Name = "BL"
        };
        var bottomCenterSection = new Area()
        {
            MapPositionCol = section.MapPositionCol + bottomLeftSection.CellCountCol,
            MapPositionRow = section.MapPositionRow + room.CellCountRow + posRow,
            CellCountCol = room.CellCountCol,
            CellCountRow = section.CellCountRow - (room.CellCountRow + posRow),
            Name = "BC"
        };

        var bottomRightSection = new Area()
        {
            MapPositionCol = section.MapPositionCol + room.CellCountCol + posCol,
            MapPositionRow = section.MapPositionRow + room.CellCountRow + posRow,
            CellCountCol = section.CellCountCol - (posCol + room.CellCountCol),
            CellCountRow = section.CellCountRow - (room.CellCountRow + posRow),
            Name = "BR"
        };
        sections.Remove(section);
        sections.Add(topLeftSection);
        sections.Add(middleLeftSection);
        sections.Add(bottomLeftSection);
        sections.Add(topCenterSection);
        sections.Add(bottomCenterSection);
        sections.Add(topRightSection);
        sections.Add(middleRightSection);
        sections.Add(bottomRightSection);
    }

    private Area SelectAvailableSection(Area room)
    {
        var availableSections = sections.Where(s => s.CellCountCol >= room.CellCountCol && s.CellCountRow >= room.CellCountRow).ToList();
        if (availableSections.Count == 0)
            throw new System.Exception("not enough space for room");

        var section = availableSections[Random.Range(0, availableSections.Count)];
        return section;
    }
}

