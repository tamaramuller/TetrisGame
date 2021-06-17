using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePoints : MonoBehaviour
{

    public TextMeshProUGUI text;

    int totalPoints = 0;


    public void AddPoints(Elements element)
    {
        totalPoints += element.points;
        text.text = totalPoints.ToString();
    }
}
