# BVC E-Sports: The Game

My first 2D-Platformer

## Implemented Features

- ### Player

  - Walk --- with keys A and D or ◄ and ►
  - Crouch --- with keys S or ▼
    - Currently unable to walk when crouched
  - Jump --- with "Space" only when grounded
  - Double Jump --- with "Space" only when in air
    - fixed amount set with [SerializedField]
    - Counter is reset on ground
  - Wallsliding --- when movement pressed against wall
    - Slide speed is a [SerializedField]
  - Wall Jump --- only when Wallsliding
    - Kick up and away from wall
    - After wall jump delay of allowing to move again
  - Death
    - if touching any object with "Trap" tag

- ### Collectibles

  - Cherries (_Prefab_)
    - Count towards global counter
    - Amount displayed on screen

- ### Traps
  - Traps
    - kills on ColliderEnter
    - One Live
    - Level Restarts immediatly after 1 Second
  - Spikes (_Prefab_)
  - OutOfBounds (_Prefab_)
