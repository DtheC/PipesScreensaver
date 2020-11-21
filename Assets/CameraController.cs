using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public static CameraController Instance;
  void Awake()
  {
    Instance = this;
  }
  public void Reset()
  {
    transform.Rotate(0, Random.Range(-50f, 50f), 0);
  }
}
