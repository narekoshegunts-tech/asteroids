using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Views
{
    public class ScoreView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        public void UpdateScore(string scoreText)
        {
            _scoreText.text = scoreText;
        }
    }
}