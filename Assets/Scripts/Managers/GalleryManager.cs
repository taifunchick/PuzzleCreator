using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace PuzzleMaster
{
    public class GalleryManager : MonoBehaviour
    {
        [Header("Gallery Settings")]
        public Transform galleryContainer;  
        public GameObject galleryItemPrefab;
        public PuzzleData[] allLevels;      

        [Header("Navigation")]
        public Button prevPageButton;
        public Button nextPageButton;
        public int itemsPerPage = 6;

        private List<GameObject> spawnedItems = new List<GameObject>();
        private int currentPage = 0;
        private HashSet<int> completedLevels;

        private void Start()
        {
            completedLevels = SaveManager.GetCompletedLevels();
            prevPageButton.onClick.AddListener(PrevPage);
            nextPageButton.onClick.AddListener(NextPage);
            UpdateGallery();
        }

        private void UpdateGallery()
        {
            foreach (var item in spawnedItems)
                Destroy(item);
            spawnedItems.Clear();

            int startIndex = currentPage * itemsPerPage;
            int endIndex = Mathf.Min(startIndex + itemsPerPage, allLevels.Length);

            for (int i = startIndex; i < endIndex; i++)
            {
                GameObject item = Instantiate(galleryItemPrefab, galleryContainer);
                Image img = item.GetComponent<Image>();
                if (completedLevels.Contains(i))
                    img.sprite = allLevels[i].fullArt; 
                int capturedIndex = i;
                Button btn = item.GetComponent<Button>();
                if (btn != null)
                    btn.onClick.AddListener(() => LoadLevel(capturedIndex));
                spawnedItems.Add(item);
            }

            prevPageButton.interactable = currentPage > 0;
            nextPageButton.interactable = endIndex < allLevels.Length;
        }

        private void PrevPage()
        {
            if (currentPage > 0)
            {
                currentPage--;
                UpdateGallery();
            }
        }

        private void NextPage()
        {
            if ((currentPage + 1) * itemsPerPage < allLevels.Length)
            {
                currentPage++;
                UpdateGallery();
            }
        }

        public void LoadLevel(int levelIndex)
        {
            PlayerPrefs.SetInt("SelectedLevel", levelIndex);
            SceneManager.LoadScene("Game");
        }

        public void PlayNextLevel()
        {
            completedLevels = SaveManager.GetCompletedLevels();

            int nextLevel = 0;
            while (completedLevels.Contains(nextLevel) && nextLevel < allLevels.Length)
            {
                nextLevel++;
            }

            if (nextLevel >= allLevels.Length)
                nextLevel = 0;

            LoadLevel(nextLevel);
        }

        // так надо
        public void Quit()
        {
            Application.Quit();
        }
    }
}