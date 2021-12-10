using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObsTest : MonoBehaviour
{


    Node Obs;
    Node OldObs;
    void Start()
    {

        OldObs = Grid.gridinstance.NodePoint(Vector3.zero,1);
    }


    // Update is called once per frame
    void Update()
    {
          

        Obs = Grid.gridinstance.NodePoint(this.transform.position, 1);
        Obs.walkable = false;
        Debug.Log(" Obs : " + Obs.gridX + " : " + Obs.gridY);
        Debug.Log(" Obs : " + Obs.walkable);

        if (Obs != OldObs)
        {
            OldObs.walkable = true;
            OldObs = Obs;
        }
        Debug.Log(" ObsOld : " + OldObs.gridX + " : " + OldObs.gridY);
        Debug.Log(" ObsOld : " + OldObs.walkable);
    }

    IEnumerator SetOld()
    {
        yield return null;
        OldObs = Grid.gridinstance.NodePoint(this.transform.position, 1);
    }


}
