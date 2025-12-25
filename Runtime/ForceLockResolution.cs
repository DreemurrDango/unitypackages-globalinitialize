using UnityEngine;

namespace DreemurrStudio.GlobalInitialize
{
    /// <summary>
    /// 强制锁定分辨率
    /// </summary>
    public class ForceLockResolution : MonoBehaviour
    {
        [SerializeField][Tooltip("锁定屏幕分辨率宽度")]
        private int width = 1920;
        [SerializeField][Tooltip("锁定屏幕分辨率高度")]
        private int height = 1080;
        [SerializeField][Tooltip("是否锁定为全屏")]
        private bool fullScreen = true;
        [SerializeField][Tooltip("是否轮询以确保分辨率为设定的分辨率")]
        private bool doRepeat = false;

        private void Start()
        {
            if (!doRepeat) Invoke(nameof(ForceLockFocus), 1f);
            else InvokeRepeating(nameof(SetResolution), 1f, 100);
        }

        /// <summary>
        /// 强制设定分辨率
        /// </summary>
        public void SetResolution()
        {
            Debug.Log($"已将分辨率从{(Screen.fullScreen ? "全屏" : "窗口")}{Screen.width}x{Screen.height}设为" +
                      $"{(fullScreen ? "全屏" : "窗口")}{width}x{height}");
            Screen.SetResolution(width, height, fullScreen);
        }
    }
}
