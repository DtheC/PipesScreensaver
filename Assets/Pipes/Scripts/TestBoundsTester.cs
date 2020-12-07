using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoundsTester : MonoBehaviour
{
  void Update()
  {
      Debug.LogError(IsInsideMesh(transform.position));
  }

  bool IsInsideMesh(Vector3 point)
  {
    Vector3 direction = new Vector3(0, 1, 0);
    if (Physics.Raycast(point, direction, Mathf.Infinity, LayerMask.GetMask("PipeBounds")) &&
        Physics.Raycast(point, -direction, Mathf.Infinity, LayerMask.GetMask("PipeBounds")))
    {
      return true;
    }
    else return false;
  }
}
