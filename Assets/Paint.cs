using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Paint : MonoBehaviour
{
    public Color bgColor;
    public Color[] colors;

    private Texture2D tex;

    void Start()
    {
        tex = GetComponent<Image>().sprite.texture;

        Redraw();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Redraw();

        if (Input.GetKeyDown(KeyCode.Return))
            Save();
    }

    private void Redraw()
    {        
        float pct = 1f;

        // Reset
        for (int i = tex.height - 1; i >= 0; i--)
        {
            for (int j = tex.width - 1; j >= 0; j--)
            {
                tex.SetPixel(j, i, bgColor);
            }
        }



        for (int i = tex.height - 1; i >= 0; i--)
        {
            for (int j = tex.width - 1; j >= 0; j--)
            {
                if (Random.value < pct)
                    tex.SetPixel(j, i, colors[Random.Range(0, colors.Length)]);
            }
            pct -= .025f;
        }

        tex.Apply();
    }

    private void Save()
    {
        byte[] bytes = tex.EncodeToPNG();
        var dirPath = Application.dataPath + "/SavedImages/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        File.WriteAllBytes(dirPath + "Image" + ".png", bytes);

        print("Saved to: " + Application.dataPath + "/SavedImages/");
    }
}
