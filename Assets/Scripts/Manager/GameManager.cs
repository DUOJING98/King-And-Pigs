using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PlayerController player;
    private Door doorExit;

    public List<Enemy> enemies = new List<Enemy>();

    public bool GameOver;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public void Update()
    {
        if (player != null)
        {
            GameOver = player.isDead;
        }

        UIManager.Instance.GameOverUI(GameOver);
    }

    public int LoadHealth() //����Ѫ��
    {
        if (!PlayerPrefs.HasKey("PlayerHealth"))
        {
            PlayerPrefs.SetInt("PlayerHealth", 3);//��ʼѪ��
        }
        int currentHealth = PlayerPrefs.GetInt("PlayerHealth");


        return currentHealth;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("PlayerHealth", player.health);//���浱ǰѪ��
        PlayerPrefs.Save();
    }

    public void IsEnemy(Enemy enemy)
    {
        enemies.Add(enemy);//��ӵ���
    }

    public void EnemyDead(Enemy enemy)
    {
        enemies.Remove(enemy);//�Ƴ�����
        if (enemies.Count == 0)
        {
            doorExit.OpenDoor();
            SaveData(); //�洢����
        }
    }

    public void IsPlayer(PlayerController controller)
    {
        player = controller;
    }

    public void isDoor(Door d)
    {
        doorExit = d;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonQuit()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        Application.Quit();
    }
}