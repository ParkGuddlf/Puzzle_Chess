using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBoard : MonoBehaviour
{
    public static BattleBoard instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    //보드 Y축
    public List<GameObject> Line = new List<GameObject>();
    //보드 전체인자
    public List<GameObject> board = new List<GameObject>();
    //판위에 총말개수
    public List<GameObject> pinCountInBpard = new List<GameObject>();

    //자신의 말의 위치
    public List<GameObject> myPinList = new List<GameObject>();
    //적의 말위치
    public List<GameObject> enemyPinList = new List<GameObject>();

    //공격 순서
    public List<GameObject> atkList = new List<GameObject>();
    //공격모션
    public List<GameObject> atkMotion = new List<GameObject>();

    public static bool battleEnd;
    public static bool battleStart;

    public GameObject Clear;
    void Start()
    {
        //배열의 라인의 A에서 2개뽑아서 00 01에 넣고
        //배열의 라인의 B에서 2개뽑아서 10 11에 넣고
        for (int i = 0; i < 6; i++)//y축개수
        {
            for (int j = 0; j < 5; j++)//x축개수
            {
                board.Add(Line[i].transform.GetChild(j).gameObject);                
            }
        }
    }
    //배틀이 시작되면 준비 페이지에서 공격가능 리스트를 초기화하고 다시 집어넣는다
    private void Update()
    {
        CountPin();
        if (TurnManager.instance.stateCur == StateCur.battleReady) 
        {
            myPinList.RemoveAll(t => t.gameObject); ;
            enemyPinList.RemoveAll(t => t.gameObject);
            atkList.RemoveAll(t => t.gameObject);
            for (int i = 0; i < board.Count; i++)
            {
                if (board[i].transform.childCount > 0)
                {
                    switch (board[i].transform.GetChild(0).tag)
                    {
                        case "Pin":
                            myPinList.Add(board[i]);
                            atkList.Add(board[i]);
                            break;
                        case "Enemy":
                            enemyPinList.Add(board[i]);
                            atkList.Add(board[i]);
                            break;
                    }
                }
            }
            TurnManager.instance.stateCur = StateCur.battle;
        }
    }

    public void BattelPage()
    {
        StartCoroutine(ATKCo());
    }
    //공격범위표시
    IEnumerator ATKCo()
    {
        battleStart = true;
        //공격범위 표시
        for (int i = 0; i < atkList.Count; i++)
        {
            List<GameObject> atkArry = atkList[i].GetComponent<AtkMode>().atkArray;
            
            foreach (var array in atkArry)
            {
                //이팩트 소환을 하자 폰은 어택 룩비숍은 레이저
                switch (atkList[i].transform.GetChild(0).GetComponent<MouseDrag>().objectType.atkType)
                {
                    case AtkType.PonA://위아래
                        GameObject ponAtk = Instantiate(atkMotion[0]);
                        ponAtk.GetComponent<PinAtk>().pinTag = atkList[i].transform.GetChild(0).tag;
                        ponAtk.transform.position = array.transform.position;
                        yield return new WaitForSeconds(0.3f);
                        //if (array.transform.childCount != 0 && atkList[i].transform.GetChild(0).tag != array.transform.GetChild(0).tag)//현재때리고있는 녀석의 적이면 플레이어만때리게 아니면 그반대
                        //{
                        //    array.transform.GetChild(0).GetComponent<MouseDrag>().hp -= 1;
                        //}
                        Destroy(ponAtk);
                        break;
                    case AtkType.PonB://양옆
                        GameObject ponAtkB = Instantiate(atkMotion[0]);
                        ponAtkB.GetComponent<PinAtk>().pinTag = atkList[i].transform.GetChild(0).tag;
                        ponAtkB.transform.position = array.transform.position;
                        yield return new WaitForSeconds(0.3f);
                        //if (array.transform.childCount != 0 && atkList[i].transform.GetChild(0).tag != array.transform.GetChild(0).tag)//현재때리고있는 녀석의 적이면 플레이어만때리게 아니면 그반대
                        //{
                        //    array.transform.GetChild(0).GetComponent<MouseDrag>().hp -= 1;
                        //}
                        Destroy(ponAtkB);
                        break;
                    case AtkType.PonC:
                        GameObject ponAtkC = Instantiate(atkMotion[0]);
                        ponAtkC.GetComponent<PinAtk>().pinTag = atkList[i].transform.GetChild(0).tag;
                        ponAtkC.transform.position = array.transform.position;
                        yield return new WaitForSeconds(0.3f);
                        //if (array.transform.childCount != 0 && atkList[i].transform.GetChild(0).tag != array.transform.GetChild(0).tag)//현재때리고있는 녀석의 적이면 플레이어만때리게 아니면 그반대
                        //{
                        //    array.transform.GetChild(0).GetComponent<MouseDrag>().hp -= 1;
                        //}
                        Destroy(ponAtkC);
                        
                        break;
                    case AtkType.PonD:
                        GameObject ponAtkD = Instantiate(atkMotion[0]);
                        ponAtkD.GetComponent<PinAtk>().pinTag = atkList[i].transform.GetChild(0).tag;
                        ponAtkD.transform.position = array.transform.position;
                        yield return new WaitForSeconds(0.3f);
                        //if (array.transform.childCount != 0 && atkList[i].transform.GetChild(0).tag != array.transform.GetChild(0).tag)//현재때리고있는 녀석의 적이면 플레이어만때리게 아니면 그반대
                        //{
                        //    array.transform.GetChild(0).GetComponent<MouseDrag>().hp -= 1;
                        //}
                        Destroy(ponAtkD);
                        break;
                    case AtkType.Lock://세로전체
                        GameObject lockAtk = Instantiate(atkMotion[1], Vector3.zero, Quaternion.Euler(0, 0, 90));
                        lockAtk.GetComponent<PinAtk>().pinTag = atkList[i].transform.GetChild(0).tag;
                        lockAtk.transform.position = array.transform.position;
                        yield return new WaitForSeconds(0.3f);
                        //if (array.transform.childCount != 0 && atkList[i].transform.GetChild(0).tag != array.transform.GetChild(0).tag)//현재때리고있는 녀석의 적이면 플레이어만때리게 아니면 그반대
                        //{
                        //    array.transform.GetChild(0).GetComponent<MouseDrag>().hp -= 1;
                        //}
                        Destroy(lockAtk);
                        yield return new WaitForSeconds(0.3f);
                        break;
                    case AtkType.Lock2://세로전체
                        GameObject lock2Atk = Instantiate(atkMotion[1], Vector3.zero, Quaternion.Euler(0, 0, 0));
                        lock2Atk.GetComponent<PinAtk>().pinTag = atkList[i].transform.GetChild(0).tag;
                        lock2Atk.transform.position = array.transform.position;
                        yield return new WaitForSeconds(0.3f);
                        //if (array.transform.childCount != 0 && atkList[i].transform.GetChild(0).tag != array.transform.GetChild(0).tag)//현재때리고있는 녀석의 적이면 플레이어만때리게 아니면 그반대
                        //{
                        //    array.transform.GetChild(0).GetComponent<MouseDrag>().hp -= 1;
                        //}
                        Destroy(lock2Atk);
                        yield return new WaitForSeconds(0.3f);
                        break;
                    case AtkType.Bishop:
                        GameObject BishopAtk = Instantiate(atkMotion[2], Vector3.zero, Quaternion.Euler(0, 0, 45));
                        BishopAtk.GetComponent<PinAtk>().pinTag = atkList[i].transform.GetChild(0).tag;
                        BishopAtk.transform.position = array.transform.position;
                        yield return new WaitForSeconds(0.3f);
                        //if (array.transform.childCount != 0 && atkList[i].transform.GetChild(0).tag != array.transform.GetChild(0).tag)//현재때리고있는 녀석의 적이면 플레이어만때리게 아니면 그반대
                        //{
                        //    array.transform.GetChild(0).GetComponent<MouseDrag>().hp -= 1;
                        //}
                        Destroy(BishopAtk);
                        break;
                    case AtkType.Bishop2:
                        GameObject Bishop2Atk = Instantiate(atkMotion[2], Vector3.zero, Quaternion.Euler(0, 0, -45));
                        Bishop2Atk.GetComponent<PinAtk>().pinTag = atkList[i].transform.GetChild(0).tag;
                        Bishop2Atk.transform.position = array.transform.position;
                        yield return new WaitForSeconds(0.3f);
                        //if (array.transform.childCount != 0 && atkList[i].transform.GetChild(0).tag != array.transform.GetChild(0).tag)//현재때리고있는 녀석의 적이면 플레이어만때리게 아니면 그반대
                        //{
                        //    array.transform.GetChild(0).GetComponent<MouseDrag>().hp -= 1;
                        //}
                        Destroy(Bishop2Atk);
                        break;
                    case AtkType.Queen:
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(0.1f);

            }
            if (enemyPinList.Count == 0)
            {
                Clear.SetActive(true);
                yield return new WaitForSeconds(2.0f);
                Clear.SetActive(false);
                battleEnd = true;
                break;
            }
            else if (i == atkList.Count - 1)
            {
                GameManager.Life -= enemyPinList.Count;
                TurnManager.instance.stateCur = StateCur.main;
            }
        }
        battleStart = false;
    }
    //판위에 말을 pinCountInBpard에 넣어준다
    void CountPin()
    {
        for (int i = 0; i <board.Count; i++)
        {
            if (board[i].transform.childCount ==0)
            {

            }
            else if (!pinCountInBpard.Contains(board[i].transform.GetChild(0).gameObject))
            {
                if(board[i].transform.GetChild(0).gameObject.tag == "Pin")
                    pinCountInBpard.Add(board[i].transform.GetChild(0).gameObject);
            }
        }
        for (int j = 0; j < pinCountInBpard.Count; j++)
        {
            if (pinCountInBpard.Count > 0 && pinCountInBpard[j] == null)
            {
                pinCountInBpard.RemoveAt(j);
            }
        }

    }

}
