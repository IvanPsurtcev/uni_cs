using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccuracyLabel : MonoBehaviour
{
    const string basic = "Accuracy: ";
    public TextMeshProUGUI textObj;
    void Start()
    {
        textObj.text = "";
    }

    public void SetScore(float accuracy)
    {
        textObj.text = basic + ((int)(accuracy * 100)).ToString() + "%";
    }
}
