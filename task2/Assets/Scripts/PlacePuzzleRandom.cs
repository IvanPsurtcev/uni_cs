using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePuzzleRandom : MonoBehaviour
{
    public LoadImages loaded;
    public PuzzleHelper hlp;

    public void Start()
    {
        float xPos = UnityEngine.Random.Range(
            hlp.widthRectPuzzle / hlp.widthRectBack,
            1 - hlp.widthRectPuzzle / hlp.widthRectBack);
        float yPos = UnityEngine.Random.Range(hlp.heightRectPuzzle / hlp.heightRectBack, 1 - hlp.heightRectPuzzle / hlp.heightRectBack);
        if (hlp.widthRectPuzzle != hlp.widthRectPlace || hlp.heightRectPuzzle != hlp.heightRectPlace)
        {
            throw new System.Exception("Puzzle piece and place piece must be the same size");
        }
        RandPosPlace(xPos, yPos);
        RandPosPuzzle(xPos, yPos);
    }

    void RandPosPlace(float x, float y)
    {
        Rect newPlaceRect = new Rect(x, y, hlp.widthRectPlace / hlp.widthRectBack, hlp.heightRectPlace / hlp.heightRectBack);
        loaded.placeRawImage.uvRect = newPlaceRect;
        Tuple<float, float> transPos = hlp.uvRectToTransformPlace(x, y);
        loaded.placeRawImage.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(transPos.Item1, transPos.Item2);
    } 

    void RandPosPuzzle(float x, float y)
    {
        Rect newPlaceRect = new Rect(x, y, hlp.widthRectPlace / hlp.widthRectBack, hlp.heightRectPlace / hlp.heightRectBack);
        loaded.puzzleRawImage.uvRect = newPlaceRect;
        Tuple<float, float> transPos = hlp.uvRectToTransformPuzzle(0, y);
        loaded.puzzleRawImage.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(transPos.Item1, transPos.Item2);
    }
    
    
}
