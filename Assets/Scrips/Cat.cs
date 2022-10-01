using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    //Hunger Counts down, 100 is full, 0 is empty stomach
    [SerializeField]
    private int startHunger = 100;

    private int hungerMeter;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        hungerMeter = startHunger;

        spriteRenderer = GetComponent<SpriteRenderer>();
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
