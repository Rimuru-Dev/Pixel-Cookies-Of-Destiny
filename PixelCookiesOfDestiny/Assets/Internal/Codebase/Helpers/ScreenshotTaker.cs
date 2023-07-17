// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //


using UnityEngine;

namespace AbyssMoth.Internal.Codebase.Helpers
{
    public sealed class ScreenshotTaker : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private string folderName = "Screenshots";
        [SerializeField] private int resolutionWidth = 1920;
        [SerializeField] private int resolutionHeight = 1080;

        [System.Diagnostics.Conditional("DEBUG")]
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
                CaptureScreenshot();
        }

        [ContextMenu("Take Screenshot")]
        [System.Diagnostics.Conditional("DEBUG")]
        private void CaptureScreenshot()
        {
            var folderPath = Application.dataPath + "/" + folderName;

            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            var screenshotPath = folderPath + "/screenshot_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

            ScreenCapture.CaptureScreenshot(screenshotPath, 1);

            Debug.Log("Скриншот сохранен: " + screenshotPath);
        }
#endif
    }
}