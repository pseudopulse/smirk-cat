using BepInEx;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Reflection;
using RoR2.UI;

namespace SmirkCat {
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    
    public class SmirkCat : BaseUnityPlugin {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "pseudopulse";
        public const string PluginName = "RiskOfSmirk";
        public const string PluginVersion = "1.0.0";

        public static AssetBundle bundle;
        public static BepInEx.Logging.ManualLogSource ModLogger;
        public static TMPro.TMP_SpriteAsset asset;

        public void Awake() {
            // assetbundle loading 
            bundle = AssetBundle.LoadFromFile(Assembly.GetExecutingAssembly().Location.Replace("SmirkCat.dll", "smirk_cat"));

            asset = bundle.LoadAsset<TMPro.TMP_SpriteAsset>("Assets/Resources/Sprites/smirk_cat.asset");

            // set logger
            ModLogger = Logger;

            On.RoR2.Chat.AddMessage_string += (orig, str) => {
                str = str.Replace(":smirk_cat:", """: <sprite=0>""");
                orig(str);
            };
        
            On.RoR2.Util.EscapeRichTextForTextMeshPro += (orig, str) => {
                return str;
            };

            On.RoR2.UI.ChatBox.Start += (orig, self) => {
                orig(self);
                HGTextMeshProUGUI gui = self.transform.Find("StandardRect").Find("Scroll View").Find("Viewport").Find("MessageArea").Find("Text Area").Find("Text").GetComponent<HGTextMeshProUGUI>();
                gui.spriteAsset = asset;
            };
        }
    }
}