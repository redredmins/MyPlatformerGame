using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Image[] imgHps;
    [SerializeField] Sprite[] hpIcons; // 0: none | 1: have

    Player player;


    void Start()
    {
        player = FindObjectOfType<Player>();

        player.UpdateHpAction += UpdateHpUI;
    }

    void UpdateHpUI(int hp)
    {
        for (int i = 0; i < imgHps.Length; i++)
        {
            if (i < hp) imgHps[i].sprite = hpIcons[1];
            else imgHps[i].sprite = hpIcons[0];
        }
    }
}
