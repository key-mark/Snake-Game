## Why

构建一个在 Unity 中运行的经典贪吃蛇游戏，作为 Unity 2D 开发的实践项目。贪吃蛇玩法简单、规则明确，适合快速验证 Unity 游戏开发流程，并为后续扩展（道具、关卡、联网对战）打下基础。

## What Changes

- 新增 Unity 2D 项目结构，包含场景、脚本、预制体
- 实现蛇的网格移动、方向控制、身体增长
- 实现随机食物生成，蛇吃到食物后身体增长、分数增加
- 实现碰撞检测：撞墙或撞自身时游戏结束
- 实现分数系统：实时计分显示与游戏结束界面
- 实现游戏状态机：开始、进行中、游戏结束

## Capabilities

### New Capabilities

- `snake-movement`: 蛇的网格化移动（上下左右）、方向输入处理、身体跟随逻辑
- `food-spawning`: 食物在网格中随机生成，不与蛇身重叠
- `collision-detection`: 边界碰撞与自身碰撞检测，触发游戏结束
- `score-system`: 实时分数累加、UI 显示、最高分持久化
- `game-state`: 游戏状态机管理（Ready / Playing / GameOver），状态切换逻辑

### Modified Capabilities

<!-- 无现有能力需要修改 -->

## Impact

- 项目目录: `F:\Unity\test` — 创建 Unity 项目文件结构（Assets、场景、脚本）
- 技术栈: Unity (C#), 纯 2D，使用 Unity 内置 UI (uGUI / Canvas)
- 无外部依赖，无 API 变更
- 目标平台: Windows Standalone，后续可扩展到 WebGL / Mobile
