# Unity 全局初始化与控制模块 (Global Initialize & Control)

## 概述

本模块提供了一系列用于在程序启动时强制应用全局设置的组件，非常适合用于需要严格控制运行环境的特殊应用

它通过一个核心的 `GlobalManager` 来引导程序的启动流程，并提供多个独立的“强制设置”组件来控制分辨率、窗口焦点、分屏和光标行为

## 核心功能

*   **场景加载与管理 (`GlobalManager`)**:
    *   作为程序的入口点，设计用于一个永不卸载的“主”场景（Build Index 0）
    *   在启动时以叠加模式（`Additive`）加载一个或多个业务场景
    *   提供全局调试模式的开关及事件回调 (`OnEnterDebugMode`, `OnExitDebugMode`)
*   **强制锁定分辨率 (`ForceLockResolution`)**: 强制将应用程序设置为指定的分辨率和全屏模式，并可持续轮询以防止被修改
*   **强制锁定焦点 (`ForceLockFocus`)**: 在 Windows 平台上，强制将应用程序窗口保持在前台，防止用户退出或切换到其他程序
*   **强制分屏 (`ForceSplitScreen`)**: 在启动时自动检测并激活多个显示器，用于多屏显示应用
*   **光标初始化设置 (`CursorInitSet`)**: 在启动时设置光标的可见性和锁定状态（如隐藏光标、将光标锁定在窗口中心等）

---

## 依赖项

*   **DreemurrStudio.Utilities**: 本模块的 `GlobalManager` 继承自 `Singleton<T>`，该基类由 `DreemurrStudio.Utilities` 包提供。请确保您的项目中已导入该依赖包

---

## 快速开始

### 1. 场景设置

推荐的工作流程是创建一个名为 `MAIN` 的主场景，并将包中的 `GlobalManager`预制体实例化放置在该场景中

1.  **创建主场景**: 创建一个新场景，命名为 `MAIN`
2.  **设置构建顺序**: 打开 `File > Build Settings`，将 `MAIN` 场景拖入列表，并***始终确保***它的索引为 **0**
3.  **添加管理器**: 在 `MAIN` 场景中创建一个空对象，命名为 `GlobalManager`，并添加 `GlobalManager` 组件
4.  **配置场景加载**: 在 `GlobalManager` 组件的 `Init Scenes Name` 数组中，填入您希望在启动后自动加载的业务场景名称（例如 `GameScene`, `UIScene`）
5.  **添加强制设置组件**: 根据您的需求，将以下一个或多个组件添加到 `MAIN` 场景中的任意对象上：
    *   `ForceLockResolution`
    *   `ForceLockFocus`
    *   `ForceSplitScreen`
    *   `CursorInitSet`
6.  **配置组件**: 在 Inspector 面板中调整每个组件的参数以满足您的需求

### 2. 组件用法说明

#### `GlobalManager`
*   **Debug Code**: 设置一个用于切换调试模式的按键（默认为 F1）
*   **Delay Time**: 设置在加载业务场景前的延迟时间（秒）
*   **Init Scenes Name**: 要在启动时叠加加载的场景名称列表

#### `ForceLockResolution`
*   **Width / Height**: 目标分辨率的宽度和高度
*   **Full Screen**: 是否强制为全屏模式
*   **Do Repeat**: 是否持续检查并强制应用该分辨率

#### `ForceLockFocus` (仅 Windows)
*   **Focus Once**: 如果勾选，则只在启动时强制聚焦一次；否则将按 `Repeat Interval` 的间隔重复聚焦
*   **First Focus Delay**: 首次执行聚焦的延迟时间
*   **Repeat Interval**: 重复执行聚焦的间隔时间

#### `ForceSplitScreen`
*   **Split Screen Num**: 您希望激活的显示器数量。程序会尝试激活最多到此数量的可用显示器

#### `CursorInitSet`
*   **Hide Cursor**: 是否在启动时隐藏光标
*   **Show State Switch Key**: 设置一个按键，用于在运行时切换光标的显示/隐藏状态
*   **Cursor Lock Mode**: 设置光标的锁定模式（`None`, `Locked` 到屏幕中心, `Confined` 到窗口内）

### 3. 监听调试事件

您可以在任何脚本中订阅 `GlobalManager` 的静态事件，以响应调试模式的切换

```csharp
using DreemurrStudio.GlobalInitialize;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    public GameObject panel;

    void OnEnable()
    {
        GlobalManager.OnEnterDebugMode += ShowPanel;
        GlobalManager.OnExitDebugMode += HidePanel;
    }

    void OnDisable()
    {
        GlobalManager.OnEnterDebugMode -= ShowPanel;
        GlobalManager.OnExitDebugMode -= HidePanel;
    }

    private void ShowPanel()
    {
        panel.SetActive(true);
    }

    private void HidePanel()
    {
        panel.SetActive(false);
    }
}
```