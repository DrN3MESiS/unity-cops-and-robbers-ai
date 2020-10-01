using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacClass : MonoBehaviour, IEquatable<CharacClass>, IComparable<CharacClass>

{
    public enum LocationsM
    {
        mine,
        bank,
        saloon,
        restroom,
        home
    }
    public enum LocationsW
    {
        bedroom,
        livingroom,
        garden,
        restroom,
        kitchen
    }
    public GlobalMovement moveCod;
    int myID;
    public enum enumCharac
    {
        Bob,
        Elsa
    }
    //*************************
    public void SetID(int val)
    {
        myID = val;
    }
    //*************************
    public int getID()
    {
        return (myID);
    }
    //*************************
    //*************************
    public int CompareTo(CharacClass other)
    {
        if (other == null)
        {
            return 1;
        }

        //return id
        return myID - other.myID;
    }
    //*************************
    public override bool Equals(System.Object obj)
    {
        CharacClass tmp = obj as CharacClass;

        return myID == tmp.myID;
    }
    //*************************
    public override int GetHashCode()
    {
        return myID;
    }
    //*************************
    public bool Equals(CharacClass other)
    {
        return myID == other.myID;
    }

}
