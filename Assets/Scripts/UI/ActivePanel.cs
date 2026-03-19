using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleMaster
{
    public class ActivePanel : MonoBehaviour
    {
        [SerializeField] private GameObject firstPanel;
        [SerializeField] private GameObject secondPanel;
        
        public void Start()
        {
            firstPanel.SetActive(true);
            secondPanel.SetActive(false);
        }

        public void ActiveSecondPanel()
        {
            firstPanel.SetActive(false);
            secondPanel.SetActive(true);
        }
    }
}
