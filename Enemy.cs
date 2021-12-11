using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    

    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if(PlayerPrefs.GetInt("EnemyState") == 1)
        {
            Destroy(gameObject);
            
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <=0)
        {
            Die();
            PlayerPrefs.SetInt("EnemyState", 1);
        }
    }

    void Die()
    {
        anim.SetTrigger("Dead");

        GetComponent<Collider2D>().enabled = false;
        
        this.enabled = false;

    }



    
}
