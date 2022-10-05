using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject turret;
    public float yOffset;
    public int colorId;

    // Start is called before the first frame update
    void Start()
    {
        SelectNextColorBall();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionToLookAt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionToLookAt.y = 0;

        turret.GetComponent<Renderer>().material = GameManager.gameManager.ballMaterials[colorId];
        transform.LookAt(positionToLookAt);

        if (Input.GetMouseButtonDown(0))
        {
            Shootball();
        }
    }

    void Shootball()
    {
        GameManager.gameManager.triesLeft--;
        Vector3 screenPosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenPosition1.y = yOffset;
        GameObject ball = Instantiate(ballPrefab, new Vector3(transform.position.x, yOffset, transform.position.z), Quaternion.identity);
        ball.GetComponent<ShootingBalls>().colorID = colorId;
        ball.GetComponent<Renderer>().material = GameManager.gameManager.ballMaterials[colorId];
        ball.GetComponent<Rigidbody>().AddForce(screenPosition1 * ball.GetComponent<ShootingBalls>().speed, ForceMode.Impulse);
        SelectNextColorBall();
    }

    void SelectNextColorBall()
    {
        colorId = Random.Range(0, GameManager.gameManager.ballMaterials.Length);
    }
}
