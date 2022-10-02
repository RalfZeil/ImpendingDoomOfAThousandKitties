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

    private float losePercentage = 35f;

    //References to components
    private SpriteRenderer spriteRenderer;
    private ParticleSystem fedParticle;
    private Animator animator;

    //Variables for movement
    private float speed = 2f;
    private bool hasArrived = false;
    private Vector2 target;

    //Lose condition
    private static LevelLoader levelLoader;
    private static List<Cat> cats = new List<Cat>();
    private static int maxCats;
    private static int angryCats;
    private bool angry;

    //Audio
    private AudioSource audioSource;
    [SerializeField] private AudioClip CatCry;
    [SerializeField] private AudioClip GrabMeow;
    [SerializeField] private AudioClip LongMeow1;
    [SerializeField] private AudioClip LongMeow2;

    //When the object spawn or gets enabled
    void OnEnable()
    {
        //Subscribe to the OnTick for every second call function
        TickEvent.OnTick += Tick;
        hungerMeter = startHunger;

        //setting all components
        spriteRenderer = GetComponent<SpriteRenderer>();
        fedParticle = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();  

        cats.Add(this);
        maxCats++;

        move();
    }

    void Tick()
    {
        ChangeHunger();
        rollMove();
        
    }

    //Check the amount of angry cats, if its more than 50% then lose
    private void Update()
    {
        angryCats = 0;

        foreach(Cat cat in cats)
        {
            if (cat.angry) angryCats++;
        }

        Debug.Log(maxCats + " " +  angryCats);

        Debug.Log(((float)angryCats / (float)maxCats) * 100f);

        if (maxCats > 0)
        {
            if (((float)angryCats / (float)maxCats) * 100f > losePercentage)
            {
                Debug.Log("Switching");
                LevelLoader.LoadLevelStatic("GameOver");
            }
        }
    }
    

    //lowers hunger of the cat
    public void ChangeHunger(int amount=-5)
    {
        hungerMeter = hungerMeter + amount;

        //Clamps the meter
        if(hungerMeter < 0) hungerMeter = 0;
        if(hungerMeter > 100) hungerMeter = 100;  
        
        //Change Hunger in animator
        animator.SetInteger("hungerMeter", hungerMeter);

        //Check the hunger and do someting according to the hunger
        if (hungerMeter < 20)
        {
            angry = true;
        }
        else 
        {
            angry = false;
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
        if (Player.currentTool == 0)
        {
            //spriteRenderer.sortingOrder = 2;
            Player.HideGrabCursor(true);
            animator.SetBool("pickedUp", true);
            Vector2 MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(MousePosition);
            transform.position = objPosition;
        }
    }

    //Dropping the cat
    void OnMouseUp()
    {
        //spriteRenderer.sortingOrder = 1;
        animator.SetBool("pickedUp", false);
        Player.HideGrabCursor(false);
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
        animator.SetBool("walking", true);

        //Change the direction of the cat based on the targets position
        if(target.x > transform.position.x)
        {
            Vector3 rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = 180;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        else
        {
            Vector3 rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = 0;
            transform.rotation = Quaternion.Euler(rotationVector);
        }
    }

    //Moving the cat to target
    void FixedUpdate()
    {
        if (!hasArrived)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, target, step);

            if(transform.position.Equals(target))
            {
                hasArrived = true;
                animator.SetBool("walking", false);
            }
        }

        MakeSound();
    }

    void MakeSound()
    {
        if (hungerMeter < 50) audioSource.clip = CatCry;

        if (Random.Range(0, 500) == 0)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    audioSource.clip = LongMeow1;
                    break;
                case 1:
                    audioSource.clip = LongMeow2;
                    break;
            }

            audioSource.Play();
        }
    }

}
