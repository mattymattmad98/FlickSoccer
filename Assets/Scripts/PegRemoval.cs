using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegRemoval : MonoBehaviour
{
    public GameObject[] objectsAllPegs;
    public GameObject[] activePegs;
    public GameObject[] rowFivePegs;
    public GameObject onPeg;
    public GameObject[] totalActivePegs;
    public GameObject[] allPegs;

    public int[] goalOrient1;
    public int[] goalOrient2;
    public int[] goalOrient3;
    public int[] goalOrient4;
    public int[] goalOrient5;
    public int[] goalOrient6;

    public int numReset = 0;
    public int maxNumReset = 100;

    public string[] gameWinners;
    public int gameWinner;
    public string winOrient;
    public string goalPosSave;

    public string goal1;
    public string goal2;
    public string goal3;
    public string goal4;
    public string goal5;
    public string goal6;

    public int[] goal1arr;
    public int[] goal2arr;
    public int[] goal3arr;
    public int[] goal4arr;
    public int[] goal5arr;
    public int[] goal6arr;

    void Awake()
    {
        allPegs = new GameObject[150];
        goalSetup();
        objectsAllPegs = GameObject.FindGameObjectsWithTag("Peg");
        gameWinners = new string[maxNumReset+1];
        winOrient = "";
        goalPosSave = "";

        int j = 0;
        for (int i = 0; i < objectsAllPegs.Length; i++)
        {
            foreach (Transform eachChild in objectsAllPegs[i].GetComponentsInChildren<Transform>())
            {
                if (eachChild.gameObject != objectsAllPegs[i])
                {
                    allPegs[j] = eachChild.gameObject;
                    j++;
                }
            }
        }

        RemovePegs();
    }

    public void ResetPegs()
    {
        //Debug.Log(allPegs.Length);
        for (int i = 0; i < allPegs.Length; i++)
        {
            allPegs[i].SetActive(true);
        }
    }

    public void goalSetup()
    {
        goalOrient1 = new int[25];
        goalOrient2 = new int[25];
        goalOrient3 = new int[25];
        goalOrient4 = new int[25];
        goalOrient5 = new int[25];
        goalOrient6 = new int[25];

        for (int i = 0; i < goalOrient1.Length; i++)
        {
            goalOrient1[i] = 0;
            goalOrient2[i] = 0;
            goalOrient3[i] = 0;
            goalOrient4[i] = 0;
            goalOrient5[i] = 0;
            goalOrient6[i] = 0;
        }
    }

    public void RemovePegs()
    {
        goalSetup();
        //Debug.Log("New Peg Orientation");
        activePegs = new GameObject[25];
        rowFivePegs = new GameObject[5];
        totalActivePegs = new GameObject[42];

        goal1arr = new int[25];
        goal2arr = new int[25];
        goal3arr = new int[25];
        goal4arr = new int[25];
        goal5arr = new int[25];
        goal6arr = new int[25];
        
        int totalPegs = 0;
        for (int i = 0; i < objectsAllPegs.Length; i++)
        {
            int j = 0;
            int z = 0;
            foreach (Transform eachChild in objectsAllPegs[i].GetComponentsInChildren<Transform>())
            {
                if (eachChild.gameObject != objectsAllPegs[i])
                {
                    activePegs[j] = eachChild.gameObject;
                    if (activePegs[j].tag == "peg_row5")
                    {
                        rowFivePegs[z] = activePegs[j];
                        z++;
                    }
                    activePegs[j].SetActive(false);
                    j++;
                }
            }

            int onPegs = 0;
            //int onPegnum1 = 0;
            //int onPegnum2 = 0;
            //int onPegEl = 0;
            int randNum = 0;
            int row1count = 0;
            int row2count = 0;
            int row3count = 0;
            int row4count = 0;
            int row5count = 0;
            int row12count = 0;
            int row23count = 0;
            int row34count = 0;
            int row45count = 0;
            while (onPegs < 7)
            {
                //need to push towards 1 then 2 then 3 then 4 then 5 - change random to a different distribution
                //probably need way to make sure middle three colums are filled always as well - Machine Learning AI that learns best orientation could be better - ask Adam
                
                if (onPegs == 6 && row5count == 0)
                {

                    randNum = Random.Range(0, rowFivePegs.Length);
                    rowFivePegs[randNum].gameObject.SetActive(true);
                    totalActivePegs[totalPegs] = rowFivePegs[randNum].gameObject;
                    onPeg = rowFivePegs[randNum].gameObject;

                    onPegs++;
                    totalPegs++;
                    //Debug.Log("# Pegs ON: " + onPegs + "   Peg On: " + rowFivePegs[randNum]);
                }
                else
                {
                    randNum = Random.Range(0, activePegs.Length);
                    onPeg = activePegs[randNum].gameObject;

                    if (onPeg.activeInHierarchy)
                    {
                        //Debug.Log("Trying To Activate Again: " + onPeg + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                    else
                    { 

                        if (onPeg.tag == "peg_row1" && row1count < 3 && row12count < 4)
                        {
                            onPeg.gameObject.SetActive(true);
                            row1count++;
                            row12count++;
                            onPegs++;
                            totalActivePegs[totalPegs] = onPeg.gameObject;
                            totalPegs++;
                            //Debug.Log("# Pegs ON: " + onPegs + "   Peg: " + onPeg);
                        }
                        else if (onPeg.tag == "peg_row2" && row2count < 3 && row12count < 4 && row23count < 4)
                        {
                            onPeg.gameObject.SetActive(true);
                            row2count++;
                            row12count++;
                            row23count++;
                            onPegs++;
                            totalActivePegs[totalPegs] = onPeg.gameObject;
                            totalPegs++;
                            //Debug.Log("# Pegs ON: " + onPegs + "   Peg: " + onPeg);
                        }
                        else if (onPeg.tag == "peg_row3" && row3count < 3 && row23count < 4 && row34count < 4)
                        {
                            onPeg.gameObject.SetActive(true);
                            row3count++;
                            row23count++;
                            row34count++;
                            onPegs++;
                            totalActivePegs[totalPegs] = onPeg.gameObject;
                            totalPegs++;
                            //Debug.Log("# Pegs ON: " + onPegs + "   Peg: " + onPeg);
                        }
                        else if (onPeg.tag == "peg_row4" && row4count < 3 && row34count < 4 && row45count < 4)
                        {
                            onPeg.gameObject.SetActive(true);
                            row4count++;
                            row34count++;
                            row45count++;
                            onPegs++;
                            totalActivePegs[totalPegs] = onPeg.gameObject;
                            totalPegs++;
                            //Debug.Log("# Pegs ON: " + onPegs + "   Peg: " + onPeg);
                        }
                        else if (onPeg.tag == "peg_row5" && row5count < 3 && row45count < 4)
                        {
                            onPeg.gameObject.SetActive(true);
                            row5count++;
                            row45count++;
                            onPegs++;
                            totalActivePegs[totalPegs] = onPeg.gameObject;
                            totalPegs++;
                            //Debug.Log("# Pegs ON: " + onPegs + "   Peg: " + onPeg);
                        }
                    }
                }
                //Debug.Log("peg# " + i + "    Num Pegs On: " + onPegs + "   PegName: " + onPeg.name);
            }
        }

        int jj = 0;
        int onPegEl = 0;
        foreach (Transform eachChild in GameObject.Find("Pegs_1").GetComponentsInChildren<Transform>())
        {
            if (eachChild.gameObject != GameObject.Find("Pegs_1"))
            {
                onPeg = eachChild.gameObject;
                //Debug.Log(jj + ": " + onPeg.name);
                jj++;

                char onPegnum1char = onPeg.name[1];
                char onPegnum2char = onPeg.name[2];
                int onPegnum1 = onPegnum1char - '0';
                int onPegnum2 = onPegnum2char - '0';
                onPegEl = (onPegnum1 - 1) * 5 + onPegnum2 - 1;

                goal1arr[onPegEl] = 1;
            }
        }

        jj = 0;
        onPegEl = 0;
        foreach (Transform eachChild in GameObject.Find("Pegs_2").GetComponentsInChildren<Transform>())
        {
            if (eachChild.gameObject != GameObject.Find("Pegs_2"))
            {
                onPeg = eachChild.gameObject;
                //Debug.Log(jj + ": " + onPeg.name);
                jj++;

                char onPegnum1char = onPeg.name[1];
                char onPegnum2char = onPeg.name[2];
                int onPegnum1 = onPegnum1char - '0';
                int onPegnum2 = onPegnum2char - '0';
                onPegEl = (onPegnum1 - 1) * 5 + onPegnum2 - 1;

                goal2arr[onPegEl] = 1;
            }
        }

        jj = 0;
        onPegEl = 0;
        foreach (Transform eachChild in GameObject.Find("Pegs_3").GetComponentsInChildren<Transform>())
        {
            if (eachChild.gameObject != GameObject.Find("Pegs_3"))
            {
                onPeg = eachChild.gameObject;
                //Debug.Log(jj + ": " + onPeg.name);
                jj++;

                char onPegnum1char = onPeg.name[1];
                char onPegnum2char = onPeg.name[2];
                int onPegnum1 = onPegnum1char - '0';
                int onPegnum2 = onPegnum2char - '0';
                onPegEl = (onPegnum1 - 1) * 5 + onPegnum2 - 1;

                goal3arr[onPegEl] = 1;
            }
        }

        jj = 0;
        onPegEl = 0;
        foreach (Transform eachChild in GameObject.Find("Pegs_4").GetComponentsInChildren<Transform>())
        {
            if (eachChild.gameObject != GameObject.Find("Pegs_4"))
            {
                onPeg = eachChild.gameObject;
                //Debug.Log(jj + ": " + onPeg.name);
                jj++;

                char onPegnum1char = onPeg.name[1];
                char onPegnum2char = onPeg.name[2];
                int onPegnum1 = onPegnum1char - '0';
                int onPegnum2 = onPegnum2char - '0';
                onPegEl = (onPegnum1 - 1) * 5 + onPegnum2 - 1;

                goal4arr[onPegEl] = 1;
            }
        }

        jj = 0;
        onPegEl = 0;
        foreach (Transform eachChild in GameObject.Find("Pegs_5").GetComponentsInChildren<Transform>())
        {
            if (eachChild.gameObject != GameObject.Find("Pegs_5"))
            {
                onPeg = eachChild.gameObject;
                //Debug.Log(jj + ": " + onPeg.name);
                jj++;

                char onPegnum1char = onPeg.name[1];
                char onPegnum2char = onPeg.name[2];
                int onPegnum1 = onPegnum1char - '0';
                int onPegnum2 = onPegnum2char - '0';
                onPegEl = (onPegnum1 - 1) * 5 + onPegnum2 - 1;

                goal5arr[onPegEl] = 1;
            }
        }

        jj = 0;
        onPegEl = 0;
        foreach (Transform eachChild in GameObject.Find("Pegs_6").GetComponentsInChildren<Transform>())
        {
            if (eachChild.gameObject != GameObject.Find("Pegs_6"))
            {
                onPeg = eachChild.gameObject;
                //Debug.Log(jj + ": " + onPeg.name);
                jj++;

                char onPegnum1char = onPeg.name[1];
                char onPegnum2char = onPeg.name[2];
                int onPegnum1 = onPegnum1char - '0';
                int onPegnum2 = onPegnum2char - '0';
                onPegEl = (onPegnum1 - 1) * 5 + onPegnum2 - 1;

                goal6arr[onPegEl] = 1;
            }
        }

        goal1 = ""; goal2 = ""; goal3 = ""; goal4 = ""; goal5 = ""; goal6 = "";
        for (int i = 0; i < goal1arr.Length; i++)
        {
            goal1 = goal1 + "" + goal1arr[i]; goal2 = goal2 + "" + goal2arr[i]; goal3 = goal3 + "" + goal3arr[i]; goal4 = goal4 + "" + goal4arr[i]; goal5 = goal5 + "" + goal5arr[i]; goal6 = goal6 + "" + goal6arr[i];
        }

    }

}
