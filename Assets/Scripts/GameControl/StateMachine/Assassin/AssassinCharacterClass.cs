using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AssassinCharacterClass : MonoBehaviour, IEquatable<AssassinCharacterClass>, IComparable<AssassinCharacterClass>

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
    public int CompareTo(AssassinCharacterClass other)
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
        AssassinCharacterClass tmp = obj as AssassinCharacterClass;

        return myID == tmp.myID;
    }
    //*************************
    public override int GetHashCode()
    {
        return myID;
    }
    //*************************
    public bool Equals(AssassinCharacterClass other)
    {
        return myID == other.myID;
    }

}
