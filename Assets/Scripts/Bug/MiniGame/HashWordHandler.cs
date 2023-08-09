using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using PlayerController;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Bug.MiniGame
{
    public class HashWordHandler : MonoBehaviour
    {
        #region Statements

        [SerializeField] private TextMeshProUGUI _inputHashWord;
        [SerializeField] private TMP_InputField _inputDecodeWord;
        private string _hashWord;
        [SerializeField] private GameObject _menuHint;
        
        [SerializeField] private Image _screen;
        [SerializeField] private Color _screenColor;
        
        private readonly List<string> _messages = new()
        {
            "HELLO",
            "I AM LOST",
            "DO YOU LIKE TO PLAY",
            "FPS OR RPG",
            "SYNTY STUDIO RULES",
            "ASSET PACK",
            "POLYGON WORLD",
            "RETRO GAMING",
            "BUY THIS DLC",
            "GAMER FOR LIFE",
            "ARCADE CLASSICS",
            "SAVE YOUR GAME",
            "LEVEL UP",
            "MULTIPLAYER ONLINE",
            "SINGLE PLAYER",
            "PLUG IN YOUR CONSOLE",
            "EXPLORE THE WORLD",
            "AVATAR CUSTOM",
            "VIRTUAL REALITY",
            "SYNTY GRAPHICS",
            "UNITY OR UNREAL",
            "PC MASTER RACE",
            "CONSOLE WARS",
            "DESIGN BY SYNTY",
            "LOOT BOX",
            "BATTLE ROYALE",
            "HD RESOLUTION",
            "GAME OVER",
            "TRY AGAIN",
            "CREDIT ROLL",
            "EPIC QUEST",
            "DUNGEON RAID",
            "MOBILE GAMING",
            "COSMETIC SKIN",
            "OPEN WORLD",
            "CHARACTER DESIGN",
            "PLAY AGAIN",
            "FREE TO PLAY",
            "FINAL BOSS",
            "ESPORTS READY",
            "MULTIPLAYER MATCH",
            "POLYGON HEROES",
            "SYNTY ADVENTURE",
            "LEVEL EDITOR",
            "BUILD YOUR WORLD",
            "MOTION CAPTURE",
            "CHARACTER MODEL",
            "TOP SCORE",
            "RETRO REVIVAL",
            "VIRTUAL CHARACTER",
            "SYNTY FAN",
            "COMPLETE TUTORIAL",
            "ACHIEVEMENTS",
            "PLOT TWIST",
            "SPEED RUN",
            "CONTINUE?",
            "WORLD BOSS",
            "VIRTUAL REALITY",
            "RPG ELEMENTS",
            "FANTASY WORLD",
            "RANK UP",
            "GAMING CONVENTION",
            "PRESS START",
            "NEXT GENERATION",
            "EARLY ACCESS",
            "DAILY QUEST",
            "MAIN MENU",
            "GAMING HEADSET",
            "PREORDER NOW",
            "NOSTALGIA TRIP",
            "STRATEGY GUIDE",
            "VIRTUAL TOUR",
            "SYNTY'S LEGENDS",
            "CUSTOM LEVEL",
            "INDIE DEVELOPER",
            "COLLECT THEM ALL",
            "GAMING SESSION",
            "CHOOSE YOUR PATH",
            "UNREAL GRAPHICS",
            "PIXEL PERFECT",
            "JOIN THE RAID",
            "STORY MODE",
            "FIGHTING GAME",
            "CHALLENGE ACCEPTED",
            "HERO OR VILLAIN",
            "VICTORY ROYALE",
            "PLAYER ONE READY",
            "ONLINE FRIENDS",
            "JOIN THE GUILD",
            "TREASURE HUNT",
            "PLAY TO WIN",
            "SURVIVAL MODE",
            "GAMING RIG",
            "BEST GRAPHICS",
            "ONLINE MATCH",
            "VOICE CHAT ON",
            "COOPERATIVE PLAY",
            "ADVENTURE AWAITS",
            "SIDE QUEST",
            "POWER UP",
            "DREAM WORLD",
            "ELITE GAMER",
            "RETRO MUSIC",
            "FANTASY ROLEPLAY",
            "MYTHICAL CREATURES",
            "GAMING MARATHON",
            "FIGHT THE DRAGON",
            "PRO GAMER MOVE",
            "FOLLOW THE STORY",
            "BATTLE ARENA",
            "COLLECTIBLE CARDS",
            "MISSION SUCCESS",
            "UNLOCKABLE CONTENT",
            "EXPANSION PACK",
            "DIGITAL DOWNLOAD",
            "IN-GAME PURCHASE",
            "GAMING COMMUNITY",
            "LEVEL DESIGN",
            "GAMING PLATFORM",
            "SYNTY FANTASY",
            "GAMING LEGACY",
            "MAIN CHARACTER",
            "MYSTICAL JOURNEY",
            "PROTECT THE REALM",
            "CHOOSE YOUR CLASS",
            "GAMING REVOLUTION",
            "FANTASY KINGDOM",
            "BATTLE SYSTEM",
            "GAMING GALAXY",
            "EPIC BATTLES",
            "SYNTY UNIVERSE",
            "GAME DEVELOPMENT",
            "JOIN THE TEAM",
            "GAMER FOR LIFE",
            "CHOOSE YOUR FATE",
            "GAMING KINGDOM",
            "PLAY THE STORY",
            "ARCADE MACHINE",
            "ONLINE LEADERBOARD",
            "VIRTUAL ADVENTURE",
            "DYNAMIC DUO",
            "EASTER EGGS",
            "SYNTY MASTERPIECE",
            "PVP BATTLEGROUND",
            "GAMING EVOLUTION",
            "CONSOLE GENERATION",
            "SYNTY'S BEST",
            "RPG LORE",
            "JUMP AND RUN",
            "GAMING LEGENDS",
            "HERO'S JOURNEY",
            "ACTION ADVENTURE",
            "WIN THE GAME",
            "GAMER'S DREAM",
            "BATTLE FOR GLORY",
            "CHOOSE YOUR HERO",
            "ONLINE ADVENTURE",
            "FANTASY ADVENTURE",
            "SYNTY EPICS",
            "MMORPG KINGDOM",
            "ACTION PACKED",
            "GAMER'S CHOICE",
            "WIN OR LOSE",
            "GAMER'S PARADISE",
            "ELITE SKILLS",
            "CHALLENGE THE BOSS",
            "SYNTY MAGIC",
            "VICTORY OR DEFEAT",
            "POWER LEVELING",
            "STORY DRIVEN",
            "MULTIPLAYER MAYHEM",
            "RPG HERO",
            "GAMING ODYSSEY",
            "PLAY WITH FRIENDS",
            "EPIC CHALLENGE",
            "GAMING WIZARD",
            "ADVENTURE QUEST",
            "GAMING GLORY",
            "HEROIC BATTLE",
            "DYNAMIC GAMEPLAY",
            "MMO ADVENTURE",
            "JOIN THE QUEST",
            "GAMER'S JOURNEY",
            "UNLOCK THE POWER",
            "FIGHT FOR HONOR",
            "CO-OP QUEST",
            "LEVEL UP FAST",
            "ACTION RPG",
            "BECOME THE HERO",
            "GAMER'S DESTINY",
            "BATTLEFIELD HERO",
            "SYNTY WORLDS",
            "GAMING TRIUMPH",
            "JOIN THE BATTLE",
            "ONLINE WARRIOR",
            "GAMING MASTERY",
            "SYNTY ADVENTURES",
            "GAMING EPIC",
            "UNSTOPPABLE HERO",
            "MMO RPG",
            "SYNTY'S WORLDS",
            "ONLINE EMPIRE",
            "BATTLE STRATEGY",
            "HERO'S QUEST",
            "EPIC GAMING",
            "GAMER'S WORLD",
            "FANTASY QUEST",
            "WIN THE BATTLE",
            "SYNTY SAGA",
            "EPIC GAMER",
            "GAMING HERO",
            "JOURNEY BEGINS",
            "BATTLE ON",
            "GAMER'S ODYSSEY",
            "SYNTY LEGEND",
            "HERO'S ADVENTURE",
            "GAMING JOURNEY",
            "GAMER'S LEGACY",
            "BATTLE ADVENTURE",
            "JOIN THE GAME",
            "ONLINE HERO",
            "BATTLE QUEST",
            "SYNTY EPIC",
            "GAMER'S QUEST",
            "HERO'S WORLD",
            "GAMING VICTORY",
            "SYNTY KINGDOM",
            "BATTLE ROYAL",
            "ONLINE VICTORY",
            "GAMING ADVENTURE",
            "SYNTY HEROES",
            "EPIC VICTORY",
            "GAMER'S KINGDOM",
            "FIGHT TO WIN",
            "GAMING CHAMPION",
            "BATTLE FOR VICTORY",
            "SYNTY ADVENTURE",
            "MMORPG ADVENTURE",
            "JOIN THE FIGHT",
            "BATTLE TO WIN",
            "HERO'S VICTORY",
            "GAMING QUEST",
            "SYNTY EPIC JOURNEY",
            "BATTLE HERO",
            "MMO RPG HERO",
            "GAMER'S ADVENTURE",
            "EPIC QUEST",
            "SYNTY BATTLE",
            "HERO'S ODYSSEY",
            "JOIN THE ADVENTURE",
            "ONLINE BATTLE",
            "MMORPG BATTLE",
            "GAMING HEROES",
            "SYNTY RPG",
            "JOIN THE JOURNEY",
            "MMORPG JOURNEY",
            "HERO'S BATTLE",
            "ADVENTURE RPG",
            "GAMING ODYSSEY",
            "SYNTY'S ADVENTURE",
            "EPIC JOURNEY",
            "HERO'S JOURNEY",
            "GAMER'S BATTLE",
            "SYNTY LEGENDS",
            "BATTLE RPG",
            "MMORPG HEROES",
            "ADVENTURE HERO",
            "BATTLE HEROES",
            "GAMING LEGENDS",
            "SYNTY'S HEROES",
            "HERO'S QUEST",
            "EPIC BATTLE",
            "HERO'S EPIC",
            "BATTLE MMORPG",
            "GAMER'S HERO",
            "SYNTY'S EPIC",
            "JOIN THE EPIC",
            "HERO'S MMORPG",
            "SYNTY'S QUEST",
            "EPIC HERO",
            "GAMER'S MMORPG",
            "SYNTY'S KINGDOM",
            "MMORPG VICTORY",
            "EPIC MMORPG",
            "BATTLE TO VICTORY",
            "SYNTY'S BATTLE",
            "MMORPG EPIC VICTORY",
            "SYNTY'S VICTORY",
            "JOIN THE VICTORY",
            "SYNTY'S MMORPG",
            "JOIN THE MMORPG",
            "GAMER'S EPIC VICTORY",
            "SYNTY'S EPIC MMORPG",
            "HERO'S VICTORY MMORPG",
            "HERO'S EPIC BATTLE",
            "MMORPG VICTORY BATTLE",
            "BATTLE VICTORY QUEST"
        };
        private readonly Dictionary<string, string> codedMessages = new();
        private readonly Dictionary<char, char> substitutionCipher = new()
        {
            { 'A', '4' },
            { 'E', '3' },
            { 'I', '1' },
            { 'O', '0' },
            { 'S', '5' },
            { 'T', '7' },
        };
        
        private void Awake()
        {
            InitializeCodedMessages();
        }

        #endregion

        #region Events

        private void OnEnable()
        {
            _menuHint.SetActive(false);
            GeneralInputReader.EnterAction += DecodeValidation;
            GeneralInputReader.ExitAction += FinishError;
            
            _screen.color = _screenColor;
            _inputDecodeWord.text = "";
            _inputDecodeWord.caretPosition = 0;
            
            SetInitializeCodedMessages();
        }

        private void OnDisable()
        {
            GeneralInputReader.EnterAction -= DecodeValidation;
            GeneralInputReader.ExitAction -= FinishError;
        }

        #endregion
        
        #region Functions
        
        private void InitializeCodedMessages()
        {
            foreach (var message in _messages)
            {
                codedMessages[Encode(message)] = message;
            }
        }

        private void SetInitializeCodedMessages()
        {
            var randomMessage = GetRandomCodedMessage();
            _hashWord = randomMessage;
            _inputHashWord.text = randomMessage;
        }
        
        private string GetRandomCodedMessage()
        {
            var index = Random.Range(0, codedMessages.Count);
            var randomKey = new List<string>(codedMessages.Keys)[index];
            return randomKey;
        }

        private string Encode(string message)
        {
            var encoded = "";
            foreach (var c in message)
            {
                if (substitutionCipher.ContainsKey(c))
                {
                    encoded += substitutionCipher[c];
                }
                else
                {
                    encoded += c;
                }
            }
            return encoded;
        }
        private string Decode(string encodedMessage)
        {
            if (codedMessages.ContainsKey(encodedMessage))
            {
                return codedMessages[encodedMessage];
            }
            return "Message inconnu";
        }
        
        public void DecodeValidation()
        {
            MusicManager.instance.MmfClick.PlayFeedbacks();
            
            if (!string.Equals(Decode(_hashWord), _inputDecodeWord.text, StringComparison.CurrentCultureIgnoreCase))
            {
                FinishError();
                return;
            }

            FinishValid();
        }
        
        private void FinishError()
        {
            if (MiniGameManager.IsOnHint) return;
            
            _screen.color = Color.red;
            
            MiniGameManager.LooseFansAndMoney();
            MiniGameManager.BugError?.Invoke();
            MusicManager.instance.MmfError.PlayFeedbacks();
            Finish();
        }
        
        private void FinishValid()
        {
            MiniGameManager.AddFansAndMoney();
            
            _screen.color = Color.green;
            
            MiniGameManager.BugValid?.Invoke();
            MusicManager.instance.MmfValidation.PlayFeedbacks();
            Finish();
        }
        
        private void Finish()
        {
            StartCoroutine(ExitHashWord());
        }
        
        private IEnumerator ExitHashWord()
        {
            yield return new WaitForSeconds(1.4f);
            
            transform.gameObject.SetActive(false);
            MiniGameManager.ResetIsOnMiniGame();
            MiniGameManager.BugCorrectedAction?.Invoke(MiniGameManager.CurrentEmployeeWorker);
        }

        #endregion
    }
}
