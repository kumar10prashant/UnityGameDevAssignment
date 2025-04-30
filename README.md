# Unity Grid-Based Game System

This project demonstrates a grid-based object interaction system in Unity. It includes features like dynamic grid generation, touch-based tile movement, axis-restricted control, physics-based drag, and snapping functionality.

---

## 📦 Features

- **Dynamic Grid Generation**  
  Automatically generate a grid layout using the `LevelGenerator` script by specifying row and column values.

- **Touch-Based Object Movement**  
  Move tiles or objects across the grid using mouse input via the `TouchHandler` script.

- **Axis-Restricted Movement**  
  Tiles can be restricted to move either vertically or horizontally using boolean flags defined in a `Tile` script.

- **Physics-Based Drag Control**  
  When selected, objects follow the mouse using physics (Rigidbody). Direction is calculated using the vector from the object's current position to the mouse position, and movement is applied accordingly.

- **Grid Snapping**  
  When the mouse button is released, the object snaps neatly to the nearest grid cell by rounding its position.

---

## 🧩 Scripts Overview

### LevelGenerator.cs
Responsible for generating a grid based on specified row and column values.

### TouchHandler.cs
Handles:
- Raycasting to detect object selection
- Mouse-follow logic for selected objects
- Movement direction and speed

### Tile.cs
Attached to each tile's parent, this script holds:
- `bool vertical`
- `bool horizontal`  
Used to determine the allowed movement direction when the object is selected.

---

## 🕹️ How It Works

1. **Grid Creation**  
   Run the scene with `LevelGenerator` attached to an empty GameObject. Set rows and columns in the inspector.

2. **Object Movement**  
   Click on an object to select it. The object will follow the mouse while respecting axis constraints.

3. **Physics Follow**  
   The Rigidbody is enabled on selection. Direction and movement are calculated based on the mouse position.

4. **Snapping**  
   On releasing the mouse button, the object snaps to the nearest grid location.

---
