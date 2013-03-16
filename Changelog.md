Changelog
=========

03-15-2013
----------
 - Finalized the fadeout/fadein of gamestates
 - Added dungeon generator for level class
 - Fixed the entity starting at an inconveniant location
 - Changed fonts to a system defined font
 - Added direction to the default save initializer
 - Made several methods in the Level class virtual for implementing different level types
 - Added DrawState function rather than Draw function --------- I will fix this

03-14-2013
----------
 - Began transition feature for game states
 - Added glowing effect to controls (ControlEffects.GLOW)
 - Changed spritebatch initialization to the Initialize() method in TestGame.cs

03-10-2013
----------
 - Added load game screen (fully functional)
 - Added a new level loading system
   - Not working yet

03-09-2013
----------
 - Allow pseudo 3D drawing by drawing specific layers first
 - Moved the drawing of entities to the level manager class


03-08-2013
----------
 - Fully functional saving of game data
 - Added the "New Game" screen
 - Added a TextBox control to the GameHelperLibrary
 - Game now loads saves with multiple characteristics
 - Added a public variable, "Texture" to the DrawableRectangle
 - Changed max health of an entity from 20 to 100 (not sure why it was 20)
 - Added "Continue Game" ability
 

03-07-2013
----------
 - Added the ability to save game data by pressing ESCAPE
 - Added another gamestate and font type
 - Fixed GameHelperLibrary.Control.LinkLabel constructor
 - Fixed title screen not having a title
