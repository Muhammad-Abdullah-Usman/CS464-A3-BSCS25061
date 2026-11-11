# CS-464 Lab 06

Name: Muhammad Abdullah Usman
Roll number: BSCS25061

A five-level ball game based on the mechanics taught in Lecture 07.

## Controls

- WASD or arrow keys: move
- R: restart the current level
- Use the buttons after winning or losing to restart or continue

## Levels

| Level | Objective | Main challenge |
|---|---|---|
| 1 | Collect 5 coins | Use a bounce pad to reach a raised coin |
| 2 | Collect 6 coins and reach the exit | Navigate barriers and damage pads |
| 3 | Collect 8 coins | Avoid a guard that patrols, chases and returns |
| 4 | Collect 6 coins and reach the exit within 35 seconds | Pass three crushers with warning signals |
| 5 | Collect 8 coins and survive 45 seconds | Avoid two guards and crushers, with temporary speed pickups |

## Health and lives

The player starts each level with 3 hearts and 3 lives.
Damage briefly grants protection against repeated hits.
Losing all hearts or falling off the arena costs one life.
The player respawns while lives remain. Losing the last life ends the game.
Collected coins remain collected after respawning.

## Speed pickups

Blue pickups increase movement force by 50% for 6 seconds.
Collecting another pickup refreshes the duration.

The five scenes are included in the build scene list in level order.