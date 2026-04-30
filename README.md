
# VR Periscope Simulator

A high-fidelity virtual reality submarine experience built with Unity 6. Take command of a naval periscope to observe the surface world, manage optical settings, and engage in tactical reconnaissance.

## Tech Stack

| Category | Technology |
|----------|------------|
| Engine | Unity 6 (6000.0.36f1) |
| XR Framework | XR Interaction Toolkit (XRI) |
| Platform | VR (OpenXR compatible) |

## Features

**Realistic Periscope Mechanics**
- 360° continuous horizontal panning
- Vertical height/extension control
- Adjustable zoom and depth of field (DoF)

**Search & Find Minigame**
- Built-in objective system requiring players to locate and identify specific environmental objects
## Getting Started

### Prerequisites

- Unity Hub and Unity 6 (6000.0.36f1)
- VR headset (Quest 3, Index, etc.) with Link cable or AirLink

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Issiins/Periscope-simulator.git
   ```

2. Open in Unity Hub

3. Packages will auto-resolve via the Package Manager (XR Interaction Toolkit dependencies)

## Controls (XRI)

| Action | Input |
|--------|-------|
| Rotate periscope | Grip + grab handles |
| Adjust periscope parameters (zoom, depth of field, etc.) | Control table (switches, dials, etc.) |
| Manual movement | Left analog stick |
| Teleport | Push right analog stick forward + release |
| Snap rotation | Right analog stick left / right |

*Controls are customizable in the XRI Action Map*

## Project Structure

```
Core/
├── Audio/                   # Sound management
│   └── SFX/                 # Sound effects
├── Materials/               # URP Shaders
├── Models/                  # Scene environment
├── Prefabs/                 # Ready-to-use XR grabables and UI elements
├── Resources/               # Runtime-loaded assets (if applicable)
├── Samples/                 # Demo scenes or example configurations
├── Scenes/                  # Main Game
├── Scripts/                 # C# Logic
│   ├── Audio/               # Sound triggers and spatializers
│   ├── Control Table/       # Input mapping for the periscope desk
│   ├── Data/                # Save data
│   ├── Periscope Trans./    # Rotation, height, and zoom logic
│   ├── Settings/            # Graphics and VR comfort options
│   └── UI/                  # Menus and Minigame HUD logic
├── Sprites/                 # 2D elements for UI and overlays
└── Textures/                # PBR maps (Albedo, Normal, Metallic)
