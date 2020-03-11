﻿using System;
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

        private readonly float gravity = -10f;

        public void Attract(Attractee bodyAttracted)
        {
            var body = bodyAttracted.GetComponent<Rigidbody>();

            var planetCenter = transform.position;
            var gravityNormal = (body.position - planetCenter).normalized;
            var gravitationalForce = gravityNormal * this.gravity;

            body.AddForce(gravitationalForce);

            var rotationAmount = Quaternion.FromToRotation(body.transform.up, gravityNormal);
            var targetRotation = body.transform.rotation * rotationAmount;
            var currentRotatino = body.transform.rotation;
            var speed = 5f;
            bodyAttracted.transform.rotation = Quaternion.Slerp(currentRotatino, targetRotation, Time.deltaTime * speed);
        }

        public void AddAttractee(Attractee attractee) =>
            UpdateManager.Instance.GlobalFixedUpdate += delegate { this.Attract(attractee); };
    }
}