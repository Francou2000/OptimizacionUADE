using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : IUpdateable
{
    [SerializeField] float _movementSpeed;
    float angle;
    [SerializeField] float RotationSpeed;
    Vector3 dir;
    Vector3 ResetPos;
    Vector3 Yoffset;
    [SerializeField] float radius;
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

        //Check with player
        if (collision.gameObject.tag == "Player")
        {
            if (dir.y < 0) { dir.y = -dir.y; }

        }

        //BlocksHit
        Block block = collision.gameObject.GetComponent<Block>();
        if (block != null)
        {
            BlockCollision(block);
            block.OnHit();
        }

        //SideWalls
        if (collision.gameObject.layer == 6) { dir.x = -dir.x; }
        //TopWall
        if (collision.gameObject.layer == 7) { dir.y = -dir.y; }

    }

    void BlockCollision(Block Block)
    {
        //Up and down collisions
        if (transform.position.x > Block._BlockLeft && transform.position.x < Block._BlockRight)
        {
            if (transform.position.y + BallRadius <= Block.transform.position.y)
            {
                dir.y = -1;
            }
            else
            {
                dir.y = 1;
            }
        }

        //Left and right collisions
        if (transform.position.y + BallRadius > Block._BlockBot && transform.position.y - BallRadius < Block._BlockTop)
        {
            if (transform.position.x + BallRadius <= Block.transform.position.x)
            {
                dir.x = -1;
            }
            else
            {
                dir.x = 1;
            }
        }

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

        int hit = Physics.OverlapSphereNonAlloc(transform.position, radius, cols);
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

    public void Reset(GameObject Player)
    {
        transform.position = ResetPos;
        dir = Vector3.zero;
        moving = false;
        player = Player;
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
