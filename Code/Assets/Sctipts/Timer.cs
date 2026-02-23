using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float MaxTime;
    private Image img;
    private float currentTime;
    public bool tick;

    void Start()
    {
        img = GetComponent<Image>();
        currentTime = MaxTime;
    }

    // Update is called once per frame
    void Update()
    {
        tick = false;
        currentTime -= Time.deltaTime;

        if(currentTime <= 0)
        {
            tick = true;
            currentTime = MaxTime;
        }
        img.fillAmount = currentTime / MaxTime;
    }
}
