using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;
using System.Collections;

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

        private void OnTriggerStay(Collider other)
        {
            var toCenterSqrdDistance = Vector3.SqrMagnitude((Planet.Instance.Position - transform.position));
            var isWithinStoppingDistance = toCenterSqrdDistance <= Mathf.Pow(Planet.Instance.Radius, 2);
            var hitPlanet = other.GetComponent<Planet>();

            if (hitPlanet && isWithinStoppingDistance)
                StartCoroutine(this.Decompose());
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

        [SerializeField] private float decomposeAfter = 5f;
        private IEnumerator Decompose()
        {
            this.StopFalling();
            yield return new WaitForSeconds(this.decomposeAfter);
            Destroy(gameObject);
        }

        private void StopFalling() => UpdateManager.Instance.GlobalFixedUpdate -= this.Fall;

    }
}
