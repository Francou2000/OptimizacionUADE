using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerPaddle : Powerup
{
    private void Start()
    {
        UpdateManager updManager = FindObjectOfType<UpdateManager>();
        updManager.AddUpdateable(this);
    }
    
    protected override void _powerup()
    {
        GameManager.instance.BigPaddle(this);
    }
}
