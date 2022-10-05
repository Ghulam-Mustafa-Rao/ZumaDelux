using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBalls : BallsParent
{
    bool triggerDone = false;
    List<GameObject> objectsToBeDestroyed;
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

        if (other.CompareTag("BallOnPlane") && !triggerDone)
        {
            //pause game
            Time.timeScale = 0;
            triggerDone = true;

            GameManager.gameManager.state = States.wait;
            //get index of ball with which this ball collides
            int index = GameManager.gameManager.ballsInGame.FindIndex(a => a.Equals(other.gameObject));
            //minus 1 index to add newball before the ball with which this ball collides
            if (index == GameManager.gameManager.ballsInGame.Count - 1
                //To prevent from aray out of bound exception 
                && GameManager.gameManager.ballsInGame.Count > 1)
            {
                index--;
            }

            GameObject CurrentBall = GameManager.gameManager.ballsInGame[index];
            Vector3 positionToBeSpwanedAt = other.transform.position;

            Vector3 nextBallPosition = Vector3.zero;
            Vector3 v = new Vector3(0, 0, 1.1f);
            //Move every ball ahed 1 unit to make place for new ball
            for (int i = 0; i <= index; i++)
            {
                nextBallPosition = GameManager.gameManager.ballsInGame[i].transform.position;
                GameManager.gameManager.ballsInGame[i].transform.position -= v;
            }
            v = new Vector3(0, 0, 0.1f);
            //create new ball , assign its next node to move to  and its colorId as of this ball
            GameObject ball = Instantiate(GameManager.gameManager.ballOnPlanePrefab, nextBallPosition - v, Quaternion.identity);
            ball.GetComponent<BallOnPlane>().nextNodeIndex = CurrentBall.GetComponent<BallOnPlane>().nextNodeIndex;
            GameManager.gameManager.ballsInGame.Insert(index, ball);
            ball.GetComponent<BallOnPlane>().colorID = colorID;
            ball.GetComponent<Renderer>().material = GameManager.gameManager.ballMaterials[colorID];
            GameManager.gameManager.MoveBalls();
            Destroy(this.gameObject);
            //Find matches on left and right side and destory them if more than 2
            FindAndDestroyMatches(ball);
            //resume game
            Time.timeScale = 1;
            GameManager.gameManager.state = States.move;
        }
    }

    void FindAndDestroyMatches(GameObject newball)
    {
        objectsToBeDestroyed = new List<GameObject>();
        objectsToBeDestroyed.Add(newball);
        int index = GameManager.gameManager.ballsInGame.FindIndex(a => a.Equals(newball));
        int lindex = index - 1;
        int rindex = index + 1;

        //check left adjacent side balls if same color then add to list to get destroyed
        while (lindex > 0)
        {
            if (GameManager.gameManager.ballsInGame[lindex].GetComponent<BallOnPlane>().colorID == colorID)
            {
                objectsToBeDestroyed.Add(GameManager.gameManager.ballsInGame[lindex]);
                lindex--;
            }
            else
                break;
        }
        //check right adjacent side balls if same color then add to list to get destroyed
        while (rindex < GameManager.gameManager.ballsInGame.Count - 1)
        {
            if (GameManager.gameManager.ballsInGame[rindex].GetComponent<BallOnPlane>().colorID == colorID)
            {
                objectsToBeDestroyed.Add(GameManager.gameManager.ballsInGame[rindex]);

                rindex++;
            }
            else
                break;
        }

        //if more than 2 adjacent balls of same color found then destroy thoes balls
        if (objectsToBeDestroyed.Count > 2)
        {
            foreach (var item in objectsToBeDestroyed)
            {
                Destroy(item);
            }
        }
    }


}
