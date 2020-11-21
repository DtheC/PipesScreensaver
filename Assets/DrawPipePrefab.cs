using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPipePrefab : MonoBehaviour
{
  public static DrawPipePrefab Instance;
  public GameObject pipePrefab;
  public GameObject joinPrefab;
  public Transform rootTransform;

  void Awake()
  {
    Instance = this;
  }

  public void Reset()
  {
    foreach (Transform child in rootTransform)
    {
      GameObject.Destroy(child.gameObject);
    }
  }
  public void DrawPrefab(Vector3 position, Vector3 direction, Color colour, bool drawJoin = false)
  {
    var trans = Instantiate(pipePrefab, position, Quaternion.identity).GetComponent<Transform>();
    trans.LookAt(position + direction);
    trans.GetComponentInChildren<MeshRenderer>().material.color = colour;
    trans.parent = rootTransform;
    if (drawJoin)
    {
      Transform t = Instantiate(joinPrefab, position, Quaternion.identity).GetComponent<Transform>();
      t.parent = rootTransform;
      MeshRenderer mf = t.GetComponentInChildren<MeshRenderer>();
      mf.material.color = colour;
    }
  }
}
