using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBall : Powerup
{
    protected override void _powerup()
    {
        GameManager.instance.BigBall(this);
    }
}
