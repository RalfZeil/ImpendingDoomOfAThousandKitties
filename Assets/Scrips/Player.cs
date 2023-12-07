using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Toolbar toolbar;

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
        SwitchTool(0);
    }

    public static void HideGrabCursor(bool on)
    {
        Cursor.visible = !on;
    }

    public void SwitchTool(int toolID)
    {
        currentTool = toolID;
        Debug.Log("Switched tool to " + currentTool);

        //Hide the toolbar when selecting a tool
        toolbar.HideToolbox();

        //Change the Cursor sprite with custom texture
        switch (toolID)
        {
            case 0:
                Cursor.SetCursor(handOpenTexture, Vector2.zero, CursorMode.Auto);
                break;
            case 1:
                Cursor.SetCursor(foodTrayTexture, Vector2.zero, CursorMode.Auto);
                break;
        }
    }
}
