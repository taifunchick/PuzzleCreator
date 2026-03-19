# Puzzle Master - Complete Jigsaw Puzzle System

[![Unity Version](https://img.shields.io/badge/Unity-2021.3%2B-blueviolet)](https://unity.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

**Puzzle Master** is a ready-to-use jigsaw puzzle template for Unity. It includes a menu with a gallery system, multiple levels, smooth drag-and-drop mechanics, and progress saving. Perfect for game jams, prototypes, or as a foundation for a full puzzle game.

![Puzzle Master Demo](Screenshots/PreviewImage)

## ✨ Features

- ✅ **Two complete scenes** – Menu (with gallery) and Game scene
- ✅ **Gallery system** – Paginated gallery with unlockable artwork
- ✅ **Level management** – Easy level creation via ScriptableObjects
- ✅ **Randomized piece placement** – Pieces spawn in shuffled positions
- ✅ **Smart snapping** – Pieces snap only when dragged over reference art
- ✅ **Progress saving** – Completed levels saved with PlayerPrefs
- ✅ **Victory panel** – With "Next Level" and "Menu" buttons
- ✅ **Clean, commented code** – Organized with namespaces to avoid conflicts
- ✅ **Documentation included** – PDF guide with setup instructions

## 🎮 How to Play

1. Select a level from the gallery (unlocked levels appear as images)
2. Drag puzzle pieces onto the semi-transparent reference art
3. Pieces snap into place when positioned correctly
4. Complete all pieces to win and unlock the next level

## 🚀 Quick Start

### Prerequisites
- Unity 2021.3 or newer
- TextMesh Pro (installs automatically on import)

### Installation

1. **Clone or download** this repository
2. Open your Unity project
3. Import the package:
   - Assets → Import Package → Custom Package
   - Select `PuzzleMaster.unitypackage`
4. If prompted, install TextMesh Pro Essentials
5. Open the `Menu` scene and press Play!

### Creating Your First Level

1. Right-click in Project window → **Create → Puzzle Master → Puzzle Data**
2. Name it (e.g., "Level_1")
3. Fill in the fields:
   - **Full Art** – Completed picture (for gallery)
   - **Reference Art** – Semi-transparent version (use same sprite with alpha 0.5)
   - **Piece Sprites** – 8 individual puzzle piece sprites
   - **Piece Positions** – Local positions where pieces snap (relative to art center)
   - **Start Positions** – Starting positions on screen
4. Add this PuzzleData to:
   - `GalleryManager` → `All Levels` array (in Menu scene)
   - `GameManager` → `All Levels` array (in Game scene)

## 🧩 Scripts Overview

| Script | Description |
|--------|-------------|
| `GameManager.cs` | Controls gameplay, loads levels, tracks piece placement |
| `GalleryManager.cs` | Manages gallery UI, pagination, level selection |
| `PuzzlePiece.cs` | Drag & drop logic, snapping to correct position |
| `SaveManager.cs` | Static class for saving/loading completed levels |
| `PuzzleData.cs` | ScriptableObject container for level data |

## ⚙️ Configuration

### Gallery Settings
- **Items Per Page** – Number of icons per gallery page
- **Navigation Buttons** – Assign Prev/Next buttons
- **Gallery Container** – Parent transform for spawned icons

### Game Settings
- **Snap Distance** – How close a piece must be to snap (default: 20)
- **Min Drag Distance** – Minimum movement to register as drag (default: 10)

## 🛠️ Customization

### Adding More Levels
1. Duplicate an existing PuzzleData or create new ones
2. Assign new sprites and positions
3. Add to both manager arrays

### Changing Gallery Layout
- Adjust `Grid Layout Group` on `GalleryContainer`
- Modify `itemsPerPage` in GalleryManager

### Saving System
Progress is stored in PlayerPrefs with key `"CompletedLevels"`. You can extend `SaveManager.cs` to use binary files or JSON.

## 📦 Dependencies

- **TextMesh Pro** – Required for UI text (auto-installs via Package Manager)

## 🤝 Contributing

Contributions are welcome! Feel free to:
- Report bugs
- Suggest features
- Submit pull requests

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📧 Contact

For questions or support:  
[TAIFUN](https://telegram.org/k/#@taifunblade)  
[GitHub Profile](https://github.com/taifunchick)

---

**⭐ If you find this useful, please consider starring the repository!**
