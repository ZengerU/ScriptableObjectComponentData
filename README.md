[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)

# Component Datafier

This tool enables the conversion of publicly available fields from any Unity Component into scriptable objects.

- [How to use](#how-to-use)
- [Installation](#install)
- [Configuration](#configuration)

## How to use

1. Right-click on the desired component and choose` Convert Data to ScriptableObject`.
![image](https://github.com/ZengerU/ComponentDatafier/assets/33237571/e6916477-6076-43db-ab48-e648e16cd46b)
2. In the project window, find the newly created scriptable object and generate an instance of it. It should be located under `Create/ScriptableData/<Namespace>/<ComponentName>`.
![image](https://github.com/ZengerU/ComponentDatafier/assets/33237571/7d144a74-7ec7-44bb-9ae2-d4c30eb5816b)
3. Attach the newly created companion component to a component with the desired data. The companion's name should be under `Data Based Companion/<ComponentName> Companion`.
![image](https://github.com/ZengerU/ComponentDatafier/assets/33237571/06178877-0fe5-447c-8a95-9b5e3b28fbbc)
4. Connect the scriptable object created in step 2 to the component created in step 3, and you're finished!


## Installation
Clone or download the repository and place its contents into the Assets folder.

## Configuration

If you want to use the sample folder but need to relocate the files outside of Assets, you may need to edit two locations:
1. [This line](https://github.com/ZengerU/ComponentDatafier/blob/main/Editor/ComponentClassCreator.cs#L12) to specify the new location of the component class template.
2. [This line](https://github.com/ZengerU/ComponentDatafier/blob/main/Editor/DataClassCreator.cs#L12) to specify the new location of the data class template.
3. 
Note: If the files are not found in the specified location, the Assets folder is searched by file name, and the first result is used.

## License

MIT License
