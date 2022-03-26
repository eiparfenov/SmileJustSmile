using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        public Vector2 HeadPosition => head.localPosition;

        [SerializeField] private RectTransform head;

        public void OnDrag(PointerEventData eventData)
        {
            head.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            head.localPosition = Vector2.zero;
        }
    }
}