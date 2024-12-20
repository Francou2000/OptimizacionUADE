using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBall : Powerup
{
    protected override void _powerup()
    {
        //Debug.Log("MultiBAll");
        GameManager.instance.MultiBall(this);
    }
}
