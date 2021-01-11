using MelonLoader;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace PhasmoMelonMod
{
    class ESP
    {
        // Used for Printing Position of Player on a KeyPress
        public static List<Vector3> saved_positions = new List<Vector3>();

        public static void Enable()
        {
            foreach (Player player in Main.players)
            {
                Vector3 w2s = Main.cameraMain.WorldToScreenPoint(player.transform.position);
                Vector3 playerPosition = Main.cameraMain.WorldToScreenPoint(player.field_Public_Transform_0.transform.position);

                if (w2s.z > 0f)
                {
                    GUI.Label(new Rect(new Vector2(w2s.x, Screen.height - (w2s.y + 1f)), new Vector2(100f, 100f)), "<color=#95E106><b>" + player.field_Public_PhotonView_0.controller.NickName + "</b></color>");
                }
            }
            if (Main.gameController != null && Main.ghostAI != null)
            {
                foreach (Vector3 pos in saved_positions)
                {

                    Vector3 w2s = Main.cameraMain.WorldToScreenPoint(pos);
                    Vector3 playerPosition = Main.cameraMain.WorldToScreenPoint(pos);

                    if (w2s.z > 0f)
                    {
                        GUI.Label(new Rect(new Vector2(w2s.x, Screen.height - (w2s.y + 1f)), new Vector2(100f, 100f)), "<color=#D900D9><b>" + string.Format("Position {0}", saved_positions.LastIndexOf(pos)) + "</b></color>");
                    }
                }

                foreach (GhostAI ghostAI in Main.ghostAIs)
                {
                    Vector3 w2s = Main.cameraMain.WorldToScreenPoint(ghostAI.transform.position);
                    Vector3 ghostPosition = Main.cameraMain.WorldToScreenPoint(ghostAI.field_Public_Transform_0.transform.position);

                    float ghostNeckMid = Screen.height - ghostPosition.y;
                    float ghostBottomMid = Screen.height - w2s.y;
                    float ghostTopMid = ghostNeckMid - (ghostBottomMid - ghostNeckMid) / 5;
                    float boxHeight = (ghostBottomMid - ghostTopMid);
                    float boxWidth = boxHeight / 1.8f;

                    if (w2s.z < 0)
                        continue;

                    Drawing.DrawBoxOutline(new Vector2(w2s.x - (boxWidth / 2f), ghostNeckMid), boxWidth, boxHeight, Color.cyan);
                }
                if (Main.dnaEvidences != null)
                {
                    foreach (DNAEvidence dnaEvidence in Main.dnaEvidences)
                    {
                        Vector3 vector3 = Main.cameraMain.WorldToScreenPoint(dnaEvidence.transform.position);
                        if (vector3.z > 0f)
                        {
                            GUI.Label(new Rect(new Vector2(vector3.x, Screen.height - (vector3.y + 1f)), new Vector2(100f, 100f)), "<color=#FFFFFF><b>Bone</b></color>");
                        }
                    }
                }
                if (Main.ouijaBoards != null)
                {
                    foreach (OuijaBoard ouijaBoard in Main.ouijaBoards)
                    {
                        Vector3 vector2 = Main.cameraMain.WorldToScreenPoint(ouijaBoard.transform.position);
                        if (vector2.z > 0f)
                        {
                            GUI.Label(new Rect(new Vector2(vector2.x, Screen.height - (vector2.y + 1f)), new Vector2(100f, 100f)), "<color=#D11500><b>Ouija Board</b></color>");
                        }
                    }
                }
                if (Main.fuseBox != null)
                {
                    Vector3 vector3 = Main.cameraMain.WorldToScreenPoint(Main.fuseBox.transform.position);
                    if (vector3.z > 0f)
                    {
                        GUI.Label(new Rect(new Vector2(vector3.x, Screen.height - (vector3.y + 1f)), new Vector2(100f, 100f)), "<color=#EBC634><b>FuseBox</b></color>");
                    }
                }
            }
        }
    }
}
