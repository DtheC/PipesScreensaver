using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterInput : MonoBehaviour
{
  public CharacterControls controller;
  void FixedUpdate()
  {
    var keyboard = Keyboard.current;
    if (keyboard == null)
      return; // No gamepad connected.

    if (keyboard.wKey.IsPressed(1f))
    {
      controller.Move(Direction.Forward);
    }
    if (keyboard.sKey.IsPressed(1f))
    {
      controller.Move(Direction.Back);
    }
    if (keyboard.aKey.IsPressed(1f))
    {
      controller.Move(Direction.Left);
    }
    if (keyboard.dKey.IsPressed(1f))
    {
      controller.Move(Direction.Right);
    }
  }
}
