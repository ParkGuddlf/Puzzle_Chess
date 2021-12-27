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
    //세이브용 참거짓
    public static bool isSave;

    //로드게임 페이지생성 allpinlist에 playerinfo에 있는 리스트의 정보를 집어넣고 소환한다 핀을 부모도 리스트에있으니 불러온다
    //그이후 기본정보 로드후 스타트페이지로 간다
    private void Awake()
    {
        for (int i = 0; i < PlayerInfo.instance.pinAttackType.Count; i++)
        {
            GameObject loadPin = Instantiate(pinPrefab);
            Transform loadPinParent = GameObject.Find(PlayerInfo.instance.pinCurrentParent[i]).transform;
            switch (PlayerInfo.instance.pinAttackType[i])
            {
                case "PonA":
                    loadPin.GetComponent<MouseDrag>().objectType = PlayerInfo.instance.pinobjectTypes[0];
                    loadPin.GetComponent<MouseDrag>().currParent = loadPinParent;
                    break;
                case "PonB":
                    loadPin.GetComponent<MouseDrag>().objectType = PlayerInfo.instance.pinobjectTypes[1];
                    loadPin.GetComponent<MouseDrag>().currParent = loadPinParent;
                    break;                
                case "Lock":
                    loadPin.GetComponent<MouseDrag>().objectType = PlayerInfo.instance.pinobjectTypes[2];
                    loadPin.GetComponent<MouseDrag>().currParent = loadPinParent;
                    break;
                case "Bishop":
                    loadPin.GetComponent<MouseDrag>().objectType = PlayerInfo.instance.pinobjectTypes[3];
                    loadPin.GetComponent<MouseDrag>().currParent = loadPinParent;
                    break;
                case "Knight":
                    loadPin.GetComponent<MouseDrag>().objectType = PlayerInfo.instance.pinobjectTypes[4];
                    loadPin.GetComponent<MouseDrag>().currParent = loadPinParent;
                    break;
                case "Queen":
                    loadPin.GetComponent<MouseDrag>().objectType = PlayerInfo.instance.pinobjectTypes[5];
                    loadPin.GetComponent<MouseDrag>().currParent = loadPinParent;
                    break;
            }
        }
    }

    [SerializeField] GameObject pinUpEffect;
    private void Update()
    {        
        if (TurnManager.instance.stateCur == StateCur.main)
            isSave = false;
        if (TurnManager.instance.stateCur == StateCur.main)
        {
            allPinList = GameObject.FindGameObjectsWithTag("Pin");
            RankUp();
        }
        else if (TurnManager.instance.stateCur == StateCur.end && !isSave)
        {
            PlayerInfo.instance.pinAttackType.Clear();
            PlayerInfo.instance.pinCurrentParent.Clear();
            for (int i = 0; i < allPinList.Length; i++)
            {
                PlayerInfo.instance.pinAttackType.Add(allPinList[i].GetComponent<MouseDrag>().objectType.atkType.ToString());
                PlayerInfo.instance.pinCurrentParent.Add(allPinList[i].GetComponent<MouseDrag>().currParent.name);
                PlayerInfo.instance.SavePinInfo(i);
            }
            
            isSave = true;
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
    void RankUpSys(List<GameObject> allpin, List<GameObject> mixpin, int i, int k)
    {
        for (int j = 0; j < mixpin.Count; j++)
        {
            if (allpin[i] == mixpin[j])
                isSame = true;
        }
        if (!isSame)
            mixpin.Add(allpin[i]);
        if (mixpin.Count == 3)
        {
            GameObject pinUp = Instantiate(pinPrefab);
            pinUp.GetComponent<MouseDrag>().currParent = mixpin[0].transform.parent;
            pinUp.GetComponent<MouseDrag>().objectType = spawnPinObjectTypes[k];
            Destroy(mixpin[0]);
            Destroy(mixpin[1]);
            Destroy(mixpin[2]);
            GameObject upEffect = Instantiate(pinUpEffect, pinUp.transform);
            Destroy(upEffect, 0.5f);
            mixpin.RemoveRange(0, 3);
        }
    }
}
