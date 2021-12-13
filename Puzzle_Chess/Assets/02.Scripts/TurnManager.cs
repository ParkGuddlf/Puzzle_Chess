using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//턴이름및 정보
    public enum StateCur{
        start,main,battleReady,battle,end
    }
public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;

    private void Awake() {
        if(instance == null)
            instance = this;    
    }

    public StateCur stateCur;
}
