using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.Player;


namespace Units.Player
{
    public class Vulture : MonoBehaviour
    {
        public GameObject vulture;

        public GameObject mine;

        void Start()
        {
            if (playerManager.instance.VulturespeedCheck == false)
            {
                vulture.GetComponent<PlayerUnit>().baseStats.speed = 3.126f;

            }
            else
            {
                vulture.GetComponent<PlayerUnit>().baseStats.speed = 4.688f;
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void speedup()
        {

            playerManager.instance.VulturespeedCheck = true;
            vulture.GetComponent<PlayerUnit>().baseStats.speed = 4.688f;
            



        }

        public void UseSpiderMines( )
        {

            Instantiate(mine, transform.position,Quaternion.identity);

        }



    }
}

