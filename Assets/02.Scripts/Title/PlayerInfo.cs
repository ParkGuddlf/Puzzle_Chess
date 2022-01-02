using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    private static int dia;
    private static int money;
    public static int Money { get => money; set => money = value; }
    public static int Dia { get => dia; set => dia = value; }

    //0번부터 1렙기물이 들어간다 이거는 아이템샵에서 변경하게한다 이 오브젝트는 정보만 이동한다
    public List<ObjectType> pinobjectTypes = new List<ObjectType>();

    //저장된게임정보 현재레벨 단계 골드 기물 경험치
    public int stageNum;
    public int level;
    public int gold;
    public int exp;
    public int life;

    public int listLength;
    public List<string> pinAttackType;
    public List<string> pinCurrentParent;

    //시스템 저장
    public float sound;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
        Load();
        sound = PlayerPrefs.GetFloat("sound");
        //게임 스킨저장
    }

    public void SaveINGameMoney()
    {
        //pinobjectTypes 정보 저장후 각 상점요소에 뿌리기
        //게임재화 저장
        PlayerPrefs.SetInt("DIA", dia);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.Save();
    }
    private void Load()
    {
        dia = PlayerPrefs.GetInt("DIA");
        money = PlayerPrefs.GetInt("Money");
    }

    public void GameSystemInfo()
    {
        PlayerPrefs.SetFloat("sound", sound);
    }

    public void GameManegerSave()
    {
        PlayerPrefs.SetInt("stageNum", stageNum);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.SetInt("exp", exp);
        PlayerPrefs.SetInt("life", life);
    }
    public void SavePinInfo(int i)
    {

        listLength = i + 1;
        PlayerPrefs.SetString("pinAttackType" + i, pinAttackType[i]);
        PlayerPrefs.SetString("pinCurrentParent" + i, pinCurrentParent[i]);
        PlayerPrefs.SetInt("listLength", listLength);
        PlayerPrefs.Save();
    }

    //버튼으로 만들어서 버튼을누르면 실행되고 불러온다
    //중요정보저장 세이브파일 불러올때 현재레벨 단계 골드 기물 경험치 엔드페이지에 저장 
    //기물은 이름으로 저장 CurrentParent와함께 리스트에 저장 불러오면서 리스트에서 게임로드시에 처음기물을 소환하고 그이후 기존작업실행
    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("listLength") > 0)
        {
            stageNum = PlayerPrefs.GetInt("stageNum");
            level = PlayerPrefs.GetInt("level");
            gold = PlayerPrefs.GetInt("gold");
            exp = PlayerPrefs.GetInt("exp");
            life = PlayerPrefs.GetInt("life");
            listLength = PlayerPrefs.GetInt("listLength");
            if (listLength != pinAttackType.Count)
            {
                for (int i = 0; i < listLength; i++)
                {
                    pinAttackType.Add(PlayerPrefs.GetString("pinAttackType" + i));
                    pinCurrentParent.Add(PlayerPrefs.GetString("pinCurrentParent" + i));
                }
            }
        }
    }
    public void EndGame()
    {
        PlayerPrefs.DeleteKey("stageNum");
        PlayerPrefs.DeleteKey("level");
        PlayerPrefs.DeleteKey("gold");
        PlayerPrefs.DeleteKey("exp");
        PlayerPrefs.DeleteKey("life");
        for (int i = 0; i < listLength; i++)
        {
            PlayerPrefs.DeleteKey("pinAttackType" + i);
            PlayerPrefs.DeleteKey("pinCurrentParent" + i);
        }
        PlayerPrefs.DeleteKey("listLength");
        stageNum=0;
        level=0;
        gold=0;
        exp=0;
        life=0;
        listLength=0;
        pinAttackType.Clear();
        pinCurrentParent.Clear();
    }

}
