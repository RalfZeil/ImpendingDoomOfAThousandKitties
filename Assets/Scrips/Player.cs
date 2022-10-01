using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Signifies the current tool the player is holding:
    // 0 == Hand
    // 1 == Food
    public static int currentTool;

    //Texture for the cursor
    public Texture2D handOpenTexture;
    public Texture2D handClosedTexture;
    public Texture2D foodTrayTexture;


    // Start is called before the first frame update
    void Start()
    {
        currentTool = 0; 
    }

    public void SwitchTool(int toolID)
    {
        currentTool = toolID;
        Debug.Log("Switched tool to " + currentTool);
    }
}
