using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        public static int playerScores { get; private set; } = 0;
        public static int enemyScores { get; private set; } = 0;

        public static void PlayerWin()
        {
            playerScores++;
            instance.Restart();
        }

        public static void EnemyWin()
        {
            enemyScores++;
            instance.Restart();
        }

        private void Restart()
        {
            SceneManager.LoadScene(0);
        }

    }
}
