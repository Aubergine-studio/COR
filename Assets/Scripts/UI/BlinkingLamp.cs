using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlinkingLamp : MonoBehaviour
{
    public GameObject light;
    private int index = 0;
    private List<float> blinksTimes = new List<float>();
    private float blinkingInterwal;

    public float blinkingInterwalRangeMin = 0f;
    public float blinkingInterwalRangeMax = 500f;
    public int minCountOfBlinks = 0;
    public int maxCountOfBlinks = 10;
    public float minTimeOfBlink = 0f;
    public float maxTimeOfBlink = 1f;

    // Use this for initialization
    void Start()
    {
        blinkingInterwal = Random.Range(blinkingInterwalRangeMin, blinkingInterwalRangeMax);
    }

    // Update is called once per frame

    void Update()
    {
        if (blinksTimes.Count > 0)
        {
            if (blinksTimes[index] <= 0)
            {
                if ((blinksTimes.Count - 1) == index)
                {
                    blinksTimes.Clear();
                    index = 0;
                }
                else
                {
                    index++;
                } 

                light.SetActive(!light.active);
            }
            else
            {
                blinksTimes[index] -= Time.deltaTime;
            }
        }
        else
        {
            if (blinkingInterwal <= 0)
            {
                blinkingInterwal = Random.Range(blinkingInterwalRangeMin, blinkingInterwalRangeMax);
                for (int i = 0; i < Random.Range(minCountOfBlinks, maxCountOfBlinks); i++)
                {
                    blinksTimes.Add(Random.Range(minTimeOfBlink, maxTimeOfBlink));
                }
            }
            else
            {
                blinkingInterwal -= Time.deltaTime;
            }
        }
    }
}
