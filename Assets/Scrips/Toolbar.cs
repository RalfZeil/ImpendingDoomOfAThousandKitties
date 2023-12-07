using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private RectTransform arrowImage;

    private static Animator toolboxAnimator;

    private bool disabled;

    
    // Start is called before the first frame update
    void Start()
    {
        toolboxAnimator = GetComponent<Animator>();
    }

    //Show toolbar when hitting raycast
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!disabled)
        {
            toolboxAnimator.SetBool("HoverOver", true);
            ChangeArrowRotation(180);
        }
    }

        public void HideToolbox()
    {
        toolboxAnimator.SetBool("HoverOver", false);
        ChangeArrowRotation(0);
        StartCoroutine(waiter());
    }


    //Make the tool bar wait before allowing access again
    IEnumerator waiter()
    {
        disabled = true;
        yield return new WaitForSeconds(0.5f);
        disabled = false;
    }

    //Changes the rotation of the arrow next to the toolbox
    void ChangeArrowRotation(int rotation)
    {
        Vector3 rotationVector = transform.rotation.eulerAngles;
        rotationVector.y = rotation;
        arrowImage.rotation = Quaternion.Euler(rotationVector);
    }

}
