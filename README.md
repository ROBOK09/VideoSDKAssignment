# VideoSDKAssignment (Target Shooter)
## Project Overview
### Project Version Details -
Project Made in Unity 6 (Version 6000.0.26f1) in Universal Render Pipeline.

### Setup Guidelines - 
You can clone the above github repository and Open the VideoSDK Assignment folder with the appropriate version through Unity Hub or by downloading the appropriate version manually.

### Goal of the Game -
You need to increase your score by hitting the spawned targets before your health reaches 0, which triggers the game over state. If you hit the target successfully before it despawns your score increments by 10 and if the enemy despawns and you are unable to hit the enemy then your health decreases by 5.

### Project Details
- The Minimap is displayed on the top right corner of the screen when the game is in ingame state.
- Enums are used to track gamestate and enemy type.
- Events of various types are used for Pub Sub architecture.
- Singleton is used for the management of UI, Audio, Health and Score.
- Getters and Setters are used for data protection.
- Coroutines are used for despawning of gameobjects with Unity Lifecycle which provides frame accurate data unlike invoke (for methods) and async/ await (close but not completely synced with unity frames).
- Please refer to the Inline Comments in the script files for more details.

### Design Patterns Used - 
- Publisher Subscriber Pattern - The Pub/Sub (Publisher/Subscriber) model is a messaging pattern used in software architecture to facilitate asynchronous communication between different components or systems. In this model, publishers produce messages that are then consumed by subscribers.
Pub Sub Architecture used in Score Manager to keep track of score update and update it on the UI accordingly when the event is fired from Score Manager and so on and so forth. Please refer to the Inline Comments in the script files for more details.

- Singleton Pattern - The Singleton method or Singleton Design pattern is one of the simplest design patterns. It ensures a class only has one instance, and provides a global point of access to it. 
Singleton Pattern used in Score Manager to keep track of score update and update it on the UI accordingly when the event is fired from Score Manager without having any local reference of Score Manager, which is accessed globally through singleton instance and so on and so forth. Please refer to the Inline Comments in the script files for more details.

- Factory Pattern - The Factory Method Design Pattern is a creational design pattern that provides an interface for creating objects in a superclass, allowing subclasses to alter the type of objects that will be created. This pattern is particularly useful when the exact types of objects to be created may vary or need to be determined at runtime, enabling flexibility and extensibility in object creation.
Factory Pattern used in Enemy Factory to specify the enemy being spawned through Enemy Spawner class. Please refer to the Inline Comments in the script files for more details.

### Assumptions Made During Development -
Made for Windows Devices for quick prototyping and testing. 

## Important Information / Controls
| Information / Controls  | Key / Button / Interactions |
| ------------- | ------------- |
| Interaction with UI Button / Elements  | Left Mouse Button  |
| WASD  | Move / Locomotion  |
| Shoot Bullet  | Left Mouse Button  |
| Pause/Resume Game  | Escape  |
| Green Ghost Enemy Despawn Time  | 7 seconds  |
| Blue Shade Enemy Despawn Time  | 5 seconds  |
| Red Phatom Enemy Despawn Time  | 3 seconds  |
| Score Increment  | Current Score + 10  |
| Health Increment  | Current Health - 5  |

## Important Links / ScreenShots
### Windows Build Link - https://drive.google.com/file/d/1y7hQUO7Jb_tjFX5_IZrOneSDhh_clfpi/view
### Build Video Link - https://drive.google.com/file/d/13-SzGC0p24C7MSEwtB34KJJdGoSR2yrA/view
### Public Github Repository Link (Along with Readme File) - https://github.com/ROBOK09/VideoSDKAssignment

## ScreenShots
![image](https://github.com/user-attachments/assets/11ec686f-8511-429c-882b-c67a7c2f0ea3)

![image](https://github.com/user-attachments/assets/90d88881-8d03-40f4-8b16-18ee0cdd8341)

![image](https://github.com/user-attachments/assets/b3ec4426-92b7-4e42-995b-35f3ef02f092)

![image](https://github.com/user-attachments/assets/87129194-a0da-4058-ab79-6c8d37a572e2)

![image](https://github.com/user-attachments/assets/bfbecc51-0394-4ac4-b230-41aed0986297)
