# New Scene Management Plugin for Unity 5.6
This Unity Editor plugin enhances the process of creating new scenes in Unity 5.6 by providing customizable scene creation options. Users can set preferences for creating new scenes, such as whether to include a camera, skybox, or directional light, or to use a predefined template scene.

## Features
    Customizable Scene Creation:
        Choose between different scene types: Default, Parametric, or Template.
        Automatically add or remove a camera, skybox, and directional light based on user preferences.
        Load a predefined template scene when creating a new scene.
        
    Template Scene Protection:
        Prevent accidental overwriting of the template scene by offering a "Save As" dialog if the user attempts to save over it.

    Editor Preferences Integration:
        Easily configure scene creation preferences through the Unity Editor Preferences window.

## Installation
    Clone or download the repository.
    Place the NewSceneManagement folder inside your Unity project's Assets/Editor directory.
    Open Unity and navigate to Edit > Preferences... to configure your scene creation preferences.

## Usage
Setting Up Scene Creation Preferences
    Go to Edit > Preferences... in the Unity Editor.
    Select the New Scene tab.
    Choose the desired scene type:
        Default: Creates a new scene with Unity's default settings.
        Parametric: Allows you to specify whether to include a camera, skybox, and directional light.
        Template: Uses a predefined template scene. You can select the template file path using the provided field and picker.

## Creating a New Scene
When you create a new scene (via File > New Scene or any other method), the plugin automatically applies your preferences. If the selected scene type is Template, the specified template scene will be loaded.
Protecting the Template Scene

If you modify the template scene and attempt to save it with the same name, the plugin will prompt you to save the scene with a new name, preventing accidental overwriting of the template.
Contributing

If you would like to contribute to this project, feel free to submit a pull request. Please ensure that your code follows the coding standards used in this project.
License

This project is licensed under the GPL-3.0 license. See the [LICENSE](/LICENSE) file for more details.

## Contact
For any questions or suggestions, feel free to open an issue on GitHub or contact me directly.
You can customize this template further to better suit your project or add any additional sections you deem necessary.
