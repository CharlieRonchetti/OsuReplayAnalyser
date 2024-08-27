
# <p align=center>Osu Replay Analyser</p>
<p align=center>
  <a href="https://www.codefactor.io/repository/github/charlieronchetti/osureplayanalyser/overview/master"><img src="https://www.codefactor.io/repository/github/charlieronchetti/osureplayanalyser/badge/master" alt="CodeFactor" /></a>
  <a href="https://github.com/CharlieRonchetti/OsuReplayAnalyser/releases/tag/v1.0.0"><img alt="Version Badge" src="https://img.shields.io/badge/version-v1.0.0-blue"></a>
  <img alt="GitHub commits since latest release" src="https://img.shields.io/github/commits-since/CharlieRonchetti/OsuReplayAnalyser/latest">
</p>
<p align=center>A quick to setup tool for analysing osu!standard replay files (.osr)</p>

## Getting Started
Firstly, head to the repositories [releases](https://github.com/CharlieRonchetti/OsuReplayAnalyser/releases) page and download the ZIP file from the latest release (currently Version 1.0) and extract the contents of the zip to a new folder.

Instructions for using the analyser:

  1. Run the application once, this will generate replays and output folders in the current directory.
  2. Put any replay files (.osr) you want to analyse inside the replays folder.
  3. Run the application to generate hold time graphs for any replay files you placed in the replays directory.
  4. Find the generated PNG files in the output directory.

## Features
### Implemented

  - Hold time analyser. Generates graphs that show the players hold times in ms for each keypress they performed during a play, for all provided replay files.
  
    Example output:

    ![Goombs - DragonForce - Valley of the Damned  Apocalypse  (2024-08-08) Osu-1 osr](https://github.com/user-attachments/assets/22360c6d-427b-4aa7-9a17-ec4496a6638a)

### Planned for future releases

  - GUI implementation to display all replay stats (total score, 300/100/50 count, max combo etc.) as well as display hold time graphs in-app rather than outputting them to a png.
  - Aim related statistics e.g. number of misses caused by under/overaiming.
  - Visually represent where the players cursor was in relation to the current circle when they received a miss judgement.

## Development

If you have ideas for features you'd like to see implemented into the project, or encounter any bugs when using the application, please feel free to open an issue about them [here](https://github.com/CharlieRonchetti/OsuReplayAnalyser/issues).
