## ADDED Requirements

### Requirement: Game state machine
The system SHALL manage the game through three distinct states: Ready, Playing, and GameOver.

#### Scenario: Initial state is Ready
- **WHEN** the game scene loads
- **THEN** the game is in the Ready state, the snake is visible but not moving, and a start prompt is displayed

#### Scenario: Start game from Ready
- **WHEN** the player presses any movement key (Arrow/WASD) while in Ready state
- **THEN** the game transitions to Playing state and the snake begins moving

### Requirement: Playing state behavior
During the Playing state, the tick timer SHALL drive snake movement and all game systems SHALL be active.

#### Scenario: Tick drives gameplay
- **WHEN** the game is in Playing state
- **THEN** the tick timer runs at the configured interval, triggering snake movement, collision checks, and food checks each tick

### Requirement: GameOver state and restart
The GameOver state SHALL halt gameplay and provide a way to restart.

#### Scenario: Game over on collision
- **WHEN** a collision (wall or self) is detected during Playing state
- **THEN** the game transitions to GameOver state, snake movement stops, and a game over panel with score and restart prompt appears

#### Scenario: Restart from GameOver
- **WHEN** the player presses any movement key while in GameOver state
- **THEN** the game resets to Playing state with a new snake (starting length), new food, and score reset to zero
