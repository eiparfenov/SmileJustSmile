using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UI
{
    public class MaskUI : MonoBehaviour
    {
        [SerializeField] private Gradient maskFillGradient;
        
        private Button _maskButton;
        private Slider _maskFill;
        private Image _maskFillImage;

        public float MaskFilled
        {
            set
            {
                float v = Mathf.Clamp01(value);
                _maskFill.value = v;
                _maskFillImage.color = maskFillGradient.Evaluate(v);
            }
        }

        public UnityEvent onMaskButtonPress = new UnityEvent();
        private void Start()
        {
            _maskButton = GetComponentInChildren<Button>();
            _maskFill = GetComponentInChildren<Slider>();
            _maskFillImage = _maskFill.transform.GetChild(1).GetComponentInChildren<Image>();
            _maskButton.onClick.AddListener(MaskButtonClickHandler);
        }

        private void MaskButtonClickHandler()
        {
            onMaskButtonPress.Invoke();
        }

    }
}