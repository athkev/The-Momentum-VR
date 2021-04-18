CPS 643 Virtual Reality
 Tae Hyun Ahn 500882444

[https://sites.google.com/view/the-momentum](https://sites.google.com/view/the-momentum)

User Manual

The Momentum is a single player Virtual Reality game where the player can experience movements that cannot be possibly done in real life. Players will take control of an android with astonishing movement strength that can jump and climb up walls with ease. The body of the android is visible to the player through Inverse Kinematics animation for deep gameplay experience.

In order to run my unity project, make sure of these few things:

- Turn on Steam
- SteamVR input settings
- Don&#39;t touch anything and play!

Go to Window-\&gt;SteamVR Input and check that all the actions are the same as the image.

![](RackMultipart20210418-4-1fganwx_html_4875f879f6b8829c.png) ![](RackMultipart20210418-4-1fganwx_html_5b39250f5bd6905c.png)
 Open binding UI and make sure the project default binding I made is activated.
 That&#39;s it! Press play and enjoy.

![](RackMultipart20210418-4-1fganwx_html_b32d22a1be44dd0.png)
 Unity has a bug where if you have a gameobject selected in the editor before pressing play, game will lag. So clear your selection before playing.

Description of my implementations

Scripts

**Inverse Kinematics:**
Animation rigging made the character move in a way the hands and head move.

- cs
 Set the position of each foot. Ground check raycast is used to determine if there is a ground below. If there is, feet are located to that hit point.
- cs
 Set the position and rotation of hands. Moving the hand bones moves the arms accordingly through the animation rigging. The script makes the bones follow the controllers with offsets.
- cs
 Makes the player body follow the player&#39;s camera (head). Offsets are used to fit camera and the body to the right place.

**Player movements:**

Configured rigidbody force and velocity.

- cs &amp; HandControls.cs
 Control of player controller actions and movements. Rigidbody velocity is set by the player&#39;s left joystick. Rotation of the player gameobject is controlled by right joystick action. Pressing the jump button adds Vector3.up force to the player. Grabbing a wall moves the player body depending on the controller velocity.
- cs
 Sets variables used for character animation (idle/walk/run/jump).

**Portal:**
Supports different render textures for both vr eyes through Vive Stereo-Renderer Plugin

- cs
 Set location of portals. Enables line renderer that guides where it will be sent.,
- cs
 Modified vive stereo-renderer plugin script so that when a player goes into a portal, velocity used to enter a portal will remain on the other side. Also changed so that the code works when the portal is set on horizontal space.

**Audio:**

- cs
 Play footstep audio when the character&#39;s feet hit the ground. Since the footIK.cs made the character feet to &#39;stick&#39; on the ground, another body that is invisible is used for accurate foot ground check.
- cs
 Play door open audio when the player enters the trigger collider. Play door close audio when the player exits the trigger collider. Spatial blend from an audio source is used for spatialized audio.

**Event:**

- cs &amp; secondPart.cs &amp; finalPart.cs

Except for firstDoor.cs, tts narrator audio plays when the player enters the trigger collider, starting the event.

FirstDoor.cs plays the event sequence from the beginning and opens the first door after a few seconds.

- cs &amp; MapOptimization2.cs
 Scripts for optimizing game performance. Disables part of the map if the player is not nearby.

Assets Imported
- Speedball Player by Character Ink (Character)

- 3D Free Modular Kit by Barking Dog (Map prefabs)

- 3D Scifi Kit Starter Kit by Creepy Cat (Map prefabs)

- SteamVR (plugin)

- Vive Stereo Rendering ToolKit (plugin)

Assets Created
- Wire cables (prop)

Audio Used
- Portal: Test Chamber 02 (Background Ambience Audio)

- Destiny 2: Mars door sound (Door open/close audio)

- TTS Amazon Brian (narration audio)

Side Note
Had lots of fun making this project. Really focused on player movements and climbing/releasing action. The hand controls are scripted so that the hand automatically releases from the wall when it gets too far from the initial grabbed position. It is possible for the player to push the body away with a hand collider while grabbing a wall which messes up the experience (you could essentially fly if i didn&#39;t implement that script). Portals were extra stuff I wanted to try out just for fun. Turned out it is harder than I expected as it seems from the original game. The portal I have configured works perfectly fine if the two portals are perpendicular from the ground, but not so much for horizontal portals. Floor colliders stop the player&#39;s velocity so making a hole to a collider on the portal position would solve it, there&#39;s that.