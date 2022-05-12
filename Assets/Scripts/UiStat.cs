using TMPro;
using UnityEngine;


namespace Assets.Scripts
{
    public class UiStat : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI gameScoresText;
        private void Start()
        {
            gameScoresText.text = $"{GameManager.playerScores} : {GameManager.enemyScores}";
        }

    }
}
