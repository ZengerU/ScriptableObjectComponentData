[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)

# Unity Folder Templator
A small tool to create templates for folders with certain files and subdirectories within.

- [How to use](#how-to-use)
- [Install](#install)
- [Configuration](#configuration)

<!-- toc -->

## How to use

The package comes with a sample folder template. To use it:
1. Right click anywhere in the project panel and navigate to `Create/Folder Templates/Featuere`  
![1](https://github.com/ZengerU/UnityFolderTemplator/assets/33237571/6d84d2b5-c108-47e5-b2eb-d16d4f965fad)
2. In the opened up window, enter a folder name and select which folders/files you want to create.  
![2](https://github.com/ZengerU/UnityFolderTemplator/assets/33237571/b4556a45-6107-4941-8c79-154fe86f13b7)
3. Press create!  
![3](https://github.com/ZengerU/UnityFolderTemplator/assets/33237571/2392c177-c67f-4455-9705-3c559b637d1e)


## Install
Clone or download the repository and place the contents in the `Assets` folder.

## Configuration

If you wish to use the sample folder but want to move the files outside of `Assets` note that there is a hard path you need to edit. Change [this](https://github.com/ZengerU/UnityFolderTemplator/blob/39e66710cfb7003279a5befa845aea518e13bd5a/Samples/Editor/FeatureFolderCreator.cs#L19) line to reflect the new location.

To create new folder templates:
1. Create a class and inherit from `Creator`.
2. Create a context menu action:
```
        [MenuItem("Assets/Create/Folder Templates/<TEMPLATE_NAME>", false, 19)]
        public static void ShowMyEditor()
        {
            EditorWindow wnd = GetWindow<FeatureFolderCreator>(true, "<TEMPLATE_NAME> Folder Creator", true);
            wnd.maxSize = new Vector2(400, 600);
        }
```
Change <TEMPLATE_NAME> to a user friend name for template. Additionally you can set the values in `wnd.maxSize = new Vector2(400, 600);` for a different max size.
3. Override `Folders` property.
```
protected override List<Folder> Folders { get; } = new()
{
}
```
4. Populate the newly created `Folders` property with your structure.

## License

MIT License
