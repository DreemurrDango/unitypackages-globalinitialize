using UnityEngine;

namespace GlobalInitialize
{
    /// <summary>
    /// 多场景并行时自动加载
    /// </summary>
    public class ForceSplitScreen : MonoBehaviour
    {
        [SerializeField] [Tooltip("若执行分屏，分屏的数量")]
        private int splitScreenNum = 2;

        public void OnEnable()
        {
            DontDestroyOnLoad(gameObject);
            //执行分屏
            Screen.fullScreen = true;
#if UNITY_EDITOR
#else
            for (int i = 1; i < Mathf.Min(splitScreenNum, Display.displays.Length); i++)
            {
                Display.displays[i].Activate();
                Display.displays[i].SetRenderingResolution(Display.displays[i].systemWidth, Display.displays[i].systemHeight);
            }
#endif
        }
    }
}