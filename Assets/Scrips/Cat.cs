using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private ParticleSystem fedParticle;

    //Hunger Counts down, 100 is full, 0 is empty stomach
    [SerializeField]
    private int startHunger = 100;

    private int hungerMeter;
    

    // Start is called before the first frame update
    void Start()
    {
        hungerMeter = startHunger;

        spriteRenderer = GetComponent<SpriteRenderer>();
        fedParticle = GetComponentInChildren<ParticleSystem>();
    }

    //Tick Event, gets called every second
    void Tick()
    {
        ChangeHunger();
    }

    //lowers hunger of the cat
    public void ChangeHunger(int amount=-5)
    {
        hungerMeter = hungerMeter + amount;
        if(hungerMeter < 0)
        {
            hungerMeter = 0;
        } else if (hungerMeter > 100)
        {
            hungerMeter = 100;
        }

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


    void OnMouseDown()
    {
        if(Player.currentTool == 1)
        {
            //Fed the Cat
            Debug.Log("Fed the cat");
            ChangeHunger(50);
            fedParticle.Play();
        }
    }
}
