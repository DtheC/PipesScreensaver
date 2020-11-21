using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellStateManager : MonoBehaviour
{
  public static CellStateManager Instance;
  public int[,,] visitedCells;
  public Vector3 cellCount;
  static System.Random random = new System.Random();
  void Awake()
  {
    Instance = this;
    Reset();
  }

  public void Reset()
  {
    visitedCells = new int[(int)cellCount.x, (int)cellCount.y, (int)cellCount.z];
    for (int x = 0; x < cellCount.x; x++)
    {
      for (int y = 0; y < cellCount.y; y++)
      {
        for (int z = 0; z < cellCount.z; z++)
        {
          visitedCells[x, y, z] = 0;
        }
      }
    }
  }

  public void MarkAsVisited(Vector3 position)
  {
    visitedCells[(int)position.x, (int)position.y, (int)position.z] = 1;
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

  // void OnDrawGizmosSelected()
  // {
  //     for (int x = 0; x < cellCount.x; x++) {
  //         for (int y = 0; y < cellCount.y; y++) {
  //             for (int z = 0; z < cellCount.z; z++) {
  //                 // Draw a semitransparent blue cube at the transforms position
  //                 Gizmos.color = new Color(1, 0, 0, 0.5f);
  //                 Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
  //             }
  //         }
  //     }
  // }
}
