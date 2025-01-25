using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildGoalArrs : MonoBehaviour
{
    public GameObject[] objectsAllGoals;
    public float[] objectsZone1ShotX;
    public float[] objectsZone1ShotY;
    public float[] objectsZone2ShotX;
    public float[] objectsZone2ShotY;
    public float[] objectsZone3ShotX;
    public float[] objectsZone3ShotY;
    public float[] objectsZone4ShotX;
    public float[] objectsZone4ShotY;
    public float[] objectsZone5ShotX;
    public float[] objectsZone5ShotY;
    public float[] objectsZone6ShotX;
    public float[] objectsZone6ShotY;
    public float percentInwards = 1f;

    public int[] shotGoodZone1;
    public int[] shotGoodZone2;
    public int[] shotGoodZone3;
    public int[] shotGoodZone4;
    public int[] shotGoodZone5;
    public int[] shotGoodZone6;

    public int[] shotZone1;
    public int[] shotZone2;
    public int[] shotZone3;
    public int[] shotZone4;
    public int[] shotZone5;
    public int[] shotZone6;

    public float[] delXInt;
    public float[] delYInt;
    public float[] rotInt;

    public int[] numGoals;
    public int numZero;

/*    public int ballZone;
    public int*/

    // Start is called before the first frame update
    void Awake()
    {
        selectGoals();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void selectGoals()
    {

        objectsZone1ShotX = new float[39];
        objectsZone1ShotY = new float[39];
        objectsZone2ShotX = new float[37];
        objectsZone2ShotY = new float[37];
        objectsZone3ShotX = new float[39];
        objectsZone3ShotY = new float[39];
        objectsZone4ShotX = new float[39];
        objectsZone4ShotY = new float[39];
        objectsZone5ShotX = new float[37];
        objectsZone5ShotY = new float[37];
        objectsZone6ShotX = new float[39];
        objectsZone6ShotY = new float[39];

        shotGoodZone1 = new int[39];
        shotGoodZone2 = new int[37];
        shotGoodZone3 = new int[39];
        shotGoodZone4 = new int[39];
        shotGoodZone5 = new int[37];
        shotGoodZone6 = new int[39];

        shotZone1 = new int[39];
        shotZone2 = new int[37];
        shotZone3 = new int[39];
        shotZone4 = new int[39];
        shotZone5 = new int[37];
        shotZone6 = new int[39];

        delXInt = new float[6];
        delYInt = new float[6];
        rotInt = new float[6];

    //ithink i need to track the delX and delY and rotGoal for all shots - use that to add error to shots


        int Z1 = 0;
        int Z2 = 0;
        int Z3 = 0;
        int Z4 = 0;
        int Z5 = 0;
        int Z6 = 0;

        objectsAllGoals = GameObject.FindGameObjectsWithTag("Shoot");
        UnityEditor.Handles.color = Color.yellow;
        int numShot = 10;

        for (int i = 0; i < objectsAllGoals.Length; i++)
        {
            Vector3 goalPos = objectsAllGoals[i].gameObject.transform.position;
            float goalRot = objectsAllGoals[i].gameObject.transform.localEulerAngles.z;
            float goalXBound = objectsAllGoals[i].GetComponent<BoxCollider2D>().bounds.size.x;
            float goalYBound = objectsAllGoals[i].GetComponent<BoxCollider2D>().bounds.size.y;
            //Debug.Log(objectsAllGoals[i]);
            //Debug.Log("Name: " + objectsAllGoals[i].gameObject.name + "   GoalRot: " + goalRot);
            if (((goalRot > 90) && (goalRot < 180)) || ((goalRot > 270) && (goalRot < 360)))
            {
                Vector3 goalLeftPos = new Vector3(goalPos.x - percentInwards * goalXBound / 2 * Mathf.Cos(goalRot * Mathf.Deg2Rad), goalPos.y - percentInwards * goalYBound / 2 * Mathf.Sin(goalRot * Mathf.Deg2Rad), 0f);
                Vector3 goalRightPos = new Vector3(goalPos.x + percentInwards * goalXBound / 2 * Mathf.Cos(goalRot * Mathf.Deg2Rad), goalPos.y + percentInwards * goalYBound / 2 * Mathf.Sin(goalRot * Mathf.Deg2Rad), 0f);
                float difY = (goalLeftPos.y - goalRightPos.y) / numShot;
                float difX = (goalLeftPos.x - goalRightPos.x) / numShot;
                delXInt[i] = difX;
                delYInt[i] = difY;
                rotInt[i] = goalRot;
                for (int j = 0; j < numShot + 1; j++)
                {
                    //Debug.Log("i: " + i + "   j: " + j);
                    if (j != 0 && j != numShot && j != 1 && j != (numShot - 1))
                    {
                        Vector3 intGoal = new Vector3(goalRightPos.x + j * difX, goalRightPos.y + j * difY, 0f);
                        if (objectsAllGoals[i].gameObject.name == "Shoot3")
                        {
                            objectsZone1ShotX[Z1] = intGoal.x;
                            objectsZone1ShotY[Z1] = intGoal.y;
                            shotGoodZone1[Z1] = 1;
                            shotZone1[Z1] = 3;
                            objectsZone2ShotX[Z2] = intGoal.x;
                            objectsZone2ShotY[Z2] = intGoal.y;
                            shotGoodZone2[Z2] = 1;
                            shotZone2[Z2] = 3;
                            objectsZone4ShotX[Z4] = intGoal.x;
                            objectsZone4ShotY[Z4] = intGoal.y;
                            shotGoodZone4[Z4] = 1;
                            shotZone4[Z4] = 3;
                            objectsZone5ShotX[Z5] = intGoal.x;
                            objectsZone5ShotY[Z5] = intGoal.y;
                            shotGoodZone5[Z5] = 1;
                            shotZone5[Z5] = 3;
                            objectsZone6ShotX[Z6] = intGoal.x;
                            objectsZone6ShotY[Z6] = intGoal.y;
                            shotGoodZone6[Z6] = 1;
                            shotZone6[Z6] = 3;

                            Z2++;
                            Z1++;
                            Z4++;
                            Z5++;
                            Z6++;
                            //Debug.Log("Shoot3   i: " + i + "   Goal Rot: " + goalRot + "   j: " + j + "   Z1: " + Z1 + "   Z2: " + Z2 + "   Z3: " + Z3 + "   Z4: " + Z4 + "   Z5: " + Z5 + "   Z6: " + Z6);
                        }
                        else if (objectsAllGoals[i].gameObject.name == "Shoot6")
                        {
                            objectsZone1ShotX[Z1] = intGoal.x;
                            objectsZone1ShotY[Z1] = intGoal.y;
                            shotGoodZone1[Z1] = 1;
                            shotZone1[Z1] = 6;
                            objectsZone2ShotX[Z2] = intGoal.x;
                            objectsZone2ShotY[Z2] = intGoal.y;
                            shotGoodZone2[Z2] = 1;
                            shotZone2[Z2] = 6;
                            objectsZone3ShotX[Z3] = intGoal.x;
                            objectsZone3ShotY[Z3] = intGoal.y;
                            shotGoodZone3[Z3] = 1;
                            shotZone3[Z3] = 6;
                            objectsZone4ShotX[Z4] = intGoal.x;
                            objectsZone4ShotY[Z4] = intGoal.y;
                            shotGoodZone4[Z4] = 1;
                            shotZone4[Z4] = 6;
                            objectsZone5ShotX[Z5] = intGoal.x;
                            objectsZone5ShotY[Z5] = intGoal.y;
                            shotGoodZone5[Z5] = 1;
                            shotZone5[Z5] = 6;
                            Z2++;
                            Z3++;
                            Z1++;
                            Z5++;
                            Z4++;
                            //Debug.Log("Shoot6   i: " + i + "   Goal Rot: " + goalRot + "  j: " + j + "   Z1: " + Z1 + "   Z2: " + Z2 + "   Z3: " + Z3 + "   Z4: " + Z4 + "   Z5: " + Z5 + "   Z6: " + Z6);
                        }

                    }
                }
            }
            else if (((goalRot > 0) && (goalRot < 90)) || ((goalRot > 180) && (goalRot < 270)))
            {
                Vector3 goalLeftPos = new Vector3(goalPos.x - percentInwards * goalXBound / 2 * Mathf.Cos(goalRot * Mathf.Deg2Rad), goalPos.y - percentInwards * goalYBound / 2 * Mathf.Sin(goalRot * Mathf.Deg2Rad), 0f);
                Vector3 goalRightPos = new Vector3(goalPos.x + percentInwards * goalXBound / 2 * Mathf.Cos(goalRot * Mathf.Deg2Rad), goalPos.y + percentInwards * goalYBound / 2 * Mathf.Sin(goalRot * Mathf.Deg2Rad), 0f);
                float difY = (goalLeftPos.y - goalRightPos.y) / numShot;
                float difX = (goalLeftPos.x - goalRightPos.x) / numShot;
                for (int j = 0; j < numShot + 1; j++)
                {
                    //Debug.Log("i: " + i + "   j: " + j);
                    if (j != 0 && j != numShot && j != 1 && j != (numShot - 1))
                    {
                        Vector3 intGoal = new Vector3(goalRightPos.x + j * difX, goalRightPos.y + j * difY, 0f);
                        if (objectsAllGoals[i].gameObject.name == "Shoot1")
                        {
                            objectsZone2ShotX[Z2] = intGoal.x;
                            objectsZone2ShotY[Z2] = intGoal.y;
                            shotGoodZone2[Z2] = 1;
                            shotZone2[Z2] = 1;
                            objectsZone3ShotX[Z3] = intGoal.x;
                            objectsZone3ShotY[Z3] = intGoal.y;
                            shotGoodZone3[Z3] = 1;
                            shotZone3[Z3] = 1;
                            objectsZone4ShotX[Z4] = intGoal.x;
                            objectsZone4ShotY[Z4] = intGoal.y;
                            shotGoodZone4[Z4] = 1;
                            shotZone4[Z4] = 1;
                            objectsZone5ShotX[Z5] = intGoal.x;
                            objectsZone5ShotY[Z5] = intGoal.y;
                            shotGoodZone5[Z5] = 1;
                            shotZone5[Z5] = 1;
                            objectsZone6ShotX[Z6] = intGoal.x;
                            objectsZone6ShotY[Z6] = intGoal.y;
                            shotGoodZone6[Z6] = 1;
                            shotZone6[Z6] = 1;
                            Z2++;
                            Z3++;
                            Z4++;
                            Z5++;
                            Z6++;
                            //Debug.Log("Shoot1   i: " + i + "   Goal Rot: " + goalRot + "  j: " + j + "   Z1: " + Z1 + "   Z2: " + Z2 + "   Z3: " + Z3 + "   Z4: " + Z4 + "   Z5: " + Z5 + "   Z6: " + Z6);
                        }
                        else if (objectsAllGoals[i].gameObject.name == "Shoot4")
                        {
                            objectsZone1ShotX[Z1] = intGoal.x;
                            objectsZone1ShotY[Z1] = intGoal.y;
                            shotGoodZone1[Z1] = 1;
                            shotZone1[Z1] = 4;
                            objectsZone2ShotX[Z2] = intGoal.x;
                            objectsZone2ShotY[Z2] = intGoal.y;
                            shotGoodZone2[Z2] = 1;
                            shotZone2[Z2] = 4;
                            objectsZone3ShotX[Z3] = intGoal.x;
                            objectsZone3ShotY[Z3] = intGoal.y;
                            shotGoodZone3[Z3] = 1;
                            shotZone3[Z3] = 4;
                            objectsZone5ShotX[Z5] = intGoal.x;
                            objectsZone5ShotY[Z5] = intGoal.y;
                            shotGoodZone5[Z5] = 1;
                            shotZone5[Z5] = 4;
                            objectsZone6ShotX[Z6] = intGoal.x;
                            objectsZone6ShotY[Z6] = intGoal.y;
                            shotGoodZone6[Z6] = 1;
                            shotZone6[Z6] = 4;
                            Z2++;
                            Z3++;
                            Z1++;
                            Z6++;
                            Z5++;
                            //Debug.Log("Shoot4   i: " + i + "   Goal Rot: " + goalRot + "  j: " + j + "   Z1: " + Z1 + "   Z2: " + Z2 + "   Z3: " + Z3 + "   Z4: " + Z4 + "   Z5: " + Z5 + "   Z6: " + Z6);
                        }
                    }
                }
            }
            else if ((goalRot == 0) || (goalRot == 180))
            {
                Vector3 goalLeftPos = new Vector3(goalPos.x - percentInwards * goalXBound / 2 * Mathf.Cos(goalRot * Mathf.Deg2Rad), goalPos.y, 0f);
                Vector3 goalRightPos = new Vector3(goalPos.x + percentInwards * goalXBound / 2 * Mathf.Cos(goalRot * Mathf.Deg2Rad), goalPos.y, 0f);
                float difY = (goalLeftPos.y - goalRightPos.y) / numShot;
                float difX = (goalLeftPos.x - goalRightPos.x) / numShot;
                for (int j = 0; j < numShot + 1; j++)
                {
                    //Debug.Log("i: " + i + "   j: " + j);
                    if (j != 0 && j != numShot)
                    {
                        Vector3 intGoal = new Vector3(goalRightPos.x + j * difX, goalRightPos.y + j * difY, 0f);
                        if (objectsAllGoals[i].gameObject.name == "Shoot2")
                        {
                            objectsZone1ShotX[Z1] = intGoal.x;
                            objectsZone1ShotY[Z1] = intGoal.y;
                            shotGoodZone1[Z1] = 1;
                            shotZone1[Z1] = 2;
                            objectsZone3ShotX[Z3] = intGoal.x;
                            objectsZone3ShotY[Z3] = intGoal.y;
                            shotGoodZone3[Z3] = 1;
                            shotZone3[Z3] = 2;
                            objectsZone4ShotX[Z4] = intGoal.x;
                            objectsZone4ShotY[Z4] = intGoal.y;
                            shotGoodZone4[Z4] = 1;
                            shotZone4[Z4] = 2;
                            objectsZone5ShotX[Z5] = intGoal.x;
                            objectsZone5ShotY[Z5] = intGoal.y;
                            shotGoodZone5[Z5] = 1;
                            shotZone5[Z5] = 2;
                            objectsZone6ShotX[Z6] = intGoal.x;
                            objectsZone6ShotY[Z6] = intGoal.y;
                            shotGoodZone6[Z6] = 1;
                            shotZone6[Z6] = 2;
                            Z3++;
                            Z1++;
                            Z4++;
                            Z5++;
                            Z6++;
                            //Debug.Log("Shoot2   i: " + i + "   Goal Rot: " + goalRot + "  j: " + j + "   Z1: " + Z1 + "   Z2: " + Z2 + "   Z3: " + Z3 + "   Z4: " + Z4 + "   Z5: " + Z5 + "   Z6: " + Z6);
                        }
                        else if (objectsAllGoals[i].gameObject.name == "Shoot5")
                        {
                            objectsZone1ShotX[Z1] = intGoal.x;
                            objectsZone1ShotY[Z1] = intGoal.y;
                            shotGoodZone1[Z1] = 1;
                            shotZone1[Z1] = 5;
                            objectsZone2ShotX[Z2] = intGoal.x;
                            objectsZone2ShotY[Z2] = intGoal.y;
                            shotGoodZone2[Z2] = 1;
                            shotZone2[Z2] = 5;
                            objectsZone3ShotX[Z3] = intGoal.x;
                            objectsZone3ShotY[Z3] = intGoal.y;
                            shotGoodZone3[Z3] = 1;
                            shotZone3[Z3] = 5;
                            objectsZone4ShotX[Z4] = intGoal.x;
                            objectsZone4ShotY[Z4] = intGoal.y;
                            shotGoodZone4[Z4] = 1;
                            shotZone4[Z4] = 5;
                            objectsZone6ShotX[Z6] = intGoal.x;
                            objectsZone6ShotY[Z6] = intGoal.y;
                            shotGoodZone6[Z6] = 1;
                            shotZone6[Z6] = 5;
                            Z2++;
                            Z3++;
                            Z1++;
                            Z4++;
                            Z6++;
                            //Debug.Log("Shoot5   i: " + i + "   Goal Rot: " + goalRot + "  j: " + j + "   Z1: " + Z1 + "   Z2: " + Z2 + "   Z3: " + Z3 + "   Z4: " + Z4 + "   Z5: " + Z5 + "   Z6: " + Z6);
                        }
                    }
                }
            }

        }
    }


}
