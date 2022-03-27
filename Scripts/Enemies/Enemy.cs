using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float angularSpeed;
        
        [Space]
        [SerializeField] private float angryTime;
        [SerializeField] private bool ignoreMask;
        
        [Space]
        [SerializeField] private float viewAngle;
        [SerializeField] private float viewDistance;
        [SerializeField] private Gradient scannerGradient;
        
        [Space]
        [SerializeField] private MoveInstruction start;
        [SerializeField] private MoveInstruction[] instructions;
        
        [Space] 
        [Header("EnemiesParts")] 
        [SerializeField] private Transform lookField;

        private Scanner _scanner;
        private Vector2 _lookDirection;
        private float _angry = 0f;
        private Image _angryDisplay;
        private MeshRenderer _scannerMeshRenderer;
        [FormerlySerializedAs("_states")] [SerializeField] private GameObject[] states;

        private void Start()
        {
            _scanner = GetComponentInChildren<Scanner>();
            _scanner.FieldOfView = viewAngle / 2;
            _scanner.Range = viewDistance;
            _angryDisplay = GetComponentInChildren<Image>();
            _scannerMeshRenderer = GetComponentInChildren<MeshRenderer>();
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            _lookDirection = start.MoveDirection;
            
            foreach (var st in states)
            {
                st.SetActive(false);
            }
            states[start.State].SetActive(true);
            
            yield return new WaitForSeconds(start.Length);
            while (true)
            {
                for (int cii = 0; cii < instructions.Length; cii++)
                {
                    if (instructions[cii].MoveDirection != Vector2.zero)
                        yield return StartCoroutine(MoveToPoint(
                            instructions[cii].MoveDirection,
                            instructions[cii].Length,
                            instructions[cii].State
                            ));
                    else
                    {
                        float t = instructions[cii].Length;
                        while (t > 0f)
                        {
                            t -= Time.deltaTime;
                            yield return StartCoroutine(Scan());
                        }
                    }
                }
            }
        }

        private IEnumerator MoveToPoint(Vector2 dir, int stepsCount, int state)
        {
            while (Vector3.Angle(_lookDirection,  dir) > 45f)
            {
                yield return StartCoroutine(Scan());
                _lookDirection = Vector3.RotateTowards(
                     _lookDirection,
                     dir,
                     angularSpeed * Mathf.Deg2Rad * Time.deltaTime,
                     0f
                     );
                var position = transform.position;
                lookField.rotation = Quaternion.Euler(
                    0f, 
                    0f,
                    Vector3.SignedAngle(_lookDirection, -transform.right, -Vector3.forward)
                    );
            }

            foreach (var st in states)
            {
                st.SetActive(false);
            }
            states[state].SetActive(true);
            
            while (Vector3.Angle(_lookDirection,  dir) > 1f)
            {
                yield return StartCoroutine(Scan());
                _lookDirection = Vector3.RotateTowards(
                    _lookDirection,
                    dir,
                    angularSpeed * Mathf.Deg2Rad * Time.deltaTime,
                    0f
                );
                var position = transform.position;
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
                    yield return StartCoroutine(Scan());
                    progress += Time.deltaTime / prTime;
                    progress = Mathf.Clamp01(progress);
                    transform.position = Vector2.Lerp(start, end, progress);
                    
                }
            }
            
        }

        private IEnumerator Scan()
        {
            if (_scanner.Scan(ignoreMask))
                _angry += Time.deltaTime / angryTime;
            else
                _angry -= Time.deltaTime / angryTime;

            _angry = Mathf.Clamp01(_angry);
            _angryDisplay.fillAmount = _angry;
            _scannerMeshRenderer.material.color = scannerGradient.Evaluate(_angry);
                
            if (_angry == 1f)
            {
                GlobalEventsManager.OnPlayerDead.Invoke(DieType.Killed);
            }

            yield return null;
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
                        return Vector2.up + .3f * Vector2.right;
                    case Direction.Down:
                        return Vector2.down + .3f * Vector2.left;
                    case Direction.Left:
                        return Vector2.left;
                    case Direction.Right:
                        return Vector2.right;
                    default:
                        return Vector2.zero;
                }
            }
        }

        public int State => (int) direction;
    }
}