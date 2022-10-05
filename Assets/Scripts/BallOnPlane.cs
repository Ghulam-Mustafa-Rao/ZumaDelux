using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOnPlane : BallsParent
{
    public bool isMoving = false;
    public int nextNodeIndex;
    private void Awake()
    {
        nextNodeIndex = 0;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager.ballsInGame.FindIndex(a => a.Equals(this.gameObject)) == GameManager.gameManager.ballsInGame.Count - 1)
            isMoving = true;
        if (isMoving)
            transform.position = Vector3.MoveTowards(transform.position, GameManager.gameManager.Nodes[nextNodeIndex].transform.position, 1f * Time.deltaTime);
        if (Vector3.Distance(transform.position, GameManager.gameManager.Nodes[nextNodeIndex].transform.position) < 0.01f)
        {
            if (nextNodeIndex < GameManager.gameManager.Nodes.Count - 1)
                nextNodeIndex++;
        }
    }

    private void OnDestroy()
    {
        GameManager.gameManager.ballsInGame.Remove(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndPoint"))
        {
            GameManager.gameManager.gameOver = true;
            Time.timeScale = 0;
        }
    }
}
