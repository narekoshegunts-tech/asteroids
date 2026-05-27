using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Scripts.UI.Views
{
    public class LaserChargeIndicatorView: MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private TMP_Text _currentLaserAttacks;
        

        public void UpdateFillAmount(float fillAmount)
        {
            _fillImage.fillAmount = fillAmount;
        }

        public void UpdateCurrentLaserAttacks(string currentLaserAttacks)
        {
            _currentLaserAttacks.text = currentLaserAttacks;
        }
    }
}