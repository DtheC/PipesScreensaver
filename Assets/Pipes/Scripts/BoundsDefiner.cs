using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsDefiner : MonoBehaviour
{
  public Vector3 point1;
  public Vector3 point2;
  void OnDrawGizmosSelected()
  {
    Gizmos.color = new Color(1, 0, 0, 0.5f);
    Vector3 center = point1 + (point2 - point1) / 2;
    //   for (int x = 0; x < cellCount.x; x++) {
    //       for (int y = 0; y < cellCount.y; y++) {
    //           for (int z = 0; z < cellCount.z; z++) {
    // Draw a semitransparent blue cube at the transforms position
    Gizmos.DrawCube(center, point2 - point1);
    //           }
    //       }
    //   }
  }
}
