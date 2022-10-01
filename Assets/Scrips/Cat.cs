using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Cat : MonoBehaviour
{
    //Hunger Counts down, 100 is full, 0 is empty stomach
    [SerializeField]
    private int startHunger = 100;

    private int hungerMeter;
    private SpriteRenderer spriteRenderer;

    //When the object spawn or gets enabled
    void OnEnable()
    {
        //Subscribe to the OnTick for every second call function
        TickEvent.OnTick += Tick;
        hungerMeter = startHunger;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Tick()
    {
        LowerHunger();
        Debug.Log("Tick");
    }

    //lowers hunger of the cat
    public void LowerHunger(int amount=5)
    {
        hungerMeter = hungerMeter - amount;
        Debug.Log("Meow " + hungerMeter);

        //Check the hunger and do someting according to the hunger
        if (hungerMeter < 20)
        {
            spriteRenderer.color = new Color(0.8f, 0.2f, 0.2f, 1f);
        }
        else if (hungerMeter < 50)
        {
            spriteRenderer.color = new Color(0.8f, 0.8f, 0.2f, 1f);
        }
        else
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }

}
