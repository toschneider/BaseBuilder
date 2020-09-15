using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class AStar : MonoBehaviour
{
    static List<Node> openList;
    static HashSet<Node> closedList;
    static Node[,] NodeArray;
    //static Vector2 Dest;
    //static Vector2 Start;

    /// <summary>
    /// Calculate Path from start to dest. TileMovementSpeedModifier included in PathCosts
    /// </summary>
    /// <param name="start">Startposition</param>
    /// <param name="dest">Destinationposition</param>
    /// <param name="world">world</param>
    /// <param name="pawn">pawn to move</param>
    /// <returns></returns>
    public static bool CalculatePath(Vector2 start, Vector2 dest, World world, Pawn pawn)
    {

        //Todo remove debug destination
        dest = new Vector2(8, 18);
        //Dest = dest;
        //Start = start;
        openList = new List<Node>();
        closedList = new HashSet<Node>();
        Node StartNode = new Node(start, 0, 0, true, NodeState.Closed, null);
        NodeArray = new Node[world.Width, world.Height];
        //Todo remove Pathfinding

        Debug.Log("Calculate Path from " + start + " to " + dest);
        List<Vector2> path = FindPath(start, dest, world, pawn);
        Debug.Log("Path:");
        foreach (Vector2 pos in path)
		{
            Debug.Log(pos.ToString());
		}
        return true;

        /*openList.Add(StartNode);
        NodeArray[(int)start.x, (int)start.y] = StartNode;

        //Todo remove debug
        int counter = 0;

        while (openList.Count != 0)
        {
            if(counter > 20)
			{
                return false;
			}
            Node currentNode = openList.Min();
            Debug.Log("CalculatePath() iteration: "+counter+" new currentNode: " + currentNode.pos.ToString());
            if(NodeArray[(int)currentNode.pos.x, (int)currentNode.pos.y] != null)
			{
                if(NodeArray[(int)currentNode.pos.x, (int)currentNode.pos.y].f >= currentNode.f)
				{
                    NodeArray[(int)currentNode.pos.x, (int)currentNode.pos.y] = currentNode;
				}


			} else
			{
                NodeArray[(int)currentNode.pos.x, (int)currentNode.pos.y] = currentNode;
            }
            //Todo remove debug arraydisplay
            if (counter%10 == 0)
			{
                StringBuilder stringBuilder;
                for (int j = 0; j < world.Height; j++)
                {
                    stringBuilder = new StringBuilder();
                    stringBuilder.Append("Line " + j + ": ");
                    for (int i = 0; i < world.Width; i++)
                    {
                        stringBuilder.Append(NodeArray[i, j] == null ? "-" : ""+NodeArray[i, j].f);
					}
                    //Debug.Log(stringBuilder.ToString());
				}
			}
            counter++;

            if(currentNode.pos == dest)
			{
                return true;
			}

            closedList.Add(currentNode);

            expandNode(currentNode, dest, world, pawn);
            openList.Remove(currentNode);
        }
        return false;
        */
    }
    public static List<Vector2> FindPath(Vector2 start, Vector2 dest, World world, Pawn pawn)
    {
        //Dest = dest;
        //Start = start;
        Node startNode = new Node(start, 0, GetDistanceBetweenPoints(start, dest), world.getTileAt((int)start.x, (int)start.y).isWalkable, NodeState.Open, null);
        Node endNode = new Node(dest, 0, 0, world.getTileAt((int)dest.x, (int)dest.y).isWalkable, NodeState.Untested, null);
        NodeArray[(int)dest.x, (int)dest.y] = startNode;
        NodeArray[(int)dest.x, (int)dest.y] = endNode;
        List<Vector2> path = new List<Vector2>();
        bool success = Search(startNode, dest);
        if (success)
        {
            Node node = NodeArray[(int)dest.x, (int)dest.y];
            while (node.ParentNode != null)
            {
                path.Add(node.pos);
                node = node.ParentNode;
            }
            path.Reverse();
        }
        Vector2 previusNode = startNode.pos;
		foreach (Vector2 pathNode in path)
		{
            Debug.DrawLine(previusNode, pathNode);
            //world.getTileAt((int)pathNode.x, (int)pathNode.y).tileType = TileType.DeepWater;
            previusNode = pathNode;
		}
        return path;
    }

    private static bool Search(Node currentNode, Vector2 dest)
	{
        currentNode.State = NodeState.Closed;
        List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode, dest);
        nextNodes.Sort((node1, node2) => node1.f.CompareTo(node2.f));
		foreach (Node nextNode in nextNodes)
		{
            if (nextNode.pos == dest)
            {
                return true;
            } else
			{
                Debug.Log("Search at: " + nextNode.pos);
                if (Search(nextNode, dest)) // Note: Recurses back into Search(Node)
			    {
                    return true;
			    }
			}

        }
        return false;
    }
    private static List<Node> GetAdjacentWalkableNodes(Node fromNode, Vector2 dest)
	{
        List<Node> walkableNodes = new List<Node>();
        List<Vector2> nextLocations = GetAdjacentLocations(fromNode.pos);
        World world = WorldController.Instance.World;
		foreach (Vector2 location in nextLocations)
		{
            int x = (int)location.x;
            int y = (int)location.y;
            if (x < 0 || x >= world.Width || y < 0 || y >= world.Height)
			{
                continue;
			}
            Node node;
            if (NodeArray[x, y] == null)
			{
                Tile tile = world.getTileAt(x, y);
                NodeArray[x, y] = new Node(new Vector2(x, y), -1, GetDistanceBetweenPoints(new Vector2(x, y), dest), tile.isWalkable, NodeState.Untested, null);
                node = NodeArray[x, y];
            }
            node = NodeArray[x, y];
            if (!node.isWalkable)
			{
                continue;
            }
            if (node.State == NodeState.Closed)
			{
                continue;
            }
            if (node.State == NodeState.Open)
			{
                float traversalCost = GetDistanceBetweenPoints(node.pos, node.ParentNode.pos);
                float gTemp = fromNode.g + traversalCost;
                if (gTemp < node.g || node.g == -1)
                {
                    node.ParentNode = fromNode;
                    NodeArray[x, y] = node;
                    walkableNodes.Add(node);
                }
            } else
			{
                node.ParentNode = fromNode;
                node.State = NodeState.Open;
                NodeArray[x, y] = node;
                walkableNodes.Add(node);
            }
        }
        return walkableNodes;
    }
    private static List<Vector2> GetAdjacentLocations(Vector2 pos)
	{
        float x = pos.x;
        float y = pos.y;
        //Todo change Wegkosten zu Terrain weggeschwindigkeit
        List<Vector2> tmpVec = new List<Vector2>()
        {
            new Vector2(x-1,y+1),
            new Vector2(x,y+1),
            new Vector2(x+1,y+1),
            new Vector2(x-1,y),
            new Vector2(x+1,y),
            new Vector2(x-1,y-1),
            new Vector2(x,y-1),
            new Vector2(x+1,y-1)
        };
        return tmpVec;
    }
    private static void expandNode(Node currentNode, Vector2 dest, World world, Pawn pawn)
	{
        //Debug.Log("Expand Nodes.");
        int i = 0;
        foreach(Node neighbour in neighbours(currentNode, dest, world, pawn))
		{
            i++;
			//if (neighbour.pos.x < 0 || neighbour.pos.x >= world.Width || neighbour.pos.y < 0 || neighbour.pos.y >= world.Height)
			//{
			//	continue;
			//}
			if (openList.Contains(neighbour))
			{
                continue;
			}
            openList.Add(neighbour);

		}
        //Debug.Log("neighbours.count: " + i);
        //Debug.Log("new OpenList.Count: " + openList.Count);

    }
    private static List<Node> neighbours(Node node, Vector2 dest, World world, Pawn pawn)
    {
        float x = node.pos.x;
        float y = node.pos.y;
        //Todo change Wegkosten zu Terrain weggeschwindigkeit
        List<Vector2> tmpVec = new List<Vector2>()
        {
            new Vector2(x-1,y+1),
            new Vector2(x,y+1),
            new Vector2(x+1,y+1),
            new Vector2(x-1,y),
            new Vector2(x+1,y),
            new Vector2(x-1,y-1),
            new Vector2(x,y-1),
            new Vector2(x+1,y-1)
        };
        //Debug.Log("neighbours().tmpVec.count: " + tmpVec.Count);
        List<Node> nodes = new List<Node>();
        foreach(Vector2 vec in tmpVec)
		{
            if(vec.x < 0 || vec.x >= world.Width || vec.y < 0 || vec.y >= world.Height)
			{
                //Debug.Log("neighbours() vec outside world: " + vec.ToString());
                continue;
			}
            Tile tile = world.getTileAt((int)vec.x, (int)vec.y);
            if(tile == null)
			{
                //Debug.Log("neighbours() tile == null: " + vec.ToString());
                continue;
			}
            bool isWalkable =tile.isWalkable;
            if(!isWalkable)
			{
                //Debug.Log("neighbours() tile not walkable: " + vec.ToString());
                continue;
			}
            if(NodeArray[(int)vec.x, (int)vec.y] != null)
			{
                nodes.Add(NodeArray[(int)vec.x, (int)vec.y]);
			}
			else
			{
                //Debug.Log("neighbours() tile adding: " + vec.ToString());
                float tmpDistToDest = GetDistanceBetweenPoints(vec, dest);

                float tmpDistFromNode = GetDistanceBetweenPoints(vec, node.pos);

                Terrain terrain = world.getTerrainAt((int)vec.x, (int)vec.y);
                Node tmpNode = new Node(vec, node.g + (tmpDistFromNode / terrain.MoveSpeedModifier), tmpDistToDest, true, NodeState.Open, node);
                nodes.Add(tmpNode);
            }

        }

        //Debug.Log("neighbours().nodes.Count: " + nodes.Count);
        return nodes;
	}
    private static float GetDistanceBetweenPoints(Vector2 one, Vector2 two)
	{
        return Mathf.Sqrt(Mathf.Abs(one.x - two.x) + Mathf.Abs(one.y - two.y));
	}


    private class Node:IComparable<Node>
	{
		public Node(Vector2 pos, float g, float h, bool isWalkable, NodeState state, Node parentNode)
		{
			this.pos = pos;
			this.g = g;
			this.h = h;
			this.isWalkable = isWalkable;
			State = state;
			ParentNode = parentNode;
		}

		public Vector2 pos { get; set; }
		public float g { get; set; }
		public float h { get; set; }
		public bool isWalkable { get; set; }
		public NodeState State { get; set; }
		public Node ParentNode { get; set; }

        public float f { get { return this.g + this.h; } }
		public int CompareTo(Node other)
		{
            return f.CompareTo(other.f);
		}

	}
    public enum NodeState { Untested, Open, Closed }
}
