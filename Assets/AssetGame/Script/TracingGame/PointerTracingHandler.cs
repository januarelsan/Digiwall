using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class PointerTracingHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    
    
    //Declare Delegate
    public delegate void OnTouchDown(Vector2 pos);

    //Declare Delegate
    public delegate void OnTouchDrag(Vector2 pos);

    //Declare Event
    public static event OnTouchDown onDown;
    public static event OnTouchDrag onDrag;

    public void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Vector2 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        onDrag(pos);

        

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Vector2 pos = Camera.main.ScreenToWorldPoint(eventData.position);
        onDown(pos);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

}
