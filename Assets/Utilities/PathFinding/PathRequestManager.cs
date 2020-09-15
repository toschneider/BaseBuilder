using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    Queue<PathResult> results = new Queue<PathResult>();

    static PathRequestManager instance;
    PathFinding pathFinding;

    void Awake()
    {
        instance = this;
        pathFinding = GetComponent<PathFinding>();
    }

    public static void RequestPath(PathRequest request)
    {
        ThreadStart threadStart = delegate {
            instance.pathFinding.FindPath(request, instance.FinishedProcessingPath);
        };
        threadStart.Invoke();
    }

    public void FinishedProcessingPath(PathResult result)
    {
        lock (results)
        {
            results.Enqueue(result);
        }
    }
}
public class PathResult
{
    public List<Waypoint> path;
    public bool success;
    public Action<List<Waypoint>, bool> callback;

    public PathResult(List<Waypoint> path, bool success, Action<List<Waypoint>, bool> callback)
    {
        this.path = path;
        this.success = success;
        this.callback = callback;
    }

}
public class PathRequest
{
    public Waypoint pathStart;
    public Waypoint pathEnd;
    public Action<List<Waypoint>, bool> callback;

    public PathRequest(Waypoint _start, Waypoint _end, Action<List<Waypoint>, bool> _callback)
    {
        pathStart = _start;
        pathEnd = _end;
        callback = _callback;
    }

}
public class Waypoint {
	public int X { get; set; }
	public int Y { get; set; }
}
public class Path
{

}