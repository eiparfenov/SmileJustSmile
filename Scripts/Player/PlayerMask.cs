using System;
using UnityEngine;
using UI;

namespace Player
{
    public class PlayerMask : MonoBehaviour
    {
        private float _illTime;
        public float IllTime
        {
            set => _illTime = value;
        }
        
        private bool _maskOn;
        private float _ill;
        private MaskUI _maskUI;

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

        private void Start()
        {
            _maskOn = false;
        }

        private void Update()
        {
            UpdateIll();
            if (_ill == 1f)
            {
                print("Dead");
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
        }
    }
}