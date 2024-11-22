using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int HitstoDestroy;
    [SerializeField] int CurrentHits;
    [SerializeField] GameObject PowerUp;
    [SerializeField] public int Points;


    void Start()
    {
        CurrentHits = HitstoDestroy;
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
