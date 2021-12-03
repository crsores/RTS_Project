using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



namespace Units {
    public class UnitStatDisplay : MonoBehaviour
    {
        public float maxHealth, armor, currentHealth,maxmp,mp;
        [SerializeField] private Image healthBarAmount;

        private bool isPlayerUnit = false;
        void Start()
        {
           
        }
        public void SetStatatDisplayUnit(UnitStatType.Base stats, bool isPlayer)
        {
            maxHealth = stats.health;
            armor = stats.armor;
            isPlayerUnit = isPlayer;
            maxmp = stats.maxmp;
            mp = stats.mp;


            currentHealth = maxHealth;
        }

        public void SetStatDisplayBasicBuilding(Building.BuildingStatType.Base stats, bool isPLayer)
        {
            maxHealth = stats.health;
            armor = stats.armor;
            isPlayerUnit = isPLayer;
            

            currentHealth = maxHealth;
        }
        // Update is called once per frame
        void Update()
        {
            HandleHealth();
        }
        public void TakeDamage(float damage)
        {
            float totalDamage = damage - armor;
            currentHealth -= totalDamage;
        }
        private void HandleHealth()
        {
            Camera camera = Camera.main;
            gameObject.transform.LookAt(gameObject.transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
            healthBarAmount.fillAmount = currentHealth / maxHealth;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        private void Die()
        {
            if (isPlayerUnit)
            {
                InputManager.InputHandler.instance.selectedUnits.Remove(gameObject.transform.parent);
                Destroy(gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

}
