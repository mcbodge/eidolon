# Table of Contents
* [Information](#information)
  * [Tutorial](#tutorial)
  * [Download](#download)
* [Developers](#developers)
  * [Rules](#rules)
  * [Requirements](#requirements)
  * [Kanban](#kanban)

<div id='information'/>
# Information

<div id='tutorial'/>
### Tutorial
Coming soon

<div id='download'/>
### Download
Latest version: <a href="https://github.com/mcbodge/eidolon/releases/tag/v0.5.1-alpha"><b>0.5.1</b></a>

You can click on the release button above, or just download one of these zip files.
* <a href="https://github.com/mcbodge/eidolon/releases/download/v0.5.1-alpha/Windows-stable.zip">Windows</a>
* <a href="https://github.com/mcbodge/eidolon/releases/download/v0.5.1-alpha/OSX-stable.zip">OSX</a>

Once downloaded, extract all the contents where you want and double click Eidolon.exe (Win) or Eidolon.app (OSX)

<div id='developers'/>
# Developers

<div id='rules'/>
### Rules
Adventure Creator has some bugs related to cameras when using Unity3D for linux. For the best experience we suggest using Unity3D with Windows. Shader with Unity3D for Linux and OSX are compiled using OpenGL, the windows version uses DirectX and could give compilation error even if on linux it was clean.

We highly recommend:
* <b>Test</b> all Shaders or Assets with a windows build of Unity before pushing.
* Always <b>pull before pushing</b>
* After importing a model from blender, remove the mark on "Import Materials" in the unity Inspector, otherwise it will import a white material for every component of the model.

<div id='requirements'/>
### Requirements
* Unity3d 5.3.x
* Blender

<div id='kanban'/>
### Project Kanban
* http://bit.ly/1PJOLI9 (please ask mcbodge for an edit-mode link)
