using UnityEngine;
using UI;

namespace Player
{
    public class PlayerMotion : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Joystick _joystick;

        private Vector2 MoveDirection
        {
            get
            {
                if (!_joystick)
                    _joystick = FindObjectOfType<Joystick>();
                if (!_joystick)
                    return Vector2.zero;
                else
                    return _joystick.HeadPosition;
            }
        }
        private void Update()
        {
            transform.Translate(MoveDirection * speed * Time.deltaTime);
        }
        
    }
}
