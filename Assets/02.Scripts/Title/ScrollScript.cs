using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ScrollScript : ScrollRect
{
    bool forParent;
    ScreenMove sm;
    ScrollRect parentScrollRect;

    protected override void Start()
    {
        sm = GameObject.FindWithTag("ScrollParent").GetComponent<ScreenMove>();
        parentScrollRect = GameObject.FindWithTag("ScrollParent").GetComponent<ScrollRect>();
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        forParent = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y);

        if (forParent)
        {
            sm.OnBeginDrag(eventData);
            parentScrollRect.OnBeginDrag(eventData);
        }
        else base.OnBeginDrag(eventData);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (forParent)
        {
            sm.OnDrag(eventData);
            parentScrollRect.OnDrag(eventData);
        }
        else base.OnDrag(eventData);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (forParent)
        {
            sm.OnEndDrag(eventData);
            parentScrollRect.OnEndDrag(eventData);
        }
        else base.OnEndDrag(eventData);
    }
}
