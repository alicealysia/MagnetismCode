using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoldObject : MonoBehaviour {
    public GameObject LeverObject;
    public Sprite UnPressed;
    public Sprite Pressed;
    public bool RequiresPressable;
    private SpriteRenderer SR;
    public int ObjectsInside;
    private void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (RequiresPressable)
        {
            if(other.tag == "CanPress")
                ObjectsInside++;
        }
        else
            ObjectsInside++;
        if (ObjectsInside > 0)
        {
            SR.sprite = Pressed;
            LeverObject.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {

        if (RequiresPressable)
        {
            if (other.tag == "CanPress")
                ObjectsInside--;
        }
        else
            ObjectsInside--;
        if (ObjectsInside <= 0)
        {
            SR.sprite = UnPressed;
            LeverObject.SetActive(true);
        }
        
    }
}
