using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDieselChangeable
{
  public int DieselDrainRate 
    { 
        get;
        set; 
    }

    public void DecreaseDiesel();
    public void IncreaseDiesel();
}

public interface IMainDieselChangable
{
    public void DecreaseDieselByAmount(int amountToDecrease);
    public void IncreaseDieselByAmount(int amountToIncrease);
}


public interface IDieselAmountsMinMaxReadable
{

    public int MaxDiesel
    {
        get;
    }

    public int CurrentDiesel
    {
        get;
        set;
    }

}
