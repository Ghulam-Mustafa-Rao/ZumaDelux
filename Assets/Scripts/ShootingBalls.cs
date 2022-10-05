using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBalls : BallsParent
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }

        if (other.CompareTag("BallOnPlane"))
        {
            Time.timeScale = 0;
            Destroy(this.gameObject);
            int index = GameManager.gameManager.ballsInGame.FindIndex(a => a.Equals(other.gameObject));
            if (index == GameManager.gameManager.ballsInGame.Count - 1)
            {
                index--;
            }
            
            GameObject CurrentBall = GameManager.gameManager.ballsInGame[index];
            Vector3 positionToBeSpwanedAt = other.transform.position;

            Vector3 nextBallPosition=Vector3.zero;
            for (int i = 0; i <= index; i++)
            {
                if (i == 0)
                {
                    float x = GameManager.gameManager.ballsInGame[i].transform.position.x - GameManager.gameManager.ballsInGame[i + 1].transform.position.x;
                    float z = GameManager.gameManager.ballsInGame[i].transform.position.z - GameManager.gameManager.ballsInGame[i + 1].transform.position.z;

                    GameManager.gameManager.ballsInGame[i].transform.position = new Vector3(transform.position.x + x, 0.4f, transform.position.z + z);
                }else
                {
                    GameManager.gameManager.ballsInGame[i].transform.position = nextBallPosition;
                }
                nextBallPosition = GameManager.gameManager.ballsInGame[i].transform.position;
            }
            //}

            GameObject ball = Instantiate(GameManager.gameManager.ballOnPlanePrefab, nextBallPosition, Quaternion.identity);
            ball.GetComponent<BallOnPlane>().nextNodeIndex = CurrentBall.GetComponent<BallOnPlane>().nextNodeIndex;
            GameManager.gameManager.ballsInGame.Insert(index, ball);
            ball.name = "naya";
            Time.timeScale = 1;
        }
    }
}
