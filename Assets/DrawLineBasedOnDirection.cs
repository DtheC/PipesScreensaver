﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineBasedOnDirection : MonoBehaviour
{
    public List<PipeDirection> PipeDirections;
    public Vector3 CurrentPosition;
    public PipeDirection LastDirection;

    Dictionary<PipeDirection, float> PipeChances;
    void Start() {
        CurrentPosition = new Vector3();
        PipeChances = new Dictionary<PipeDirection, float>();
        PipeDirections.ForEach(dir => PipeChances.Add(dir, 1f));
        StartCoroutine(DrawRoutine());
    }

    IEnumerator DrawRoutine() {
        while(true) {
            DrawPipe();
            yield return new WaitForSeconds(1);
        }
    }

    void DrawPipe() {
        ResetPossibilities();
        SetPossibilities();
        PipeDirection nextDir = GetRandomDirection();
        if (nextDir == null) {
          Debug.LogError("reached end of possible moves!");
          return;
        }
        // int index = Random.Range(0, PipeDirections.Count);
        Debug.DrawLine(CurrentPosition, CurrentPosition + nextDir.AsVector3(), Color.red, 10000f);
        CurrentPosition += nextDir.AsVector3();
        LastDirection = nextDir;
        // Set cell as visited in CellManager
        CellStateManager.Instance.MarkAsVisited(CurrentPosition);
    }

  void ResetPossibilities()
  {
      List<PipeDirection> keys = new List<PipeDirection>(PipeChances.Keys);
      keys.ForEach(x => PipeChances[x] = 1f);
  }

  void SetPossibilities() {
      if (LastDirection == null) return;
      if (LastDirection.opposite == null) return;
      PipeChances[LastDirection.opposite] = 0f; // Do not go the direction you came

      // Get blocked cells (previously visited) and set their possibility to 0
      List<PipeDirection> blockedSpaces = CellStateManager.Instance.GetBlockedDirections(CurrentPosition, PipeDirections);
      blockedSpaces.ForEach(x => {
          PipeChances[x] = 0f;
      });
  }

  PipeDirection GetRandomDirection()
  {
    float possibilityCount = 0;
    foreach (KeyValuePair<PipeDirection, float> entry in PipeChances)
    {
      possibilityCount += entry.Value;
    }
    if (possibilityCount == 0) return null;
    float randomNumber = Random.RandomRange(0f, possibilityCount);
    float count = 0;
    foreach (KeyValuePair<PipeDirection, float> entry in PipeChances)
    {
      count += entry.Value;
      if (count >= randomNumber)
      {
        return entry.Key;
      }
    }
    return null;
  }
}
