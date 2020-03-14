using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;

namespace CustomScripts.Entities
{
    public class Asteroid : MonoBehaviour
    {
        private Vector3 movement;
        private void OnEnable()
        {
            this.movement = GetDirection();

            UpdateManager.Instance.GlobalFixedUpdate += this.Fall;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger");
            if (other.GetComponent<Planet>())
                StopFalling();

            void StopFalling() => 
                UpdateManager.Instance.GlobalFixedUpdate -= this.Fall;
        }

        private Vector3 GetDirection()
        {
            var planetCenter = Planet.Instance.transform.position;
            var fallDir = (planetCenter - transform.position).normalized;
            return fallDir;
        }
        
        [SerializeField] private float fallingSpeed = 15f;
        private void Fall()
        {
            transform.position += this.movement * this.fallingSpeed * Time.fixedDeltaTime;
        }
    }
}
