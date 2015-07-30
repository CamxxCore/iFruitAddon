using System;
using GTA;
using GTA.Math;
using GTA.Native;
using iFruitContacts;

namespace ExampleScript
{
    public class ExampleScript : Script
    {
        iFruitContactCollection contactList;

        public ExampleScript()
        {
            this.Tick += OnTick;

            contactList = new iFruitContactCollection();
            var contact = new iFruitContact("Teleport to Waypoint", 8);
            contact.Selected += (parent, item) => Scripts.TeleportToWaypoint();
            contactList.Add(contact);
            contact = new iFruitContact("Spawn Adder", 9);
            contact.Selected += (parent, item) => Scripts.SpawnVehicle("ADDER", Game.Player.Character.Position + Game.Player.Character.ForwardVector * 4);
            contactList.Add(contact);
        }

        void OnTick(object sender, EventArgs e)
        {
            contactList.Update();
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
