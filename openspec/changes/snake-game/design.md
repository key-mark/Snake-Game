## Context

在 Unity 中创建一个 2D 贪吃蛇游戏。项目目录当前为空，需要从零搭建 Unity 项目结构。目标是一个功能完整的经典贪吃蛇：蛇在网格中移动、吃食物增长、撞墙或撞自身游戏结束，带有分数系统。

**约束：**
- 纯 Unity 2D，无第三方插件
- 使用 Unity 内置 uGUI (Canvas) 做 UI
- 不依赖外部美术资源，使用程序化生成的简单色块精灵
- 脚本语言：C#

## Goals / Non-Goals

**Goals:**
- 实现基于网格的蛇移动系统，Tick 驱动而非帧驱动
- 支持键盘方向键和 WASD 输入
- 食物随机生成，不与蛇身重叠
- 碰撞检测（边界 + 自身）
- 分数实时显示 + 最高分持久化（PlayerPrefs）
- 游戏状态机：准备 → 游戏中 → 结束，可重新开始

**Non-Goals:**
- 不实现关卡系统或道具
- 不实现音效
- 不做移动端触屏适配
- 不做多人/联网
- 不做动画或粒子效果

## Decisions

| 决策 | 选项 | 选择 | 理由 |
|------|------|------|------|
| 移动模型 | 网格 vs 自由移动 | 网格 | 经典贪吃蛇体验，实现简单，碰撞检测精确 |
| 移动驱动 | Tick 定时器 vs 每帧 | Tick 定时器 | 可控速度，随分数提升可加速；与帧率解耦 |
| 蛇数据结构 | `List<Vector2Int>` 头部在末尾 | `List<Vector2Int>`（头部在 [0]） | 添加头部方便，移除尾部用 RemoveAt(Count-1) |
| 输入处理 | `Input.GetKeyDown` vs Input System | `Input.GetKeyDown` | 简单直接，无需额外 Package |
| 精灵生成 | 程序化 Texture2D vs 导入图片 | 程序化 Texture2D | 零外部依赖，项目自包含 |
| 分数持久化 | PlayerPrefs vs JSON 文件 | PlayerPrefs | 最简单的持久化方案，适合单值存储 |
| 游戏架构 | 单 MonoBehaviour (GameManager) | 多组件分工 | 游戏逻辑简单，但为可维护性拆分为 GameManager + Snake + FoodSpawner |
| UI 方案 | uGUI Canvas vs UI Toolkit | uGUI Canvas | Unity 默认 UI 方案，最稳定成熟 |

**架构概览：**
```
GameManager (状态机、Tick 驱动、分数)
  ├── Snake (位置列表、移动、增长、碰撞检测)
  ├── FoodSpawner (随机生成、避免蛇身)
  └── UIManager (分数显示、GameOver 面板)
```

## Risks / Trade-offs

- **[风险] 蛇增长后 Tick 频率不变会使游戏难度不变** → 可在后续迭代中添加速度递增逻辑
- **[风险] 使用 PlayerPrefs 不适合大量数据** → 当前仅存一个最高分 int，完全够用
- **[权衡] 选择 uGUI 而非 UI Toolkit** → uGUI 更成熟稳定，但 UI Toolkit 是未来方向。优先稳定性
- **[权衡] 单 Scene 架构 vs 多 Scene** → 单 Scene 更简单，当前功能不需要多 Scene
