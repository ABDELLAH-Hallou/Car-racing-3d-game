using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCar : MonoBehaviour
{
    public float speedSensitivity = 15f;
    public float smoothSpeed = 0.125f;
    public GameObject maincamera;
    public GameObject[] redcars;
    public GameObject[] myCoin;
    public Vector3 offset = new Vector3(15f, 10f, -5f);
    public static int score = 0;
    float force = -50f;
    public bool start = false;
    float x = -50f;
    // Start is called before the first frame update
    void Start()
    {
        maincamera = GameObject.Find("Main Camera");
        //InvokeRepeating("spawnCoin", 0, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float z = Input.GetAxis("Horizontal") * speedSensitivity * Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Vertical") == 0)
        {
            if (x == -1 * force)
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            x = force;
            start = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetAxis("Vertical") !=0)
        {
            if (x == force)
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            x = -1 * force;
        }

        this.GetComponent<Rigidbody>().AddForce(x, 0, z*100);
        this.transform.Rotate(new Vector3(0, z*5, 0));
        if (Input.GetKeyDown(KeyCode.Space) && score > 5)
        {
            force = -100f;
            Invoke("resetNitro", 5f);
        }
        //if (start)
        //{
            redcars = GameObject.FindGameObjectsWithTag("redcar");
            foreach (GameObject redcar in redcars)
            {
                redcar.GetComponent<Rigidbody>().AddForce(-40, 0, 0);
            }
        //}
    }
    public void resetNitro()
    {
        force = -50f;
    }
    private void LateUpdate()
    {
        Vector3 desiredPosition = this.transform.position + offset;
        maincamera.transform.position = desiredPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "leftSide" || collision.gameObject.tag == "rightSide")
        {
            score = (score == 0) ? 0 : score - 1;
            print(score);
        }

        if (collision.gameObject.tag == "redCar")
        {
            score = (score == 0) ? 0 : score - 1;
            print(score);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "coin")
        {
            score++;
            print(score);
            Destroy(other.gameObject);
        }
    }
    //private void spawnCoin()
    //{
    //    float gap = 50;
    //    float coinPosition = Random.Range(-15, 15);
    //    myCoin = GameObject.FindGameObjectsWithTag("coin");
    //    for (int j = 0; j < 10; j++)
    //    {

    //        GameObject newObj = Instantiate(myCoin[myCoin.Length - 1], new Vector3(this.transform.position.x - gap, 3, coinPosition), myCoin[myCoin.Length - 1].transform.rotation);
    //        gap += 10;

    //    }
    //}
}
