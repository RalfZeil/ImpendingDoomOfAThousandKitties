using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Cat : MonoBehaviour
{
    //Hunger Counts down, 100 is full, 0 is empty stomach
    [SerializeField]
    private int startHunger = 100;
    private int hungerMeter;

    private SpriteRenderer spriteRenderer;
    private ParticleSystem fedParticle;

    //Variables for movement
    private float speed = 2f;
    private bool hasArrived = false;
    private Vector2 target;

    //When the object spawn or gets enabled
    void OnEnable()
    {
        //Subscribe to the OnTick for every second call function
        TickEvent.OnTick += Tick;
        hungerMeter = startHunger;
        spriteRenderer = GetComponent<SpriteRenderer>();
        fedParticle = GetComponentInChildren<ParticleSystem>();
    }

    void Tick()
    {
        ChangeHunger();
        rollMove();
    }

    //lowers hunger of the cat
    public void ChangeHunger(int amount=-5)
    {
        hungerMeter = hungerMeter + amount;

        //Clamps the meter
        if(hungerMeter < 0) hungerMeter = 0;
        if(hungerMeter > 100) hungerMeter = 100;   

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
        //Clicked on the cat with food
        if(Player.currentTool == 1)
        {
            hungerMeter = 100;
            fedParticle.Play();
        }
    }

    void OnMouseDrag()
    {
        //Drags aroud the cat
        if(Player.currentTool == 0)
        {
            Vector2 MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(MousePosition);
            transform.position = objPosition;
        }
    }



    //10% every second to move to a different location if the cat arrived at his last destination
    private void rollMove()
    {
        if (hasArrived)
        {
            if (Random.Range(0, 10) == 0)
            {
                move();
            }
        }
    }

    private void move()
    {
        float randX = Random.Range(-7f, 8f);
        float randY = Random.Range(-2.5f, 2f);
        target = new Vector2(randX, randY);

        hasArrived = false;

        //Change the direction of the cat based on the targets position
        if(target.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    //Moving the cat to target
    void Update()
    {
        if (!hasArrived)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, target, step);

            if(transform.position.Equals(target))
            {
                hasArrived = true;
            }
        }
        
    }



}
