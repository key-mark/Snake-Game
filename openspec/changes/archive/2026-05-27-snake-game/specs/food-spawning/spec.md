## ADDED Requirements

### Requirement: Food spawns at random grid position
The system SHALL spawn a single food item at a random unoccupied grid position after the previous food is eaten.

#### Scenario: Food spawns on empty cell
- **WHEN** the system spawns food
- **THEN** the food position is a random grid cell that is not occupied by any snake segment

#### Scenario: Food spawns after being eaten
- **WHEN** the snake eats the current food
- **THEN** a new food item spawns at a new random unoccupied position within the same tick cycle

### Requirement: Food visual representation
The food SHALL be visually distinct from the snake and background.

#### Scenario: Food is visible and distinct
- **WHEN** food is spawned on the grid
- **THEN** it is rendered with a color that contrasts with both the snake and the background
