using System;
using UnityEngine;
using UI;

namespace Player
{
    public class PlayerMotion : MonoBehaviour
    {
        private float _speed;
        private Animator[] _animators;

        public float Speed
        {
            set => _speed = value;
        }
        private Joystick _joystick;

        private Vector2 MoveDirection
        {
            get
            {
                if (!_joystick)
                {
                    _joystick = FindObjectOfType<Joystick>();
                    if (_joystick)
                        _joystick.ResetHeadPosition();
                }

                if (!_joystick)
                    return Vector2.zero;
                else
                    return _joystick.HeadPosition;
            }
        }

        private void Start()
        {
            _animators = GetComponentsInChildren<Animator>();
        }

        private void Update()
        {
            transform.Translate(MoveDirection * _speed * Time.deltaTime);
            Vector3 md = MoveDirection;
            
            if (md == Vector3.zero)
            {
                foreach (var animator in _animators)
                {
                    animator.SetBool("up", false);
                    animator.SetBool("down", false);
                    animator.SetBool("left", false);
                    animator.SetBool("right", false);
                }
            }
            else if (Vector2.Angle(md, Vector2.up) <= 45)
            {
                foreach (var animator in _animators)
                {
                    animator.SetBool("up", true);
                    animator.SetBool("down", false);
                    animator.SetBool("left", false);
                    animator.SetBool("right", false);
                }
            }
            else if (Vector2.Angle(md, Vector2.down) <= 45)
            {
                foreach (var animator in _animators)
                {
                    animator.SetBool("up", false);
                    animator.SetBool("down", true);
                    animator.SetBool("left", false);
                    animator.SetBool("right", false);
                }
            }
            else if (Vector2.Angle(md, Vector2.left) <= 45)
            {
                foreach (var animator in _animators)
                {
                    animator.SetBool("up", false);
                    animator.SetBool("down", false);
                    animator.SetBool("left", true);
                    animator.SetBool("right", false);
                }
            }
            else if (Vector2.Angle(md, Vector2.right) <= 45)
            {
                foreach (var animator in _animators)
                {
                    animator.SetBool("up", false);
                    animator.SetBool("down", false);
                    animator.SetBool("left", false);
                    animator.SetBool("right", true);
                }
            }
        }
        
    }
}
