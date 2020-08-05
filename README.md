# SRVTracker

Vehicle Tracker for Elite: Dangerous

The SRVTracker client monitors the local status.json file to track the location of whichever vehicle the player is flying.  It can be configured to upload that data to a server (implemented in the DataCollator project) that will accept data from multiple clients and provide a single feed with all their movements.  The RaceMonitor project (currently under development) will connect to the feed and provide live race services.  Races will need to be configured beforehand, with start/end and waypoints.

VR support is via OpenVR.  Before building, you'll need to add OpenVRApiModule to the solution as per this guide: https://steamcommunity.com/app/358720/discussions/0/142260895142733091/.
