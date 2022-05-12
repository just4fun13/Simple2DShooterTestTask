using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class EnemyLogger : MonoBehaviour
    {
        private static int maxLineCount = 5;
        private static List<string> rows = new List<string>();
        [SerializeField] private TextMeshProUGUI logArea;
        public static EnemyLogger enemyLogger;

        private void Awake()
        {
                enemyLogger = this;
        }

        public static void Log(string inp)
        {
            if (rows.Count >= maxLineCount)
                rows.RemoveAt(0);
            rows.Add(inp);
            RefreshText();
        }

        private static void RefreshText()
        {
            enemyLogger.logArea.text = "";
            foreach (string s in rows)
                enemyLogger.logArea.text += s + Environment.NewLine;
        }
    }
}
