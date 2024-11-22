using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : IUpdateable
{
    [SerializeField] float _movementSpeed;
    [SerializeField] int _maxLife;
    [SerializeField] int _currentLife;
    bool MatchStarted;
    public static event Action OnStartMatch;
    //public static event Action OnLoseLife;
    BoxCollider boxCollider;

    void Start()
    {
        GameManager.instance.AddToUpdateList(this.GetComponent<Player>());
        _currentLife = _maxLife;
    }

    public override void CustomUpdate()
    {
        Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        transform.position += horizontal * _movementSpeed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5.6f, 4.91f), transform.position.y, transform.position.z);
        if (!MatchStarted && Input.GetKeyDown(KeyCode.Space))
        {
            OnStartMatch?.Invoke();
            MatchStarted = true;
        }
    }

    public int GetLifes()
    {
        return _currentLife;
    }

    public void LoseLife()
    {
        --_currentLife;
        MatchStarted = false;
    }
}
