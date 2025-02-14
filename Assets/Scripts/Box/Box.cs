using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    [Header("��Ƭ���")]
    public GameObject[] piecePrefabs; // ������ƬԤ��������
    public int pieceCount = 4; // ���ɵ���Ƭ����
    public float explosionForce = 5f; // ��ը����
    public float pieceLifetime = 2f; // ��Ƭ����ʱ��

    [Header("������Ʒ")]
    public GameObject[] itemPrefabs; // ������ƷԤ��������
    public Transform dropPosition; // ����λ��
    public float dropChance = 0.4f; // ������ʣ�70%��

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
        GeneratePieces(); // ������Ƭ
        GenerateDropItem(); // ���ɵ�����Ʒ
        Destroy(gameObject); // ɾ������
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
                pieceRb.AddTorque(Random.Range(-360f, 360f)); // �����תЧ��
            }

            Destroy(piece, pieceLifetime); // һ��ʱ���������Ƭ
        }
    }

    private void GenerateDropItem()
    {
        if (itemPrefabs.Length == 0) return;

        if (Random.value < dropChance) // ���ݸ��ʾ����Ƿ����
        {
            int index = Random.Range(0, itemPrefabs.Length);
            Instantiate(itemPrefabs[index], dropPosition != null ? dropPosition.position : transform.position, Quaternion.identity);
        }
    }
}
