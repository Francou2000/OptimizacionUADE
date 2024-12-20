using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBall : Powerup
{
    private void Start()
    {
        UpdateManager updManager = FindObjectOfType<UpdateManager>();
        updManager.AddUpdateable(this);
    }

    protected override void _powerup()
    {
        GameManager.instance.BigBall(this);
    }
}
