# WhiteFilecodeGenerator
<br>![Image Text](app-img_repo.png)
<br><br>
This small program allows you to generate filecodes for files stored in the white_img archive files from the FINAL FANTASY XIII trilogy.

# Usage
The below steps will assume that you are familiar with unpacking the filelist file with the WhiteBinTools and have some idea on how to add filepaths to the filelist file.

- Select the game for which you are generating the virutal path.
- Type in the path that matches the structure of a valid virtual path supported by the trilogy.
- For example, if you want to add a new music file path, then the virtual path would be like this:
  - ``sound/pack/8000/music_custom.win32.scd``
- On entering the file path, there will be an optional prompt to enter a file id or zone id. the former can be your choice but the latter is dependent on the game's own zone ids.
- After entering the id, the program will generate a filecode along with a binary breakdown of the filecode. you can use this filecode in the filelist file.

# Important
- You can currently generate filecodes only for these following directories and files:

  | Directories | File Types | 
  | --- | --- |
  | btscene/wdb/entry | .wdb files |
  | btscene/pack/clb | .clb files |  
  | btscene/pack/wdb | .wdb files |  
  | chr | .trb, .imgb and .mpk files |
  | event | .xwb, .bin and .imgb files |
  | gui/resident/autoclip | .imgb and .xgr files |
  | gui/resident/clipbg | .imgb and .xgr files |
  | gui/resident/monster | .imgb and .xgr files |
  | gui/resident/mission | .imgb and .xgr files |
  | gui/resident/tutorial | .imgb and .xgr files |
  | mot | .bin files |
  | sound/pack | .scd and .wpd files |
  | txtres/ac | .ztr files |
  | txtres/event | .ztr files |
  | txtres/zone | .ztr files |
  | vfx/chr | .imgb and .xfv files |
  | zone/z### | .wdb and .clb files |
