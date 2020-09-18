using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopPopup : MonoBehaviour
{
    [SerializeField] Text txtCoin;

    Player player;


    void OnEnable()
    {
        player = FindObjectOfType<Player>();

        ShowCoin(player.coin);
        player.UpdateCoinAction += ShowCoin;
    }

    void OnDisable()
    {
        player.UpdateCoinAction -= ShowCoin;
    }

    void ShowCoin(int coin)
    {
        txtCoin.text = coin.ToString();
    }
    
}
