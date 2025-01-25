using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShoot : MonoBehaviour
{
    public Rigidbody2D rb;
    public float[] shotsTakenX;
    public float[] shotsTakenY;
    public int zoneIn;
    public Vector3 ballPos;
    public Vector3 prevBallPos;
    public GameObject[] activePegs;
    public int[] goodShot;
    public int[] goodShotMult;
    public Vector3 pegPos;
    public int numGoodShots;
    public int[] goodShotEl;
    public int[] goodShotZone;
    public int elGoodShot;
    public float actShotX;
    public float actShotY;
    public float shootpower;
    public float minWait = .5f;
    public float minWaitGoal = .5f;
    public float timeElapsed = 0f;
    public float timeElapsedGoal = 0f;
    public float normCrossNum = 0.0075f;
    public float powerMult = 5f;
    public int mainScore;
    public int mainScorePrev;
    public int zoneFromShot;
    public int shotsTaken;
    public float diffMult = 0;
    public int zoneShot;
    public int zonePrev;
    public int shooterPrev;
    public int zoneSwitch;
    public GameObject[] allGoals;
    public int[] goalOn;
    public float distCalc;
    public float distMin;
    public int goalZone;
    public float debugLineTime = 0.15f;
    public int keepGoals = 0;
    public int numPlayer;
    public int numPlayerPrev;
    public int totShots = 0;
    public float ballCurPosx;
    public float ballCurPosy;
    public string ballGoalPosx;
    public string ballGoalPosy;
    public string ballShotDist;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        activePegs = GameObject.Find("Main Camera").GetComponent<PegRemoval>().totalActivePegs;
        goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone1ShotX.Length];
        goodShotMult = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone1ShotX.Length];
        prevBallPos = rb.transform.position;
        mainScore = GameObject.Find("Goal1").GetComponent<Goal1Score>().Goals;
        mainScorePrev = mainScore;
        zoneShot = 0;
        shooterPrev = 0;
        zoneFromShot = 1;
        numPlayer = GameObject.Find("BinhoBall").GetComponent<playerController>().numPlayers;
        numPlayerPrev = 0;

        ballGoalPosx = "";
        ballGoalPosy = "";
        ballShotDist = "";

        //new GameObject[] allGoals[6];

        allGoals = new GameObject[6];
        goalOn = new int[6];
        int j = 0;
        foreach (Transform eachChild in GameObject.FindGameObjectWithTag("AllShoots").GetComponentsInChildren<Transform>())
        {
            if (eachChild.gameObject != GameObject.FindGameObjectWithTag("AllShoots"))
            {
                allGoals[j] = eachChild.gameObject;
                goalOn[j] = 1;
                //Debug.Log(eachChild.gameObject);
                j++;
            }
        }

    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //shotsTaken = 0;
    }

    public Vector2 rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude < 0.025f)
        {
            int randGoodShot = 0;
            int randShotStart = 1;

            ballCurPosx = rb.transform.position.x;
            ballCurPosy = rb.transform.position.y;

            //Debug.Log("Waited Time: " + timeElapsed + "   Required Wait: " + minWait);
            if (timeElapsed < minWait)
            {
                timeElapsed += Time.deltaTime;
            }
            else
            {
                ballPos = rb.transform.position;
                //Debug.Log("Ball Pos: " + ballPos);
                if (ballPos != new Vector3(0f, 0f, 0f))
                {
                    //Debug.Log("Waited Time: " + timeElapsedGoal + "   Required Wait: " + minWaitGoal);
                    while (timeElapsedGoal < minWaitGoal)
                    {
                        timeElapsedGoal += Time.deltaTime;
                        //Debug.Log("Waited Time: " + timeElapsedGoal + "   Required Wait: " + minWaitGoal);
                    }

                    float distMin = 1000000000;
                    goalZone = 7;
                    float distCalc = 1000000000;
                    for (int i = 0;i < allGoals.Length; i++)
                    {
                        if (goalOn[i]==1)
                        {
                            //Mathf.Sqrt(
                            //Mathf.Pow(num, 2);
                            distCalc = Mathf.Sqrt(Mathf.Pow(allGoals[i].gameObject.transform.position.x - rb.transform.position.x, 2) + Mathf.Pow(allGoals[i].gameObject.transform.position.y - rb.transform.position.y, 2));

                            Color color = new Color(1.0f, 0.73f, 0.79f);
                            //Debug.DrawLine(allGoals[i].gameObject.transform.position, rb.transform.position, color, 2f, false);

                            //Debug.Log("Dist From Zone " + (i+1) + " to ball: " + distCalc);
                            if (distCalc < distMin)
                            {
                                distMin = distCalc;
                                goalZone = i+1;
                            }
                        
                        }
                        else
                        {
                            //Debug.Log("Goal Not On: " + i + 1);
                        }
                    }
/*                    Color color1 = new Color(0f, 0f, 0f);
                    if (goalZone == 1) { Debug.DrawLine(allGoals[0].gameObject.transform.position, rb.transform.position, color1, 2f, false); }
                    else if (goalZone == 2) {Debug.DrawLine(allGoals[1].gameObject.transform.position, rb.transform.position, color1, 2f, false); }
                    else if (goalZone == 3) { Debug.DrawLine(allGoals[2].gameObject.transform.position, rb.transform.position, color1, 2f, false); }
                    else if (goalZone == 4) { Debug.DrawLine(allGoals[3].gameObject.transform.position, rb.transform.position, color1, 2f, false); }
                    else if (goalZone == 5) { Debug.DrawLine(allGoals[4].gameObject.transform.position, rb.transform.position, color1, 2f, false); }
                    else if (goalZone == 6) { Debug.DrawLine(allGoals[5].gameObject.transform.position, rb.transform.position, color1, 2f, false); }*/

                    if (zonePrev == goalZone)
                    {
                        zoneSwitch++;
                    }
                    else
                    {
                        zoneSwitch = 0;
                    }

                    zonePrev = goalZone;
                    //Debug.Log("round 2: " + zonePrev + "  =?= " + goalZone);

                    //Debug.Log("Switch Zones: " + zoneSwitch);
                    if (zoneSwitch % 2 == 0)
                    {
                        //if (GameObject.Find("zone1").GetComponent<Zone>().zone == 1)
                        if (goalZone == 1)
                        {
                            //Debug.Log("Zone1Shots");
                            goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone1ShotX.Length];
                            shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone1ShotX;
                            shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone1ShotY;
                            zoneFromShot = 1;
                        }
                        //else if (GameObject.Find("zone1").GetComponent<Zone>().zone == 2)
                        else if (goalZone ==2)
                        {
                            //Debug.Log("Zone2Shots");
                            goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone2ShotX.Length];
                            shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone2ShotX;
                            shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone2ShotY;
                            zoneFromShot = 2;
                        }
                        //else if (GameObject.Find("zone1").GetComponent<Zone>().zone == 3)
                        else if (goalZone == 3)
                        {
                            //Debug.Log("Zone3Shots");
                            goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone3ShotX.Length];
                            shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone3ShotX;
                            shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone3ShotY;
                            zoneFromShot = 3;
                        }
                        //else if (GameObject.Find("zone1").GetComponent<Zone>().zone == 4)
                        else if (goalZone == 4)
                        {
                            //Debug.Log("Zone4Shots");
                            goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone4ShotX.Length];
                            shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone4ShotX;
                            shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone4ShotY;
                            zoneFromShot = 4;
                        }
                        //else if (GameObject.Find("zone1").GetComponent<Zone>().zone == 5)
                        else if (goalZone == 5)
                        {
                            //Debug.Log("Zone5Shots");
                            goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone5ShotX.Length];
                            shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone5ShotX;
                            shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone5ShotY;
                            zoneFromShot = 5;
                        }
                        //else if (GameObject.Find("zone1").GetComponent<Zone>().zone == 6)
                        else if (goalZone == 6)
                        {
                            //Debug.Log("Zone6Shots");
                            goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone6ShotX.Length];
                            shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone6ShotX;
                            shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone6ShotY;
                            zoneFromShot = 6;
                        }
                    }
                    else
                    {
                        //if the shot does not leave the zone, then the aimed at zone gets the next shot unless the person misses that shot on the same goal
                        if (zoneShot==1)
                        {
                            goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone1ShotX.Length];
                            shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone1ShotX;
                            shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone1ShotY;
                            zoneFromShot = 1;
                            goalZone = 1;
                        }
                        else if (zoneShot == 2)
                        {
                            //Debug.Log("Zone2Shots");
                            goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone2ShotX.Length];
                            shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone2ShotX;
                            shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone2ShotY;
                            zoneFromShot = 2;
                            goalZone = 2;
                        }
                        else if (zoneShot == 3)
                        {
                            //Debug.Log("Zone3Shots");
                            goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone3ShotX.Length];
                            shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone3ShotX;
                            shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone3ShotY;
                            zoneFromShot = 3;
                            goalZone = 3;
                        }
                        else if (zoneShot == 4)
                        {
                            //Debug.Log("Zone4Shots");
                            goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone4ShotX.Length];
                            shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone4ShotX;
                            shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone4ShotY;
                            zoneFromShot = 4;
                            goalZone = 4;
                        }
                        else if (zoneShot == 5)
                        {
                            //Debug.Log("Zone5Shots");
                            goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone5ShotX.Length];
                            shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone5ShotX;
                            shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone5ShotY;
                            zoneFromShot = 5;
                            goalZone = 5;
                        }
                        else if (zoneShot == 6)
                        {
                            //Debug.Log("Zone6Shots");
                            goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone6ShotX.Length];
                            shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone6ShotX;
                            shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone6ShotY;
                            zoneFromShot = 6;
                            goalZone = 6;
                        }
                    }
                    timeElapsedGoal = 0f;

                    //}
                }
                else
                {
                    numPlayer = GameObject.Find("BinhoBall").GetComponent<playerController>().numPlayers;

                    //the person knocks someone else out gets to go again
                    if (numPlayer != numPlayerPrev) 
                    {
                        randShotStart = shooterPrev;
                    }
                    else
                    {
                        //kickoff
                        if (GameObject.Find("Goal1").GetComponent<Goal1Score>().scored_onGoal == 0)
                        {
                            randShotStart = Random.Range(1, GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsAllGoals.Length + 1);
                            //Debug.Log("Rand Zone from Start: " + zoneFromShot);
                        }
                        //person scored on gets to shoot next
                        else if (GameObject.Find("Goal1").GetComponent<Goal1Score>().scored_onGoal == 1)
                        {
                            randShotStart = 1;
                            //Debug.Log("Last Scored Zone: " + randShotStart);
                        }
                        else if (GameObject.Find("Goal1").GetComponent<Goal1Score>().scored_onGoal == 2)
                        {
                            randShotStart = 2;
                            //Debug.Log("Last Scored Zone: " + randShotStart);
                        }
                        else if (GameObject.Find("Goal1").GetComponent<Goal1Score>().scored_onGoal == 3)
                        {
                            randShotStart = 3;
                            //Debug.Log("Last Scored Zone: " + randShotStart);
                        }
                        else if (GameObject.Find("Goal1").GetComponent<Goal1Score>().scored_onGoal == 4)
                        {
                            randShotStart = 4;
                            //Debug.Log("Last Scored Zone: " + randShotStart);
                        }
                        else if (GameObject.Find("Goal1").GetComponent<Goal1Score>().scored_onGoal == 5)
                        {
                            randShotStart = 5;
                            //Debug.Log("Last Scored Zone: " + randShotStart);
                        }
                        else if (GameObject.Find("Goal1").GetComponent<Goal1Score>().scored_onGoal == 6)
                        {
                            randShotStart = 6;
                            //Debug.Log("Last Scored Zone: " + randShotStart);
                        }
                        //else { Debug.Log("no options"); }
                    }

                    //Debug.Log(randShotStart);
                    if (randShotStart == 1)
                    {
                        //Debug.Log("Zone1Shots");
                        goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone1ShotX.Length];
                        shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone1ShotX;
                        shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone1ShotY;
                        zoneFromShot = 1;
                        goalZone = 1;
                    }
                    else if (randShotStart == 2)
                    {
                        //Debug.Log("Zone2Shots");
                        goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone2ShotX.Length];
                        shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone2ShotX;
                        shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone2ShotY;
                        zoneFromShot = 2;
                        goalZone = 2;
                    }
                    else if (randShotStart == 3)
                    {
                        //Debug.Log("Zone3Shots");
                        goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone3ShotX.Length];
                        shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone3ShotX;
                        shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone3ShotY;
                        zoneFromShot = 3;
                        goalZone = 3;
                    }
                    else if (randShotStart == 4)
                    {
                        //Debug.Log("Zone4Shots");
                        goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone4ShotX.Length];
                        shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone4ShotX;
                        shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone4ShotY;
                        zoneFromShot = 4;
                        goalZone = 4;
                    }
                    else if (randShotStart == 5)
                    {
                        //Debug.Log("Zone5Shots");
                        goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone5ShotX.Length];
                        shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone5ShotX;
                        shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone5ShotY;
                        zoneFromShot = 5;
                        goalZone = 5;
                    }
                    else if (randShotStart==6)
                    {
                        //Debug.Log("Zone6Shots");
                        goodShot = new int[GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone6ShotX.Length];
                        shotsTakenX = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone6ShotX;
                        shotsTakenY = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().objectsZone6ShotY;
                        zoneFromShot = 6;
                        goalZone = 6;
                    }
                    //Debug.Log("Rand Zone from Start: " + zoneFromShot);
                }
                shooterPrev = zoneFromShot;
                //Debug.Log(shotsTakenX.Length);

                int shotCount = 0;
                int numGoodShots = 0;
                int multShots = 0;
                for (int i = 0; i < shotsTakenX.Length; i++)
                {
                    ballPos = rb.transform.position;
                    Vector3 shotPos = new Vector3(shotsTakenX[i], shotsTakenY[i], 0f);
                    Vector3 dirOfShot = new Vector3(0, 0, 0);
                    Vector3 lineDir = new Vector3(0, 0, 0);
                    Vector3 pointDir = new Vector3(0, 0, 0);
                    Vector3 crossProduct = new Vector3(0, 0, 0);
                    float normCross;
                    float dotProd;
                    int goodShotPegs = 0;
                    float minNormCross = 10000000000000000000;
                    for (int j = 0; j < activePegs.Length; j++)
                    {
                        pegPos = activePegs[j].transform.position;
                        pointDir = pegPos - ballPos;
                        dirOfShot = shotPos - ballPos;
                        lineDir = dirOfShot;
                        crossProduct = Vector3.Cross(Vector3.Normalize(pointDir), Vector3.Normalize(lineDir));
                        normCross = crossProduct.magnitude;
                        dotProd = Vector3.Dot(lineDir, pointDir);
                        //2nd and 3rd term are for inbetween ball and goal pos - 1st term is if peg is on line
                        if (normCross < normCrossNum && dotProd > 0 && dotProd < lineDir.sqrMagnitude)
                        {
                            //Debug.Log("Shot: " + i + "   ActivePeg: " + activePegs[j] + "   :   " + normCross + " < " + normCrossNum + "   " + dotProd + " > 0   " + dotProd + " < " + lineDir.sqrMagnitude);
                            goodShotPegs = 1; //collision
                            minNormCross= 0;
                            //Debug.Log("Collision");
                            break;
                        }
                        else
                        {
                            if (normCross > normCrossNum)
                            {
                                if (normCross < minNormCross)
                                {
                                    minNormCross = normCross;
                                }
                            }
                        }
                    }

                    if (goodShotPegs == 1) //bad shot
                    {
                        goodShot[shotCount] = 0;
                        goodShotMult[shotCount] = 0;
                        //Debug.Log("Zone: " + zoneFromShot + " Shot#: " + shotCount + " ON/OFF " + GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone1[shotCount]);

                        shotCount++;
                        //Color color = new Color(0, 0, 1.0f);
                        //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                    }
                    else
                    {

                        if (zoneFromShot == 1 && GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone1[shotCount] == 1)
                        {
                            goodShot[shotCount] = 1; //good shot
                            if (minNormCross > (normCrossNum * 3f))
                            {
                                goodShotMult[shotCount] = 6;
                                multShots = multShots + 6;
                                //Color color = new Color(1.0f, 0, 0);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            else if (minNormCross > (normCrossNum * 2f))
                            {
                                goodShotMult[shotCount] = 3;
                                multShots = multShots + 3;
                                //Color color = new Color(1.0f, 0.64f, 0);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            else
                            {
                                goodShotMult[shotCount] = 1;
                                multShots = multShots + 1;
                                //Color color = new Color(1.0f, 0.92f, 0.016f);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }

                            //Debug.Log("Zone: " + zoneFromShot + " Shot#: " + shotCount + " ON/OFF " + GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone1[shotCount]);

                            shotCount++;
                            numGoodShots++;
                        }
                        else if (zoneFromShot == 2 && GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone2[shotCount] == 1)
                        {
                            goodShot[shotCount] = 1; //good shot
                            if (minNormCross > (normCrossNum * 3f))
                            {
                                goodShotMult[shotCount] = 6;
                                multShots = multShots + 6;
                                //Color color = new Color(1.0f, 0, 0);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            else if (minNormCross > (normCrossNum * 2f))
                            {
                                goodShotMult[shotCount] = 3;
                                multShots = multShots + 3;
                                //Color color = new Color(1.0f, 0.64f, 0);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            else
                            {
                                goodShotMult[shotCount] = 1;
                                multShots = multShots + 1;
                                //Color color = new Color(1.0f, 0.92f, 0.016f);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            //Debug.Log("Zone: " + zoneFromShot + " Shot#: " + shotCount + " ON/OFF " + GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone1[shotCount]);

                            shotCount++;
                            numGoodShots++;
                        }
                        else if (zoneFromShot == 3 && GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone3[shotCount] == 1)
                        {
                            goodShot[shotCount] = 1; //good shot
                            if (minNormCross > (normCrossNum * 3f))
                            {
                                goodShotMult[shotCount] = 6;
                                multShots = multShots + 6;
                                //Color color = new Color(1.0f, 0, 0);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            else if (minNormCross > (normCrossNum * 2f))
                            {
                                goodShotMult[shotCount] = 3;
                                multShots = multShots + 3;
                                //Color color = new Color(1.0f, 0.64f, 0);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            else
                            {
                                goodShotMult[shotCount] = 1;
                                multShots = multShots + 1;
                                //Color color = new Color(1.0f, 0.92f, 0.016f);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            //Debug.Log("Zone: " + zoneFromShot + " Shot#: " + shotCount + " ON/OFF " + GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone1[shotCount]);

                            shotCount++;
                            numGoodShots++;
                        }
                        else if (zoneFromShot == 4 && GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone4[shotCount] == 1)
                        {
                            goodShot[shotCount] = 1; //good shot
                            if (minNormCross > (normCrossNum * 3f))
                            {
                                goodShotMult[shotCount] = 6;
                                multShots = multShots + 6;
                                //Color color = new Color(1.0f, 0, 0);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            else if (minNormCross > (normCrossNum * 2f))
                            {
                                goodShotMult[shotCount] = 3;
                                multShots = multShots + 3;
                                //Color color = new Color(1.0f, 0.64f, 0);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            else
                            {
                                goodShotMult[shotCount] = 1;
                                multShots = multShots + 1;
                                //Color color = new Color(1.0f, 0.92f, 0.016f);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            //Debug.Log("Zone: " + zoneFromShot + " Shot#: " + shotCount + " ON/OFF " + GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone1[shotCount]);

                            shotCount++;
                            numGoodShots++;
                        }
                        else if (zoneFromShot == 5 && GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone5[shotCount] == 1)
                        {
                            goodShot[shotCount] = 1; //good shot
                            if (minNormCross > (normCrossNum * 3f))
                            {
                                goodShotMult[shotCount] = 6;
                                multShots = multShots + 6;
                                //Color color = new Color(1.0f, 0, 0);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            else if (minNormCross > (normCrossNum * 2f))
                            {
                                goodShotMult[shotCount] = 3;
                                multShots = multShots + 3;
                                //Color color = new Color(1.0f, 0.64f, 0);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            else
                            {
                                goodShotMult[shotCount] = 1;
                                multShots = multShots + 1;
                                //Color color = new Color(1.0f, 0.92f, 0.016f);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            //Debug.Log("Zone: " + zoneFromShot + " Shot#: " + shotCount + " ON/OFF " + GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone1[shotCount]);

                            shotCount++;
                            numGoodShots++;
                        }
                        else if (zoneFromShot == 6 && GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone6[shotCount] == 1)
                        {
                            goodShot[shotCount] = 1; //good shot
                            if (minNormCross > (normCrossNum * 3f))
                            {
                                goodShotMult[shotCount] = 6;
                                multShots = multShots + 6;
                                //Color color = new Color(1.0f, 0, 0);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            else if (minNormCross > (normCrossNum * 2f))
                            {
                                goodShotMult[shotCount] = 3;
                                multShots = multShots + 3;
                                //Color color = new Color(1.0f, 0.64f, 0);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            else
                            {
                                goodShotMult[shotCount] = 1;
                                multShots = multShots + 1;
                                //Color color = new Color(1.0f, 0.92f, 0.016f);
                                //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                            }
                            //Debug.Log("Zone: " + zoneFromShot + " Shot#: " + shotCount + " ON/OFF " + GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone1[shotCount]);

                            shotCount++;
                            numGoodShots++;
                        }
                        else
                        {
                            goodShot[shotCount] = 0;
                            goodShotMult[shotCount] = 0;
                            //Debug.Log("Zone: " + zoneFromShot + " Shot#: " + shotCount + " ON/OFF " + GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone1[shotCount]);
                            shotCount++;
                            //Color color = new Color(0, 0, 1.0f);
                            //Debug.DrawLine(shotPos, ballPos, color, debugLineTime, false);
                        }
                        //Debug.Log(shotCount + " ON/OFF " + GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone1[shotCount]);

                        //Debug.Log("ShotCount: " + shotCount + "numGoodShots: " + numGoodShots + "   MultShots: " + multShots);

                    }


                }

                int elGoodShot = 0;
                float actShotX = 0f;
                float actShotY = 0f;
                goodShotEl = new int[multShots];
                for (int z = 0; z < goodShot.Length; z++)
                {
                    if (goodShot[z] == 1)
                    {
                        int ii = 0;
                        while (ii < goodShotMult[z])
                        {
                            goodShotEl[elGoodShot] = z;
                            elGoodShot++;
                            ii++;
                        }

                    }
                }

                //Debug.Log("Numer of Good Shots: " + numGoodShots + "   Possible Shots W Mult: " + multShots + "   Num Els Good Shots: " + elGoodShot);

                if (elGoodShot > 0)
                {
                    randGoodShot = Random.Range(0, multShots);
                    int elUse = goodShotEl[randGoodShot];
                    
                    try
                    {
                        if (goalZone == 1) { zoneShot = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotZone1[elUse]; }
                        else if (goalZone == 2) { zoneShot = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotZone2[elUse]; }
                        else if (goalZone == 3) { zoneShot = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotZone3[elUse]; }
                        else if (goalZone == 4) { zoneShot = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotZone4[elUse]; }
                        else if (goalZone == 5) { zoneShot = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotZone5[elUse]; }
                        else if (goalZone == 6) { zoneShot = GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotZone6[elUse]; }

                        actShotX = shotsTakenX[elUse];
                        actShotY = shotsTakenY[elUse];
                    }
                    //we are seemingly using two different sized arrays: elUse is out of bounds for arr sizing
                    catch (System.Exception e)
                    {
                       //Debug.LogException(e);
                    }
                }
                else
                {
                    if (GameObject.Find("Goal1").GetComponent<Goal1Score>().scored_onGoal != 0)
                    {
                        randGoodShot = Random.Range(0, goodShot.Length);
                        actShotX = shotsTakenX[randGoodShot];
                        actShotY = shotsTakenY[randGoodShot];
                    }
                }

                //Debug.Log("What Now");
                int newRand1 = Random.Range(0, 2);

                //Bell Distribution About 
                float mean = 0f; // the mean of the distribution
                float stdDev = 1.2312f; // the standard deviation of the distribution - 2/3 of the angle between pegs - allows for error of up to +-2 shots
                float u1 = Random.Range(0f, 1f);
                float u2 = Random.Range(0f, 1f);
                float z1 = Mathf.Sqrt(-2f * Mathf.Log(u1)) * Mathf.Sin(2f * Mathf.PI * u2);
                float x1 = z1 * stdDev + mean;
                //Debug.Log(x1);
                //x1 = Mathf.Clamp(x1, -10f, 10f);

                Vector2 shotPos2D = new Vector2(actShotX, actShotY);
                Vector2 ballPos2D = new Vector2(rb.transform.position.x, rb.transform.position.y);
                Vector2 dirForShot = new Vector2(shotPos2D.x - ballPos2D.x, shotPos2D.y - ballPos2D.y);
                Vector2 dirForShotUse = new Vector2(0f, 0f);

                dirForShotUse = rotate(dirForShot, x1 * Mathf.Deg2Rad);

                Vector3 shotPosUse = shotPos2D;
                Vector3 ballPosUse = ballPos2D;
                //Color colorUse = new Color(0f, 0f, 0f);
                //Debug.DrawLine(shotPosUse, ballPosUse, colorUse, debugLineTime, false);

                dirForShotUse.Normalize();
                shootpower = Mathf.Abs(GameObject.Find("BinhoBall").GetComponent<playerController>().maxdistance) * 1;
                Vector2 push = dirForShotUse * shootpower * powerMult; 

                numPlayerPrev = GameObject.Find("BinhoBall").GetComponent<playerController>().numPlayers;
                totShots++;
                rb.AddForce(push, ForceMode2D.Impulse);

                mainScore = GameObject.Find("Goal1").GetComponent<Goal1Score>().Goals;
                timeElapsed = 0f;
            }
        }
    }
}
