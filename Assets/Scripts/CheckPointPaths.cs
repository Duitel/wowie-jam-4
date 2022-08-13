using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckPointList
{
    public List<Transform> checkPoints = new List<Transform>();
}

public class CheckPointPaths : MonoBehaviour
{
    public List<CheckPointList> paths = new List<CheckPointList>();

    public List<Transform> GetRandomPath()
    {
        return paths[Random.Range(0, paths.Count)].checkPoints;
    }
}

