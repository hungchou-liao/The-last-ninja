using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class timeScore : MonoBehaviour
{
    public int gemsCollected;
    int totalGems;
    float timer;

    public TextMeshProUGUI time, Score;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        totalGems = GameObject.FindGameObjectsWithTag("Collectible").Length;
        gemsCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // set time text
        timer += Time.deltaTime;

        string seconds = (timer % 60).ToString("0#.00");
        string minutes = Mathf.FloorToInt(timer / 60).ToString("0#");
        time.text = minutes + ":" + seconds;

        Score.text = "Scroll:" + gemsCollected.ToString() + "/" + totalGems.ToString();


    }
}
