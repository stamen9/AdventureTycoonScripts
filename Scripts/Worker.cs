using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//what the f is the point of this class?
//I mean it's logical that all workers inherit a base worker class
//but the different workers share no stats :S
//if all workers have exactly 3 skills there won't be a need to have them in seperate classes?
//Alternative have all worker have all skills available?
//I'll stick to shity inheretence for now but should consider changing it
public class Worker 
{
    public string Name;
    public int Pay;
}

public class GuildHallWorker: Worker
{
    public float BuildingReasearch;
    public int BarterSkill;
    //determines what missions are available?
    //have it per mission type?
    public int Connections;

    public GuildHallWorker()
    {
        BuildingReasearch = 0f;
        BarterSkill = 1;
        Connections = 1;
    }
    public GuildHallWorker(float _bR, int _bS, int _c)
    {
        BuildingReasearch = _bR;
        BarterSkill = _bS;
        Connections = _c;
    }
}

public class BlacksmithWorker: Worker
{
    public float BlacksmithReasearch;
    public int BlacksmithSkill;
    //determines what missions are available?
    //have it per mission type?
    public int BlacksmithSpeed;

    public BlacksmithWorker()
    {
        BlacksmithReasearch = 0f;
        BlacksmithSkill = 1;
        BlacksmithSpeed = 1;
    }
    public BlacksmithWorker(float _bR, int _bS, int _s)
    {
        BlacksmithReasearch = _bR;
        BlacksmithSkill = _bS;
        BlacksmithSpeed = _s;
    }
}