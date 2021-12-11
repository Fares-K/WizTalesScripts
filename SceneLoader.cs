using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    GameObject Player;

    private void Start()
    {
        if(PlayerPrefs.GetInt("Playerpos") == 1)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Player.transform.position = new Vector3(173.5f, -10.33f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {

            PlayerPrefs.SetInt("Playerpos", 1);
            SceneManager.LoadScene(2);
        }
    }
}
