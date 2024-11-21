using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBall : IUpdateable
{
    [SerializeField] float _fallSpeed;
    [SerializeField] float _rotationSpeed;


    public override void CustomUpdate()
    {
        transform.position += new Vector3(0, -1, 0) * _fallSpeed * Time.deltaTime;
        transform.Rotate(Vector3.left, _rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("MultiBAll");
            GameManager.instance.MultiBall();
            Destroy(gameObject);
        }
    }


}
