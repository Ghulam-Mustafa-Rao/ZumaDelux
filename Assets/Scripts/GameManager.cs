using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum States
{
    move,
    wait
}
public class GameManager : MonoBehaviour
{
    public List<GameObject> Nodes;
    public GameObject ballOnPlanePrefab;
    public static GameManager gameManager;
    public float waitTime;
    public List<GameObject> ballsInGame;
    public bool gameOver = false;

    public Material[] ballMaterials;
    public States state = States.move;
    private void Awake()
    {
        if (gameManager == null)
            gameManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpwanBalls());
    }

    private void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        MoveBalls(); 
    }

    IEnumerator SpwanBalls()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(waitTime);
            GameManager.gameManager.ballsInGame.Add(Instantiate(ballOnPlanePrefab, Nodes[0].transform.position, Quaternion.identity));
        }
    }

    void MoveBalls()
    {
        if(ballsInGame.Count>3)
        {
            if (Vector3.Distance(ballsInGame[ballsInGame.Count - 2].transform.position, ballsInGame[ballsInGame.Count - 1].transform.position) < 0.9f)
            {
                ballsInGame[ballsInGame.Count - 2].GetComponent<BallOnPlane>().isMoving = true;
            }
            else
                ballsInGame[ballsInGame.Count - 2].GetComponent<BallOnPlane>().isMoving = false;
            for (int i = ballsInGame.Count - 3; i >= 0; i--)
            {
                if (Vector3.Distance(ballsInGame[i].transform.position, ballsInGame[i + 1].transform.position) < 0.9f
                    && ballsInGame[i + 1].GetComponent<BallOnPlane>().isMoving)
                {
                    ballsInGame[i].GetComponent<BallOnPlane>().isMoving = true;
                }
                else
                    ballsInGame[i].GetComponent<BallOnPlane>().isMoving = false;
            }
        }
    }
}
