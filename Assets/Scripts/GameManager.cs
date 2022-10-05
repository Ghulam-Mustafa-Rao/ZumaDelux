using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

    public float timeTaken;
    public int triesLeft;
    public TextMeshProUGUI timeTakenText;
    public TextMeshProUGUI triesLeftText;
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
        //MoveBalls();
    }
    // Update is called once per frame
    void Update()
    {
        timeTaken += Time.deltaTime;
        triesLeftText.text = "Tries Left : " + triesLeft;
        
        timeTakenText.text = "Time Taken : " + (int)(timeTaken);
        
    }

    IEnumerator SpwanBalls()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(waitTime);
            GameObject ball = Instantiate(ballOnPlanePrefab, Nodes[0].transform.position, Quaternion.identity);
            //set ball colorId and Color
            ball.GetComponent<BallOnPlane>().colorID = Random.Range(0, ballMaterials.Length);
            ball.GetComponent<Renderer>().material = ballMaterials[ball.GetComponent<BallOnPlane>().colorID];
            //add it to list
            ballsInGame.Add(ball);
        }
    }

    public void MoveBalls()
    {
        if (ballsInGame.Count > 3 && state == States.move)
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
