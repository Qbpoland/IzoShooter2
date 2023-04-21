using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    int hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()

    {
        var seesPlayer = false;
        var hearsPlayer = false;
        var playerVector = player.transform.position - transform.position;

        Debug.DrawRay(transform.position, playerVector, Color.yellow);
        Physics.Raycast(transform.position, playerVector, out var hit);

        if (hit.collider.gameObject.CompareTag("Player"))
        {
            seesPlayer = true;
        }

        Collider[] nerby = Physics.OverlapSphere(transform.position, 5f);
        foreach(Collider collider in nerby)
        {
            if (collider.transform.CompareTag("Player"))
            {
                //g³os gracza s³uch 
                hearsPlayer = true;
            }
          
        }
        if (seesPlayer || hearsPlayer)
        {
            agent.destination = player.transform.position;
        }



    
        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Bullet"))
    //    {
    //        Destroy(collision.gameObject);
    //        hp--;
    //        if(hp <= 0)
    //        {
    //            transform.Translate(Vector3.up);
    //            transform.Rotate(Vector3.right * -90);
    //            GetComponent<BoxCollider>().enabled = false;
    //            Destroy(transform.gameObject, 10);
    //        }
    //    }
    //}
    public void ReciveDamage(int ammount)
    {
        hp -= ammount;
            if (hp <= 0)
            Die();
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
