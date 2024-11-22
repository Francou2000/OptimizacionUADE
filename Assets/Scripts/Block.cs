using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int HitstoDestroy;
    [SerializeField] int CurrentHits;
    [SerializeField] GameObject PowerUp;
    [SerializeField] public int Points;


    //Position of each side of the block
    float BlockLeft;
    float BlockRight;
    float BlockTop;
    float BlockBot;

    public float _BlockLeft => BlockLeft;
    public float _BlockRight => BlockRight;
    public float _BlockTop => BlockTop;
    public float _BlockBot => BlockBot;

    void Start()
    {
        CurrentHits = HitstoDestroy;

        //Get the Actual Size of the Block
        var blockCollider = GetComponent<BoxCollider>();

        //Calculate the exact positions of each side of the block
        BlockLeft = transform.position.x - blockCollider.bounds.size.x / 2;
        BlockRight = transform.position.x + blockCollider.bounds.size.x / 2;

        BlockTop = transform.position.y + blockCollider.bounds.size.y / 2;
        BlockBot = transform.position.y - blockCollider.bounds.size.y / 2;
    }

    public void OnHit()
    {
        CurrentHits--;
        if (CurrentHits <= 0)
        {
            if (PowerUp)
            {
                GameObject powerUP = Instantiate(PowerUp, this.transform.position, PowerUp.transform.rotation);
                GameManager.instance.AddToUpdateList(powerUP.GetComponent<MultiBall>());
            }
            DestroyBlock();
            Destroy(this.gameObject);
        }
    }

    void DestroyBlock()
    {
        GameManager.instance.BlockDestroyed(this, Points);

    }

}
