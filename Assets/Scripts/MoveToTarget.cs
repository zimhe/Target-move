using System.Collections;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework.Internal.Commands;
using UnityEngine;
using Color = UnityEngine.Color;

public class MoveToTarget : MonoBehaviour
{

    public GameObject Prefab;

    private GameObject Target;

    private GameObject Agent;

    private Vector3 TargetPosition;

    private Vector3 AgentPosition;

    Quaternion rotation = Quaternion.identity;

    private int TargetX;
    private int AgentX;
    private int AgentXBeginX;

    private int TargetY;
    private int AgentY;
    private int AgentYBeginY;

    private int TargetZ;
    private int AgentZ;
    private int AgentZBeginZ;

    private int distanceX;
    private int beginDistanceX;

    private int distanceY;
    private int beginDistanceY;

    private int distanceZ;
    private int beginDistanceZ;

    private float beginDistance3d;
    private bool set;
    private bool reached = false;

    private int cellCount;

    int runTimes = 0;

    private int thershold = 5;

    private List<GameObject> paths = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        TargetX = (int)Random.Range(40, 50);
        AgentXBeginX = (int)Random.Range(0, 10);

        TargetY = 60;
        AgentYBeginY = 0;

        TargetZ = (int)Random.Range(40, 50);
        AgentZBeginZ = (int)Random.Range(0, 10);

        TargetPosition = new Vector3(TargetX, TargetY, TargetZ);
        Target = Instantiate(Prefab, TargetPosition, rotation);

        Target.GetComponent<MeshRenderer>().material.color = Color.red;

        beginDistanceX = TargetX - AgentXBeginX;
        beginDistanceY = TargetY - AgentYBeginY;
        beginDistanceZ = TargetZ - AgentZBeginZ;

        Vector3 AgentBeginPosition = new Vector3(AgentXBeginX, AgentYBeginY, AgentZBeginZ);

        beginDistance3d = Vector3.Distance(TargetPosition, AgentBeginPosition);

        ResetAgent();
    }

    // Update is called once per frame
    void Update()
    {
        if (runTimes < 10)
        {
            if (reached == true)
            {
                runTimes++;
                if (runTimes < 10)
                {
                    ResetAgent();
                }
            }
            calculatePath();
           
        }
    }

    void  calculatePath()
    {
            int growDirection;

            if (distanceX == 0 && distanceY == 0 && distanceZ == 0)
            {
                reached = true;
            }
            else
            {
                reached = false;

            }
        if (set == true && reached == false)
        {
            growDirection = (int) Random.Range(0, 3);

            if (growDirection == 0)
            {
                GrowX();
            }
            if (growDirection == 1)
            {
                GrowY();
            }
            if (growDirection == 2)
            {
                GrowZ();
            }
        }

    }
    void ResetAgent()
    {
        AgentX = AgentXBeginX;
        AgentY = AgentYBeginY;
        AgentZ = AgentZBeginZ;

        AgentPosition = new Vector3(AgentX, AgentY, AgentZ);
        Agent = Instantiate(Prefab, AgentPosition, rotation);

        Agent.GetComponent<MeshRenderer>().material.color = Color.green;

        distanceX = TargetX - AgentX;
        distanceY = TargetY - AgentY;
        distanceZ = TargetZ - AgentZ;

        GameObject path = new GameObject();

        
        path.name = "Path " + runTimes;

        paths.Add(path);

        Agent.transform.SetParent(path.transform);

        set = true;

        reached = false;
    }

    void GrowX()
    {
        
        if (distanceX != 0&&AgentX >=0&&AgentX <=200)
        {
            distanceX = TargetX - AgentX;

            if (distanceX < 0)
            {
                AgentX--;
            }
            if (distanceX > 0)
            {
                AgentX++;
            }

            newAgent();
        }
    }

    void GrowY()
    {
        if (distanceY != 0)
        {
            distanceY = TargetY - AgentY;

            if (distanceY < 0)
            {
                AgentY--;
            }
            if (distanceY > 0)
            {
                AgentY++;
            }
            newAgent();

        }
    }

    void GrowZ()
    {
        if (distanceZ != 0 && AgentZ  >= 0 && AgentZ <= 200)
        {
            distanceZ = TargetZ - AgentZ;

            if (distanceZ < 0)
            {
                AgentZ--;
            }
            if (distanceZ > 0)
            {
                AgentZ++;
            }
            newAgent();
        }
    }

    void newAgent()
    {
        AgentPosition = new Vector3(AgentX, AgentY, AgentZ);
        float currentDistance3d = Vector3.Distance(TargetPosition, AgentPosition);
        float t = currentDistance3d / beginDistance3d;
        Agent = Instantiate(Prefab, AgentPosition, rotation);
        Agent.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.red, Color.green, t);
        cellCount++;
        Agent.transform.SetParent(paths[runTimes].transform);
    }
}
