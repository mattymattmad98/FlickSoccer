using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class playerController : MonoBehaviour
{

    private GameObject mousePointA;
    private GameObject mousePointB;

    private IEnumerator coroutine;

    private float currentdistance;
    public float maxdistance = 3f;
    public float mindistance = 0.85f;
    public int CollisionHappen;
    private float safeSpace;
    private float shootpower;

    public int numPlayers=6;

    private Vector3 shootDirection;
    public Rigidbody2D rb;
	public Vector3 goalPos;
    private int flickCount;

   
    public void Awake()
    {
        Time.timeScale = 100f;
        mousePointA = GameObject.FindGameObjectWithTag("PointA");
        mousePointB = GameObject.FindGameObjectWithTag("PointB");
        rb = GetComponent<Rigidbody2D>();
	
    }

    // Update is called once per frame
    private void OnMouseDrag()
    {
        UnityEngine.Cursor.visible = false;
        currentdistance = Vector3.Distance(mousePointA.transform.position, transform.position);

        //Debug.Log("Drag Distance: " + currentdistance);

        if (currentdistance >= mindistance)
        {
            if (currentdistance <= maxdistance)
            {
                safeSpace = currentdistance;
            }
            else
            {
                safeSpace = maxdistance;
            }
        }
        else
        {
            return;
        }

       // doArrowAndCircleStuff();

        shootpower = Mathf.Abs(safeSpace) * 12;

        Vector3 dimxy = mousePointA.transform.position - transform.position;
        float difference = dimxy.magnitude;
        mousePointB.transform.position = transform.position + ((dimxy / difference) * currentdistance * -1);
        mousePointB.transform.position = new Vector3(mousePointB.transform.position.x, mousePointB.transform.position.y, -0.5f);

        shootDirection = Vector3.Normalize(mousePointA.transform.position - transform.position);
    }

    private void OnMouseUp()
    {

        Vector2 push = shootDirection * shootpower * -1;

        if (rb.velocity.magnitude < 0.15) //Add in logic to alternate which player can drag ball - do with a counter div2 0 or 1 remainder 
        {
            forceBall(push);
        }
        UnityEngine.Cursor.visible = true;

    }

    private void forceBall(Vector2 push)
    {
        //Debug.Log("Hit");
        rb.AddForce(push, ForceMode2D.Impulse);
    }

}