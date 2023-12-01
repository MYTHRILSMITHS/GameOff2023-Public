using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RowUIController : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Score;

    public void SetName(string name)
    {
        Name.text = name;
    }

    public void SetScore(string score)
    {
        Score.text = score;
    }
}
