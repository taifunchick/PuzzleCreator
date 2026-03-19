using System.Collections.Generic;
using UnityEngine;

namespace PuzzleMaster
{
    public static class SaveManager
    {
        private const string CompletedLevelsKey = "CompletedLevels";

        public static void MarkLevelCompleted(int levelIndex)
        {
            HashSet<int> completed = GetCompletedLevels();
            completed.Add(levelIndex);
            SaveCompletedLevels(completed);
        }

        public static HashSet<int> GetCompletedLevels()
        {
            string saved = PlayerPrefs.GetString(CompletedLevelsKey, "");
            HashSet<int> result = new HashSet<int>();
            if (!string.IsNullOrEmpty(saved))
            {
                string[] parts = saved.Split(',');
                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int index))
                        result.Add(index);
                }
                
            }
            return result;
        }

        private static void SaveCompletedLevels(HashSet<int> completed)
        {
            string value = string.Join(",", completed);
            PlayerPrefs.SetString(CompletedLevelsKey, value);
            PlayerPrefs.Save();
        }

        public static bool IsLevelCompleted(int levelIndex)
        {
            return GetCompletedLevels().Contains(levelIndex);
        }
    }
}