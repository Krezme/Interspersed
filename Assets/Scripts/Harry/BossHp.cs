using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    public Slider slider;

    public void SetMaxBossHealth(int bosshealth)
    {
        slider.maxValue = bosshealth;
        slider.value = bosshealth;
    }

    public void SetBossHealth(int bosshealth)
    {
        slider.value = bosshealth;
    }
}
