using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMaker : MonoBehaviour
{
    public Vector3 scale;
    public static int[,,] grid;

    int lastX = 0;
    int lastY = 0;
    int lastZ = 0;

    // List<Direction> allDirections;

    void Start() {
        grid = new int[5,5,5];
        for (int x = 0; x < 5; x++) {
            for (int y = 0; y < 5; y++) {
                for (int z = 0; z < 5; z++) {
                    grid[x,y,z] = 0;
                }
            }
        }
        StartCoroutine(DrawRoutine());
    }

    IEnumerator DrawRoutine() {
        while(true) {
            DrawPipe();
            yield return new WaitForSeconds(1);
        }
    }

    void DrawPipe() {
        int nextX = lastX;
        int nextY = lastY;
        int nextZ = lastZ;
        int direction = Random.Range(0, 1f) < .5f ? -1 : 1;
        float vector = Random.Range(0f, 1f);
        if (vector < .3f) { // X
            nextX += direction;
        } else if (vector < .6f) {
            nextY += direction;
        } else {
            nextZ += direction;
        }
        // grid[nextX, nextY, nextZ] = 1;
        Debug.LogError(direction);
        Vector3 last = new Vector3(lastX, lastY, lastZ);
        Vector3 next = new Vector3(nextX, nextY, nextZ);
        Debug.LogError(last);
        Debug.LogError(next);
        Debug.DrawLine(last, next, Color.red, 10000f);
        lastX = nextX;
        lastY = nextY;
        lastZ = nextZ;
    }

}
