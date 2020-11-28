using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
  Forward,
  Right,
  Left,
  Back
};

public class CharacterControls : MonoBehaviour
{
  bool moving;
  public void Move(Direction movement)
  {
    switch (movement)
    {
      case Direction.Forward:
        transform.position = transform.position + transform.forward;
        break;
      case Direction.Back:
        transform.position = transform.position - transform.forward;
        break;

      case Direction.Right:
        transform.position = transform.position + transform.right;
        break;

      case Direction.Left:
        transform.position = transform.position - transform.right;
        break;
    }
  }
}
