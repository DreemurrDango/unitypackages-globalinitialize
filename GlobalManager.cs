using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GlobalInitialize
{
    /// <summary>
    /// 游戏全局管理器：控制初始化加载的场景，封装场景刷新、叠加加载
    /// 注意： 全局场景的BuildIndex必须为0
    /// </summary>
    public class GlobalManager : Singleton<GlobalManager>
    {
        /// <summary>
        /// 全局管理场景名
        /// </summary>
        public const string GlobalSceneName = "MAIN";

        [SerializeField][Tooltip("调试模式开关按钮")]
        private KeyCode debugCode = KeyCode.F1;
        [SerializeField][Tooltip("延迟加载其他并行场景的时间")]
        private float delayTime = 0f;
        [SerializeField]
        [Tooltip("所有要并行加载的场景")]
        private string[] initScenesName;

        /// <summary>
        /// 进入DEBUG模式时动作
        /// </summary>
        public static UnityAction OnEnterDebugMode;
        /// <summary>
        /// 离开DEBUG模式时动作
        /// </summary>
        public static UnityAction OnExitDebugMode;
        
        /// <summary>
        /// 当前是否处于调试模式
        /// </summary>
        private bool inDebug = false;
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(delayTime);
            foreach (var n in initScenesName)
            {
                if (SceneManager.GetSceneByName(n).isLoaded) continue;
                SceneManager.LoadScene(n, LoadSceneMode.Additive);
            }
        }

        private void Update()
        {
            if (!Input.GetKeyDown(debugCode)) return;
            inDebug = !inDebug;
            if (inDebug) OnEnterDebugMode?.Invoke();
            else OnExitDebugMode?.Invoke();
        }

        /// <summary>
        /// 加载所有并行的场景
        /// </summary>
        /// <param name="sceneName">要加载的场景名</param>
        /// <param name="solo">是否为独播模式，若是则将关闭其他场景</param>
        public static void LoadScene(string sceneName = "",bool solo = false)
        {
           if (string.IsNullOrEmpty(sceneName)) sceneName = SceneManager.GetActiveScene().name;
           if (sceneName == GlobalSceneName) Debug.LogWarning("当前未加载其他场景");
           //独播模式下，将先卸载除全局场景外的所有其他场景
           if (solo)
           {
               var index = SceneManager.GetSceneByName(sceneName).buildIndex;
               for (var i = 1; i < SceneManager.loadedSceneCount; i++)
                   if (i != index)
                       SceneManager.UnloadSceneAsync(i);
           }
           SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
           SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }
    }
}
