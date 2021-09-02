using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class Health : MonoBehaviour
{
    
    public int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject LifeUI;
    public static bool GameIsPaused = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI(health);
       
    }

    void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;

            }

        }

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.tag == "Enemy")
        {
            health--;
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        LifeUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }
    void Pause()
    {
        LifeUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }


}