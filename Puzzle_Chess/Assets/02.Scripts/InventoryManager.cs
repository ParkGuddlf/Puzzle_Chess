using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //합성
    //메인턴에 말들을 같은 말이 3개면 합체시킨다
    //필드위에 말과 인벤토리에 말을 가지고 와서 같은게 3개면 다음 걸로 합성 

    [SerializeField] GameObject[] allPinList;

    [SerializeField] GameObject pinPrefab;

    [SerializeField] List<ObjectType> spawnPinObjectTypes;
    public bool isSame;

    [SerializeField] GameObject pinUpEffect;
    private void Update()
    {
        if (TurnManager.instance.stateCur == StateCur.main)
        {
            allPinList = GameObject.FindGameObjectsWithTag("Pin");
            RankUp();
        }
    }

    public List<GameObject> ponAList = new List<GameObject>();
    public List<GameObject> ponBList = new List<GameObject>();
    public List<GameObject> ponCList = new List<GameObject>();
    public List<GameObject> lockList = new List<GameObject>();
    public List<GameObject> bishopList = new List<GameObject>();
    int k;
    //합체
    void RankUp()
    {
        List<GameObject> pinList = new List<GameObject>(allPinList);
        
        for (int i = 0; i < pinList.Count; i++)
        {
            
            isSame = false;
            switch (pinList[i].GetComponent<MouseDrag>().objectType.atkType)
            {
                case AtkType.PonA:
                    k = 0;
                    RankUpSys(pinList, ponAList, i, k);
                    break;
                case AtkType.PonB:
                    k = 1;
                    RankUpSys(pinList, ponBList, i, k);
                    break;
                case AtkType.PonC:
                    break;
                case AtkType.Lock:
                    break;
                case AtkType.Bishop:
                    break;
                case AtkType.Knight:
                    break;
                case AtkType.Queen:
                    break;
            }
        }


    }
    void RankUpSys(List<GameObject> asdqwe, List<GameObject> asd , int i, int k)
    {
        for (int j = 0; j < asd.Count; j++)
        {
            if (asdqwe[i] == asd[j])
                isSame = true;
        }
        if (!isSame)
            asd.Add(asdqwe[i]);
        if (asd.Count == 3)
        {
            GameObject pinUp = Instantiate(pinPrefab);
            pinUp.GetComponent<MouseDrag>().currParent = asd[0].transform.parent;
            pinUp.GetComponent<MouseDrag>().objectType = spawnPinObjectTypes[k];
            Destroy(asd[0]);
            Destroy(asd[1]);
            Destroy(asd[2]);
            GameObject upEffect = Instantiate(pinUpEffect, pinUp.transform);
            Destroy(upEffect, 0.5f);
            asd.RemoveRange(0, 3);
        }
    }



}
