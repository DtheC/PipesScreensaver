using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PipeDirection", menuName = "Pipe/PipeDirection", order = 1)]
public class PipeDirection : ScriptableObject
{
  [SerializeField]
  public PossibleDirection direction;
  public int x;
  public int y;
  public int z;
  [SerializeField]
  public PipeDirection opposite;

  public Vector3 AsVector3() {
      return new Vector3(x, y, z);
  }
}

public enum PossibleDirection
{
  Unavailable,
  XPos,
  XNeg,
  YPos,
  YNeg,
  ZPos,
  ZNeg
}