using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader2 : MonoBehaviour
{
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("Playerpos", 0);
            PlayerPrefs.SetInt("EnemyState", 0);
            SceneManager.LoadScene(0);

        }
    }
}
