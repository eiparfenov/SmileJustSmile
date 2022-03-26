using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMask), typeof(PlayerMotion))]
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float illTime;

        private PlayerMask _mask;
        private PlayerMotion _motion;
        
        private void Start()
        {
            _mask = GetComponent<PlayerMask>();
            _mask.IllTime = illTime;
            
            _motion = GetComponent<PlayerMotion>();
            _motion.Speed = speed;
        }
    }
}