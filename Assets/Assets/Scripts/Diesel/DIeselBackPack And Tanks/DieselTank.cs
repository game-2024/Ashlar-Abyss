using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieselTank : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField][Range(0,50)] private int CurrentDiesel = 50;
    [SerializeField][Range(0,50)] private int MaxDiesel = 50;

    public int TankCurrentDiesel
    {
        get { return CurrentDiesel; }
    }

    public int TankMaxDiesel
    {
        get { return MaxDiesel; }
    }



    void Start()
    {
        
    }

}
