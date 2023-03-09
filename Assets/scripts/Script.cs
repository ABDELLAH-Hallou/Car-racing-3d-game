using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    public GameObject redcar;
    public GameObject piece;
    public GameObject greencar;
    public GameObject theroad;
    public GameObject[] theroads;
    public GameObject[] left;
    public GameObject[] right;
    public GameObject[] mycoins;
    public int i = 10;
    public float maxPosition = 1000;
    public float minPosition = 1000f;
    public bool createRoad = true;
    void Start()
    {
        greencar = GameObject.Find("greenCar");
        redcar =GameObject.Find("redCar");
        piece=GameObject.Find("coin");
        mycoins = GameObject.FindGameObjectsWithTag("coin");
        spawn(new Vector3(-5f,1f,-4f));
        spawn(new Vector3(-5f,1f,-14f));
        theroads = GameObject.FindGameObjectsWithTag("road");
            spawnCoin();
    }
    private void FixedUpdate()
    {
        
        if (maxPosition + greencar.transform.position.x < 500 && createRoad)
        {
            
            spawnRoad();
            spawnCoin();
            maxPosition += 1000;
            createRoad = false;
        }
        theroads = GameObject.FindGameObjectsWithTag("road");
        //mycoins = GameObject.FindGameObjectsWithTag("coin");
        right = GameObject.FindGameObjectsWithTag("rightSide");
        left = GameObject.FindGameObjectsWithTag("leftSide");
        
        if (minPosition + greencar.transform.position.x < 0)
        {
            destroyRoad();
            minPosition += 1000;
            createRoad = true;
        }
    }
    private void spawn(Vector3 pos)
    {
        GameObject newObj = Instantiate(redcar,pos,redcar.transform.rotation);
    }
    //private void spawnCoin()
    //{
    //    GameObject newObj = Instantiate(piece, new Vector3(piece.transform.position.x - i, 3, Random.Range(-15, 15)), piece.transform.rotation);
    //    i += 10;
    //}
    private void spawnCoin()
    {
        float gap = 50;
        float coinPosition = Random.Range(-15, 15);
        mycoins = GameObject.FindGameObjectsWithTag("coin");
        theroads = GameObject.FindGameObjectsWithTag("road");
        float line = theroads[theroads.Length - 1].transform.position.x + 500;
        for (int j = 0; j < 10; j++)
        {
            GameObject newObj = Instantiate(mycoins[mycoins.Length - 1], new Vector3( line  - gap, 3, coinPosition), mycoins[mycoins.Length - 1].transform.rotation);
            gap += 10;

        }
    }
    private void spawnRoad()
    {
        
            GameObject r = Instantiate(theroads[0], new Vector3(-1*(maxPosition+500), 0, 0), theroads[0].transform.rotation);
            GameObject l = Instantiate(left[0], new Vector3(-1*(maxPosition+500), left[0].transform.position.y, left[0].transform.position.z), left[0].transform.rotation);
            GameObject ight = Instantiate(right[0], new Vector3(-1*(maxPosition+500), right[0].transform.position.y, right[0].transform.position.z), right[0].transform.rotation);
        

    }
    private void destroyRoad()
    {
        
        Destroy(theroads[0].transform.gameObject);
        Destroy(left[0].transform.gameObject);
        Destroy(right[0].transform.gameObject);
        foreach (GameObject c in mycoins)
        {
            if (minPosition + c.transform.position.x > 0)
            {
                Destroy(c.transform.gameObject);
            }
        }


    }


}
