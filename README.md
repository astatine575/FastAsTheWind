# FastAsTheWind
To run the project, open the root of the repository in Unity 3d.
Next make sure that the build settings are setup properly. Go to File-Build Settings and select that option.
Be sure that the build heirarchy is as follows:
Menu/Menu Temp     				   0
WorldMap/WorldMap   			   1
Combat/Battle      				   2
IslandVisitation/IslandVisitation  3
If not all 4 scenes are selected in the build settings(which may happen) do the following:
Go into the assets folder in the project tab in unity and from there go into Menu and open up TempMenu scene by double clicking. Then reselect File-Build Settings then select "Add Open Scenes" to add TempMenu to the build settings if its not already there.
Do the same for the other 3 scenes, WorldMap in the WorldMap folder, Battle in the Combat folder, and IslandVisitation in the IslandVisitation folder.
Then drag each scene into the corresponding order above to be sure the game is properly built in that order.

With the build settings set up you can run the game in two ways, either select Build and Run from the Build Settings window and play the game from the generated executable, or if you want to run the game while still in Unity3D, go back to the assets tab and open again the TempMenu scene from the Menu folder.
From there select the "Play" Button at the top of the Scene window, and that will run the game from the start up menu scene.
