using UnityEngine;

namespace SUP.GameUtils {
    [RequireComponent(typeof(Transform))]
    public class FloatingEffect3D : MonoBehaviour {
        public float amplitude = 1f, speed = 1f, startTime;
        private float _originalY;
        private Vector3 _targetPos;

        private void Awake() {
            _targetPos = transform.position;
            _originalY = _targetPos.y;
        }

        private void LateUpdate() {
            _targetPos.y = _originalY + amplitude * Mathf.Sin(amplitude + speed * Time.time + startTime);
            transform.position = _targetPos;
        }
    }
}