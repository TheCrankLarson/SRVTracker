# SRVTracker

Vehicle Tracker for Elite: Dangerous

The SRVTracker client monitors the local status.json file to track the location of whichever vehicle the player is flying.  It can be configured to upload that data to a server (implemented in the DataCollator project) that will accept data from multiple clients and provide a single feed with all their movements.  The RaceMonitor project (currently under development) will connect to the feed and provide live race services.  Races will need to be configured beforehand, with start/end and waypoints.
