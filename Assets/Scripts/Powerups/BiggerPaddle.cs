using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerPaddle : Powerup
{
    protected override void _powerup()
    {
        GameManager.instance.BigPaddle(this);
    }
}
