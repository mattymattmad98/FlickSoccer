using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ballBounce : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector3 lastVelocity;
    public Vector2 lVel;
    private float bF;
    public float bFPeg = 0.5F;
    public float bfOther = 0.95F;
    public float lowSpeed = 0.05F;

    public float dotProd;
    public float interpVal;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    public void ResetPosition()
    {
        rb.position = new Vector3(0, 0, 0);
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {

        var direction = Vector3.Reflect(lastVelocity.normalized, coll.contacts[0].normal);
        dotProd = Vector3.Dot(lastVelocity.normalized, coll.contacts[0].normal);

        if ((coll.gameObject.tag == "peg_row1") || (coll.gameObject.tag == "peg_row2") || (coll.gameObject.tag == "peg_row3") || (coll.gameObject.tag == "peg_row4") || (coll.gameObject.tag == "peg_row5"))
        {
            bF = Mathf.Lerp(0.5f, 0.1f, Mathf.Abs(dotProd));
            //Debug.Log("Dot Product: " + dotProd);
            //Debug.Log("Pegs Shot Bounciness: " + bF);
        }
        else if ((coll.gameObject.tag == "Rings") || (coll.gameObject.tag == "GoalBounce"))
        {
           // Debug.Log("Hit Wall");
            bF = Mathf.Lerp(0.95f, 0.9f, Mathf.Abs(dotProd));
            //Debug.Log("Dot Product: " + dotProd);
            //Debug.Log("Wall Shot Bounciness: " + bF);
        }


        var speed = lastVelocity.magnitude * bF;
        rb.velocity = direction * Mathf.Max(speed, 0f);
    }

/*    void OnCollisionStay2D(Collision2D collisionInfo)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint2D contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 10, Color.black, 5f);
        }
    }*/

}