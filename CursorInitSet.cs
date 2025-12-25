using UnityEngine;

namespace Assets.Scripts.GlobalInitialize
{
    /// <summary>
    /// 光标初始化设置
    /// </summary>
    public class CursorInitSet : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("是否隐藏光标")]
        private bool hideCursor = false;
        [SerializeField]
        [Tooltip("切换光标可见状态的按键")]
        private KeyCode showStateSwitchKey = KeyCode.None;
        [SerializeField]
        [Tooltip("光标锁定模式")]
        private CursorLockMode cursorLockMode = CursorLockMode.None;

        private void Start()
        {
            //应用光标设置
            Cursor.visible = !hideCursor;
            Cursor.lockState = cursorLockMode;
        }

        private void Update()
        {
            if(Input.anyKeyDown && Input.GetKeyDown(showStateSwitchKey))
                Cursor.visible = !Cursor.visible; //切换光标可见状态
        }
    }
}