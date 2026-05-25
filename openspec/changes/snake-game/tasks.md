## 1. 项目搭建

- [ ] 1.1 创建 Unity 2D 项目结构（Assets/Scenes, Assets/Scripts, Assets/Prefabs 目录）
- [ ] 1.2 创建主场景 `MainScene`，配置 Camera（正交投影、背景色）
- [ ] 1.3 创建 Canvas，设置 Screen Space - Overlay，添加 ScoreText 和 GameOverPanel

## 2. 网格与渲染系统

- [ ] 2.1 创建 `GridConfig` ScriptableObject 或常量类，定义网格宽度、高度、单元格大小
- [ ] 2.2 创建 `SpriteGenerator` 工具类，程序化生成纯色方块 Sprite（用于蛇身、食物）
- [ ] 2.3 创建 `GridRenderer` 脚本，在 Scene 中绘制网格边界参考线

## 3. 蛇系统 (snake-movement)

- [ ] 3.1 创建 `Snake` MonoBehaviour：维护 `List<Vector2Int>` 位置列表，头部在 [0]
- [ ] 3.2 实现 `Move()` 方法：在头部前方插入新位置，若未吃食物则移除尾部
- [ ] 3.3 实现方向输入处理：`SetDirection(Vector2Int dir)`，阻止反向输入
- [ ] 3.4 监听 Input（箭头键 + WASD），在 Update 中缓存最新方向
- [ ] 3.5 实现蛇身渲染：遍历位置列表，生成/回收 GameObject，头部与身体不同颜色

## 4. 食物系统 (food-spawning)

- [ ] 4.1 创建 `FoodSpawner` MonoBehaviour：维护单个食物的 `Vector2Int` 位置
- [ ] 4.2 实现 `SpawnFood()`：随机选取不重叠于蛇身的网格位置
- [ ] 4.3 食物渲染：单个 GameObject，使用程序化 Sprite，颜色与蛇身区分

## 5. 碰撞检测 (collision-detection)

- [ ] 5.1 在 `Snake` 中实现 `CheckWallCollision(Vector2Int headPos)`：判断是否超出网格边界
- [ ] 5.2 在 `Snake` 中实现 `CheckSelfCollision(Vector2Int headPos)`：判断头是否与身体重叠
- [ ] 5.3 每次移动后调用碰撞检测，任一命中则通知 GameManager 游戏结束

## 6. 分数系统 (score-system)

- [ ] 6.1 创建 `ScoreManager` MonoBehaviour：维护 currentScore、highScore
- [ ] 6.2 实现 `AddScore(int amount)` 和 UI 更新逻辑
- [ ] 6.3 实现 PlayerPrefs 读写：`LoadHighScore()` / `SaveHighScore()`
- [ ] 6.4 GameOver 时比较并更新最高分

## 7. 游戏状态机 (game-state)

- [ ] 7.1 创建 `GameManager` MonoBehaviour，定义 `GameState` 枚举（Ready, Playing, GameOver）
- [ ] 7.2 实现 Tick 定时器（使用 `InvokeRepeating` 或协程），驱动蛇移动
- [ ] 7.3 实现状态切换：Ready → 按方向键 → Playing；碰撞 → GameOver
- [ ] 7.4 实现 `Restart()`：重置蛇位置、清除食物、分数归零、状态回到 Playing

## 8. UI 系统

- [ ] 8.1 创建 `UIManager` MonoBehaviour，管理 ScoreText 和 GameOverPanel 的显示/隐藏
- [ ] 8.2 Ready 状态显示「按方向键开始」提示文字
- [ ] 8.3 GameOver 状态显示最终分数、最高分、「按方向键重新开始」提示

## 9. 集成与测试

- [ ] 9.1 在 `MainScene` 中组合所有组件：GameManager、Snake、FoodSpawner、UIManager
- [ ] 9.2 编辑器 Play Mode 全流程测试：开始 → 移动 → 吃食物 → 增长 → 撞墙/撞自身 → 重新开始
- [ ] 9.3 测试最高分持久化：关闭再打开游戏，验证最高分保留
