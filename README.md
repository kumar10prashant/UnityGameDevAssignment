Unity Game Dev Assignment
Grid Generation
The grid is generated using a script called LevelGenerator. You just need to provide the number of rows and columns, and the script will generate the grid accordingly.

Object Movement in the Grid
To handle object or tile movement within the grid, I created a script called TouchHandler.

How TouchHandler Works
The TouchHandler uses raycasting to detect the mouse position on the screen. When the mouse is clicked and the raycast hits an object on the specified layer, that object is stored in a variable. The selected object then follows the mouse position.

Handling Vertical and Horizontal Movement
Each tile has a parent with a Tile script attached to it. This script contains two boolean values: vertical and horizontal. When a tile is clicked, it checks this script to determine whether the object can move vertically or horizontally. Based on those values, the object starts moving accordingly and follows the mouse.

Object Collision During Movement
This part is simple:
When the object is selected (on mouse click), set its Rigidbody's isKinematic to false. This allows physics to take over. To make the object follow the mouse with physics, calculate the direction by subtracting the target position from the object's position, normalize it, and then multiply by a speed factor. This creates a physics-based mouse-following movement.

Snapping the Object to Grid
On mouse release (mouse up event), the object's position is snapped to the grid by rounding its transform position values.
