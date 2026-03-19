using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace PuzzleMaster
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("UI References")]
        public Image referenceImage;   
        public GameObject victoryPanel;
        public Button menuButton;
        public Button nextLevelButton;

        [Header("Puzzle Settings")]
        public Transform piecesParent; 
        public PuzzlePiece piecePrefab; 
        public PuzzleData[] allLevels;
        public int currentLevelIndex = 0;

        private List<PuzzlePiece> spawnedPieces = new List<PuzzlePiece>();
        private int piecesPlaced = 0;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            if (PlayerPrefs.HasKey("SelectedLevel"))
                currentLevelIndex = PlayerPrefs.GetInt("SelectedLevel");

            LoadLevel(currentLevelIndex);
            menuButton.onClick.AddListener(GoToMenu);
            nextLevelButton.onClick.AddListener(LoadNextLevel);
            victoryPanel.SetActive(false);
        }

        public void LoadLevel(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= allLevels.Length) return;

            currentLevelIndex = levelIndex;
            PuzzleData data = allLevels[levelIndex];

            if (referenceImage != null && data.referenceArt != null)
                referenceImage.sprite = data.referenceArt;

            foreach (var piece in spawnedPieces)
                Destroy(piece.gameObject);
            spawnedPieces.Clear();
            piecesPlaced = 0;

            Vector2[] shuffledStartPositions = (Vector2[])data.startPositions.Clone();
            ShuffleArray(shuffledStartPositions);

            for (int i = 0; i < data.pieceSprites.Length; i++)
            {
                PuzzlePiece piece = Instantiate(piecePrefab, piecesParent);
                piece.Initialize(i, data.pieceSprites[i], data.piecePositions[i]);
                piece.transform.position = shuffledStartPositions[i];
                spawnedPieces.Add(piece);
            }

            victoryPanel.SetActive(false);
        }

        private void ShuffleArray(Vector2[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                Vector2 temp = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
        }

        public void PiecePlaced()
        {
            piecesPlaced++;
            if (piecesPlaced >= spawnedPieces.Count)
            {
                victoryPanel.SetActive(true);
                SaveManager.MarkLevelCompleted(currentLevelIndex);
            }
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void LoadNextLevel()
        {
            int next = currentLevelIndex + 1;
            if (next < allLevels.Length)
                LoadLevel(next);
            else
                GoToMenu();
        }
    }
}