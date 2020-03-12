using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;
using CustomScripts.Utility;

namespace CustomScripts.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 3f;
        private new Rigidbody rigidbody;
        private void Start()
        {
            this.rigidbody = GetComponent<Rigidbody>();

            UpdateManager.Instance.GlobalFixedUpdate += this.Move;
            UpdateManager.Instance.GlobalFixedUpdate += this.Rotate;
        }

        private Vector3 GetMovement()
        {
            var verticalMovement = Input.GetAxisRaw("Vertical");
            return new Vector3(0, 0, verticalMovement);
        }

        private void Rotate()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            if (horizontal == 0)
                return;

            var smooth = 100f;
            var rotation = horizontal * smooth * Time.fixedDeltaTime;
            var lookRotation = Quaternion.LookRotation(horizontal * transform.right, transform.up);
            rigidbody.rotation = Quaternion.RotateTowards(rigidbody.rotation, lookRotation, smooth * Time.fixedDeltaTime);
            //transform.Rotate(0, rotation, 0, Space.Self);
            CustomCamera.Instance.Rotate(rotation);
        }

        public void Move()
        {
            var localDir = this.GetMovement();
            var worldDir = transform.TransformDirection(localDir);
            var targetPosition = rigidbody.position + worldDir * this.speed * Time.fixedDeltaTime;
            this.rigidbody.MovePosition(targetPosition);
        }
    }
}
