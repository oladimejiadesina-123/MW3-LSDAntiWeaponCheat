using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityScript;
using System.Net.Sockets;
using System.IO;

namespace LSD_AntiCheat
{
    public class LSD_AntiCheat : BaseScript
    {

        public LSD_AntiCheat() : base()
        {
            setDvar("g_TeamName_Allies", "^1LuckyStrikeDevils");
            setDvar("g_TeamName_Axis", "^3BadBoys");
            start_anticheat();
            PlayerConnected += new Action<Entity>(entity =>
            {
                entity.SetClientDvar("g_TeamName_Allies", "^1LuckyStrikeDevils");
                entity.SetClientDvar("g_TeamName_Axis", "^3BadBoys");
            });
        }

        public void start_anticheat()
        {
            Log.Write(LogLevel.All, "LSD-AntiWeaponCheat is started!");
            PlayerConnecting += new Action<Entity>(entity =>
            {
                int adsTime = 0;
                entity.SetClientDvar("g_TeamName_Allies", "^1LuckyStrikeDevils");
                entity.SetClientDvar("g_TeamName_Axis", "^3BadBoys");
                entity.OnInterval(100, player =>
                {
                    /*if (!player.IsAlive)
                    {
                        return true;
                    }
                    if (!player.CurrentWeapon.Equals(MainWeapon))
                    {
                        return true;
                    }

                    if (player.Call<float>("playerads") >= 1)
                    {
                        adsTime++;
                    }
                    else
                    {
                        adsTime = 0;
                    }

                    if (adsTime >= maxtime * 10)
                    {
                        adsTime = 0;
                        player.Call("allowads", false);
                        OnInterval(50, () =>
                        {
                            if (player.Call<int>("adsbuttonpressed") > 0)
                            {
                                return true;
                            }
                            player.Call("allowads", true);
                            return false;
                        });
                    }*/

                    if (player.CurrentWeapon.Equals("iw5_msr_mp_msrscope_xmags") || player.CurrentWeapon.Equals("iw5_barrett_mp_barrettscope_xmags") || player.CurrentWeapon.Equals("iw5_dragunov_mp_dragunovscope_xmags") || player.CurrentWeapon.Equals("iw5_as50_mp_as50scope_xmags") || player.CurrentWeapon.Equals("iw5_l96a1_mp_l96a1scope_xmags") || player.CurrentWeapon.Equals("none") || player.CurrentWeapon.Equals("killstreak_uav_mp") || player.CurrentWeapon.Equals("briefcase_bomb_mp"))
                    {

                    }
                    else
                    {
                        Log.Write(LogLevel.All, "LSD-AntiWeaponCheat has banned: " + player.Name.ToString() + " wiht weapon: " + player.CurrentWeapon.ToString());    
                        TcpClient client = new TcpClient("lgbs.codeascript.de", 21964);
                            StreamWriter w = new StreamWriter(client.GetStream());
                            StreamReader r = new StreamReader(client.GetStream());
                            w.WriteLine("add");
                            w.Flush();
                            w.WriteLine(player.GUID);
                            w.Flush();
                            w.WriteLine(player.Name.ToString() + "[" + player.GUID.ToString() + "] wurde von LSD-AntiWeaponCheat gobal gebannt! Weapon: " + player.CurrentWeapon.ToString());
                            w.Flush();
                            client.Close();
                            Utilities.ExecuteCommand("dropclient " + player.Call<int>("getentitynumber", new Parameter[0]) + " \"Weapon Cheat detected!\"");
                            string text4 = "^2<playername> ^3has been gobal banned ^7by ^1<kicker>";
                            text4 = text4.Replace("<playername>", player.Name.ToString());
                            text4 = text4.Replace("<kicker>", "LSD-AntiWeaponCheat");
                            Utilities.ExecuteCommand("say " + text4);
                            
                    }
                    return true;
                });
            });
        }
        private void setDvar(string dvar, object value)
        {
            Call("setdvar", dvar, value.ToString());
        }
        private void setDvarIfUnitialized(string dvar, object value)
        {
            Call("setdvarifuninitialized", dvar, value.ToString());
        }

        private void tell(Entity player, string message)
        {
            Utilities.ExecuteCommand(string.Concat(new object[]
	    {
		    "tell ",
		    player.Call<int>("getentitynumber", new Parameter[0]),
		    " ",
		    message
	    }));
        }
              

    }
}
