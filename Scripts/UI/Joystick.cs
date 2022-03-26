using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        public Vector2 HeadPosition => head.localPosition / _size * 2f;
        private float _size;

        [SerializeField] private RectTransform head;

        public void OnDrag(PointerEventData eventData)
        {
            head.position = eventData.position;
            head.localPosition = Vector2.ClampMagnitude(head.localPosition, _size);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            head.localPosition = Vector2.zero;
        }

        private void Start()
        {
            _size = GetComponent<RectTransform>().rect.height / 2f;
        }
    }
}