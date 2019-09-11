using UnityEngine;

public class AStarNode
{
    public Vector2 Position { get; set; }
    public AStarNode PreviousNode { get; set; }
    public float Cost { get; set; }

    public float f { get; set; }
    public float g { get; set; }
    public float h { get; set; }
    //public int DistanceTraveled { get; set; }
    //public int DistanceToGoal { get; set; }
    //public int TotalNodeValue { get { return DistanceToGoal + DistanceTraveled; } }
    public AStarNode(float x, float y, float cost)
    {
        Position = new Vector2(x, y);
        Cost = cost;
    }
}