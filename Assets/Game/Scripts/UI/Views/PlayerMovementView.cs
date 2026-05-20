using MVVM;
using UnityEngine;
using TMPro;

namespace Game.Scripts.UI.Views
{
    public class PlayerMovementView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _positionText;
        [SerializeField] private TMP_Text _rotationText;
        [SerializeField] private TMP_Text _velocityText;


        public void Initialize()
        {
            UpdatePosition("0");
            UpdateRotation("0");
            UpdateVelocity("0");
        }
        [Method("Position")]
        public void UpdatePosition(string position)
        {
            _positionText.text = position;
        }

        [Method("Rotation")]
        public void UpdateRotation(string rotation)
        {
            _rotationText.text = rotation;
        }

        [Method("Velocity")]
        public void UpdateVelocity(string velocity)
        {
            _velocityText.text = velocity;
        }
    }
}