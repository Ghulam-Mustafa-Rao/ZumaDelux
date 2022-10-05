using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject turret;
    public float yOffset;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionToLookAt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionToLookAt.y = 0;
        
        transform.LookAt(positionToLookAt);

        if (Input.GetMouseButtonDown(0))
        {
            Shootball();
        }
    }

    void Shootball()
    {
        Vector3 screenPosition1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenPosition1.y = yOffset;
        GameObject ball = Instantiate(ballPrefab, new Vector3(transform.position.x, yOffset, transform.position.z), Quaternion.identity);

        ball.GetComponent<Rigidbody>().AddForce(screenPosition1 * 1, ForceMode.Impulse);
    }

    void SelectNextColorBall()
    {

    }
}
