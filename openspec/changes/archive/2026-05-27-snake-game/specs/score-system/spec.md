## ADDED Requirements

### Requirement: Score increments on eating food
The system SHALL increment the player's score by a fixed amount each time the snake eats food.

#### Scenario: Score increases when food eaten
- **WHEN** the snake head position matches the food position
- **THEN** the current score increases by a configurable amount (default: 10)

### Requirement: Score display during gameplay
The system SHALL display the current score on screen at all times during gameplay.

#### Scenario: Score updates in real time
- **WHEN** the score value changes
- **THEN** the on-screen score text updates to reflect the new value immediately

### Requirement: High score persistence
The system SHALL persist the highest achieved score across game sessions using PlayerPrefs.

#### Scenario: New high score saved
- **WHEN** the game ends and the current score exceeds the stored high score
- **THEN** the high score in PlayerPrefs is updated to the current score

#### Scenario: High score loaded on start
- **WHEN** the game starts
- **THEN** the stored high score is loaded from PlayerPrefs and displayed

#### Scenario: High score displayed on game over
- **WHEN** the game ends
- **THEN** the GameOver screen displays both the current score and the all-time high score
