using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;

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
            UpdateManager.Instance.GlobalUpdate += this.Rotate;
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

            var smooth = 300f;
            transform.Rotate(0, horizontal * smooth * Time.deltaTime, 0, Space.Self);
        }

        public void Move()
        {
            var localDir = this.GetMovement();
            var worldDir = transform.TransformDirection(localDir);
            var targetPosition = rigidbody.position + worldDir * this.speed * Time.deltaTime;
            this.rigidbody.MovePosition(targetPosition);
        }
    }
}
