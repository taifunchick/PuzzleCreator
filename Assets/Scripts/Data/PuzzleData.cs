using UnityEngine;

namespace PuzzleMaster
{
    [CreateAssetMenu(fileName = "NewPuzzle", menuName = "Puzzle Manager/Puzzle Data")]
    public class PuzzleData : ScriptableObject
    {
        public Sprite fullArt;
        public Sprite referenceArt;
        public Sprite[] pieceSprites;
        public Vector2[] piecePositions;
        public Vector2[] startPositions;
    }
}