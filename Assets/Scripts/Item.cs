using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int healAmount = 1; // 恢复的血量

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 只有玩家能捡起
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Health(healAmount);
                Destroy(gameObject); // 物品被捡起后销毁
            }
        }
    }
}
