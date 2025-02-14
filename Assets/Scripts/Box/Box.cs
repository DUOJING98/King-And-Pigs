using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    [Header("碎片相关")]
    public GameObject[] piecePrefabs; // 箱子碎片预制体数组
    public int pieceCount = 4; // 生成的碎片数量
    public float explosionForce = 5f; // 爆炸力度
    public float pieceLifetime = 2f; // 碎片生存时间

    [Header("掉落物品")]
    public GameObject[] itemPrefabs; // 掉落物品预制体数组
    public Transform dropPosition; // 掉落位置
    public float dropChance = 0.4f; // 掉落概率（70%）

    public int health = 3;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitPoint"))
        {
            anim.SetTrigger("hit");
            health--;
            if(health==0)
                DestroyBox();
        }
    }

    

    public void DestroyBox()
    {
        GeneratePieces(); // 生成碎片
        GenerateDropItem(); // 生成掉落物品
        Destroy(gameObject); // 删除箱子
    }

    private void GeneratePieces()
    {
        if (piecePrefabs.Length == 0) return;

        for (int i = 0; i < pieceCount; i++)
        {
            int index = Random.Range(0, piecePrefabs.Length);
            GameObject piece = Instantiate(piecePrefabs[index], transform.position, Quaternion.identity);
            Rigidbody2D pieceRb = piece.GetComponent<Rigidbody2D>();

            if (pieceRb != null)
            {
                Vector2 randomForce = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1.5f)) * explosionForce;
                pieceRb.AddForce(randomForce, ForceMode2D.Impulse);
                pieceRb.AddTorque(Random.Range(-360f, 360f)); // 添加旋转效果
            }

            Destroy(piece, pieceLifetime); // 一定时间后销毁碎片
        }
    }

    private void GenerateDropItem()
    {
        if (itemPrefabs.Length == 0) return;

        if (Random.value < dropChance) // 根据概率决定是否掉落
        {
            int index = Random.Range(0, itemPrefabs.Length);
            Instantiate(itemPrefabs[index], dropPosition != null ? dropPosition.position : transform.position, Quaternion.identity);
        }
    }
}
