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
            if (dir.y < 0)
            {
                dir.y = -dir.y;
            }

        }

        //BlocksHit
        if (collision.gameObject.GetComponent<Block>())
        {

            BlockCollision(collision.gameObject);
            //collision.gameObject.GetComponent<Block>().OnHit();
        }

        //SideWalls
        if (collision.gameObject.layer == 6)
            dir.x = -dir.x;
        //TopWall
        if (collision.gameObject.layer == 7)
            dir.y = -dir.y;
    }

    void BlockCollision(GameObject Block)
    {
        //Get the Actual Size of the Block
        var blockCollider = Block.GetComponent<Renderer>();

        //Calculate the exact positions of each side of the block
        float BlockLeft = Block.transform.position.x - blockCollider.bounds.size.x / 2;
        float BlockRight = Block.transform.position.x + blockCollider.bounds.size.x / 2;

        float BlockTop = Block.transform.position.y + blockCollider.bounds.size.y / 2;
        float BlockBot = Block.transform.position.y - blockCollider.bounds.size.y / 2;


        if (transform.position.y + BallRadius <= Block.transform.position.y && transform.position.x > BlockLeft && transform.position.x < BlockRight)
        {
            dir.y = -1;

        }

        if (transform.position.y - BallRadius >= Block.transform.position.y && transform.position.x > BlockLeft && transform.position.x < BlockRight)
        {
            dir.y = 1;

        }

        if (transform.position.x >= BlockRight && transform.position.y + BallRadius > BlockBot && transform.position.y - BallRadius < BlockTop)
        {

            dir.x = 1;
        }

        else if (transform.position.x <= BlockLeft && transform.position.y + BallRadius > BlockBot && transform.position.y - BallRadius < BlockTop)
        {

            dir.x = -1;
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
        if (transform.position.y <= -4.90)
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 0);
        Gizmos.DrawSphere(transform.position, radius);
    }
}
