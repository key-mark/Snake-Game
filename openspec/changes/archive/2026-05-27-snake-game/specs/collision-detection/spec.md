## ADDED Requirements

### Requirement: Wall collision detection
The system SHALL detect when the snake head moves outside the grid boundary and trigger game over.

#### Scenario: Snake hits top wall
- **WHEN** the snake head moves to a position with Y coordinate greater than the grid height boundary
- **THEN** the game state transitions to GameOver

#### Scenario: Snake hits bottom wall
- **WHEN** the snake head moves to a position with Y coordinate less than 0
- **THEN** the game state transitions to GameOver

#### Scenario: Snake hits left wall
- **WHEN** the snake head moves to a position with X coordinate less than 0
- **THEN** the game state transitions to GameOver

#### Scenario: Snake hits right wall
- **WHEN** the snake head moves to a position with X coordinate greater than the grid width boundary
- **THEN** the game state transitions to GameOver

### Requirement: Self collision detection
The system SHALL detect when the snake head moves into a position occupied by its own body and trigger game over.

#### Scenario: Snake collides with its own body
- **WHEN** the snake head moves to a grid cell that is already occupied by any body segment (excluding the head position before the move)
- **THEN** the game state transitions to GameOver
