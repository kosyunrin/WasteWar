using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SK;

public class CSPlayerBase : MonoBehaviour
{
    protected int mID;
 
    public virtual void SetMoveMonoID(int ID)
    {
        mID = ID;
    }
    public virtual int GetMoveMonoID()
    {
        return mID;
    }
}
