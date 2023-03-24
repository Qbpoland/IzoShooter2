using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour

{
    
    private int maxZombies = 3;
    public GameObject ZombiePrefab;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       var zombies = GameObject.FindGameObjectsWithTag("Enemy");
        if (zombies.Count() < maxZombies)
        {
            SpawnZombie();
        }
       
    }
    void SpawnZombie()
    {
        Vector3 spawnPosition = Random.insideUnitSphere * Random.Range(10, 20);
        spawnPosition.y = 1;
        if (Physics.CheckSphere(spawnPosition, 0.9f))
        {
            spawnPosition = Random.insideUnitSphere * Random.Range(10, 20);
            spawnPosition.y = 0;
        }
        GameObject newZombie = Instantiate(ZombiePrefab, spawnPosition, Quaternion.identity);
        
    }

}
