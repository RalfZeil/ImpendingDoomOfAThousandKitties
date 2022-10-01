using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    //Hunger Counts down, 100 is full, 0 is empty stomach
    [SerializeField]
    private int startHunger = 100;

    private int hungerMeter;

    // Start is called before the first frame update
    void Start()
    {
        hungerMeter = startHunger;
    }

    //lowers hunger of the cat
    public void LowerHunger(int amount=5)
    {
        hungerMeter = hungerMeter - amount;
        Debug.Log("Meow " + hungerMeter);
    }
}
