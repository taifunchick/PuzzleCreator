using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PuzzleMaster
{
    public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Settings")]
        [SerializeField] private float snapDistance = 20f;     
        [SerializeField] private float minDragDistance = 10f;
        
        private int pieceIndex;
        private bool isPlacedCorrectly = false;
        private Vector2 correctPosition; 
        private Canvas canvas;
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Vector2 startDragPosition;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            canvas = GetComponentInParent<Canvas>();
        }

        public void Initialize(int index, Sprite sprite, Vector2 localCorrectPos)
        {
            pieceIndex = index;
            GetComponent<Image>().sprite = sprite;
            correctPosition = transform.parent.TransformPoint(localCorrectPos);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (isPlacedCorrectly) return;

            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.8f;
            startDragPosition = rectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isPlacedCorrectly) return;
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isPlacedCorrectly) return;

            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;

            float dragDistance = Vector2.Distance(rectTransform.anchoredPosition, startDragPosition);
            if (dragDistance < minDragDistance)
            {
                rectTransform.anchoredPosition = startDragPosition;
                return;
            }

            if (!IsPointerOverReferenceImage(eventData))
            {
                rectTransform.anchoredPosition = startDragPosition;
                return;
            }

            float dist = Vector2.Distance(rectTransform.position, correctPosition);
            if (dist <= snapDistance)
            {
                rectTransform.position = correctPosition;
                isPlacedCorrectly = true;
                GameManager.Instance.PiecePlaced();
                enabled = false;
            }
            else
            {
                rectTransform.anchoredPosition = startDragPosition;
            }
        }

        private bool IsPointerOverReferenceImage(PointerEventData eventData)
        {
            RectTransform referenceRect = GameManager.Instance.referenceImage.rectTransform;
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(referenceRect, eventData.position, eventData.pressEventCamera, out localPoint);
            return referenceRect.rect.Contains(localPoint);
        }
    }
}