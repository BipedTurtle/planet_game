using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;
using CustomScripts.Entities;

namespace CustomScripts.Utility
{
    public class CustomCamera : MonoBehaviour
    {
        public static CustomCamera Instance { get; private set; }
        private Camera main;

        private void Awake()
        {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            this.main = Camera.main;
        }

        private void Start()
        {
            UpdateManager.Instance.GlobalLateUpdate += this.TrackPlayer;
        }

        [SerializeField] private float offset = 25f;
        [SerializeField] private Transform tracking;
        private void TrackPlayer()
        {
            var camera = this.main.transform;
            camera.position = tracking.position + tracking.transform.up * this.offset;

            var lookDirection = tracking.position - transform.position;
            var rotationAmount = Quaternion.FromToRotation(camera.forward, lookDirection);
            var targetRotaiton = rotationAmount * camera.rotation;
            camera.rotation = targetRotaiton;
        }

        public void Rotate(float rotation)
        {
            this.main.transform.Rotate(0, 0, -rotation, Space.Self);
        }
    }
}
