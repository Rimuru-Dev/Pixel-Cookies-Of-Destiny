// ReSharper disable All
// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//
// **************************************************************** //

using AbyssMoth.Internal.Codebase.Boot;
using UnityEngine;
using UnityEngine.UI;

namespace AbyssMoth.Internal.Codebase.Cookies
{
    public sealed class MoreCookiesPanel : MonoBehaviour, IInitialialize
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Button button;
        [SerializeField] private CookiesController cookiesController;

        public void Initialize()
        {
            button.onClick.AddListener(() =>
            {
                cookiesController.GetMoreCookies();
                panel.SetActive(false);
            });

            cookiesController.OnWin += ShowPanel; 
            
            panel.SetActive(false);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
            cookiesController.OnWin -= ShowPanel;
        }

        private void ShowPanel() =>
            panel.SetActive(true);
    }
}