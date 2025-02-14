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

    public int LoadHealth() //加载血量
    {
        if (!PlayerPrefs.HasKey("PlayerHealth"))
        {
            PlayerPrefs.SetInt("PlayerHealth", 3);//初始血量
        }
        int currentHealth = PlayerPrefs.GetInt("PlayerHealth");


        return currentHealth;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("PlayerHealth", player.health);//保存当前血量
        PlayerPrefs.Save();
    }

    public void IsEnemy(Enemy enemy)
    {
        enemies.Add(enemy);//添加敌人
    }

    public void EnemyDead(Enemy enemy)
    {
        enemies.Remove(enemy);//移除敌人
        if (enemies.Count == 0)
        {
            doorExit.OpenDoor();
            SaveData(); //存储数据
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