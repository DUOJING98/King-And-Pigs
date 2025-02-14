using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int healAmount = 1; // �ָ���Ѫ��

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ֻ������ܼ���
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Health(healAmount);
                Destroy(gameObject); // ��Ʒ�����������
            }
        }
    }
}
