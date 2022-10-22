using UnityEngine;

namespace Player.Visual
{
    public class PlayerAssBoxScale : MonoBehaviour
    {
        [SerializeField] Transform _assBox;
        [Header("Scale Settings")]
        [SerializeField] Vector3 _maxUpScale = new Vector3(0.8f, 1.2f, 0.8f);
        [SerializeField] Vector3 _maxDownScale = new Vector3(1.2f, 0.8f, 1.2f);
        [SerializeField] float _scaleKoefficient;

        private void Update()
        {
            Vector3 relativePosition = transform.InverseTransformPoint(_assBox.position);
            float interpolant = relativePosition.y * _scaleKoefficient;
            Vector3 scale = Lerp3(_maxDownScale, Vector3.one, _maxUpScale, interpolant);
            transform.localScale = scale;
        }
        Vector3 Lerp3(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            if (t < 0)  return Vector3.LerpUnclamped(a, b, t + 1);
            else return Vector3.LerpUnclamped(b, c, t);
        }
    }
}
