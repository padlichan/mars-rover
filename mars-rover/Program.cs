namespace mars_rover;
using mars_rover.RoverHandlers;
using mars_rover.InputHandlers;
using mars_rover.Grids;


internal class Program
{
    static void Main(string[] args)
    {
        MissionControl missionControl = MissionControl.GetInstance();
        missionControl.LaunchMission();
    }
}
