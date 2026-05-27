## ADDED Requirements

### Requirement: Snake grid-based movement
The snake SHALL move on a fixed-size grid in discrete steps driven by a configurable tick timer (not per-frame).

#### Scenario: Snake moves in current direction each tick
- **WHEN** a tick elapses during gameplay
- **THEN** the snake head advances one grid cell in the current direction and each body segment moves to the position of the segment ahead of it

#### Scenario: Snake cannot reverse direction
- **WHEN** the player presses the opposite of the current movement direction (e.g., pressing Left while moving Right)
- **THEN** the input SHALL be ignored and the snake continues in its current direction

### Requirement: Player input controls direction
The system SHALL accept keyboard input to change the snake's direction.

#### Scenario: Arrow key changes direction
- **WHEN** the player presses an arrow key (Up/Down/Left/Right) during gameplay
- **THEN** the snake's next-move direction is set to the corresponding direction

#### Scenario: WASD key changes direction
- **WHEN** the player presses W, A, S, or D during gameplay
- **THEN** the snake's next-move direction is set to Up, Left, Down, or Right respectively

### Requirement: Snake body growth
The snake SHALL grow by one segment when it eats food.

#### Scenario: Snake eats food and grows
- **WHEN** the snake head moves onto a grid cell containing food
- **THEN** a new body segment is added at the tail position and the tail is not removed on the next tick

### Requirement: Snake body visualization
The snake body SHALL be visually represented on screen with distinct head and body segment colors.

#### Scenario: Head and body rendered differently
- **WHEN** the snake is rendered
- **THEN** the head segment is displayed in one color and all body segments in a different color
