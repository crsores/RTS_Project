using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.HUD
{
    public class ActionTimer : MonoBehaviour
    {
        public static ActionTimer instance = null;

        private void Awake()
        {
            instance = this;
        }

        //public IEnumerator SpawnQueueTimer()
        //{
        //    if (Interactables.IBuilding.instance.SpawnQueue.Count > 0)
        //    {
        //        Debug.Log($"Wating for {Interactables.IBuilding.instance.SpawnQueue[0]}");

        //        yield return new WaitForSeconds(Interactables.IBuilding.instance.SpawnQueue[0]);
        //        Interactables.IBuilding.instance.SpawnObject();

        //        Interactables.IBuilding.instance.SpawnQueue.Remove(Interactables.IBuilding.instance.SpawnQueue[0]);

        //        if (Interactables.IBuilding.instance.SpawnQueue.Count > 0)
        //        {
        //            StartCoroutine(SpawnQueueTimer());
        //        }
        //    }
        //}

    }
}
