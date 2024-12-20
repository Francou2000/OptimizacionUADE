using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : IUpdateable
{
    [SerializeField] float _fallSpeed;
    [SerializeField] float _rotationSpeed;

    [SerializeField] float _collisionHigth;
    [SerializeField] float _destroyHeigth;
    Collider[] cols = new Collider[1];
    [SerializeField] LayerMask _playerMask;
    
    public override void CustomUpdate()
    {
        transform.position += new Vector3(0, -1, 0) * _fallSpeed * Time.deltaTime;
        transform.Rotate(Vector3.left, _rotationSpeed * Time.deltaTime, Space.Self);

        if(transform.position.y <= _collisionHigth)
        {
            int hit = Physics.OverlapSphereNonAlloc(transform.position, 1, cols, _playerMask);
            //Debug.Log(hit);
            if (hit >= 1)
            {
                _powerup();
                Destroy(gameObject);
            }

            if(transform.position.y <= _destroyHeigth) { Destroy(gameObject); }
        }
    }

    protected abstract void _powerup();
}
