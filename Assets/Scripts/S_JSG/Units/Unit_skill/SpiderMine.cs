using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Units.Player
{

    public class SpiderMine : MonoBehaviour
    {
        public GameObject Mine;

        public Collider[] rangeColliders;//private

        public float eyesight;
        public Transform aggerTarget; //private
        public Enemy.enemyUnit atkUnit;

        public List<GameObject> target = new List<GameObject>();


        Vector3 moveDir = Vector3.zero; //에너미의 이동방향

        enum mode
        {
            stop,
            chase
        }

        mode _Mode = mode.stop;
        void Start()
        {
            eyesight = 3;

        }

        // Update is called once per frame
        void Update()
        {

            checkForEnemyTargets();

            minechangMode();

            mineaction();

            this.transform.position += moveDir * Time.deltaTime*7.5f;
        }

        public void OnCollisionEnter(Collision collision)
        {

           // collision.gameObject.GetComponent<UnitStatDisplay>().TakeDamage(Mine.GetComponent<PlayerUnit>().baseStats.attack);
            Destroy(gameObject);


        }
        private void checkForEnemyTargets() //범위안 타겟찾음
        {
            rangeColliders = Physics.OverlapSphere(transform.position, eyesight, UnitHandler.instance.eUnitLayer);

            for (int i = 0; i < rangeColliders.Length;)
            {
                

                //aggerTarget = rangeColliders[i].gameObject.transform;
                //// aggroUnit = aggerTarget.gameObject.GetComponentInChildren<UnitStatDisplay>();
                //atkUnit = aggerTarget.gameObject.GetComponent<Enemy.enemyUnit>();
                if (rangeColliders[i].GetComponent<Enemy.enemyUnit>().baseStats.ground==true&& rangeColliders[i].GetComponent<Enemy.enemyUnit>().baseStats.floating == false)
                {
                    aggerTarget = rangeColliders[i].gameObject.transform;
                    atkUnit = aggerTarget.gameObject.GetComponent<Enemy.enemyUnit>();
                }
                

                //hasAggero = true;
                break;
            }
        }

        void minechangMode()
        {
            if (atkUnit == null)
            {

                _Mode = mode.stop;

            }
            else
            {
                _Mode = mode.chase;

            }



        }

        void mineaction()
        {
            switch (_Mode)
            {
                case mode.chase:
                    moveDir = (atkUnit.transform.position - this.transform.position).normalized;
                    break;

                case mode.stop:
                    moveDir = Vector3.zero;
                    break;

            }


        }

    }

}