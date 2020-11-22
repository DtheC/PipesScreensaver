using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeVisuals : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    float alpha = 0f;
    Color col;
    public void Init(Color color) {
        col = color;
        col.a = 0f;
        meshRenderer.material.color = col;
        StartCoroutine(FadeIn());
    }

  private IEnumerator FadeIn()
  {
    while (alpha < 1f) {
        alpha += 0.1f;
        col.a = alpha;
        meshRenderer.material.color = col;
        yield return new WaitForSeconds(0.01f);
    }
  }
}
