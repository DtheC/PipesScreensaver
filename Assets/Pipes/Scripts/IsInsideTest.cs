using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInsideTest : MonoBehaviour
{
    public MeshCollider meshCol;
  public Material mat;
  void Update()
  {
    if (IsInside())
    {
      mat.color = new Color(1f, 0, 0);
    }
    else
    {
      mat.color = new Color(0, 1f, 0);
    }
  }

  bool IsInside()
  {
      return IsInCollider(meshCol, transform.position);
    // int layerMask = 1 << 8;
    // RaycastHit[] hit = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), Mathf.Infinity, layerMask);
    // // https://blender.stackexchange.com/questions/31693/how-to-find-if-a-point-is-inside-a-mesh
    // // Does the ray intersect any objects excluding the player layer
    // Debug.LogError(hit.Length);
    // if (hit.Length % 2 != 0)
    // {
    //   Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit[0].distance, Color.yellow);
    //   return true;
    // }
    // else
    // {
    //   Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
    //   return false;
    // }
    // return false;
    // // return false;
  }

  public bool IsInCollider(MeshCollider other, Vector3 point)
  {
    Vector3 from = (Vector3.up * 5000f);
    Vector3 dir = (point - from).normalized;
    float dist = Vector3.Distance(from, point);
    //fwd      
    int hit_count = Cast_Till(from, point, other);
    //back
    dir = (from - point).normalized;
    hit_count += Cast_Till(point, point + (dir * dist), other);

    if (hit_count % 2 == 1)
    {
      return (true);
    }
    return (false);
  }

  int Cast_Till(Vector3 from, Vector3 to, MeshCollider other)
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
}
