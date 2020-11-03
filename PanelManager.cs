using System;
using System.Linq;
using UnityEngine;

namespace Tools
{
    [RequireComponent(typeof(PanelManager))]
    public class PanelManager : MonoBehaviour
    {
        public static PanelManager Instance;
        [SerializeField] private Panel[] panels;

        public Panel this[int i] => panels[i];

        public Panel this[string key]
        {
            get { return panels.FirstOrDefault(p => p.Name == key); }
        }

        private void Awake()
        {
            Instance = this;
        }

        private void OnValidate()
        {
            for (var i = 0; i < panels.Length; i++)
            {
                if (panels[i].Object == null)
                    continue;
                if (!string.IsNullOrEmpty(panels[i].Name))
                    continue;
                panels[i].Name = panels[i].Object.name;
            }
        }

        public void HideAllPopup()
        {
            foreach (var panel in panels)
            {
                if (!panel.Pupup)
                    continue;
                panel.Hide();
            }
        }

        public void HideAllPanel()
        {
            foreach (var panel in panels)
            {
                panel.Hide();
            }
        }

        public void ShowPanel(string key)
        {
            this[key].Show();
        }
        public void HidePanel(string key)
        {
            this[key].Hide();
        }


        [System.Serializable]
        public struct Panel
        {
            public string Name;
            public GameObject Object;
            public bool Pupup;

            public GameObject[] relatedObjects;
            public GameObject[] unrelatedObjects;

            public void Hide()
            {
                Object.gameObject.SetActive(false);

                foreach (var obj in relatedObjects)
                    obj.SetActive(false);
            }

            public void Show()
            {
                Object.gameObject.SetActive(true);

                foreach (var obj in relatedObjects)
                    obj.SetActive(true);
                foreach (var obj in unrelatedObjects)
                    obj.SetActive(false);
            }
        }
    }
}