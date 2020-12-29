using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechNode 
{

    /*public class TechEvnetArgs : EventArgs
    {
        private bool v;

        public TechEvnetArgs(bool v)
        {
            this.v = v;
        }
    }*/

    public bool isTaken = false;

    public event EventHandler OnNodeTaken;

    public void Subscribe(EventHandler Function)
    {
        OnNodeTaken += Function;
    }

}
