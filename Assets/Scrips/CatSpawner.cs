using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    //Spawning of cats
    [SerializeField]
    private GameObject prefab;
    private Vector3 spawnLocation = new Vector3(-11, 0, 0);
    private int counter;

    private int score = 1;

    // Start is called before the first frame update
    void Start()
    {
        TickEvent.OnTick += Tick;
    }

    void Tick()
    {
        SpawnCat();
    }

    void SpawnCat()
    {
        counter++;

        if (counter >= 10)
        {
            counter = 0;

            Instantiate(prefab, spawnLocation, Quaternion.identity);

            score++;

            PlayerPrefs.SetInt("Score", score);
        }
    }

    protected void OnDestroy()
    {
        TickEvent.OnTick -= Tick; //Unsubscribe on event
    }
}
