using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Toolbar : MonoBehaviour, IPointerEnterHandler
{
    private static Animator toolboxAnimator;

    bool disabled;

    
    // Start is called before the first frame update
    void Start()
    {
        toolboxAnimator = GetComponent<Animator>();
    }

    //Show toolbar when hitting raycast
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!disabled) toolboxAnimator.SetBool("HoverOver", true);
    }

    public void HideToolbox()
    {
        toolboxAnimator.SetBool("HoverOver", false);
        StartCoroutine(waiter());
    }


    //Make the tool bar wait before allowing access again
    IEnumerator waiter()
    {
        disabled = true;
        yield return new WaitForSeconds(0.5f);
        disabled = false;
    }

}
