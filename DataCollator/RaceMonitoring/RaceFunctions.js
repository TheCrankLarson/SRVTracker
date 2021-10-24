var minX = -1;
var minY = -1;
var maxX = -1;
var maxY = -1;
var courseWidth = -1;
var courseHeight = -1
var heightAdjust = 0;
var widthAdjust = 0;
var racerIndex = 1;
var raceEventIndex = 0;
var raceEventTick = 0;
var waitForRaceCount = 0;
var lastRaceUpdate = null;
var noUpdateCount = 0;

var updateTimer = null;
var raceInitTimer = null;

//  Various tests
var animationsStylesheet;
var racerColourIndex = 0;
var racerHueRotate = 0;
var racerColours = ["Red", "Yellow", "Blue", "Green", "Purple", "Orange", "Chartreuse", "BurlyWood", "DeepPink"];

var animationsStylesheetElement = document.createElement('style');
animationsStylesheetElement.type = 'text/css';
animationsStylesheetElement.title = "RacerAnimations";
document.head.appendChild(animationsStylesheetElement)
animationsStylesheet = animationsStylesheetElement.sheet


function DebugLog(debugData) {
    document.getElementById('debugLog').appendChild(document.createTextNode(debugData + "\r\n"));
}

function GetText(sourceUrl) {
    // Retrieve a JSON object from a given URL

    try {
        var Httpreq = new XMLHttpRequest();
        Httpreq.open("GET", sourceUrl, false);
        Httpreq.send(null);
        return Httpreq.responseText;
    }
    catch {
        return "";
    }
}

function GetJSON(sourceUrl) {
    // Retrieve a JSON object from a given URL

    try {
        return JSON.parse(GetText(sourceUrl));
    }
    catch {
        return null;
    }
}

function GetActiveRaces() {
    // Retrieve list of active races from the server
    return GetText(dataUrl + "getactiveraces");
}

function GetRaceText(raceId) {
    // Retrieve details for the specified race
    return GetText(dataUrl + "getrace/" + raceId);
}

function GetRace(raceId) {
    // Retrieve details for the specified race
    return GetJSON(dataUrl + "getrace/" + raceId);
}

function LongitudeToX(longitude, scaleWidth) {
    // Return the x value for the given longitude
    return (scaleWidth / 360.0) * (180 + longitude);
}

function LatitudeToY(latitude, scaleHeight) {
    // Return the y value for the given latitude
    return (scaleHeight / 180.0) * (90 - latitude);
}

function CalculateCourseBounds(waypoints) {
    // Calculate the bounds of a course given the list of waypoints

    minX = LongitudeToX(waypoints[0].Location.Longitude, 360);
    minY = LatitudeToY(waypoints[0].Location.Latitude, 360);
    maxX = minX;
    maxY = minY;
    var i = 1;
    while (i < waypoints.length) {
        var wpX = LongitudeToX(waypoints[i].Location.Longitude, 360);
        var wpY = LatitudeToY(waypoints[i].Location.Latitude, 360);

        if (wpX < minX) { minX = wpX; }
        if (wpX > maxX) { maxX = wpX; }
        if (wpY < minY) { minY = wpY; }
        if (wpY > maxY) { maxY = wpY; }
        i++;
    }

    courseWidth = maxX - minX;
    courseHeight = maxY - minY;

    // Scale to the longest edge (so that we don't warp the course)
    if (courseWidth > courseHeight) {
        // Course is wider than high
        var midY = minY + (courseHeight / 2);
        courseHeight = courseWidth;
        minY = midY - (courseHeight / 2);
        maxY = minY + courseHeight;
    }
    else if (courseHeight > courseWidth) {
        // Course is higher than wide
        var midX = minX + (courseWidth / 2);
        courseWidth = courseHeight;
        minX = midX - (courseWidth / 2);
        maxX = minX + courseWidth;
    }

    // Now we add 10% boundary
    heightAdjust = courseHeight * 0.1;
    widthAdjust = courseWidth * 0.1;
    minX = minX - widthAdjust;
    maxX = maxX + widthAdjust;
    minY = minY - heightAdjust;
    maxY = maxY + heightAdjust;
    courseWidth = maxX - minX;
    courseHeight = maxY - minY;
}

function LatitudeToCanvassY(latitude, canvassHeight) {
    // Return the Y position of the given latitude for the current canvas

    var absoluteY = LatitudeToY(latitude, 360);
    if ( absoluteY < minY) {
        // Out of range
        return 0;
    }
    if (absoluteY > maxY) {
        // Out of range
        return canvassHeight - heightAdjust;
    }
    return Math.floor((absoluteY - minY) * (canvassHeight / courseHeight));
}

function LongitudeToCanvassX(longitude, canvassWidth) {
    // Return the X position of the given longitude for the current canvas

    var absoluteX = LongitudeToX(longitude, 360);
    if (absoluteX < minX) {
        // Out of range
        return 0;
    }
    if (absoluteX > maxX) {
        // Out of range
        return canvassWidth - widthAdjust;
    }
    return Math.floor((absoluteX - minX) * (canvassWidth / courseWidth));
}

function CreateWaypointDiv(waypoint, canvass, waypointContainer) {
    // Create the waypoint info and position within the given canvass

    var wpDisplay = document.createElement("div");
    wpDisplay.classList.add("waypoint");
    wpDisplay.id = waypoint.Name;

    var wpX = LongitudeToCanvassX(waypoint.Location.Longitude, canvass.clientWidth);
    var wpY = LatitudeToCanvassY(waypoint.Location.Latitude, canvass.clientHeight);
    wpDisplay.style.left = wpX + "px";
    wpDisplay.style.top = wpY + "px";

    // Add waypoint marker
    var waypointImage = document.createElement("img");
    waypointImage.classList.add("waypointImage");
    waypointImage.src = "images/Waypoint.png";
    waypointImage.id = waypoint.Name + "img";
    wpDisplay.appendChild(waypointImage);

    // Add waypoint info
    var waypointInfo = document.createElement("P");
    waypointInfo.innerHTML = waypoint.Name;
    waypointInfo.classList.add("waypointInfo");
    wpDisplay.appendChild(waypointInfo);

    //DebugLog(waypoint.Name + ': x=' + wpX + ', y=' + wpY);

    waypointContainer.appendChild(wpDisplay);
}

function GenerateAnimation(racer, startX, startY, endX, endY, ruleIndex) {
    // Returns an @keyframes animation style

    var anim = "from { top: " + startY + "px; left: " + startX + "px; }  to { top: " + endY + "px; left: " + endX + "px; }"

    if (ruleIndex > -1) {
        // Already have a rule created, so delete it first
        animationsStylesheet.deleteRule(ruleIndex);
    }
    else {
        ruleIndex = animationsStylesheet.cssRules.length;
    }
    animationsStylesheet.insertRule("@keyframes " + racer + "{" + anim + "}", ruleIndex);
    return ruleIndex;
}

function UpdateRacerStatus(racerStatus, canvass, racerContainer) {
    // Create or update the racer info and position within the given canvass

    // Calculate position of the racer
    var racerLocation = activeRace.Statuses[racerStatus].Location;
    if (racerLocation == null) {
        return;
    }

    var racerInfoElement = document.getElementById(activeRace.Statuses[racerStatus].Commander);

    var racerX = LongitudeToCanvassX(activeRace.Statuses[racerStatus].Location.Longitude, canvass.clientWidth);
    var racerY = LatitudeToCanvassY(activeRace.Statuses[racerStatus].Location.Latitude, canvass.clientHeight);
    var racerHeading = activeRace.Statuses[racerStatus].Heading + 180;
    if (racerHeading > 360)
        racerHeading = racerHeading - 360;


    var racerSRV = null;
    var racerProfileElement = null;
    var racerProfileRaceInfo = null;

    var racerRaceInfo = "Position: " + activeRace.Statuses[racerStatus].RacePosition;
    if ((activeRace.Laps > 1) && (!activeRace.Statuses[racerStatus].Finished)) {
        racerRaceInfo = racerRaceInfo + "<br/>Lap: " + activeRace.Statuses[racerStatus].Lap;
    }

    if (racerInfoElement == null) {
        // Need to create the info div for this racer
        racerInfoElement = document.createElement("div");
        racerInfoElement.classList.add("racerInfo");
        racerInfoElement.id = activeRace.Statuses[racerStatus].Commander;
        //racerInfoElement.style.backgroundColor = racerColours[racerColourIndex];
        //racerColourIndex++;
        //if (racerColourIndex >= racerColours.length) {
        //    racerColourIndex = 0;
        //}
        racerInfoElement.style.left = racerX + "px";
        racerInfoElement.style.top = racerY - (racerInfoElement.clientHeight / 2) + "px";

        // Add the SRV image
        racerSRV = document.createElement("img");
        racerColourIndex++;
        if (racerColourIndex > 9) {
            racerColourIndex = 1;
        }
        racerSRV.src = "images/64x64 " + racerColourIndex + ".png";
        racerSRV.width = 32;
        racerSRV.height = 32;
        racerSRV.id = activeRace.Statuses[racerStatus].Commander + "img";
        racerSRV.style.filter = "hue-rotate(" + racerHueRotate + "turn)";

        racerHueRotate = racerHueRotate + 0.1;
        if (racerHueRotate > 1)
            racerHueRotate = 0.05;
        racerInfoElement.appendChild(racerSRV);

        // Add the racer profile div
        racerProfileElement = document.createElement("div");
        racerProfileElement.classList.add("racerProfile");
        racerProfileElement.id = activeRace.Statuses[racerStatus].Commander + "profile";
        racerProfileElement.appendChild(document.createTextNode(activeRace.Statuses[racerStatus].Commander));
        racerInfoElement.appendChild(racerProfileElement);

        racerProfileRaceInfo = document.createElement("P");
        racerProfileRaceInfo.innerHTML = racerRaceInfo;
        //racerProfileRaceInfo = document.createTextNode(racerRaceInfo);
        racerProfileRaceInfo.id = activeRace.Statuses[racerStatus].Commander + "raceinfo";
        racerProfileElement.appendChild(racerProfileRaceInfo);

        racerContainer.appendChild(racerInfoElement);
    }
    else {
        racerSRV = document.getElementById(activeRace.Statuses[racerStatus].Commander + "img");
        racerProfileElement = document.getElementById(activeRace.Statuses[racerStatus].Commander + "profile");
        racerProfileRaceInfo = document.getElementById(activeRace.Statuses[racerStatus].Commander + "raceinfo");
    }

    racerX = racerX - (racerInfoElement.clientWidth / 2);
    racerY = racerY - (racerInfoElement.clientHeight / 2);

    //var racerid = racerInfoElement.getAttribute('racerid');
    //var racerStyleName = activeRace.Statuses[racerStatus].Commander.split(" ").join("");

    if (activeRace.Statuses[racerStatus].Eliminated) {
        // Eliminated, so change colour to red
        racerProfileElement.style.color = "red";
    }
    else if (activeRace.Statuses[racerStatus].Finished) {
        racerProfileElement.style.color = "green";
    }
    else {
        // We don't update the position after a racer is eliminated
        racerInfoElement.style.left = racerX + "px";
        racerInfoElement.style.top = racerY + "px";
        racerSRV.style.transform = "rotate(" + racerHeading + "deg)";
        racerProfileRaceInfo.innerHTML = racerRaceInfo;
    }
}

function UpdateLeaderboard() {
    // Update the leaderboard
    var leaderboardElement = document.getElementById('leaderBoard');
    var leaderboardTableElement = document.getElementById('leaderBoardTable');

    while (leaderboardTableElement.rows.length <= activeRace.Contestants.length) {
        leaderboardTableElement.insertRow();
    }

    for (var racerStatus in activeRace.Statuses) {
        var updateRow = leaderboardTableElement.rows[activeRace.Statuses[racerStatus].RacePosition];
        while (updateRow.cells.length < 3) {
            updateRow.insertCell(0);
        }
        updateRow.cells[0].innerHTML = activeRace.Statuses[racerStatus].RacePosition;
        updateRow.cells[1].innerHTML = activeRace.Statuses[racerStatus].Commander;
        if (activeRace.Statuses[racerStatus].Eliminated) {
            updateRow.cells[2].innerHTML = activeRace.CustomStatusMessages.Eliminated;
        }
        else if (activeRace.Statuses[racerStatus].Finished) {
            updateRow.cells[2].innerHTML = activeRace.CustomStatusMessages.Completed;
        }
        else if (activeRace.Laps > 0) {
            updateRow.cells[2].innerHTML = "Lap " + activeRace.Statuses[racerStatus].Lap + " of " + activeRace.Laps;
        }
        
    };
}

function UpdateRaceAnnouncement() {
    // Display the next announcement (if there is one)

    var bannerElement = document.getElementById("raceAnnouncements");
    if (activeRace.NotableEvents.EventQueue.length > raceEventIndex) {
        bannerElement.innerText = activeRace.NotableEvents.EventQueue[raceEventIndex];
        raceEventIndex++;
        raceEventTick = 0;
    }
    else {
        raceEventTick++;
        if (raceEventTick > 3) {
            bannerElement.innerText = activeRace.Name;
            raceEventTick = 0;
        }
    }
}

function UpdateRace() {
    // Retrieve race information and update the display
    
    var raceSelectElement = document.getElementById('activeRaces');
    var raceUpdate = GetRaceText(raceSelectElement.options[raceSelectElement.selectedIndex].value);
    if (raceUpdate == null) {
        return;
    }

    if (lastRaceUpdate != null && raceUpdate == lastRaceUpdate) {
        // Nothing to update
        noUpdateCount++;
        if (noUpdateCount > 30) {
            // We haven't had an update for over 30 seconds.
            if (updateTimer != null) {
                clearInterval(updateTimer);
                updateTimer = null;
            }

            if (noUpdateCount > 80) {
                // We don't reset any timer as we haven't had an update for too long
                return;
            }
            if (noUpdateCount > 60) {
                // No update for over five minutes - we go back into race init mode
                if (waitForRaceCount < 20) {
                    raceInitTimer = setInterval(InitRaces, 15000);
                }
            }
            else {
                // Reduce the polling period
                updateTimer = setInterval(UpdateRace, 10000);
            }
        }
        return;
    }

    lastRaceUpdate = raceUpdate;
    raceUpdate = JSON.parse(raceUpdate);
    if (noUpdateCount > 0) {
        // We've received an update, so we ensure we are checking every second
        clearInterval(updateTimer);
        updateTimer = setInterval(UpdateRace, 1000);
        noUpdateCount = 0;
    }
    
    UpdateLeaderboard();

    var racersContainerElement = document.getElementById('courseParticipants');
    var canvassElement = document.getElementById('courseCanvass');

    //DebugLog("Updated race information acquired");
    activeRace = raceUpdate;
    var racersContainerElement = document.getElementById('courseParticipants');
    for (var racerStatus in activeRace.Statuses) {
        UpdateRacerStatus(racerStatus, canvassElement, racersContainerElement)
    };
    //DumpAnimations()
    UpdateRaceAnnouncement();
}


// UI functions

function ConnectRace() {
    // Get references to our UI elements
    var waypointContainerElement = document.getElementById('courseWaypoints');
    var racersContainerElement = document.getElementById('courseParticipants');
    var canvassElement = document.getElementById('courseCanvass');

    // Check we are not already polling a race
    if (updateTimer != null) {
        clearInterval(updateTimer);
        updateTimer = null;
    }

    waitForRaceCount = 0;

    // Clear any existing race data
    if (activeRace != null) {
        activeRace = null;
        // Clear all waypoints
        while (waypointContainerElement.lastChild) {
            waypointContainerElement.removeChild(waypointContainerElement.lastChild);
        }
        // Clear all racers
        while (racersContainerElement.lastChild) {
            racersContainerElement.removeChild(racersContainerElement.lastChild);
        }
        // Clear race info
        document.getElementById('raceInfoSystem').textContent = "";
        document.getElementById('raceInfoPlanet').textContent = "";
        document.getElementById('raceInfoDate').textContent = "";

        raceEventIndex = 0;
    }

    // Race selection has changed.  If valid, connect to the race
    var raceSelectElement = document.getElementById('activeRaces');
    var selectedRaceId = raceSelectElement.options[raceSelectElement.selectedIndex].value;
    activeRace = GetRace(selectedRaceId);
    if (activeRace == null) return;

    // Display the race information
    var raceSelectElement = document.getElementById('raceInfo');
    document.getElementById('raceInfoSystem').textContent = activeRace.Route.Waypoints[0].Location.SystemName;
    if (activeRace.Route.Waypoints[0].Location.SystemName == null || activeRace.Route.Waypoints[0].Location.SystemName == "") {
        document.getElementById('raceInfoSystemInfo').hidden = true;
    }
    else {
        document.getElementById('raceInfoSystemInfo').hidden = false;
    }
    document.getElementById('raceInfoPlanet').textContent = activeRace.Route.Waypoints[0].Location.PlanetName;
    document.getElementById('raceInfoDate').textContent = activeRace.Start.substring(0, 10);
    document.getElementById('raceInfoName').textContent = activeRace.Name;

    // Create and display the waypoints
    CalculateCourseBounds(activeRace.Route.Waypoints);
    for (let i = 0; i < activeRace.Route.Waypoints.length; i++) {
        CreateWaypointDiv(activeRace.Route.Waypoints[i], canvassElement, waypointContainerElement)
    }

    if (activeRace.NotableEvents.EventQueue.length>0)
        raceEventIndex = activeRace.NotableEvents.EventQueue.length;
    UpdateRaceAnnouncement();
    updateTimer = setInterval(UpdateRace, 1000);
}

function GetAbsolutePosition(element) {
    const rect = element.getBoundingClientRect();
    return {
        left: rect.left + window.scrollX,
        top: rect.top + window.scrollY,
        width: rect.width,
        height: rect.height
    };
}

function UpdateActiveRaces() {
    var activeRaces = GetActiveRaces().split("\r\n");
    var raceSelectElement = document.getElementById('activeRaces');

    if (activeRaces.length > 0) {
        // Remove any existing options
        while (raceSelectElement.options.length > 0) {
            raceSelectElement.remove(0);
        }
        // Add selection prompt
        var raceOption = document.createElement('option');
        raceOption.appendChild(document.createTextNode("Please select:"));
        raceOption.setAttribute('value', "");
        raceSelectElement.appendChild(raceOption);

        // Add active races
        for (let i = 0; i < activeRaces.length; i++) {
            // Races are returned as a list in format id,name
            if (activeRaces[i].length > 3) {
                var raceInfo = activeRaces[i].split(",");

                var raceOption = document.createElement('option');
                raceOption.appendChild(document.createTextNode(raceInfo[1]));
                raceOption.setAttribute('value', raceInfo[0]);
                raceSelectElement.appendChild(raceOption);
            }
        }
    }
}

function InitRaces() {

    if (raceInitTimer != null) {
        clearInterval(raceInitTimer);
        raceInitTimer = null;
    }    

    UpdateActiveRaces();
    waitForRaceCount++;

    var raceSelectElement = document.getElementById('activeRaces');
    if (raceSelectElement.children.length == 2) {
        // There is only one active race, so connect to that
        raceSelectElement.selectedIndex = 1;
        ConnectRace();
    }
    else if (raceSelectElement.children.length < 2) {
        if (waitForRaceCount < 20) {
            // No active races, so we set a timer to check every fifteen seconds (for up to five minutes)
            raceInitTimer = setInterval(InitRaces, 15000);
        }
    }
}

function InitialisePage() {
    // Retrieve active races and add them to our select box

    var canvassPos = GetAbsolutePosition(document.getElementById('courseContainer'));
    var canvassElement = document.getElementById('courseCanvass');
    canvassElement.style.left = Math.floor(canvassPos.left) + "px";
    canvassElement.style.top = Math.floor(canvassPos.top) + "px";
    canvassElement.style.width = Math.floor(canvassPos.width) + "px";
    canvassElement.style.height = Math.floor(canvassPos.height) + "px";

    InitRaces();
}