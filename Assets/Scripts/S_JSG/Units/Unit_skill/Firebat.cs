using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Units.Player
{

    public class Firebat : MonoBehaviour
    {

        public GameObject firebat;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
                stempack();
        }
        public void stempack()
        {
            if (firebat.GetComponent<PlayerUnit>().statDisplay.currentHealth <= 10)
            {
                Debug.Log("Ã¼·ÂÀÌ ³·½À´Ï´Ù");
                return;

            }

            else if (firebat.GetComponent<PlayerUnit>().statDisplay.currentHealth > 10)
            {
                firebat.GetComponent<PlayerUnit>().atkspeed -= firebat.GetComponent<PlayerUnit>().atkspeed * 0.5f;
                firebat.GetComponent<PlayerUnit>().speed += firebat.GetComponent<PlayerUnit>().speed * 0.5f;
                firebat.GetComponent<PlayerUnit>().statDisplay.currentHealth -= 10;
                Debug.Log("½ºÆÀÆÑ");

                Invoke("returnstem", 12.33f);
            }


        }
        public void returnstem()
        {
            firebat.GetComponent<PlayerUnit>().atkspeed += firebat.GetComponent<PlayerUnit>().atkspeed * 0.5f;
            firebat.GetComponent<PlayerUnit>().speed -= firebat.GetComponent<PlayerUnit>().speed * 0.5f;
        }

    }
}