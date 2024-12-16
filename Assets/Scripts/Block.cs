using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int HitstoDestroy;
    [SerializeField] int CurrentHits;
    [SerializeField] GameObject PowerUp;
    [SerializeField] int powerUpChance;
    [SerializeField] public int Points;
    [SerializeField] GameObject hitParticlesPrefab;

    // Position of each side of the block
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

        // Get the actual size of the block
        var blockCollider = GetComponent<BoxCollider>();

        // Calculate the exact positions of each side of the block
        BlockLeft = blockCollider.bounds.min.x;
        BlockRight = blockCollider.bounds.max.x;

        BlockTop = blockCollider.bounds.max.y;
        BlockBot = blockCollider.bounds.min.y;
    }

    public void OnHit()
    {
        CurrentHits--;
        if (CurrentHits <= 0)
        {
            if (PowerUp && Random.Range(1, 101) < powerUpChance)
            {
                GameObject powerUP = Instantiate(PowerUp, this.transform.position, PowerUp.transform.rotation);
                GameManager.instance.AddToUpdateList(powerUP.GetComponent<MultiBall>());
            }
            DestroyBlock();
            Destroy(this.gameObject);
        }
        else
        {
            if (hitParticlesPrefab)
            {
                Vector3 center = GetComponent<Renderer>().bounds.center;
                GameObject particles = Instantiate(hitParticlesPrefab, center, Quaternion.identity);
                Destroy(particles, 1f); 
            }
        }
    }

    void DestroyBlock()
    {
        GameManager.instance.BlockDestroyed(this, Points);
    }
}
