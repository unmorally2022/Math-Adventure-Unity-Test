using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    Hero hero;
    [SerializeField]
    Enemy[] enemies;

    float TimerCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        hero.setToLive();
        hero.enemies = enemies;
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].hero = hero;
            enemies[i].setToLive();
        }
    }

    private void FixedUpdate()
    {
        TimerCounter += Time.deltaTime;
        if (TimerCounter >= 5)
        {
            //check if enemy is deactive then activate it again
            //this one is to see if the hero damage is increased
            for (int i = 0; i < enemies.Length; i++)
            {
                if (!enemies[i].gameObject.activeSelf)
                {
                    enemies[i].hero = hero;
                    enemies[i].setToLive();
                    enemies[i].gameObject.SetActive(true);
                }
            }

            TimerCounter = 0;
        }
    }
}
