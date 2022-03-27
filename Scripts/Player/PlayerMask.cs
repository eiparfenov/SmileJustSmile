using System;
using UnityEngine;
using UI;
using UnityEngine.Events;

namespace Player
{
    public class PlayerMask : MonoBehaviour
    {
        [SerializeField] private LayerMask seeWithMask;
        [SerializeField] private LayerMask seeWithoutMask;
        public UnityEvent onPlayerDiedByMask = new UnityEvent();
        
        public float IllTime
        {
            set => _illTime = value;
        }
        
        private float _illTime;
        private bool _maskOn;
        private float _ill;
        private MaskUI _maskUI;
        private GameObject _collider;
        private Camera _mainCamera;

        private MaskUI MaskUI
        {
            get
            {
                if (!_maskUI)
                {
                    _maskUI = FindObjectOfType<MaskUI>();
                    if (_maskUI)
                        _maskUI.onMaskButtonPress.AddListener(MaskUpDownHandler);
                }

                return _maskUI;
            }
        }

        private void Awake()
        {
            print("Awake");
            _maskOn = false;
            _collider = GetComponentInChildren<Collider2D>().gameObject;
            _mainCamera = FindObjectOfType<Camera>();
            _mainCamera.cullingMask = seeWithoutMask;
        }

        private void Update()
        {
            UpdateIll();
            if (_ill == 1f)
            {
                GlobalEventsManager.OnPlayerDead.Invoke(DieType.Mask);
            }
        }

        private void UpdateIll()
        {
            if (_maskOn)
                _ill += Time.deltaTime / _illTime;
            else
                _ill -= Time.deltaTime / _illTime;
            _ill = Mathf.Clamp01(_ill);
            if (MaskUI)
                MaskUI.MaskFilled = _ill;
        }

        private void MaskUpDownHandler()
        {
            _maskOn = !_maskOn;
            if (_maskOn)
            {
                _mainCamera.cullingMask = seeWithMask;
                _collider.tag = "Masked";
            }
            else
            {
                _mainCamera.cullingMask = seeWithoutMask;
                _collider.tag = "Unmasked";
            }
        }
    }
}