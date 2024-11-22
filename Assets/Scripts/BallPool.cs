using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPool : MonoBehaviour
{
    Queue<GameObject> ballsPool;
    public GameObject BallPrefab;
    public int MaxBalls;

    void Awake()
    {
        ballsPool = new Queue<GameObject>();

        for (int i = 0; i < MaxBalls; i++)
        {
            GameObject Ball = Instantiate(BallPrefab);
            Ball.SetActive(false);
            ballsPool.Enqueue(Ball);
        }
    }


    public GameObject GetBall()
    {
        GameObject Ball;
        if (ballsPool.Count > 0)
        {
            Ball = ballsPool.Dequeue();
            Ball.SetActive(true);
        }
        else
        {
            Ball = Instantiate(BallPrefab);
        }
        return Ball;
    }

    public void ReturnToPool(GameObject Ball)
    {
        Ball.SetActive(false);
        ballsPool.Enqueue(Ball);
    }
}
