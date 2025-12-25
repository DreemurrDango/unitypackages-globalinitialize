# 更新日志

> 此文件记录了该软件包所有重要的变更。文件格式基于 [Keep a Changelog](http://keepachangelog.com/en/1.0.0/) 更新日志规范，且此项目版本号遵循 [语义化版本](http://semver.org/spec/v2.0.0.html) 规范

## [1.1.0] - 2025-08-17
### 新增
- **光标设置 (`CursorInitSet`)**: 添加了 `CursorInitSet` 组件，用于在程序启动时初始化鼠标光标的状态，包括：
    - 设置光标的默认可见性
    - 设置光标的锁定模式（`None`, `Locked`, `Confined`）
    - 支持通过特定按键在运行时切换光标的可见性

## [1.0.0] - 2025-05-23
### 新增
- **初始版本发布**: 提供了一系列用于在程序启动时强制应用全局设置的组件
- **全局管理器 (`GlobalManager`)**:
    - 作为核心管理器，用于在启动时以叠加模式（`Additive`）加载一系列初始化场景
    - 提供全局调试模式的开关（默认为 `F1`），并暴露 `OnEnterDebugMode` 和 `OnExitDebugMode` 静态事件
- **强制锁定分辨率 (`ForceLockResolution`)**: 一个用于强制设定并保持特定屏幕分辨率和全屏状态的组件
- **强制锁定焦点 (`ForceLockFocus`)**: 一个用于在 Windows 平台上强制将程序窗口保持在前台焦点的组件，支持单次或重复执行
- **强制分屏 (`ForceSplitScreen`)**: 一个用于在启动时自动激活多个显示器以实现分屏显示的组件