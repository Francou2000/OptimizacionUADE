using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ButtonToChangeScene;

public class GameManager : MonoBehaviour
{
    BallPool ballPool;
    [SerializeField] List<GameObject> ActiveBalls;
    [SerializeField] List<GameObject> ActiveBlocks;
    public static GameManager instance;
    UpdateManager updateManager;
    [SerializeField] Player player;
    [SerializeField] private HP hpui;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        updateManager = GetComponent<UpdateManager>();
        ballPool = GetComponent<BallPool>();
        ActiveBalls = new List<GameObject>();

        StartCoroutine(SetBalls());
    }

    IEnumerator SetBalls()
    {
        yield return new WaitForEndOfFrame();
        ActiveBalls.Add(ballPool.GetBall());
        ActiveBalls[0].GetComponent<Ball>().Reset(player.gameObject);
        updateManager.AddUpdateable(ActiveBalls[0].GetComponent<Ball>());
        AddToUpdateList(player);

    }

    public void MultiBall(IUpdateable pwrup)
    {
        int currentBalls = ActiveBalls.Count;
        //Debug.Log(currentBalls);
        for (int i = 0; i < currentBalls; i++)
        {
            var cloneI = i;
            for (int b = 0; b < 2; b++)
            {
                GameObject Ball = ballPool.GetBall();
                Ball.transform.position = ActiveBalls[cloneI].transform.position;
                ActiveBalls.Add(Ball);
                Ball ball = Ball.GetComponent<Ball>();
                updateManager.AddUpdateable(ball);
                ball.moving = true;
                ball.StartMovement();
            }
        }
        updateManager.RemoveUpdateable(pwrup);
    }

    public void BigBall(IUpdateable pwrup)
    {
        int currentBalls = ActiveBalls.Count;
        for (int i = 0; i < currentBalls; i++)
        {
            var cloneI = i;
            Ball ball = ActiveBalls[cloneI].GetComponent<Ball>();
            ball.SetBigBall();
        }
        updateManager.RemoveUpdateable(pwrup);
    }

    public void BigPaddle(IUpdateable pwrup)
    {
        player.IncreaseSize();
        updateManager.RemoveUpdateable(pwrup);
    }

    public void AddToUpdateList(IUpdateable updateable)
    {
        updateManager.AddUpdateable(updateable);
    }

    public void LostBall(GameObject Ball)
    {
        ballPool.ReturnToPool(Ball);
        ActiveBalls.Remove(Ball);
        Ball ball = Ball.GetComponent<Ball>();
        updateManager.RemoveUpdateable(ball);

        if (ActiveBalls.Count <= 0)
        {
            int lives = player.GetLifes();
            if (lives > 1)
            {
                player.LoseLife();
                if (player.CurrentLife <= 3) hpui.UpdateHP(player.CurrentLife);
                //Debug.Log("Lost life");
                DestroyPowerUps();
                RestartLevel();
            }
            else
            {
                SceneManager.LoadScene((int)Scenes.LoseScreen);
            }
        }
    }

    void DestroyPowerUps()
    {
        GameObject[] activepowerUP = GameObject.FindGameObjectsWithTag("PowerUp");
        if (activepowerUP != null)
        {
            foreach (var powerup in activepowerUP)
            {
                Destroy(powerup);
            }
        }
    }
    public void BlockDestroyed(Block block, int Points)
    {
        ActiveBlocks.Remove(block.gameObject);
        
        if (ActiveBlocks.Count <= 0)
        {
            updateManager.isplaying = false;
            SceneManager.LoadScene((int)Scenes.WinScreen);
        }
    }

    void RestartLevel()
    {
        GameObject Ball = ballPool.GetBall();
        ActiveBalls.Add(Ball);
        Ball ball = Ball.GetComponent<Ball>();
        ball.Reset(player.gameObject);
        updateManager.AddUpdateable(ball);
    }
}

