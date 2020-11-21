﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPipePrefab : MonoBehaviour
{
  public static DrawPipePrefab Instance;
  public GameObject pipePrefab;
  public GameObject joinPrefab;

  void Awake()
  {
    Instance = this;
  }
  public void DrawPrefab(Vector3 position, Vector3 direction, Color colour, bool drawJoin = false)
  {
    var trans = Instantiate(pipePrefab, position, Quaternion.identity).GetComponent<Transform>();
    trans.LookAt(position + direction);
    trans.GetComponentInChildren<MeshRenderer>().material.color = colour;
    if (drawJoin) {
        MeshRenderer mf = Instantiate(joinPrefab, position, Quaternion.identity).GetComponentInChildren<MeshRenderer>();
        mf.material.color = colour;
    }
  }
}
