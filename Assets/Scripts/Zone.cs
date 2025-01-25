using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{

    public int zone;

    void Awake()
    {
        zone = 0;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Modify this such that if goals are zero then it goes to next nearest object
        if (gameObject.name == "zone1")
        {
            GameObject.Find("zone1").GetComponent<Zone>().zone = 1;
            GameObject.Find("zone2").GetComponent<Zone>().zone = 1;
            GameObject.Find("zone3").GetComponent<Zone>().zone = 1;
            GameObject.Find("zone4").GetComponent<Zone>().zone = 1;
            GameObject.Find("zone5").GetComponent<Zone>().zone = 1;
            GameObject.Find("zone6").GetComponent<Zone>().zone = 1;

            //Debug.Log("Zone #: " + GameObject.Find("zone6").GetComponent<Zone>().zone);
        }
        else if (gameObject.name == "zone2")
        {
            GameObject.Find("zone1").GetComponent<Zone>().zone = 2;
            GameObject.Find("zone2").GetComponent<Zone>().zone = 2;
            GameObject.Find("zone3").GetComponent<Zone>().zone = 2;
            GameObject.Find("zone4").GetComponent<Zone>().zone = 2;
            GameObject.Find("zone5").GetComponent<Zone>().zone = 2;
            GameObject.Find("zone6").GetComponent<Zone>().zone = 2;

            //Debug.Log("Zone #: " + GameObject.Find("zone6").GetComponent<Zone>().zone);
        }
        else if (gameObject.name == "zone3")
        {
            GameObject.Find("zone1").GetComponent<Zone>().zone = 3;
            GameObject.Find("zone2").GetComponent<Zone>().zone = 3;
            GameObject.Find("zone3").GetComponent<Zone>().zone = 3;
            GameObject.Find("zone4").GetComponent<Zone>().zone = 3;
            GameObject.Find("zone5").GetComponent<Zone>().zone = 3;
            GameObject.Find("zone6").GetComponent<Zone>().zone = 3;

            //Debug.Log("Zone #: " + GameObject.Find("zone6").GetComponent<Zone>().zone);
        }
        else if (gameObject.name == "zone4")
        {
            GameObject.Find("zone1").GetComponent<Zone>().zone = 4;
            GameObject.Find("zone2").GetComponent<Zone>().zone = 4;
            GameObject.Find("zone3").GetComponent<Zone>().zone = 4;
            GameObject.Find("zone4").GetComponent<Zone>().zone = 4;
            GameObject.Find("zone5").GetComponent<Zone>().zone = 4;
            GameObject.Find("zone6").GetComponent<Zone>().zone = 4;

            //Debug.Log("Zone #: " + GameObject.Find("zone6").GetComponent<Zone>().zone);
        }
        else if (gameObject.name == "zone5")
        {
            GameObject.Find("zone1").GetComponent<Zone>().zone = 5; 
            GameObject.Find("zone2").GetComponent<Zone>().zone = 5;
            GameObject.Find("zone3").GetComponent<Zone>().zone = 5;
            GameObject.Find("zone4").GetComponent<Zone>().zone = 5;
            GameObject.Find("zone5").GetComponent<Zone>().zone = 5;
            GameObject.Find("zone6").GetComponent<Zone>().zone = 5;

            //Debug.Log("Zone #: " + GameObject.Find("zone6").GetComponent<Zone>().zone);
        }
        else if (gameObject.name == "zone6")
        {
            GameObject.Find("zone1").GetComponent<Zone>().zone = 6;
            GameObject.Find("zone2").GetComponent<Zone>().zone = 6;
            GameObject.Find("zone3").GetComponent<Zone>().zone = 6;
            GameObject.Find("zone4").GetComponent<Zone>().zone = 6;
            GameObject.Find("zone5").GetComponent<Zone>().zone = 6;
            GameObject.Find("zone6").GetComponent<Zone>().zone = 6;

            //Debug.Log("Zone #: " + GameObject.Find("zone6").GetComponent<Zone>().zone);
        }

        //GameObject.Find("Main Camera").GetComponent<zoneReset>().resetZone();

    }

}
