using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;

namespace CustomScripts.Entities
{
    public class Planet : MonoBehaviour
    {
        #region Singleton
        public static Planet Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        #endregion

        public Vector3 Position { get => transform.position; }
        public float Radius { get => transform.localScale.x / 2; }
        private readonly float gravity = -10f;

        public void Attract(Attractee bodyAttracted)
        {
            var body = bodyAttracted.GetComponent<Rigidbody>();

            var planetCenter = transform.position;
            var gravityNormal = (body.position - planetCenter).normalized;
            var gravitationalForce = gravityNormal * this.gravity;

            body.AddForce(gravitationalForce);

            //the below code was causing the wobbling error, but why?
            //var rotationAmount = Quaternion.FromToRotation(body.transform.up, gravityNormal);
            //body.rotation = body.rotation * rotationAmount;

            //below works
            var rotationAmount = Quaternion.FromToRotation(body.transform.up, gravityNormal);
            var targetRotation = rotationAmount * body.rotation;
            body.rotation = targetRotation;
        }

        public void ExertGravity(Attractee attractee) =>
            UpdateManager.Instance.GlobalFixedUpdate += delegate { this.Attract(attractee);
    };
}
}
