using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
    public PuzzleHelper hlp;
    public LoadImages loader;
    public PlacePuzzleRandom randomizer;
    private float position = 0.0f;
    public UnityEvent<float> gameFinished;
    public const float backlash = 0.05f;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            float accuracy = CheckPosition();
            if (accuracy > 0)
            {
                gameFinished.Invoke(accuracy);
            }
        }
    }
    private float CheckPosition()
    {
        float distance = Math.Abs(hlp.getPosXPlace() - position);
        //Debug.Log(distance);
        if (distance < backlash) 
        {
            return (backlash - distance) / backlash;
        }
        return 0.0f;
    }

    public void SetPosition(float value)
    {
        position = value;
        hlp.puzzle.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                            hlp.uvXToTransformPuzzle(position),
                            hlp.puzzle.GetComponent<RectTransform>().anchoredPosition.y);
    }

    public void Restart()
    {
        loader.SetImages();
        randomizer.Start();
    }
}
