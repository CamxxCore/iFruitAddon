using System;
using GTA;
using GTA.Math;
using GTA.Native;
using iFruitAddon;

namespace ExampleScript
{
    public class ExampleScript : Script
    {
        CustomiFruit ifruit;

        public ExampleScript()
        {
            ifruit = new CustomiFruit() {
                SelectButtonColor = System.Drawing.Color.Orange,
                ReturnButtonColor = System.Drawing.Color.LimeGreen,
                KeypadButtonColor = System.Drawing.Color.Purple
            };

            var contact = new iFruitContact("Spawn Adder", 10);
            contact.Selected += (sender, args) => Scripts.SpawnVehicle("ADDER", Game.Player.Character.Position);
            ifruit.Contacts.Add(contact);
            contact = new iFruitContact("Teleport to Waypoint", 11);
            contact.Selected += (sender, args) => Scripts.TeleportToWaypoint();
            ifruit.Contacts.Add(contact);
            this.Tick += OnTick;

        }

        void OnTick(object sender, EventArgs e)
        {
            ifruit.Update();
        }

    }

    public static class Scripts
    {
        public static void TeleportToWaypoint()
        {
            Blip wpBlip = new Blip(Function.Call<int>(Hash.GET_FIRST_BLIP_INFO_ID, 8));

            if (Function.Call<bool>(Hash.IS_WAYPOINT_ACTIVE))
            {
                GTA.Math.Vector3 wpVec = Function.Call<GTA.Math.Vector3>(Hash.GET_BLIP_COORDS, wpBlip);
                Game.Player.Character.Position = wpVec;
            }
            else
            {
                UI.ShowSubtitle("Waypoint not active.");
            }
        }

        public static void SpawnVehicle(string vehiclename, Vector3 pos)
        {
            Model model = new Model(vehiclename);
            model.Request(1000);
            var veh = Function.Call<Vehicle>((Hash)0xAF35D0D2583051B0, model.Hash, pos.X, pos.Y, pos.Z, Game.Player.Character.Heading, 0, 0);
            Function.Call(Hash.SET_PED_INTO_VEHICLE, Game.Player.Character.Handle, veh.Handle, -1);
        }
    }

}
