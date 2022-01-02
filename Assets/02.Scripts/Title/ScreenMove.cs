using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScreenMove : MonoBehaviour,IBeginDragHandler, IDragHandler,IEndDragHandler
{
    public Scrollbar scrollbar;
    public Transform contentTr;
    public Slider tabslider;
    public RectTransform[] rectTransforms;

    //스크린 메뉴 개수
    const int SIZE = 2;
    float[] pos = new float[SIZE];
    float dis , targetPos, curPos;
    bool isDrag;
    int targetindex;

    void Start()
    {
        dis = 1f / (SIZE - 1);
        for (int i = 0; i < SIZE; i++)
        {
            pos[i] = dis * i;
        }
    }

    void Update()
    {
        tabslider.value = scrollbar.value;

        if (!isDrag)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);

            for (int i = 0; i < SIZE; i++)
            {
                rectTransforms[i].sizeDelta = new Vector2(i == targetindex ? 360 : 360, rectTransforms[i].sizeDelta.y);
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        curPos = SetPos();
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        targetPos = SetPos();
        print(curPos + "/" + targetPos + "/" + targetindex);
        if(curPos == targetPos)
        {
            //왼쪽으로
            if(eventData.delta.x >18 && curPos - dis >= 0)
            {
                --targetindex;
                targetPos = curPos - dis;
            }
            //오른쪽으로
            else if (eventData.delta.x < -18 && curPos + dis <= 1.01f)
            {
                ++targetindex;
                targetPos = curPos + dis;
            }
        }
        for (int i = 0; i < SIZE; i++)
        {
            if (contentTr.GetChild(i).GetComponent<ScrollScript>() && curPos != pos[i] && targetPos == pos[i])
                contentTr.GetChild(i).GetChild(1).GetComponent<Scrollbar>().value = 1;
        }
    }
    float SetPos()
    {
        for (int i = 0; i < SIZE; i++)
        {
            if (scrollbar.value < pos[i] + dis * 0.5f && scrollbar.value > pos[i] - dis * 0.5f)
            {
                targetindex = i;
                return pos[i];
            }
        }
        return 0;
    }

    public void TabClick(int n)
    {
        targetindex = n;
        targetPos = pos[n];
    }
}
