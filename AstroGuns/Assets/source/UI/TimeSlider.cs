using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    public WeaponSpawner Spawner;
    public Slider ProgressSlider;

    void Update()
    {
        ProgressSlider.value = Spawner.CurrentProgress;
    }
}
