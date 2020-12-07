using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CaptureFrames : MonoBehaviour
{
    // Take a shot immediately
    double count;
    void Start()
    {
        StartCoroutine(UploadPNG());
    }

    IEnumerator UploadPNG()
    {
        while(true) {
            // We should only read the screen buffer after rendering is complete
            yield return new WaitForEndOfFrame();
            count++;
            string countString = count.ToString();
            while (countString.Length < 10) countString = "0" + countString;
            string filename = "screen" + countString;

            // Create a texture the size of the screen, RGB24 format
            int width = Screen.width;
            int height = Screen.height;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);

            // Read screen contents into the texture
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();

            // Encode texture into PNG
            byte[] bytes = tex.EncodeToPNG();
            Destroy(tex);
            Debug.LogError(Application.dataPath + "/../screenshots/SavedScreen.png");
            // For testing purposes, also write to a file in the project folder
            File.WriteAllBytes(Application.dataPath + "/../screenshots/" + filename + ".png", bytes);
            yield return null;
        }
    }
}
