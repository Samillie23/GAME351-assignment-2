# GAME351-assignment-2
School of Information, University of Arizona 
September 20th, 2025

Members:
Anis Feria
Kristal Gutierrez
Rudy (Rodolfo Bours)
Alani Jordan

Implemented features:
1. Driving a Hovercraft
   Movement was done through the physics engine. "A" and "D" turn the car while "W" and "S" move forwards and backwards. Raycasts are used on the corners of the cars to make them go up whenever they start to approach a hill. This way it tilts while moving over terrain. Follow camera was done using cinemachine.
2. Three Car Types
   The average car is the starting purple and green car. The fast one is the long, black and yellow car. The cornering car is the white and purble one. Each has stats matching the type of car it is with their fast speed and slower cornering or vice-versa. All are framerate independent and the speed and turning values can be changed through the inspector.
3. Hovercraft Levitation
   Using the same Raycasts from before and Unity's physics the car is pushed upwards from each of the four corners to create a hover effect. Because of the usage of the physics engine to do this the car already moves up and down slightly while idling until the car stabilizes.
4. Toggling Between Cars
   "C" cycles between the 3 cars starting with the fast one, average and then cornering. The active camera and car is changed through a script. While the priority of the camera is changed to swap between them, the inactive cars' movement script is disabled. The cars can be swapped continously and will loop back to the first car.
5. More Realistic Effects
   A sound script was added to each car so that it makes sounds when accelerating and decelerating through AudioSource. A hover engine (cube) is added to the bottom of all three cars and both the engine block and lights/tron features on the cars glow through the use of bloom.

To install the project, follow these steps:
1. Unpack the zip file
2. In Unity "Add project from disk"
3. Then select the files that you unpacked
4. Open and load the new project
5. The assignment should then be usable.
   
Rendering Pipeline: None (Render Pipeline Assist)

Credits:
 
Sample code by Leonard D. Brown, University of Arizona.
This program was developed for educational purposes.

Freeware media assets were used from the following sources:
(1) http://millionthvector.blogspot.com/
(2) https://assetstore.unity.com/
(3) https://www.gameartguppy.com/
(4) https://www.videvo.net/royalty-free-sound-effects/
(5) https://freesound.org/search/
