# SRVTracker

Vehicle Tracker for Elite: Dangerous

The SRVTracker client monitors the local status.json file to track the location of whichever vehicle the player is flying.  It can be configured to upload that data to a server that keeps track of races.

The server software is included (the DataCollator project), and races are controlled by the Race Manager client (also included).  Further work will likely add a race management web interface.

VR support is via OpenVR.  Before building, you'll need to add OpenVRApiModule to the solution as per this guide: https://steamcommunity.com/app/358720/discussions/0/142260895142733091/.
