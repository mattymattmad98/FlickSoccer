using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class Goal5Score : MonoBehaviour
{
    private GameObject[] objectsActiveBeginPeg;
    private GameObject[] objectsActiveBeginBut;

    public GameObject[] objectsActive;

    private TMPro.TextMeshProUGUI playerLeft;
    private TMPro.TextMeshProUGUI playerRight;

    public int goalsPer = 1;
    public int Goals5=1;
    public int Goals = 18;
    public int scored_onGoal = 0;
    public string whoScored;
    public float prevPosx;
    public float prevPosy;


    public GameObject GoalBounceOn;

    public void Awake()
    {
        GameObject.Find("Goal1").GetComponent<Goal1Score>().scored_onGoal = 0;
        GameObject.Find("Goal2").GetComponent<Goal2Score>().scored_onGoal = 0;
        GameObject.Find("Goal3").GetComponent<Goal3Score>().scored_onGoal = 0;
        GameObject.Find("Goal4").GetComponent<Goal4Score>().scored_onGoal = 0;
        GameObject.Find("Goal5").GetComponent<Goal5Score>().scored_onGoal = 0;
        GameObject.Find("Goal6").GetComponent<Goal6Score>().scored_onGoal = 0;

        GoalBounceOn = GameObject.Find("GoalB5");
        GoalBounceOn.gameObject.SetActive(false);

        whoScored = "";

    }

//need a solid for bringing score down per side
    public void OnTriggerEnter2D(Collider2D collision)
    {
        prevPosx = GameObject.Find("BinhoBall").GetComponent<AIShoot>().rb.transform.position.x;
        prevPosy = GameObject.Find("BinhoBall").GetComponent<AIShoot>().rb.transform.position.y;
        if (GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballGoalPosx == "")
        {
            GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballGoalPosx = GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballCurPosx.ToString("#.000");
            GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballGoalPosy = GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballCurPosy.ToString("#.000");
            GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballShotDist = Mathf.Sqrt(Mathf.Pow(GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballCurPosx - prevPosx, 2f) + Mathf.Pow(GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballCurPosy - prevPosy, 2f)).ToString("#.000");

        }
        else
        {
            GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballGoalPosx = GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballGoalPosx + "<>" + GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballCurPosx.ToString("#.000");
            GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballGoalPosy = GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballGoalPosy + "<>" + GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballCurPosy.ToString("#.000");
            GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballShotDist = GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballShotDist + "<>" + Mathf.Sqrt(Mathf.Pow(GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballCurPosx - prevPosx, 2f) + Mathf.Pow(GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballCurPosy - prevPosy, 2f)).ToString("#.000");

        }
        //Debug.Log(gameObject.name);
        GameObject.Find("BinhoBall").GetComponent<ballBounce>().ResetPosition();

        GameObject.Find("Goal1").GetComponent<Goal1Score>().scored_onGoal = 5;
        GameObject.Find("Goal2").GetComponent<Goal2Score>().scored_onGoal = 5;
        GameObject.Find("Goal3").GetComponent<Goal3Score>().scored_onGoal = 5;
        GameObject.Find("Goal4").GetComponent<Goal4Score>().scored_onGoal = 5;
        GameObject.Find("Goal5").GetComponent<Goal5Score>().scored_onGoal = 5;
        GameObject.Find("Goal6").GetComponent<Goal6Score>().scored_onGoal = 5;

        if (Goals5>0)
        {
            Goals--;
            GameObject.Find("Goal1").GetComponent<Goal1Score>().Goals = Goals;
            GameObject.Find("Goal2").GetComponent<Goal2Score>().Goals = Goals;
            GameObject.Find("Goal3").GetComponent<Goal3Score>().Goals = Goals;
            GameObject.Find("Goal4").GetComponent<Goal4Score>().Goals = Goals;
            GameObject.Find("Goal5").GetComponent<Goal5Score>().Goals = Goals;
            GameObject.Find("Goal6").GetComponent<Goal6Score>().Goals = Goals;

            whoScored = whoScored + GameObject.Find("BinhoBall").GetComponent<AIShoot>().goalZone;
        }


        if (Goals5 > 1)
        {
            Goals5--;

        }
        else if (Goals5==1)
        {
            Goals5--;
            //Debug.Log("Resetting");
            for (int i = 0; i < 9; i++)
            { 
                GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone1[i + 23] = 0;
                GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone2[i + 21] = 0;
                GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone3[i + 23] = 0;
                GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone4[i + 23] = 0;
                GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone6[i + 30] = 0;
            }

            if (GameObject.Find("BinhoBall").GetComponent<AIShoot>().keepGoals == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    GameObject.Find("Main Camera").GetComponent<PegRemoval>().totalActivePegs[i + 28].SetActive(false);
                }
            }
            GoalBounceOn.gameObject.SetActive(true);
            GameObject.Find("BinhoBall").GetComponent<AIShoot>().goalOn[4] = 0;
            GameObject.Find("Main Camera").GetComponent<PegRemoval>().winOrient = GameObject.Find("Main Camera").GetComponent<PegRemoval>().winOrient + "5";


        }

        // Debug.Log("Score: " + GameObject.Find("Goal1").GetComponent<Goal1Score>().Goals1 + " to " +
        //   GameObject.Find("Goal2").GetComponent<Goal2Score>().Goals2 + " to " + GameObject.Find("Goal3").GetComponent<Goal3Score>().Goals3 + " to "
        // + GameObject.Find("Goal4").GetComponent<Goal4Score>().Goals4 + " to " + Goals5 + " to " + GameObject.Find("Goal6").GetComponent<Goal6Score>().Goals6);

        if (Goals5 == 0)
        {
            GameObject.Find("BinhoBall").GetComponent<playerController>().numPlayers--;
        }
        //Debug.Log(GameObject.Find("BinhoBall").GetComponent<playerController>().numPlayers);
        if (GameObject.Find("BinhoBall").GetComponent<playerController>().numPlayers == 1)
        {
            //Debug.Log(GameObject.Find("Main Camera").GetComponent<PegRemoval>().numReset);
            //Debug.Log(GameObject.Find("Main Camera").GetComponent<PegRemoval>().maxNumReset);

            if (GameObject.Find("Goal2").GetComponent<Goal2Score>().Goals2 != 0) { GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinners[GameObject.Find("Main Camera").GetComponent<PegRemoval>().numReset] = GameObject.Find("Main Camera").GetComponent<PegRemoval>().goal2; GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinner = 2; }
            else if (GameObject.Find("Goal3").GetComponent<Goal3Score>().Goals3 != 0) { GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinners[GameObject.Find("Main Camera").GetComponent<PegRemoval>().numReset] = GameObject.Find("Main Camera").GetComponent<PegRemoval>().goal3; GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinner = 3; }
            else if (GameObject.Find("Goal4").GetComponent<Goal4Score>().Goals4 != 0) { GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinners[GameObject.Find("Main Camera").GetComponent<PegRemoval>().numReset] = GameObject.Find("Main Camera").GetComponent<PegRemoval>().goal4; GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinner = 4; }
            else if (GameObject.Find("Goal1").GetComponent<Goal1Score>().Goals1 != 0) { GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinners[GameObject.Find("Main Camera").GetComponent<PegRemoval>().numReset] = GameObject.Find("Main Camera").GetComponent<PegRemoval>().goal1; GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinner = 1; }
            else if (GameObject.Find("Goal6").GetComponent<Goal6Score>().Goals6 != 0) { GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinners[GameObject.Find("Main Camera").GetComponent<PegRemoval>().numReset] = GameObject.Find("Main Camera").GetComponent<PegRemoval>().goal6; GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinner = 6; }

            GameObject.Find("Main Camera").GetComponent<PegRemoval>().winOrient = GameObject.Find("Main Camera").GetComponent<PegRemoval>().winOrient + GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinner.ToString();
            Debug.Log("Win # : " + (GameObject.Find("Main Camera").GetComponent<PegRemoval>().numReset + 1) + "   Zone #: " + GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinner + "   Pegs:" + GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinners[GameObject.Find("Main Camera").GetComponent<PegRemoval>().numReset]);

            string filePath = "C:\\Users\\Matthew\\My project\\winners3.txt";
            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.WriteAllText(filePath, "Goal1, Goal2, Goal3, Goal4, Goal5, Goal6, Winner, Knockout Order, G1Scored, G2Scored, G3Scored, G4Scored, G5Scored, G6Scored, Goals Left, Total Shots, XCoord, YCoord \n\n");
            }
            string txtSave =
                GameObject.Find("Main Camera").GetComponent<PegRemoval>().goal1 + ", " + GameObject.Find("Main Camera").GetComponent<PegRemoval>().goal2 + ", " +
                GameObject.Find("Main Camera").GetComponent<PegRemoval>().goal3 + ", " + GameObject.Find("Main Camera").GetComponent<PegRemoval>().goal4 + ", " +
                GameObject.Find("Main Camera").GetComponent<PegRemoval>().goal5 + ", " + GameObject.Find("Main Camera").GetComponent<PegRemoval>().goal6 + ", " +
                GameObject.Find("Main Camera").GetComponent<PegRemoval>().gameWinner + ", " + GameObject.Find("Main Camera").GetComponent<PegRemoval>().winOrient + ", " +
                GameObject.Find("Goal1").GetComponent<Goal1Score>().whoScored + ", " + GameObject.Find("Goal2").GetComponent<Goal2Score>().whoScored + ", " +
                GameObject.Find("Goal3").GetComponent<Goal3Score>().whoScored + ", " + GameObject.Find("Goal4").GetComponent<Goal4Score>().whoScored + ", " +
                GameObject.Find("Goal5").GetComponent<Goal5Score>().whoScored + ", " + GameObject.Find("Goal6").GetComponent<Goal6Score>().whoScored + ", " +
                GameObject.Find("Goal5").GetComponent<Goal5Score>().Goals + ", " + GameObject.Find("BinhoBall").GetComponent<AIShoot>().totShots + ", " +
                GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballShotDist + ", " +
                GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballGoalPosx + ", " + GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballGoalPosy;

            System.IO.File.AppendAllText(filePath, txtSave + "\n");

            if (GameObject.Find("Main Camera").GetComponent<PegRemoval>().numReset < GameObject.Find("Main Camera").GetComponent<PegRemoval>().maxNumReset)
            {
                //Debug.Log("Resetting Pegs");
                GameObject.Find("Main Camera").GetComponent<PegRemoval>().ResetPegs();
                GameObject.Find("Main Camera").GetComponent<PegRemoval>().RemovePegs();
                GameObject.Find("Main Camera").GetComponent<PegRemoval>().numReset++;

                GameObject.Find("Goal1").GetComponent<Goal1Score>().Goals1 = goalsPer;
                GameObject.Find("Goal2").GetComponent<Goal2Score>().Goals2 = goalsPer;
                GameObject.Find("Goal3").GetComponent<Goal3Score>().Goals3 = goalsPer;
                GameObject.Find("Goal4").GetComponent<Goal4Score>().Goals4 = goalsPer;
                GameObject.Find("Goal5").GetComponent<Goal5Score>().Goals5 = goalsPer;
                GameObject.Find("Goal6").GetComponent<Goal6Score>().Goals6 = goalsPer;

                GameObject.Find("BinhoBall").GetComponent<playerController>().numPlayers = 6;

                for (int i = 0; i < GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone1.Length; i++)
                {
                    GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone1[i] = 1;
                    GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone3[i] = 1;
                    GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone4[i] = 1;
                    GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone6[i] = 1;
                }
                for (int i = 0; i < GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone2.Length; i++)
                {
                    GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone2[i] = 1;
                    GameObject.Find("Main Camera").GetComponent<buildGoalArrs>().shotGoodZone5[i] = 1;
                }
                for (int i = 0; i < GameObject.Find("BinhoBall").GetComponent<AIShoot>().goalOn.Length; i++)
                {
                    GameObject.Find("BinhoBall").GetComponent<AIShoot>().goalOn[i] = 1;
                }

                GameObject.Find("Goal1").GetComponent<Goal1Score>().GoalBounceOn.gameObject.SetActive(false);
                GameObject.Find("Goal2").GetComponent<Goal2Score>().GoalBounceOn.gameObject.SetActive(false);
                GameObject.Find("Goal3").GetComponent<Goal3Score>().GoalBounceOn.gameObject.SetActive(false);
                GameObject.Find("Goal4").GetComponent<Goal4Score>().GoalBounceOn.gameObject.SetActive(false);
                GameObject.Find("Goal5").GetComponent<Goal5Score>().GoalBounceOn.gameObject.SetActive(false);
                GameObject.Find("Goal6").GetComponent<Goal6Score>().GoalBounceOn.gameObject.SetActive(false);

                GameObject.Find("Goal1").GetComponent<Goal1Score>().Goals = 18;
                GameObject.Find("Goal2").GetComponent<Goal2Score>().Goals = 18;
                GameObject.Find("Goal3").GetComponent<Goal3Score>().Goals = 18;
                GameObject.Find("Goal4").GetComponent<Goal4Score>().Goals = 18;
                GameObject.Find("Goal5").GetComponent<Goal5Score>().Goals = 18;
                GameObject.Find("Goal6").GetComponent<Goal6Score>().Goals = 18;

                GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballGoalPosx = "";
                GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballGoalPosy = "";
                GameObject.Find("BinhoBall").GetComponent<AIShoot>().ballShotDist = "";

                GameObject.Find("BinhoBall").GetComponent<AIShoot>().totShots = 0;
                GameObject.Find("Main Camera").GetComponent<PegRemoval>().winOrient = "";
                GameObject.Find("Goal1").GetComponent<Goal1Score>().whoScored = ""; GameObject.Find("Goal2").GetComponent<Goal2Score>().whoScored = ""; GameObject.Find("Goal3").GetComponent<Goal3Score>().whoScored = ""; GameObject.Find("Goal4").GetComponent<Goal4Score>().whoScored = ""; GameObject.Find("Goal5").GetComponent<Goal5Score>().whoScored = ""; GameObject.Find("Goal6").GetComponent<Goal6Score>().whoScored = "";
            }
            else
            {
                Debug.Log("Time To Simulate " + (GameObject.Find("Main Camera").GetComponent<PegRemoval>().maxNumReset + 1) + " Games: " + Mathf.FloorToInt(Time.realtimeSinceStartup / 60f) + "Minutes " + Mathf.Round(Time.realtimeSinceStartup % 60f) + "Seconds");
                Debug.Log("Quit");
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
        }
    }
}