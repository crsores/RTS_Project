using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.Player;

namespace Units.Player
{

    public class Wraith : MonoBehaviour
    {
        public GameObject wraith;
        void Start()
        {
            if (playerManager.instance.WraithMpCheck == false)
            {
                wraith.GetComponent<PlayerUnit>().baseStats.maxmp = 200;
                wraith.GetComponent<PlayerUnit>().baseStats.mp = 50;
            }
            else
            {
                wraith.GetComponent<PlayerUnit>().baseStats.maxmp = 250;
                wraith.GetComponent<PlayerUnit>().baseStats.mp = 62.5f;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void mpup()
        {

            playerManager.instance.WraithMpCheck = true;
            wraith.GetComponent<PlayerUnit>().baseStats.maxmp = 250;
            
           
        }
    }

}