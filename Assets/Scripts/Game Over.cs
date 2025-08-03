using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBar : MonoBehaviour
{
    [SerializeField] private FruitDropper m_fruitDropper;
    [SerializeField] private GameObject m_gameOverText;


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<FruitItem>() != null)
        {
            m_fruitDropper.GameOver();
            m_gameOverText.SetActive(true);
        }
    }

    public void Back2Title()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}

