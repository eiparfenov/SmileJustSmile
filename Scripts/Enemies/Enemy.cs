using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float angularSpeed;
        [Space]
        [SerializeField] private float viewAngle;
        [SerializeField] private float viewDistance;
        [Space]
        [SerializeField] private MoveInstruction[] instructions;

        [Space] 
        [Header("EnemiesParts")] 
        [SerializeField] private Transform lookField;

        private Vector2 _lookDirection;
        
        private void Start()
        {
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            _lookDirection = Vector2.down;
            while (true)
            {
                for (int cii = 0; cii < instructions.Length; cii++)
                {
                    if (instructions[cii].MoveDirection != Vector2.zero)
                        yield return StartCoroutine(MoveToPoint(
                            instructions[cii].MoveDirection,
                            instructions[cii].Length
                            ));
                    else
                        yield return new WaitForSeconds(instructions[cii].Length);
                }
            }
        }

        private IEnumerator MoveToPoint(Vector2 dir, int stepsCount)
        {
            while (Vector3.Angle(_lookDirection,  dir) > 1f)
            {
                yield return null;
                _lookDirection = Vector3.RotateTowards(
                     _lookDirection,
                     dir,
                     angularSpeed * Mathf.Deg2Rad * Time.deltaTime,
                     0f
                     );
                var position = transform.position;
                Debug.DrawRay(position, _lookDirection);
                lookField.rotation = Quaternion.Euler(
                    0f, 
                    0f,
                    Vector3.SignedAngle(_lookDirection, -transform.right, -Vector3.forward)
                    );
            }
            
            _lookDirection = dir;
            
            for (int steps = 0; steps < stepsCount; steps++)
            {
                Vector2 start = transform.position;
                Vector2 end = start + dir;
                
                float progress = 0f;
                float prTime = dir.magnitude / speed;
                while (progress < 1f)
                {
                    yield return null;
                    progress += Time.deltaTime / prTime;
                    progress = Mathf.Clamp01(progress);
                    transform.position = Vector2.Lerp(start, end, progress);
                    
                }
            }
            
        }
        
    }

    [System.Serializable]
    class MoveInstruction
    {
        enum Direction
        {
            Up, Down, Left, Right, Wait
        }

        [SerializeField] private Direction direction;
        [SerializeField] private int lenght;
        public int Length => lenght;

        public Vector2 MoveDirection
        {
            get
            {
                switch (direction)
                {
                    case Direction.Up:
                        return Vector2.up;
                    case Direction.Down:
                        return Vector2.down;
                    case Direction.Left:
                        return Vector2.left;
                    case Direction.Right:
                        return Vector2.right;
                    default:
                        return Vector2.zero;
                }
            }
        }
    }
}