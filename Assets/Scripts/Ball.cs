using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : IUpdateable
{
    [SerializeField] float _movementSpeed;
    float angle;
    [SerializeField] float RotationSpeed;
    Vector3 dir;
    Vector3 ResetPos;
    Vector3 Yoffset;
    float BallRadius;
    public bool moving;
    GameObject player;
    Collider[] cols = new Collider[10];

    void Start()
    {
        BallRadius = GetComponent<SphereCollider>().radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
        
        if (!moving)
        {
            dir = new Vector2(0, 0);
        }
        else
        {
            dir = new Vector2(0, 1);
            dir.x = GiveRandom();
        }
        Player.OnStartMatch += StartMovement;
        ResetPos = new Vector2(0, 0);
        Yoffset = new Vector3(0, 0.5f, 0);
    }
    private void CheckCollision(Collider collision)
    {
        var currentPos = transform.position;
        
        //Collision with player
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.position = new Vector3(currentPos.x, collision.bounds.max.y + BallRadius, currentPos.z);
            dir.y *= -1;
            return;
        }

        //Collision with blocks
        Block block = collision.GetComponent<Block>();
        if (block != null)
        {
            BlockCollision(block);
            block.OnHit();
            return;
        }

        //Colision with map
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            transform.position = new Vector3(collision.bounds.max.x + BallRadius, currentPos.y, currentPos.z);
            dir.x *= -1;
            return;
        }
        if (collision.gameObject.CompareTag("RightWall"))
        {
            transform.position = new Vector3(collision.bounds.min.x - BallRadius, currentPos.y, currentPos.z);
            dir.x *= -1;
            return;
        }
        if (collision.gameObject.CompareTag("TopWall"))
        {
            transform.position = new Vector3(currentPos.x, collision.bounds.min.y - BallRadius, currentPos.z);
            dir.y *= -1;
            return;
        }
    }

    void BlockCollision(Block block)
    {
        Vector3 closestPoint = SphereRectangleCollision(block.GetComponent<Collider>(), GetComponent<SphereCollider>());
        float difX = closestPoint.x - transform.position.x;
        float difY = closestPoint.y - transform.position.y;
        transform.position = closestPoint + BallRadius * (transform.position - closestPoint).normalized;
        if(difX > difY)
        {
            dir.x *= -1;
        }
        else
        {
            dir.y *= -1;
        }
    }
    bool CollisionCircleLine(Vector3 startLine, Vector3 finishLine)
    {
        Vector3 dif = transform.position - startLine;
        Vector3 line = finishLine - startLine;
        float magni = line.magnitude;
        Vector3 dir = line.normalized;
        float dot = dif.x * dir.x + dif.y * dir.y;
        if (dot < 0) { dot = 0; }
        if (dot > magni) { dot = magni; }
        Vector3 final = dir * dot;
        Vector3 diference = transform.position - (final + startLine);
        float distance = diference.magnitude;
        return distance >= BallRadius;
    }
    Vector3 SphereRectangleCollision(Collider rec, SphereCollider sphere)
    {
        Vector3 closestPoint = sphere.transform.position;
        if (closestPoint.x < rec.bounds.min.x) closestPoint.x = rec.bounds.min.x;
        if (closestPoint.x > rec.bounds.max.x) closestPoint.x = rec.bounds.max.x;
        if (closestPoint.y < rec.bounds.min.y) closestPoint.y = rec.bounds.min.y;
        if (closestPoint.y > rec.bounds.max.y) closestPoint.y = rec.bounds.max.y;

        return closestPoint;
    }
    public override void CustomUpdate()
    {
        if (!moving)
        {
            FollowPlayer();
        }
        else
        {
            Move(dir);
        }
        
        int hit = Physics.OverlapSphereNonAlloc(transform.position, BallRadius, cols);
        Debug.Log(hit);
        if (hit >= 2)
        {
            for (int i = 0; i < hit; i++)
            {
                CheckCollision(cols[i]);
            }
        }
        if (transform.position.y <= -8)
        {
            dir = Vector3.zero;
            Debug.Log(dir);
            GameManager.instance.LostBall(this.gameObject);
        }
    }

    void Move(Vector3 direction)
    {
        transform.position += direction * _movementSpeed * Time.deltaTime;
    }

    float GiveRandom()
    {
        float i = Random.Range(-1f, 1f);
        if (i == 0)
        {
            i = 0.5f;
        }
        return i;

    }

    public void StartMovement()
    {
        moving = true;
        dir.y = 1;
        dir.x = GiveRandom();
    }

    public void Reset(GameObject player)
    {
        transform.position = ResetPos;
        dir = Vector3.zero;
        moving = false;
        player = player;
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            transform.position = player.transform.position;
            transform.position += Yoffset;
        }
    }

}
