﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    [SerializeField]
    private Light sun;

    [SerializeField]
    private float secondsInFullDay = 120f;

    [Range(0, 1), SerializeField]
    private float currentTimeOfDay = 0;

    private float timeMultiplier = 1f;

    private float sunInitialIntensity;

    private void Update()
    {
        if (GameController.instance.isGameStarted)
        {
            updateSun();

            currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

            if (currentTimeOfDay >= 1)
            {
                currentTimeOfDay = 0;
            }
        }
    }

    public void findSun()
    {
        if (sun == null)
        {
            sun = GameObject.Find("Sun").GetComponent<Light>();
            sunInitialIntensity = sun.intensity;
        }
    }

    private void updateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);

        float intensityMultiplier = 1;

        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1/0.02f));
        }else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
