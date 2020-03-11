using System;
using UnityEngine;

namespace CustomScripts.Managers
{
    public class UpdateManager : MonoBehaviour
    {
        #region Singleton
        public static UpdateManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        #endregion

        public event Action GlobalUpdate;
        public event Action FixedGlboalUpdate;

        private void Update()
        {
            this.GlobalUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            this.FixedGlboalUpdate?.Invoke();
        }
    }
}