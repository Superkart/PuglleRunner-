# Puggle Runner

**A 3D Endless Runner Game Built in Unity**

**Play Now**:  [Puggle Runner on itch.io](https://superkart.itch.io/puggle-runner)  
**Documentation**: [Project Documentation (Google Drive)](https://drive.google.com/drive/folders/1rZpKz6iwcojcCCZgeZKf-6mKarU1tSNy?usp=sharing)

---

## Overview

**Puggle Runner** is a fast-paced 3D endless runner game where players navigate through procedurally generated obstacles while collecting coins and achieving high scores. The game features dynamic difficulty scaling, smooth character controls, and polished visual effects that create an engaging arcade experience.

Players embark on an endless run, dodging obstacles and collecting coins as the game progressively increases in speed and difficulty. With intuitive lane-switching mechanics and responsive controls, Puggle Runner challenges players to beat their personal best while competing for high scores.

---

## Game Features

### Core Gameplay Mechanics
- **Endless Runner Gameplay**: Infinite procedurally generated levels with increasing difficulty
- **Lane-Based Movement**: Three-lane system with smooth horizontal transitions
- **Dynamic Speed Scaling**: Game pace increases progressively to challenge player reflexes
- **Obstacle Avoidance**: Various obstacle types requiring quick decision-making
- **Automatic Forward Movement**: Player moves forward continuously

### Player Systems
- **Smooth Movement Controls**: Responsive left/right lane switching with horizontal input
- **Character Animation**: Dynamic rolling animation tied to forward movement speed
- **Camera Follow System**: Smooth camera tracking with configurable smoothness factor
- **Collision Detection**: Tag-based collision system for obstacles and collectibles
- **Boundary Constraints**: Movement limited to playable area

### Progression and Scoring
- **Coin Collection System**: Collect coins throughout the run for score multipliers
- **Distance Tracking**: Real-time distance measurement in meters
- **High Score System**:  Persistent high score tracking across sessions
- **Score Calculation**: Combined score based on distance traveled and coins collected
- **Data Persistence**: Saves high scores between game sessions

### Visual and Audio
- **Fade Transition Effects**: Smooth fade-in on game start
- **Coin Rotation Animation**: Visual feedback for collectibles
- **Background Music**: Continuous audio loop during gameplay
- **Sound Effects**: Audio feedback for menu interactions and game events
- **Custom Shaders**: TextMesh Pro shaders for high-quality UI rendering

### UI and Menus
- **Main Menu**: Play and quit options with clean interface
- **Pause Menu**: In-game pause with resume, main menu, and quit functionality
- **Game Over Screen**: Display final score, distance, and high score with replay option
- **HUD Display**: Real-time coin count and distance display during gameplay

---

## Technical Implementation

### Game Architecture

**Technology Stack:**
- Unity 2020.2.5f1
- C# (. NET Framework)
- Visual Studio 2019
- Unity Physics System
- TextMesh Pro

**Project Structure:**

```
PuglleRunner-/
├── Assets/
│   ├── Scrips/
│   │   ├── PlayerController.cs         # Main player movement and collision
│   │   ├── PlayerSpin.cs              # Character rolling animation
│   │   ├── CameraFollow.cs            # Smooth camera tracking
│   │   ├── GameOver.cs                # Game over state management
│   │   ├── PauseMenu.cs               # Pause functionality
│   │   ├── MainMenu.cs                # Main menu controls
│   │   ├── SpawnManager.cs            # Coordinate road and obstacle spawning
│   │   ├── RoadSpawn.cs               # Procedural road generation
│   │   ├── ObstacleSpawner.cs         # Random obstacle placement
│   │   ├── CoinSpinning.cs            # Coin rotation animation
│   │   ├── Fadescreen.cs              # Scene transition effects
│   │   └── Manager Scripts/
│   │       ├── DataManager.cs         # Score and coin persistence
│   │       ├── AudioManager.cs        # Audio playback system
│   │       └── UiManager.cs           # UI updates and display
│   ├── GameManager.cs                 # Central game state controller
│   ├── Scenes/
│   │   ├── MainMenu
│   │   └── GameScene
│   ├── Prefabs/
│   │   ├── Road Tiles
│   │   ├── Obstacles
│   │   └── Collectibles
│   ├── Materials/
│   ├── TextMesh Pro/
│   │   └── Shaders/                   # Custom text rendering shaders
│   └── Resources/
│       └── AudioFiles/
├── ProjectSettings/
└── README.md
```

---

## Core Systems Implementation

### 1. Player Controller

**Movement System:**
```csharp
// PlayerController.cs - Lane-based movement with constraints
public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 3f;
    public SpawnManager spawnManager;
    public PlayerSpin PSpinRef;
    
    void Update()
    {
        float verticalMovement = 1f; // Constant forward movement
        float horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed / 2;
        float vericalClampedMovment = Mathf.Clamp01(verticalMovement) * movementSpeed;

        var currPos = transform.localPosition;

        // Constrain horizontal movement to lane boundaries
        if (currPos.x < -5. 6f)
        {
            if(horizontalMovement < 0f)
                horizontalMovement = 0f;
        }
        if (currPos.x > 1.6f)
        {
            if (horizontalMovement > 0f)
                horizontalMovement = 0f;
        }

        transform. Translate(
            new Vector3(horizontalMovement, 0, vericalClampedMovment) * Time.deltaTime
        );

        // Rotate character based on movement speed
        if (PSpinRef != null && ! GameOver. gameOver)
            PSpinRef. Rotate(vericalClampedMovment);
    }
}
```

**Collision Detection:**
```csharp
// PlayerController.cs - Tag-based collision handling
private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Obstacle"))
    {
        Debug.Log("GameOver");
        GameOver.gameOver = true;
    }

    if (other.CompareTag("SpawnTrigger"))
    {
        spawnManager.SpawnTriggerEnter();
    }
}
```

### 2. Character Animation

**Rolling Animation:**
```csharp
// PlayerSpin. cs - Dynamic rotation based on speed
public class PlayerSpin : MonoBehaviour
{
    float Speed = 1. 2f;
    
    public void Rotate(float horizontalMovement)
    {
        float fSpeed = horizontalMovement * Speed;
        transform.Rotate(fSpeed, 0, 0, Space.World);
    }
}
```

### 3. Camera System

**Smooth Follow Camera:**
```csharp
// CameraFollow.cs - Slerp-based smooth tracking
public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTransform;
    private Vector3 CameraOffset;

    [Range(0.01f, 1f)]
    public float SmoothnessFactor = 0.5f;

    void Start()
    {
        CameraOffset = transform.position - PlayerTransform.position;
    }

    void LateUpdate()
    {
        Vector3 NewPosition = PlayerTransform.position + CameraOffset;
        transform.position = Vector3.Slerp(
            transform.position, 
            NewPosition, 
            SmoothnessFactor
        );
    }
}
```

### 4. Procedural Level Generation

**Road Spawning System:**
```csharp
// SpawnManager.cs - Coordinate spawning systems
public class SpawnManager : MonoBehaviour
{
    public RoadSpawn roadSpawner;
    public ObstacleSpawner obstacleSpawn;

    void Start()
    {
        roadSpawner = GetComponent<RoadSpawn>();
        obstacleSpawn = GetComponent<ObstacleSpawner>();
    }

    public void SpawnTriggerEnter()
    {
        roadSpawner.MoveRoad();
    }
}
```

**Obstacle Placement:**
```csharp
// ObstacleSpawner.cs - Random obstacle generation
public class ObstacleSpawner : MonoBehaviour
{
    private List<Transform> m_obstacleTiles;
    public List<GameObject> ObstalePrefabs;
    public int ObjectCount = 20;
    List<int> m_spawnedElements;
    public List<GameObject> Lanes;
    List<GameObject> m_SpawnedObstacles;

    public void SpawnObstacles()
    {
        for (int i = 0; i < randomNumberObjects; i++)
        {
            int tileNumber = Random.Range(0, listSize);
            if (! m_spawnedElements.Contains(tileNumber))
            {
                m_spawnedElements.Add(tileNumber);
                SpawnObstacleAtPosition(m_obstacleTiles[tileNumber]. transform);
            }
        }
    }

    void SpawnObstacleAtPosition(Transform trans)
    {
        var obstacle = ObstalePrefabs[Random.Range(0, ObstalePrefabs.Count)];
        var obj = GameObject.Instantiate(obstacle, Vector3.zero, Quaternion. identity, trans);
        obj.transform. localPosition = Vector3.zero;
        m_SpawnedObstacles.Add(obj);
    }

    public void ResetAllObstacles()
    {
        foreach (var go in m_SpawnedObstacles)
        {
            GameObject.Destroy(go);
        }
        m_spawnedElements.Clear();
        SpawnObstacles();
    }
}
```

### 5. Game State Management

**Game Manager:**
```csharp
// GameManager.cs - Central game controller
public class GameManager : MonoBehaviour
{
    private GameObject player;
    int playerStartScore;
    DataManager datmgrRef;
    
    public static DataManager m_DataManager;
    public static AudioManager m_AudioManager;

    public static DataManager GetDataManager()
    {
        if (m_DataManager == null)
        {
            m_DataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        }
        return m_DataManager;
    }

    public static AudioManager GetAudioManager()
    {
        if (m_AudioManager == null)
        {
            m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
        return m_AudioManager;
    }

    private void Start()
    {
        GetAudioManager().PlayBackgroundMusic();
        player = GameObject.Find("Player");
        playerStartScore = Mathf.RoundToInt(player.transform.position.z);
        datmgrRef = GameManager.GetDataManager();
    }

    void Update()
    {
        int currPlayerDistance = Mathf.RoundToInt(player.transform.position.z);
        int currDistance = currPlayerDistance - playerStartScore;
        datmgrRef.SetDistance(currDistance);
    }
}
```

**Game Over System:**
```csharp
// GameOver.cs - End game state handling
public class GameOver : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            GameManager.GetDataManager().SaveData();
            GameManager.GetAudioManager().StopBackgroundMusic();
        }
    }

    public void Replay()
    {
        GameManager.GetAudioManager().PlaySfx();
        gameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        GameManager.GetAudioManager().PlaySfx();
        gameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
```

### 6. Pause System

**Pause Menu Controller:**
```csharp
// PauseMenu.cs - Pause functionality
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode. Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        GameManager.GetAudioManager().PlaySfx();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        GameManager.GetAudioManager().PlaySfx();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
```

### 7. Audio System

**Audio Manager:**
```csharp
// AudioManager.cs - Sound and music management
public class AudioManager : MonoBehaviour
{
    public AudioSource BackgroundMusicSource;
    public List<AudioSource> AudioSources;
    private AudioClip sfxSound;

    private void Start()
    {
        sfxSound = Resources.Load<AudioClip>("AudioFiles/game_sfx");
    }

    public void PlayBackgroundMusic()
    {
        if (BackgroundMusicSource != null)
        {
            BackgroundMusicSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (BackgroundMusicSource != null)
        {
            BackgroundMusicSource.Stop();
        }
    }

    public void PlaySoundEffect(string audioName)
    {
        var audioClip = Resources.Load<AudioClip>("AudioFiles/" + audioName);
        if (AudioSources[0] != null)
        {
            AudioSources[0].clip = audioClip;
            AudioSources[0].Play();
        }
    }

    public void PlaySfx()
    {
        if (AudioSources[1] != null)
        {
            AudioSources[1].clip = sfxSound;
            AudioSources[1].Play();
        }
    }
}
```

### 8. UI Management

**UI Controller:**
```csharp
// UiManager.cs - Real-time UI updates
public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI CoinsLabel;
    public Text GameDistanceLabel;
    public TextMeshProUGUI FinalScoreLabel;
    public TextMeshProUGUI HighScoreLabel;

    void Update()
    {
        if (CoinsLabel != null)
        {
            CoinsLabel.text = "Coins " + 
                GameManager.GetDataManager().GetCoins().ToString(); 
        }
        
        if (GameDistanceLabel != null)
        {
            GameDistanceLabel.text = 
                GameManager.GetDataManager().GetDistance().ToString() + "m";
        }

        if (GameOver.gameOver)
        {
            GameDistanceLabel.text = 
                GameManager.GetDataManager().GetDistance().ToString();
            FinalScoreLabel.text = "Score" + 
                GameManager.GetDataManager().GetScore().ToString();
            HighScoreLabel.text = "High Score" + 
                GameManager.GetDataManager().GetHighScore();
        }
    }
}
```

### 9. Visual Effects

**Fade Transition:**
```csharp
// Fadescreen.cs - Scene transition effect
public class Fadescreen:  MonoBehaviour
{
    public Image BlackFade;
    
    void Start()
    {
        BlackFade.canvasRenderer.SetAlpha(1f);
        Fadein();
    }

    void Fadein()
    {
        BlackFade.CrossFadeAlpha(0f, 2, false);
    }
}
```

**Coin Animation:**
```csharp
// CoinSpinning.cs - Collectible rotation
public class CoinSpinning :  MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 2, 0f, Space.World);  
    }
}
```

---

## Controls

### Keyboard Controls

| Key | Action |
|-----|--------|
| **Left Arrow** / **A** | Move Left |
| **Right Arrow** / **D** | Move Right |
| **Esc** | Pause / Resume Game |

**Gameplay Notes:**
- Character moves forward automatically
- Use horizontal input to switch between three lanes
- Movement is constrained to playable area boundaries

---

## How to Play

### Getting Started
1. **Visit itch.io**:  Go to [https://superkart.itch.io/puggle-runner](https://superkart.itch.io/puggle-runner)
2. **Click "Run game"**: Play directly in your browser - no download required
3. **Start running**: Use arrow keys or A/D to move between lanes

### Gameplay Tips
- **Master Lane Switching**: Quick reflexes are key to avoiding obstacles
- **Collect Coins**: Grab coins for bonus points and score multipliers
- **Watch the Distance**: Your distance traveled contributes to your final score
- **Progressive Difficulty**: The game speeds up as you progress - stay focused
- **Beat Your High Score**: Track your best performance and aim higher

### Scoring System
- **Distance**: Every meter traveled increases your score
- **Coins**:  Each coin collected adds bonus points
- **Combined Score**: Final score is calculated from distance and coins
- **High Score**: Best performance is saved and displayed

---

## Installation and Setup

### Playing the Game

**Option 1: Play Online (Recommended)**
- Visit [https://superkart.itch.io/puggle-runner](https://superkart.itch.io/puggle-runner)
- Click "Run game" to play in browser
- No download or installation required
- Works on Windows, Mac, and Linux browsers

**Option 2: Download Executable**
1. Visit the [itch.io page](https://superkart.itch.io/puggle-runner)
2. Download the platform-specific build
3. Extract the ZIP file
4. Run the executable
5. Enjoy offline gameplay

### Development Setup

**Prerequisites:**
- Unity 2020.2.5f1
- Unity Hub
- Visual Studio 2019

**Steps:**
1. Clone the repository
   ```bash
   git clone https://github.com/Superkart/PuglleRunner-. git
   cd PuglleRunner-
   ```

2. Open in Unity Hub
   - Open Unity Hub
   - Click "Add" and select the cloned folder
   - Open with Unity 2020.2.5f1

3. Open the MainMenu scene
   - Navigate to `Assets/Scenes/MainMenu`
   - Double-click to open

4. Press Play to test in Unity Editor

---

## Game Design Highlights

### Endless Runner Mechanics
- **Progressive Difficulty**: Speed increases over time to challenge players
- **Procedural Generation**: Infinite levels with randomized obstacle placement
- **Three-Lane System**: Classic endless runner lane-switching mechanics
- **Risk vs Reward**: Coin collection balanced against obstacle avoidance

### Visual Polish
- **Smooth Camera**:  Slerp-based camera following for cinematic feel
- **Character Animation**: Rolling animation tied to movement speed
- **Visual Feedback**: Clear indicators for collectibles and obstacles
- **Fade Transitions**: Professional scene transitions

### Player Feedback
- **Audio Cues**: Sound effects for all interactions
- **Visual Indicators**: Clear coin and distance display
- **Score Tracking**: Real-time performance metrics
- **High Score System**: Encourages replayability

---

## Development Highlights

### What Makes This Project Stand Out

**Procedural Generation**
- Dynamic road spawning system
- Random obstacle placement with no-repeat logic
- Scalable architecture for adding new obstacle types
- Efficient object management for performance

**Clean Architecture**
- Singleton pattern for manager classes
- Modular component design
- Clear separation of concerns
- Reusable prefab system

**Polish and User Experience**
- Smooth camera following with configurable parameters
- Responsive controls with boundary constraints
- Professional UI with TextMesh Pro
- Complete game loop from menu to game over

**Performance Optimization**
- Object destruction and recreation for obstacle reset
- Efficient collision detection with tags
- Resource loading for audio files
- Time-based movement for frame-rate independence

---

## Challenges and Solutions

### Challenge 1: Smooth Camera Following
**Problem**:  Jerky camera movement breaking immersion  
**Solution**: Implemented Slerp-based interpolation with configurable smoothness factor for cinematic tracking

### Challenge 2: Procedural Level Generation
**Problem**: Creating endless content without repetition  
**Solution**:  Developed trigger-based spawning system with random obstacle placement and track recycling

### Challenge 3: Lane Constraint System
**Problem**: Preventing player from moving outside playable area  
**Solution**:  Implemented boundary checks that zero out movement when limits are reached

### Challenge 4: Game State Persistence
**Problem**: Maintaining data across scenes and sessions  
**Solution**: Created DataManager singleton with save/load functionality and static access

### Challenge 5: Performance with Many Obstacles
**Problem**: Frame rate drops with numerous spawned objects  
**Solution**: Reset system that destroys and recreates obstacles, preventing accumulation

---

## Future Enhancements

### Planned Features

**Character System**
- **Character Selection Screen**: UI for choosing from multiple characters before gameplay
- **Multiple Playable Characters**: Different character models with unique visual styles
- **Character Unlocking**: Progression system to unlock new characters
- **Character Abilities**: Unique abilities per character (speed boost, shield, double jump)
- **Character Customization**:  Skins, colors, and accessories for personalization

**Power-Up System**
- **Speed Boost**: Temporary increase in movement speed
- **Shield**: Protection from one obstacle collision
- **Coin Magnet**: Automatically collect nearby coins
- **Score Multiplier**: Double or triple points for limited time
- **Invincibility**: Brief period of obstacle immunity

**Obstacle and Environment Variety**
- **Moving Obstacles**: Obstacles that shift between lanes
- **Animated Hazards**: Rotating barriers and swinging pendulums
- **Environmental Themes**: City, forest, desert, and space environments
- **Weather Effects**: Rain, snow, and fog for visual variety
- **Day/Night Cycle**: Dynamic lighting changes during gameplay

**Progression and Rewards**
- **Achievement System**: Unlockable achievements for milestones
- **Daily Challenges**: Special obstacle patterns with bonus rewards
- **Level System**: Player leveling with experience points
- **Currency System**: In-game currency for purchasing upgrades
- **Cosmetic Shop**: Purchase character skins and accessories

**Social Features**
- **Online Leaderboards**: Global score comparison
- **Friend Challenges**: Compete directly with friends' high scores
- **Social Sharing**: Share achievements and scores on social media
- **Replay System**: Save and share best runs

**Mobile Platform**
- **Touch Controls**: Swipe left/right for lane switching
- **Tilt Controls**: Device tilt for movement (optional)
- **Mobile UI Optimization**: Larger buttons and HUD elements
- **Performance Optimization**: Reduced graphics settings for mobile devices
- **Cross-Platform Save**:  Sync progress between PC and mobile

### Technical Improvements

**Performance Optimization**
- **Object Pooling**: Reuse game objects instead of destroy/instantiate cycles
- **Level of Detail (LOD)**: Reduce polygon count for distant objects
- **Occlusion Culling**: Hide objects not visible to camera
- **Texture Atlasing**: Combine multiple textures to reduce draw calls

**Gameplay Enhancements**
- **Dynamic Difficulty Curve**: Mathematical progression for balanced challenge
- **Adaptive Audio**: Music tempo increases with game speed
- **Tutorial System**: Interactive tutorial for first-time players
- **Checkpoint System**: Resume from checkpoints in extended runs

**Data and Analytics**
- **Cloud Save System**: Store player data in cloud for cross-device play
- **Analytics Integration**: Track player behavior and performance metrics
- **A/B Testing Framework**: Test different gameplay parameters
- **Crash Reporting**:  Automated error tracking and reporting

**Visual Enhancements**
- **Particle Effects**: Enhanced visual feedback for coin collection and collisions
- **Post-Processing Stack**: Bloom, color grading, and motion blur
- **Trail Effects**: Visual trail behind character during movement
- **Screen Shake**: Impact feedback on collisions
- **Lighting System**: Dynamic shadows and lighting effects

---

## Learning Outcomes

This project demonstrates proficiency in:

**Unity Game Development:**
- 3D game mechanics and physics
- Procedural content generation
- Scene management and transitions
- Prefab system and instantiation
- Component-based architecture
- Camera systems and following

**C# Programming:**
- Object-oriented design
- Singleton pattern implementation
- Event-driven programming
- Collision detection and response
- State management
- Resource management

**Game Design:**
- Endless runner mechanics
- Difficulty progression
- Player feedback systems
- Score and progression systems
- UI/UX design

**Software Engineering:**
- Modular code architecture
- Manager pattern for systems
- Code reusability
- Performance optimization
- Version control with Git

---

## Documentation and Resources

**Comprehensive Documentation**:  [Google Drive Folder](https://drive.google.com/drive/folders/1rZpKz6iwcojcCCZgeZKf-6mKarU1tSNy? usp=sharing)

The documentation folder includes:
- **Design Documents**: Game design specifications and mechanics
- **Technical Presentations**: Architecture diagrams and implementation details
- **Development Materials**: Progress reports and iteration logs
- **Visual Assets**: Screenshots and promotional materials

---

## Credits and Acknowledgments

**Development:**
- Game Design & Programming:  Superkart
- Unity Engine: Unity Technologies
- TextMesh Pro: Unity Technologies

**Tools and Resources:**
- Unity 2020.2.5f1
- Visual Studio 2019
- Git version control
- itch.io platform

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## Developer

**Superkart**

- GitHub: [@Superkart](https://github.com/Superkart)
- itch.io: [superkart.itch.io](https://superkart.itch.io)
- Project Repository: [PuglleRunner-](https://github.com/Superkart/PuglleRunner-)

---

## Play the Game

**Play Now on itch.io**:  [https://superkart.itch.io/puggle-runner](https://superkart.itch.io/puggle-runner)

**View Complete Documentation**: [Google Drive](https://drive.google.com/drive/folders/1rZpKz6iwcojcCCZgeZKf-6mKarU1tSNy?usp=sharing)

Experience endless runner gameplay with smooth controls and progressive difficulty.  Dodge obstacles, collect coins, and beat your high score in this polished arcade adventure. 

---

**Endless Runner | 3D Game Development | Unity | Procedural Generation**
