using MelonLoader;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PhasmoMelonMod
{
    public class Spawner
    {
        public static List<Vector3> GeneratedVectors = new List<Vector3>();
        public static List<GhostAI> GeneratedGhosts = new List<GhostAI>();

        public string Item;
        private Vector3 Position;
        private GameObject ItemObject;
        private string Type;

        public enum ItemType
        {
            [Description("Crucifix")]
            CRUCIFIX = 0,
            [Description("GlowStick")]
            GLOWSTICK = 1,
            [Description("OuijaBoard")]
            OUIJABOARD = 2,
            [Description("EMF Reader(Clone)")]
            EMFREADER = 3,
            [Description("Thermometer")]
            THERMOMETER = 4,
            [Description("Fluorescence")]
            UVTORCH = 5,
            [Description("Car")]
            CAR = 6,
            [Description("Door")]
            DOOR = 7,
            [Description("DNAEvidence")]
            DNAEVIDENCE = 8,
            [Description("Candle")]
            CANDLE = 9,
            [Description("EVP Recorder(Clone)")]
            SPIRITBOX = 10,
            [Description("Bunny")]
            BUNNY = 11
        }

        public enum GhostModel
        {
            [Description("Ghost_Greybeard")]
            GREYBEARD = 0,
            [Description("Ghost_BruteOneHand")]
            BRUTEONEHAND = 1,
            [Description("Ghost_Butcher")]
            BUTCHER = 2,
            [Description("Ghost_Female")]
            FEMALE = 3,
            [Description("Ghost_Fisherman")]
            FISHERMAN = 4,
            [Description("Ghost_Girl")]
            GIRL = 5,
            [Description("Ghost_NerdOneHand")]
            NERDONEHAND = 6,
            [Description("Ghost_OldCrone")]
            OLDCRONE = 7,
            [Description("Ghost_RingGirl")]
            RINGGIRL = 8,
            [Description("Ghost_Skeleton")]
            SKELETON = 9,
            [Description("Ghost_Male")]
            MALE = 10,
            [Description("Ghost_Nerd")]
            NERD = 11,
            [Description("Ghost_Female_1")]
            FEMALE_1 = 12,
            [Description("Ghost_Girl_1")]
            GIRL_1 = 13
        }


        public Spawner(ItemType itemType, Vector3 position)
        {
            this.Item = GetEnumDescription(itemType);
            position.y = position.y + 0.5f;
            this.Position = position;
            this.Type = "Item";
            GeneratedVectors.Add(position);
        }

        public Spawner(GhostModel ghostModel, Vector3 position)
        {
            this.Item = GetEnumDescription(ghostModel);
            this.Position = position;
            this.Type = "Ghost";
        }

        public static List<Candle> Spawned_Candles = new List<Candle>();

        public void Spawn()
        {
            if(Type == "Ghost")
            {
                try
                {
                    this.ItemObject = PhotonNetwork.InstantiateRoomObject(Item, Position, Quaternion.identity, 0, null);

                    GhostController ghostC = Main.ghostController;
                    ghostC.field_Private_PhotonView_0.controller = ghostC.field_Private_PhotonView_0.Controller;

                }
                catch (System.Exception ex)
                {
                    Debug.Out("Error Spawning Ghost Model");
                }
            }
            else
            {
                // Attempt at trying to spawn a Bunny/Bear
                /*if(Item.Contains("Bunny") == true)
                {
                    string[] names = { "Bunny (1)", "Bunny", "Bear", "bear", "bear l0", "bear l1", "bear l2", "bear (1)" };
                    foreach(string name in names)
                    {
                        PhotonNetwork.Instantiate(name, Main.myPlayer.transform.position, Quaternion.identity, 0, null);
                        PhotonNetwork.InstantiateRoomObject(name, Main.myPlayer.transform.position, Quaternion.identity, 0, null);
                        PhotonNetwork.Instantiate(name, Main.myPlayer.transform.position, Quaternion.identity, 1, null);
                        PhotonNetwork.InstantiateRoomObject(name, Main.myPlayer.transform.position, Quaternion.identity, 1, null);
                        PhotonNetwork.Instantiate(name, Main.myPlayer.transform.position, Quaternion.identity, 2, null);
                        PhotonNetwork.InstantiateRoomObject(name, Main.myPlayer.transform.position, Quaternion.identity, 2, null);
                        PhotonNetwork.Instantiate(name, Main.myPlayer.transform.position, Quaternion.identity, 0x1, null);
                        PhotonNetwork.InstantiateRoomObject(name, Main.myPlayer.transform.position, Quaternion.identity, 0x1, null);
                    }
                    
                }*/

                if (this.ItemObject == null)
                {
                    this.ItemObject = PhotonNetwork.InstantiateRoomObject(Item, Position, Quaternion.identity, 0, null);
                }

                foreach (Candle candle in Object.FindObjectsOfType<Candle>().ToList<Candle>())
                {
                    if (Spawned_Candles.Contains(candle) || GeneratedVectors.Contains(candle.transform.position))
                    {
                        if (!Spawned_Candles.Contains(candle))
                        {
                            Spawned_Candles.Add(candle);
                        }
                        candle.Use();
                        candle.NetworkedUse(true);
                    }
                }
            }
            //this.ItemObject = PhotonNetwork.InstantiateRoomObject(Item, Position, Quaternion.identity, 0, null);

        }

        public static void UpdateCandles()
        {
            foreach (Candle candle in Object.FindObjectsOfType<Candle>().ToList<Candle>())
            {
                if (Spawned_Candles.Contains(candle) || GeneratedVectors.Contains(candle.transform.position))
                {
                    if (!Spawned_Candles.Contains(candle))
                    {
                        Spawned_Candles.Add(candle);
                    }
                    candle.Use();
                    candle.NetworkedUse(true);
                }
            }
        }

        public void Destroy()
        {
            this.ItemObject.SetActive(false);
            UnityEngine.Object.Destroy(this.ItemObject);

            foreach (DNAEvidence bone in Object.FindObjectsOfType<DNAEvidence>().ToList<DNAEvidence>())
            {
                bone.enabled = false;
                UnityEngine.Object.Destroy(bone);
            }
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }
            return value.ToString();
        }
        
    }
}
