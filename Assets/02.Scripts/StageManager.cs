using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        if (PlayerInfo.instance.stageNum > 0)
            stageNum = PlayerInfo.instance.stageNum;
    }
    public int stageNum = 0;
    public bool enemyArrangeEnd = false;

    [SerializeField] GameObject[] enemyPrefab;
    //스타트턴에 적을 배치 단 배치는 비어있는 공간에 무작위로 배치한다

    private void Start()
    {
        //playerinfo에 스테이지정보가 있으면 거기서부터 없으면 0부터

    }
    private void Update()
    {
        EnemyArrange(stageNum);
        if (TurnManager.instance.stateCur == StateCur.end)
        {
            enemyArrangeEnd = false;
        }
    }

    void EnemyArrange(int _stageNum)
    {
        if (TurnManager.instance.stateCur == StateCur.start)
        {
            _stageNum++;
            stageNum = _stageNum;
            EmptyBorad();
        }
    }
    public int k = 0;
    //보드의 빈공간에 스테이지에 맞는 적군배치
    void EmptyBorad()
    {
        randNum.Clear();
        RanNum();
        k = 0;
        for (int i = 0; i < BattleBoard.instance.board.Count; i++)
        {
            GameObject emptyBoardEnemy = BattleBoard.instance.board[randNum[i]];
            GameObject enemy = Instantiate(enemyPrefab[Random.Range(0, 2)], emptyBoardEnemy.transform);
            BattleBoard.instance.enemyPinList.Add(enemy);
            enemy.GetComponent<MouseDrag>().currParent = emptyBoardEnemy.transform;
            k++;
            if (k == stageNum)
            {
                enemyArrangeEnd = true;
                break;
            }
        }
    }

    //난수발생
    public List<int> randNum = new List<int>();
    int ran = 0;
    bool isSame;
    void RanNum()
    {
        while (randNum.Count != stageNum)
        {
            ran = Random.Range(0, BattleBoard.instance.board.Count);
            isSame = false;
            if (randNum.Count == 0 && BattleBoard.instance.board[ran].transform.childCount == 0)
                randNum.Add(ran);
            else
            {
                for (int j = 0; j < randNum.Count; j++)
                {
                    if (ran == randNum[j])
                        isSame = true;
                    break;
                }
                if (!isSame && BattleBoard.instance.board[ran].transform.childCount == 0)
                    randNum.Add(ran);
            }
        }
    }
}
