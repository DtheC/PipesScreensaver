using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellStateManager : MonoBehaviour
{
  public static CellStateManager Instance;

  List<Vector3> visitedCoordinates;
  public BoundsDefiner boundsDefiner;
  public MeshCollider[] meshCol;

  int[,,] baseCells;
  int[,,] visitedCells;
  Vector3 cellCount;
  static System.Random random = new System.Random();
  void Awake()
  {
    Instance = this;
    Reset();
  }

  public void Reset()
  {
    visitedCoordinates = new List<Vector3>();
    if (baseCells != null) {
      visitedCells = baseCells.Clone() as int[,,];
      return;
    }
    int cellX = (int)Math.Round(Vector3.Distance(boundsDefiner.point1, new Vector3(boundsDefiner.point2.x, boundsDefiner.point1.y, boundsDefiner.point1.z)));
    int cellY = (int)Math.Round(Vector3.Distance(boundsDefiner.point1, new Vector3(boundsDefiner.point1.x, boundsDefiner.point2.y, boundsDefiner.point1.z)));
    int cellZ = (int)Math.Round(Vector3.Distance(boundsDefiner.point1, new Vector3(boundsDefiner.point1.x, boundsDefiner.point1.y, boundsDefiner.point2.z)));
    cellCount = new Vector3(cellX, cellY, cellZ);
    visitedCells = new int[cellX, cellY, cellZ];
    baseCells = new int[cellX, cellY, cellZ];
    Debug.LogError(cellCount);
    for (int x = 0; x < cellCount.x; x++)
    {
      for (int y = 0; y < cellCount.y; y++)
      {
        for (int z = 0; z < cellCount.z; z++)
        {
          visitedCells[x, y, z] = IsInAnyCollider(meshCol, new Vector3(x, y, z)) ? 0 : 1;
        }
      }
    }
    baseCells = visitedCells.Clone() as int[,,];
  }

  bool IsInAnyCollider(MeshCollider[] colliders, Vector3 point)
  {
    bool any = false;
    foreach (var item in colliders)
    {
        bool o = IsInCollider(item, point);
        if (o) any = true;
    }
    return any;
  }

   public bool IsInCollider(MeshCollider other, Vector3 point)
  {
    Vector3 from = (Vector3.up * 5000f);
    Vector3 dir = (point - from).normalized;
    float dist = Vector3.Distance(from, point);
    //fwd
    int hit_count = CastTill(from, point, other);
    //back
    dir = (from - point).normalized;
    hit_count += CastTill(point, point + (dir * dist), other);

    if (hit_count % 2 == 1)
    {
      return (true);
    }
    return (false);
  }

  int CastTill(Vector3 from, Vector3 to, MeshCollider other)
  {
    int counter = 0;
    Vector3 dir = (to - from).normalized;
    float dist = Vector3.Distance(from, to);
    bool Break = false;
    while (!Break)
    {
      Break = true;
      RaycastHit[] hit = Physics.RaycastAll(from, dir, dist);
      for (int tt = 0; tt < hit.Length; tt++)
      {
        if (hit[tt].collider == other)
        {
          counter++;
          from = hit[tt].point + dir.normalized * .001f;
          dist = Vector3.Distance(from, to);
          Break = false;
          break;
        }
      }
    }
    return (counter);
  }

  bool IsInsideMesh(Vector3 point) {
    Vector3 direction = new Vector3(0, 1, 0);
    if(Physics.Raycast(point, direction, Mathf.Infinity) &&
        Physics.Raycast(point, -direction, Mathf.Infinity)) {
            return true;
    }
    else return false;
}

  public void MarkAsVisited(Vector3 position)
  {
    visitedCells[(int)position.x, (int)position.y, (int)position.z] = 1;
  }

  public void MarkasFree(Vector3 position)
  {
    visitedCells[(int)position.x, (int)position.y, (int)position.z] = 0;
  }

  public Vector3 GetFreeCell()
  {
    List<Vector3> freeCells = new List<Vector3>();
    for (int x = 0; x < cellCount.x; x++)
    {
      for (int y = 0; y < cellCount.y; y++)
      {
        for (int z = 0; z < cellCount.z; z++)
        {
          if (visitedCells[x, y, z] == 0) freeCells.Add(new Vector3(x, y, z));
        }
      }
    }
    if (freeCells.Count == 0) return new Vector3();
    return freeCells[random.Next(freeCells.Count)];
  }

  public List<PipeDirection> GetFreeDirections(Vector3 currentPos, List<PipeDirection> directions)
  {
    List<PipeDirection> freeDirections = new List<PipeDirection>();
    directions.ForEach(dir =>
    {
      Vector3 toCheck = currentPos + dir.AsVector3();
      if (toCheck.x > cellCount.x || toCheck.x < 0) return;
      if (toCheck.y > cellCount.y || toCheck.y < 0) return;
      if (toCheck.z > cellCount.z || toCheck.z < 0) return;
      if (visitedCells[(int)toCheck.x, (int)toCheck.y, (int)toCheck.z] != 0) return;
      freeDirections.Add(dir);
    });
    return freeDirections;
  }

  public List<PipeDirection> GetBlockedDirections(Vector3 currentPos, List<PipeDirection> directions)
  {
    List<PipeDirection> blockedDirections = new List<PipeDirection>();
    directions.ForEach(dir =>
    {
      Vector3 toCheck = currentPos + dir.AsVector3();
      if (toCheck.x > cellCount.x - 1 || toCheck.x < 0)
      {
        blockedDirections.Add(dir);
        return;
      }
      if (toCheck.y > cellCount.y - 1 || toCheck.y < 0)
      {
        blockedDirections.Add(dir);
        return;
      }
      if (toCheck.z > cellCount.z - 1 || toCheck.z < 0)
      {

        blockedDirections.Add(dir); return;
      }
      if (visitedCells[(int)toCheck.x, (int)toCheck.y, (int)toCheck.z] == 0) return;
      blockedDirections.Add(dir);
    });
    return blockedDirections;
  }
}
