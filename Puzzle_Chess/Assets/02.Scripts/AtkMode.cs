using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkMode : MonoBehaviour
{
    BattleBoard battleBoard;

    public int curX;
    public int curY;

    public List<GameObject> atkArray = new List<GameObject>();

    [SerializeField] GameObject RazerSpawnPos;
    private void Start()
    {
        battleBoard = transform.parent.GetComponentInParent<BattleBoard>();
        curY = 1;
        for (int i = 0; i < battleBoard.board.Count; i++)
        {
            if (battleBoard.board[i] == gameObject)
            {
                RazerSpawnPos = battleBoard.board[i];
            }
        }
    }

    void Update()
    {
        if (transform.childCount == 0)
        {
            if (atkArray.Count > 0)
                atkArray.RemoveAt(0);
            return;
        }
        else if (atkArray.Count == 0)
        {
            //공격방식결정
            switch (transform.GetComponentInChildren<MouseDrag>().objectType.atkType)
            {
                //위아래
                case AtkType.PonA:
                    //Y축찾기
                    for (int i = 0; i < battleBoard.Line.Count; i++)
                    {
                        if (battleBoard.Line[i] == transform.parent.gameObject)
                            curY = i;
                    }
                    switch (curY)
                    {
                        case 0:
                            atkArray.Add(battleBoard.Line[curY + 1].transform.GetChild(transform.GetSiblingIndex()).gameObject);
                            break;
                        case 5://최대값
                            atkArray.Add(battleBoard.Line[curY - 1].transform.GetChild(transform.GetSiblingIndex()).gameObject);
                            break;
                        default:
                            atkArray.Add(battleBoard.Line[curY - 1].transform.GetChild(transform.GetSiblingIndex()).gameObject);
                            atkArray.Add(battleBoard.Line[curY + 1].transform.GetChild(transform.GetSiblingIndex()).gameObject);
                            break;
                    }
                    break;
                //양옆
                case AtkType.PonB:
                    for (int i = 0; i < battleBoard.Line.Count; i++)
                    {
                        if (battleBoard.Line[i] == transform.parent.gameObject)
                        {
                            curY = i;
                        }
                    }
                    for (int j = 0; j < battleBoard.Line[curY].transform.childCount; j++)
                    {
                        if (battleBoard.Line[curY].transform.GetChild(j).gameObject == transform.gameObject)
                        {
                            curX = j;
                        }
                    }
                    switch (curX)
                    {
                        case 0:
                            atkArray.Add(battleBoard.Line[curY].transform.GetChild(curX + 1).gameObject);
                            break;
                        case 4://최대값
                            atkArray.Add(battleBoard.Line[curY].transform.GetChild(curX - 1).gameObject);
                            break;
                        default:
                            atkArray.Add(battleBoard.Line[curY].transform.GetChild(curX + 1).gameObject);
                            atkArray.Add(battleBoard.Line[curY].transform.GetChild(curX - 1).gameObject);
                            break;
                    }
                    break;
                case AtkType.Lock:
                    //가운데에서 소환
                    atkArray.Add(RazerSpawnPos);
                    break;
                case AtkType.Bishop:
                    atkArray.Add(RazerSpawnPos);
                    break;
                case AtkType.Knight:
                    atkArray.Add(RazerSpawnPos);
                    break;
            }
        }
    }

}