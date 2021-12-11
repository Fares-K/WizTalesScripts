using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;

    public LayerMask enemyLayer;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            attack();
        }
    }

    void attack()
    {
        anim.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(100);
        }
        
    }


    private void OnDrawGizmosSelected()
    {
        if(attackPoint==null)
        {
            return;
        }
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
