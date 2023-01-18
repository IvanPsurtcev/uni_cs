using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadImages : MonoBehaviour
{
    // Start is called before the first frame update
    public Image backImage;
    public RawImage placeRawImage;
    public RawImage puzzleRawImage;
    string imagesLocation = "Images/Background";
    string resourcesPath = "/Resources";
    string imagePath;
    List<string> files;
    System.Random rnd;
    void Awake()
    {
        rnd = new System.Random();
        string path = Application.dataPath + resourcesPath + "/" + imagesLocation;
        //Debug.Log(path);
        if (!Directory.Exists(path))
        {
            throw new System.Exception("Background images directory was not found");
        }
        string[] filesArray = Directory.GetFiles(path);
        files = filesArray.ToList().Where(x => !x.EndsWith("meta")).ToList();
        if (files.Count == 0)
        {
            throw new System.Exception("No background images were found");
        }
        SetImages();
    }

    public void SetImages()
    {
        string randFile = files[rnd.Next(files.Count)];
        imagePath = imagesLocation + "/" + Path.GetFileName(randFile.Remove(randFile.IndexOf('.')));
        //Debug.Log(imagePath);
        LoadBack();
        LoadPuzzles();
    }

    void LoadBack()
    {
        Sprite loadTexture = Resources.Load<Sprite>(imagePath);
        backImage.sprite = loadTexture;
    }

    void LoadPuzzles()
    {
        Texture2D image = Resources.Load<Texture2D>(imagePath);
        placeRawImage.texture = image;
        puzzleRawImage.texture = image;
    }
}
