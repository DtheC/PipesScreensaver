using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PipeColourManager : MonoBehaviour
{
    public static PipeColourManager Instance;
    public Color[] colours;
    List<Color> nextColors;
    static System.Random random;

    void Awake() {
        Instance = this;
        random = new System.Random();
        Reset();
    }

    public void Reset() {
        nextColors = colours.OfType<Color>().ToList();
    }

    public Color GetRandomColour() {
        return NextCol(); // colours[Random.Range(0, colours.Length)];
    }

    Color NextCol() {
        if (nextColors.Count == 0) Reset();
        int randomNumber = random.Next(0, nextColors.Count);
        //MessageBox.Show(Convert.ToString(randomNumber));
        Color c = nextColors[randomNumber];
        //iconButton.ForeColor = iconButton.BackColor;
        nextColors.RemoveAt(randomNumber);
        return c;
    }
}
