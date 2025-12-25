using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace GlobalInitialize
{
    /// <summary>
    /// 强制锁定焦点在此软件
    /// 注意：仅在WINDOWS平台上可用，打包时需设为后台运行
    /// </summary>
    public class ForceLockFocus : MonoBehaviour
    {
#if UNITY_STANDALONE_WIN
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);
        
        [SerializeField][Tooltip("是否只在开始时强制聚焦一次")]
        private bool focusOnce = false;
        [SerializeField][Tooltip("初次强制聚焦的延迟时间")]
        private float firstFocusDelay = 1f;
        [SerializeField][Tooltip("后续重复强制聚焦的轮询间隔时间")]
        private float repeatInterval = 5f;
            
        private void OnEnable()
        {
            if (focusOnce) Invoke(nameof(ForceWindowFocus), firstFocusDelay);
            // 每秒尝试一次
            else InvokeRepeating(nameof(ForceWindowFocus), firstFocusDelay, repeatInterval); 
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(ForceWindowFocus));
        }
        
        private void ForceWindowFocus()
        {
            // 模拟按下 Alt 键（欺骗 Windows 允许 SetForegroundWindow）
            keybd_event(0x12, 0, 0, IntPtr.Zero); // 0x12 = Alt key
            keybd_event(0x12, 0, 2, IntPtr.Zero); // 2 = KEYEVENTF_KEYUP

            // 获取当前窗口句柄并强制置顶
            SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
        }
#endif
    }
}