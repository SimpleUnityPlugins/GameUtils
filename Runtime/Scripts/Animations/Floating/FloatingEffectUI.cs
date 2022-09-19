using UnityEngine;

namespace SUP.GameUtils {
    [RequireComponent(typeof(RectTransform))]
    public class FloatingEffectUI : MonoBehaviour {
        public float amplitude = 30f, speed = 1f, startTime;
        private float _originalY;
        private Vector3 _targetPos;
        private RectTransform _rectTransform;

        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
            _targetPos = _rectTransform.anchoredPosition;
            _originalY = _targetPos.y;
        }

        private void LateUpdate() {
            _targetPos.y = _originalY + amplitude * Mathf.Sin(amplitude + speed * Time.time + startTime);
            _rectTransform.anchoredPosition = _targetPos;
        }
    }
}