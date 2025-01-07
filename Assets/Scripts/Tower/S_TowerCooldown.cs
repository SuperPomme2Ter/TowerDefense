using System;
using UnityEngine;

public class S_TowerCooldown : MonoBehaviour
{
    internal Func<bool> firing;
    bool stopFiring;
    internal float delay;
    int TurnBeforeSleep = 0;
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            if (TurnBeforeSleep >= 2)
            {
                this.enabled = false;
                TurnBeforeSleep = 0;
            }
            else if (!firing())
            {
                TurnBeforeSleep += 1;
            }
            else 
            {
                TurnBeforeSleep = 0;
            }
            timer = 0;
        }
    }

}
