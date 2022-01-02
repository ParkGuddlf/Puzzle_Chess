using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//핀의정보
public enum AtkType
{
    PonA, PonB, PonC, PonD, Lock, Lock2, Bishop, Bishop2, Knight, Queen
}
[CreateAssetMenu(fileName = "Pin.asset", menuName = "Pin/PinObject")]
public class ObjectType : ScriptableObject
{
    public string pinName;
    public int price;
    public Sprite pinSprite;
    public Sprite pinBoardSprite;
    public AtkType atkType;
    public GameObject pinPrefab;
    public Sprite atkArrow;
    public int hp;

}
