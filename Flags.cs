using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRVTracker
{
    [Flags]
    public enum StatusFlags
    {
        Docked_on_a_landing_pad = 1,
        Landed_on_planet_surface = 2,
        Landing_Gear_Down = 4,
        Shields_Up = 8,
        Supercruise = 16,
        FlightAssist_Off = 32,
        Hardpoints_Deployed = 64,
        In_Wing = 128,
        LightsOn = 256,
        Cargo_Scoop_Deployed = 512,
        Silent_Running = 1024,
        Scooping_Fuel = 2048,
        Srv_Handbrake = 4096,
        Srv_Turret = 8192,
        Srv_UnderShip = 16384,
        Srv_DriveAssist = 32768,
        Fsd_MassLocked = 65536,
        Fsd_Charging = 131072,
        Fsd_Cooldown = 262144,
        Low_Fuel = 524288,
        Over_Heating = 1048576,
        Has_Lat_Long = 2097152,
        IsInDanger = 4194304,
        Being_Interdicted = 8388608,
        In_MainShip = 16777216,
        In_Fighter = 33554432,
        In_SRV = 67108864
    }
}
