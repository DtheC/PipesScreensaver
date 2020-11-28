using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSceneMonitor : MonoBehaviour
{
    public static ResetSceneMonitor Instance;
    public int ResetNumber;
    int resetCount = 0;

    void Awake() {
        Instance = this;
    }

    public void AddReset() {
        resetCount++;
        if (resetCount > ResetNumber) Reset();
    }

    void Reset() {
        resetCount = 0;
        CameraController.Instance.Reset();
        CellStateManager.Instance.Reset();
        DrawPipePrefab.Instance.Reset();
    }
}
