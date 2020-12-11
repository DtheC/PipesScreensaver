using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPipePrefab : MonoBehaviour
{
  public static DrawPipePrefab Instance;
  public GameObject pipePrefab;
  public GameObject joinPrefab;
  public Transform rootTransform;

  public Action<Vector3> PipeDestroyed;

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
    var pipeTransform = Instantiate(pipePrefab, position, Quaternion.identity).GetComponent<Transform>();
    pipeTransform.LookAt(position + direction);
    var pipeVisuals = pipeTransform.GetComponentInChildren<PipeVisuals>();
    pipeVisuals.Init(colour);
    pipeVisuals.cellPosition = position;
    pipeTransform.parent = rootTransform;
    if (drawJoin)
    {
      Transform joinTransform = Instantiate(joinPrefab, position, Quaternion.identity).GetComponent<Transform>();
      joinTransform.parent = rootTransform;
      pipeVisuals = joinTransform.GetComponentInChildren<PipeVisuals>();
      pipeVisuals.Init(colour);
      pipeVisuals.cellPosition = position;
    }
  }
}
