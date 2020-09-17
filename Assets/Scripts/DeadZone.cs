using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof (TilemapCollider2D))]
public class DeadZone : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("쥬금");
        
        Player player = collider.transform.GetComponent<Player>();

        if (player != null)
        {
            player.OnDead();
        }
    }
}
