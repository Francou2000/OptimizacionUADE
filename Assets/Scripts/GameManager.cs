using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    BallPool ballPool;
    [SerializeField] List<GameObject> ActiveBalls;
    [SerializeField] List<GameObject> ActiveBlocks;
    public static GameManager instance;
    UpdateManager updateManager;
    [SerializeField] Player player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        updateManager = GetComponent<UpdateManager>();
        ballPool = GetComponent<BallPool>();
        ActiveBalls = new List<GameObject>();

        ActiveBalls.Add(ballPool.GetBall());
        ActiveBalls[0].GetComponent<Ball>().Reset(player.gameObject);
        updateManager.AddUpdateable(ActiveBalls[0].GetComponent<Ball>());
        AddToUpdateList(player);

        //Player.OnLoseLife += RestartLevel;
    }

    public void MultiBall(IUpdateable multiball)
    {
        int currentBalls = ActiveBalls.Count;
        Debug.Log(currentBalls);
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
        updateManager.RemoveUpdateable(multiball);
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
            if (lives > 0)
            {
                player.LoseLife();
                //BallSprite[lives].enabled = false;
                DestroyPowerUps();
                RestartLevel();
            }
            else
            {
                //BallSprite[lives].enabled = false;
                //panelLose.SetActive(true);
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

        GetComponent<AudioSource>().Play();
        ActiveBlocks.Remove(block.gameObject);
        //PlayerScore += Points;
        //Debug.Log(PlayerScore);

        //score.text = PlayerScore.ToString();

        if (ActiveBlocks.Count <= 0)
        {
            //panelWin.SetActive(true);
            updateManager.isplaying = false;
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