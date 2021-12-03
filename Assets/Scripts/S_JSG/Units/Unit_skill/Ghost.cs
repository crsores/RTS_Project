using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.Player;

namespace Units.Player
{
    public class Ghost : MonoBehaviour
    {
        public GameObject ghost;
        void Start()
        {
            if (playerManager.instance.GhosteyeCheck == false)
            {
                ghost.GetComponent<PlayerUnit>().baseStats.eyesight = 9;
            }
            else
            {
                ghost.GetComponent<PlayerUnit>().baseStats.eyesight = 11;
            }

            if (playerManager.instance.GhosMpCheck == false)
            {
                ghost.GetComponent<PlayerUnit>().baseStats.maxmp = 200;
                ghost.GetComponent<PlayerUnit>().baseStats.mp = 50;

            }
            else
            {
                ghost.GetComponent<PlayerUnit>().baseStats.maxmp = 250;
                ghost.GetComponent<PlayerUnit>().baseStats.mp = 62.5f;

            }


        }

        // Update is called once per frame
        void Update()
        {


        }
        public void mpup()
        {
            playerManager.instance.GhosMpCheck = true;

            ghost.GetComponent<PlayerUnit>().baseStats.maxmp = 250;



        }
        public void eyesightup()
        {
            playerManager.instance.GhosteyeCheck = true;

            ghost.GetComponent<PlayerUnit>().baseStats.eyesight = 11;
           

        }


    }
}
