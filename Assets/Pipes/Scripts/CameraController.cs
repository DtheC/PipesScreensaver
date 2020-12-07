using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public static CameraController Instance;
  public bool rotateCamera;
  void Awake()
  {
    Instance = this;
  }
  public void Reset()
  {
    if (rotateCamera) transform.Rotate(0, Random.Range(-50f, 50f), 0);
  }
}
