using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomScripts.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class Attractee : MonoBehaviour
    {
        private void Start()
        {
            //disable default physics settings
            var rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.useGravity = false;

            //use custom gravity system
            Planet.Instance.AddAttractee(this);
        }
    }
}
