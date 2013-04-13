Changelog
=========

__04-13-2013__
----------
 - Added a knockback method to the Entity class
 - Added traits to player class
   - Strength: how much damage the player deals
   - Dexterity: how fast the player moves
   - Intelligence: how fast the player's mana recovers; how strong magic is
   - Athleticism: how fast the player's stamina recovered
   - Constitution: how much of a chance there is for a critical hit
   
__03-24-2013__
----------
 - Began working on pathfinder AI for entities

__03-23-2013__
----------
 - Added SelectLevelType screen

__03-22-2013__
----------
 - Fixed bug when saving experience data
 - Added features to GameHelperLibrary
   - Added the glow effect for picture boxes and labels

__03-18-2013__
----------
 - Fixed the crash when you pressed enter before the game loads using epsilon
 - Fixed crash when pressing enter twice going into new game screen
 - Changed zooming to be updated with each game update
 - __I NEED HELP WITH THE CAMERA CLASS!__

__03-17-2013__
----------
 - Added full functionality to camera zooming
 - Added new sprites for the player... I'm just reusing TLOZ: ripped sprites
 - Added attacking function + animation to the player ----> Very unorganized, would like fixing there
 - Added features to the GameHelperLibrary
   - Allowed for drawing horizontally flipped sprites and animtations
 - Added the feature to import custom spritesheets and animations

__03-16-2013__
----------
 - Fixed the Move function in Entity
 - Fixed the Camera when the player stops moving

__03-15-2013__
----------
 - Finalized the fadeout/fadein of gamestates
 - Added dungeon generator for level class
 - Fixed the entity starting at an inconveniant location
 - Changed fonts to a system defined font
 - Added direction to the default save initializer
 - Made several methods in the Level class virtual for implementing different level types
 - Added DrawState function rather than Draw function ---------> I will fix this

__03-14-2013__
----------
 - Began transition feature for game states
 - Added glowing effect to controls (ControlEffects.GLOW)
 - Changed spritebatch initialization to the Initialize() method in TestGame.cs

__03-10-2013__
----------
 - Added load game screen (fully functional)
 - Added a new level loading system
   - Not working yet

__03-09-2013__
----------
 - Allow pseudo 3D drawing by drawing specific layers first
 - Moved the drawing of entities to the level manager class


__03-08-2013__
----------
 - Fully functional saving of game data
 - Added the "New Game" screen
 - Added a TextBox control to the GameHelperLibrary
 - Game now loads saves with multiple characteristics
 - Added a public variable, "Texture" to the DrawableRectangle
 - Changed max health of an entity from 20 to 100 (not sure why it was 20)
 - Added "Continue Game" ability
 

__03-07-2013__
----------
 - Added the ability to save game data by pressing ESCAPE
 - Added another gamestate and font type
 - Fixed GameHelperLibrary.Control.LinkLabel constructor
 - Fixed title screen not having a title
