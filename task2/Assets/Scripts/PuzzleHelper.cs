using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleHelper: MonoBehaviour
{
    public GameObject puzzle;
    public GameObject place;
    public GameObject back; 
    public float widthRectPuzzle;
    public float widthRectPlace;
    public float widthRectBack;
    public float heightRectPuzzle;
    public float heightRectPlace;
    public float heightRectBack;
    public void Awake()
    {
        widthRectPuzzle = puzzle.GetComponent<RectTransform>().rect.width;
        heightRectPuzzle = puzzle.GetComponent<RectTransform>().rect.height;
        widthRectPlace = place.GetComponent<RectTransform>().rect.width;
        heightRectPlace = place.GetComponent<RectTransform>().rect.height;
        widthRectBack = back.GetComponent<RectTransform>().rect.width;
        heightRectBack = back.GetComponent<RectTransform>().rect.height;
    }
    public static Tuple<float, float> uvRectToTransform(float x, float y, float backWidth, float pieceWidth, float backHeight, float pieceHeight)
    {
        float transX = backWidth * x + pieceWidth / 2;
        float transY = backHeight * y + pieceHeight / 2;
        Tuple<float, float> result = new Tuple<float, float>(transX, transY);
        return result;
    }

    public Tuple<float, float> uvRectToTransformPlace(float x, float y)
    {
        return uvRectToTransform(x, y, widthRectBack, widthRectPlace, heightRectBack, heightRectPlace);
    }

    public Tuple<float, float> uvRectToTransformPuzzle(float x, float y)
    {
        return uvRectToTransform(x, y, widthRectBack, widthRectPuzzle, heightRectBack, heightRectPuzzle);
    }

    public float uvXToTransformPuzzle(float x)
    {
        return uvRectToTransform(x, 0, widthRectBack, widthRectPuzzle, heightRectBack, heightRectPuzzle).Item1;
    }

    public float getPosXPlace()
    {
        return place.GetComponent<RawImage>().uvRect.x;
    }

    public float getPosYPlace()
    {
        return place.GetComponent<RawImage>().uvRect.y;
    }

    public float getPosXPuzzle()
    {
        return puzzle.GetComponent<RawImage>().uvRect.x;
    }

    public float getPosYPuzzle()
    {
        return puzzle.GetComponent<RawImage>().uvRect.y;
    }
}
