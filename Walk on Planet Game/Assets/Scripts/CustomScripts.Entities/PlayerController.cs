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
        }

        private Vector3 GetMovement()
        {
            var horizontalMovement = Input.GetAxisRaw("Horizontal");
            var verticalMovement = Input.GetAxisRaw("Vertical");
            return new Vector3(horizontalMovement, 0, verticalMovement);
        }

        private void Rotate(Vector3 lookDir)
        {
            //transform.rotation = Quaternion.LookRotation(lookDir);
        }

        public void Move()
        {
            var localMovement = this.GetMovement();
            var worldMovement = transform.TransformDirection(localMovement);
            var targetPosition = rigidbody.position + worldMovement * this.speed * Time.deltaTime;
            this.rigidbody.MovePosition(targetPosition);

            this.Rotate(localMovement);
        }
    }
}
