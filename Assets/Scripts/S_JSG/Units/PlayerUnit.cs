using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Units.Player
{
    public class PlayerUnit : MonoBehaviour
    {
        public UnitStatDisplay statDisplay;

        public BasicUnit unitType;


        public UnitStatType.Base baseStats;

        private Camera camera;

        

        

        private Vector3 destination;

      

        public float speed; 


        private bool isMove;

        private void Awake()
        {
            camera = Camera.main;
        }

        // Start is called before the first frame update
        void Start()
        {
            baseStats = unitType.baseStats;
            statDisplay.SetStatatDisplayUnit(baseStats, true);
            
        }

        // Update is called once per frame
        void Update()
        {
            MoveUnit();
           
        }
        public void SetDestinatin(Vector3 dest) //목표지점
        {
            destination = dest;
            isMove = true;
        }
        public void MoveUnit()
        {
            if (isMove)
            {
                var dir = destination - transform.position;
                transform.position += dir.normalized * Time.deltaTime * 5;
            }
            if (Vector3.Distance(transform.position, destination) <= 0.1f)
            {
                isMove = false;

            }
        }
       

       
        }
        
    }


