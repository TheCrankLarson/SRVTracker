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

function GetCommanderProfile(commander) {
    // Retrieve the profile information for the given commander

    if (commander != "Crank Larson") {
        return {
            "Description": "Racer bio.",
        };
    }

    return {
        "Description": "He loves to flyve.",
        "TwitchChannel": "cmdr_crank_larson"
    };
    //return GetJSON(dataUrl + "getcommanderprofile/" + commander);
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

function CreateWaypointCircleMarker(targetElement) {
    var circle = targetElement.getContext("2d");
    circle.beginPath();
    circle.arc(180, 100, 90, 0, 2 * Math.PI);
    circle.stroke();
}

function CreateWaypointDiv(waypoint, canvass, waypointContainer) {
    // Create the waypoint info and position within the given canvass

    var wpDisplay = document.createElement("div");
    wpDisplay.classList.add("waypoint");
    wpDisplay.id = waypoint.Name; // This will cause problems if two waypoints have the same name

    var wpX = LongitudeToCanvassX(waypoint.Location.Longitude, canvass.clientWidth);
    var wpY = LatitudeToCanvassY(waypoint.Location.Latitude, canvass.clientHeight);
    wpDisplay.style.left = wpX + "px";
    wpDisplay.style.top = wpY + "px";
    waypointContainer.appendChild(wpDisplay);

    // Determine the waypoint type and draw it (to scale if possible)
    var waypointRender = document.createElement("canvas");
    waypointRender.id = waypoint.Name + "render";
    waypointRender.style.width = "50px";
    waypointRender.style.height = "50px";
    waypointRender.style.left = wpX + "px";
    waypointRender.style.top = wpY + "px";
    waypointRender.style.display = "block";
    wpDisplay.appendChild(waypointRender);
    if (waypoint.ExtendedWaypointInformation.WaypointType) {
        // This is an extended waypoint
        if (waypoint.ExtendedWaypointInformation.WaypointType == "Gate") {
            // Gate type, so we can create a square div and plot the gate within that
            var corner1 = waypoint.AdditionalLocations[0];
            var corner2 = waypoint.AdditionalLocations[1];
            var x1 = LongitudeToCanvassX(corner1.Longitude, canvass.clientWidth);
            var x2 = LongitudeToCanvassX(corner2.Longitude, canvass.clientWidth);
            var y1 = LatitudeToCanvassY(corner1.Latitude, canvass.clientHeight);
            var y2 = LatitudeToCanvassY(corner2.Latitude, canvass.clientHeight);
            var fromX = 0;
            var fromY = 0;
            var toX = x2 - x1;
            var toY = y2 - y1;

            if (x1 > x2) {
                fromX = x1;
                x1 = x2;
                x2 = fromX;
                fromX = x2 - x1;
                toX = 0;
            }
            if (y1 > y2) {
                fromY = y1;
                y1 = y2;
                y2 = fromY;
                fromY = y2 - y1;
                toY = 0;
            }

            var wpW = (x2 - x1);
            var wpH = (y2 - y1);
            if (wpW < 5) {
                wpW = 5;
            }
            if (wpH < 5) {
                wpH = 5;
            }
            waypointRender.style.width = wpW + "px";
            waypointRender.style.height = wpH + "px";
            waypointRender.style.left = x1 + "px";
            waypointRender.style.top = y1 + "px";

            if (wpW < 10 || wpH < 10) {
                // Waypoint too narrow so we just fill in the background
                waypointRender.style.backgroundColor = "yellow";
            }
            else {
                var context = document.getElementById(waypoint.Name + "render").getContext("2d");
                DebugLog(waypoint.Name + "render: fromX=" + fromX + " fromY=" + fromY + " toX=" + toX + " toY=" + toY);

                //context.beginPath();
                context.moveTo(fromX, fromY);
                context.lineTo(toX, toY);
                context.strokeStyle = "yellow";
                context.lineWidth = 20;
                context.lineCap = "square";
                context.stroke();
            }
        }
    }
    else {
        // This is a basic waypoint
        // Calculate the radius of the waypoint in pixels

        // Add waypoint marker
        var waypointImage = document.createElement("img");
        waypointImage.classList.add("waypointImage");
        waypointImage.src = "images/Waypoint.png";
        waypointImage.id = waypoint.Name + "img";
        wpDisplay.appendChild(waypointImage);
    }

    // Add waypoint info
    var waypointInfo = document.createElement("P");
    waypointInfo.innerHTML = waypoint.Name;
    waypointInfo.classList.add("waypointInfo");
    wpDisplay.appendChild(waypointInfo);

    //DebugLog(waypoint.Name + ': x=' + wpX + ', y=' + wpY);   
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

function RotationAnimation(racer, startBearing, endBearing, ruleIndex) {
    var rotateSteps = " 0% { transform: rotate(" + startBearing + "deg); }";

    var midBearing = ((360 - startBearing) + endBearing) / 2;
    midBearing = midBearing + startBearing;
    if (midBearing > 360) {
        midBearing = midBearing - 360;
    }

    rotateSteps = rotateSteps + " 50% { transform: rotate(" + midBearing + "deg); }";
    rotateSteps = rotateSteps + " 100% { transform: rotate(" + endBearing + "deg); }";

    if (ruleIndex > -1) {
        // Already have a rule created, so delete it first
        animationsStylesheet.deleteRule(ruleIndex);
    }
    else {
        ruleIndex = animationsStylesheet.cssRules.length;
    }
    animationsStylesheet.insertRule("@keyframes " + racer + "{" + rotateSteps + "}", ruleIndex);
    return ruleIndex;
}

function AddCommanderProfile(commander) {
    // Retrieve and add the commander profile

//  <div id="commanderProfiles">
//    <div id="Crank Larsonprofile" class="commanderProfile" style="display:block;">
//        <div class="commanderProfileName" onclick="ToggleProfile('Crank Larson');">Crank Larson</div>
//        <div class="commanderProfileBio" id="Crank LarsonProfileBio">He just loves to flyve.</div>
//        <div class="commanderProfileStream" id="Crank LarsonProfileStream" onclick="CreateTwitchEmbed('cmdr_crank_larson','Crank LarsonProfileStream');">Connect to Twitch</div>
//    </div>

    if (document.getElementById(commander + "profile") != null) {
        return; // Profile already created
    }

    var profileData = GetCommanderProfile(commander);
    if (profileData == null) {
        return;
    }

    var commanderProfilesDiv = document.getElementById("commanderProfiles");

    // Create main profile
    var commanderProfileDiv = document.createElement("div");
    commanderProfileDiv.classList.add("commanderProfile");
    commanderProfileDiv.id = commander + "profile";
    commanderProfileDiv.style.display = "block";
    commanderProfilesDiv.appendChild(commanderProfileDiv);

    // Create name div (clicking on this expands the profile)
    var commanderProfileNameDiv = document.createElement("div");
    commanderProfileNameDiv.classList.add("commanderProfileName");
    commanderProfileNameDiv.id = commander + "ProfileName";
    commanderProfileNameDiv.innerText = commander;
    commanderProfileNameDiv.setAttribute("onclick", "ToggleProfile('" + commander + "');");
    commanderProfileDiv.appendChild(commanderProfileNameDiv);

    // Create Bio div
    var commanderProfileBioDiv = document.createElement("div");
    commanderProfileBioDiv.classList.add("commanderProfileBio");
    commanderProfileBioDiv.id = commander + "ProfileBio";
    commanderProfileBioDiv.innerText = profileData.Description;
    commanderProfileBioDiv.style.display = "none";
    commanderProfileDiv.appendChild(commanderProfileBioDiv);

    // Create Twitch div
    if (profileData.TwitchChannel != null) {
        var commanderProfileStreamDiv = document.createElement("div");
        commanderProfileStreamDiv.classList.add("commanderProfileStream");
        commanderProfileStreamDiv.id = commander + "ProfileStream";
        commanderProfileStreamDiv.innerText = "Connect to stream";
        commanderProfileStreamDiv.setAttribute("onclick", "CreateTwitchEmbed('" + profileData.TwitchChannel + "', '" + commander + "');");
        commanderProfileStreamDiv.style.display = "none";
        commanderProfileDiv.appendChild(commanderProfileStreamDiv);
    }
}

function ShowCommanderProfiles() {
    if (activeRace == null) {
        return;
    }

    for (var racerStatus in activeRace.Statuses) {
        AddCommanderProfile(racerStatus);
    }
    AddCommanderProfile("Crank Larson");
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
    var racerHeading = activeRace.Statuses[racerStatus].Heading;

    var racerSRV = null;
    var racerProfileElement = null;
    var racerProfileRaceInfo = null;

    var racerRaceInfo;
    if (activeRace.Statuses[racerStatus].Eliminated) {
        racerRaceInfo = activeRace.CustomStatusMessages.Eliminated;
    }
    else {
        racerRaceInfo = "Position: " + activeRace.Statuses[racerStatus].RacePosition;
        if ((activeRace.Laps > 1) && (!activeRace.Statuses[racerStatus].Finished)) {
            racerRaceInfo = racerRaceInfo + "<br/>Lap: " + activeRace.Statuses[racerStatus].Lap;
        }
    }

    if (racerInfoElement == null) {
        // Need to create the info div for this racer
        racerInfoElement = document.createElement("div");
        racerInfoElement.classList.add("racerInfo");
        racerInfoElement.id = activeRace.Statuses[racerStatus].Commander;
        racerInfoElement.style.left = racerX + "px";
        racerInfoElement.style.top = racerY - (racerInfoElement.clientHeight / 2) + "px";

        // Add the SRV image
        racerSRV = document.createElement("img");
        racerColourIndex++;
        if (racerColourIndex > 9) {
            racerColourIndex = 1;
        }
        //racerSRV.src = "images/SRV64N.png";
        racerSRV.src = "images/64x64 " + racerColourIndex + ".png";
        racerSRV.width = 32;
        racerSRV.height = 32;
        racerSRV.id = activeRace.Statuses[racerStatus].Commander + "img";
        //racerSRV.style.filter = "hue-rotate(" + racerHueRotate + "turn)";

        //racerHueRotate = racerHueRotate + 0.1;
        //if (racerHueRotate > 1)
        //    racerHueRotate = 0.05;
        racerInfoElement.appendChild(racerSRV);

        // Add the racer profile div
        racerProfileElement = document.createElement("div");
        racerProfileElement.classList.add("racerProfile");
        racerProfileElement.id = activeRace.Statuses[racerStatus].Commander + "profile";
        racerProfileElement.appendChild(document.createTextNode(activeRace.Statuses[racerStatus].Commander));
        racerInfoElement.appendChild(racerProfileElement);

        racerProfileRaceInfo = document.createElement("P");
        racerProfileRaceInfo.classList.add("racerProfileRaceInfo");
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

    racerProfileRaceInfo.innerHTML = racerRaceInfo;

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
        // Update the racer's position
        racerInfoElement.style.left = racerX + "px";
        racerInfoElement.style.top = racerY + "px";

        var previousHeading = racerSRV.getAttribute("previousHeading");
        var offsetAngle = racerSRV.getAttribute("offsetAngle");
        if (offsetAngle == null) {
            offsetAngle = -180; // The starting offset is due to the orientation of the SRV image
        }
        else {
            offsetAngle = parseInt(offsetAngle);
        }
        var srvTransition = "transform 2s linear";
        if (previousHeading != null) {
            if ((previousHeading > 225) && (racerHeading < 135)) {
                offsetAngle += 360;
            }
            else if ((previousHeading < 135) && (racerHeading > 225)) {
                offsetAngle -= 360;
            }
        }
        else {
            srvTransition = null;
        }
        //if (activeRace.Statuses[racerStatus].Commander == "Osashes") {
        //    DebugLog("Previous heading: " + previousHeading + "   Current heading: " + racerHeading + "   Offset: " + offsetAngle);
        //}
        var rotateAngle = racerHeading + offsetAngle;
        racerSRV.style.transition = srvTransition;
        racerSRV.style.transform = "rotate(" + rotateAngle + "deg)";
        racerSRV.setAttribute("previousHeading", racerHeading);
        racerSRV.setAttribute("offsetAngle", offsetAngle);       
    }
}

function UpdateLeaderboard() {
    // Update the leaderboard
    var leaderboardElement = document.getElementById('leaderBoard');
    var leaderboardTableElement = document.getElementById('leaderBoardTable');

    while (leaderboardTableElement.rows.length <= activeRace.Contestants.length) {
        leaderboardTableElement.insertRow();
    }

    var numCols = 3;
    if (document.getElementById('LeaderboardShowHullStatus').checked) { numCols++; }
    if (document.getElementById('LeaderboardShowCurrentSpeed').checked) { numCols++; }

    for (var racerStatus in activeRace.Statuses) {
        var updateRow = leaderboardTableElement.rows[activeRace.Statuses[racerStatus].RacePosition];
        while (updateRow.cells.length < numCols) {
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
        else {
            // Racer is currently active (not eliminated or finished)
            if (activeRace.Laps > 0) {
                updateRow.cells[2].innerHTML = "Lap " + activeRace.Statuses[racerStatus].Lap + " of " + activeRace.Laps;
            }
            else {
                updateRow.cells[2].innerHTML = "";
            }
            var updateCol = 3;
            if (document.getElementById('LeaderboardShowHullStatus').checked) {
                updateRow.cells[updateCol++].innerHTML = Math.floor(activeRace.Statuses[racerStatus].Hull * 100) + "%";
            }
            if (document.getElementById('LeaderboardShowCurrentSpeed').checked) {
                updateRow.cells[updateCol++].innerHTML = Math.floor(activeRace.Statuses[racerStatus].SpeedInMS) + " m/s";
            }
        }
    };
}

function UpdateRaceAnnouncement() {
    // Display the next announcement (if there is one)

    if (activeRace.NotableEvents.EventQueue.length < 1) {
        return;
    }
    var bannerElement = document.getElementById("raceAnnouncements");
    raceEventTick++;

    if (activeRace.NotableEvents.EventQueue.length > raceEventIndex && raceEventTick > 1) {
        bannerElement.innerText = activeRace.NotableEvents.EventQueue[raceEventIndex];
        raceEventIndex++;
        raceEventTick = 0;
    }
    else {
        if (raceEventTick > 4) {
            bannerElement.innerText = "";
            raceEventTick = 1;
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

    ShowCommanderProfiles();

    // Create and display the waypoints
    CalculateCourseBounds(activeRace.Route.Waypoints);
    InitWaypoints();

    if (activeRace.NotableEvents.EventQueue.length>0)
        raceEventIndex = activeRace.NotableEvents.EventQueue.length;
    UpdateRaceAnnouncement();
    updateTimer = setInterval(UpdateRace, 1000);
}

function GetAbsolutePosition(element) {
    const rect = element.getBoundingClientRect();
    return {
        left: rect.left,
        top: rect.top,
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

        if (raceSelectElement.options.length == 1) {
            raceSelectElement.remove(0);
            raceOption = document.createElement('option');
            raceOption.appendChild(document.createTextNode("No active races"));
            raceOption.setAttribute('value', "");
            raceSelectElement.appendChild(raceOption);
        }
    }
}

function InitWaypoints() {
    if (activeRace != null) {
        // Clear all waypoints
        var waypointContainerElement = document.getElementById('courseWaypoints');
        var canvassElement = document.getElementById('courseCanvass');
        while (waypointContainerElement.lastChild) {
            waypointContainerElement.removeChild(waypointContainerElement.lastChild);
        }
        // Reposition the waypoints
        for (let i = 0; i < activeRace.Route.Waypoints.length; i++) {
            CreateWaypointDiv(activeRace.Route.Waypoints[i], canvassElement, waypointContainerElement)
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

    var canvassPos = document.getElementById('courseContainer').getBoundingClientRect();
    var canvassElement = document.getElementById('courseCanvass');
    canvassElement.style.width = Math.floor(canvassPos.width) + "px";
    canvassElement.style.height = Math.floor(canvassPos.height) + "px";
    document.getElementById("settings").hidden = true;

    InitRaces();

    const resizeObserver = new ResizeObserver(entries => {
        // courseCanvass size changed, so adjust courseContainer then update waypoints
        var canvassPos = document.getElementById('courseCanvass').getBoundingClientRect();
        var containerElement = document.getElementById('courseContainer');
        containerElement.style.width = Math.floor(canvassPos.width) + "px";
        containerElement.style.height = Math.floor(canvassPos.height) + "px";
        InitWaypoints();
    });

    resizeObserver.observe(canvassElement);
}

function CreateYouTubeEmbed(channelName, commander) {
    // We create an iFrame for the stream
    var youTubeUrl = "https://www.youtube.com/embed/" + channelName;
    var youTubeInfoElement = document.getElementById(commander + "ProfileStream");
    while (youTubeInfoElement.lastChild) {
        youTubeInfoElement.removeChild(twitchInfoElement.lastChild);
    }

    var iframe = document.createElement("iframe");
    iframe.setAttribute("src", youTubeUrl);
    iframe.setAttribute("allowfullscreen", "true");
    iframe.width = youTubeInfoElement.width;
    iframe.height = youTubeInfoElement.width * 16 / 9;
    iframe.frameBorder = 0;
    youTubeInfoElement.appendChild(iframe);
    youTubeInfoElement.setAttribute("HideBio", "true");
    youTubeInfoElement.style.cursor = "default";
    document.getElementById(commander + "ProfileBio").style.display = "none";
}

function CreateTwitchEmbed(channelName, commander) {
    // We create an iFrame for the stream
    var twitchUrl = "https://player.twitch.tv/?channel=" + channelName + "&parent=" + document.domain;
    var twitchInfoElement = document.getElementById(commander + "ProfileStream");
    while (twitchInfoElement.lastChild) {
        twitchInfoElement.removeChild(twitchInfoElement.lastChild);
    }

    var iframe = document.createElement("iframe");
    iframe.setAttribute("src", twitchUrl);
    iframe.setAttribute("allowfullscreen", "true");
    iframe.width = twitchInfoElement.width;
    //iframe.height = twitchInfoElement.width * 108 / 192;
    twitchInfoElement.appendChild(iframe);
    twitchInfoElement.setAttribute("HideBio", "true");
    twitchInfoElement.style.cursor = "default";
    document.getElementById(commander + "ProfileBio").style.display = "none";
}

function ToggleProfile(commander) {

    var profileStream = document.getElementById(commander + 'ProfileStream');
    if (profileStream != null) {
        if (profileStream.style.display == "none") {
            profileStream.style.display = "block";
        }
        else {
            profileStream.style.display = "none";
        }

        if (profileStream.getAttribute("HideBio") == "true") {
            return;  // If we are showing a stream, we don't toggle the Bio div
        }        
    }

    var profileBio = document.getElementById(commander + 'ProfileBio');
    if (profileBio != null) {
        if (profileBio.style.display == "none") {
            profileBio.style.display = "block";
        }
        else {
            profileBio.style.display = "none";
        }
    }
}

function ToggleSettings() {
    document.getElementById("settings").hidden = !document.getElementById("settings").hidden;
//    if (!document.getElementById("settings").hidden) {
//        DisplaySettings();
//    }
}

function UpdateImageFromFile(elementName, fileElementName) {
    // Replace the background image of the element with the contents of the given file
    var element = document.getElementById(elementName);
    var reader = new FileReader();

    reader.onload = function () {
        element.style.backgroundImage = 'url(' + reader.result + ')';
        element.style.backgroundColor = "transparent";
        document.getElementById("courseContainer").style.opacity = 1;
        document.getElementById("courseContainer").style.backgroundColor = "transparent";
    }
    reader.readAsDataURL(document.getElementById(fileElementName).files[0]);
}

function UpdateImageUrl(elementName, imageUrl) {
    // Replace the background image of the element with the contents of the given file
    var element = document.getElementById(elementName);

    element.style.backgroundImage = 'url(' + imageUrl + ')';
    element.style.backgroundColor = "black";
    document.getElementById("courseContainer").style.opacity = 1;
    document.getElementById("courseContainer").style.backgroundColor = "transparent";
}

function SelectCourseImage() {
    var courseImages = document.getElementById('courseImages');
    var selectedCourse = courseImages.options[courseImages.selectedIndex].value;
    if (selectedCourse == "Blank") {
        document.getElementById("courseContainer").style.opacity = 0.9;
        document.getElementById('courseCanvass').style.backgroundImage = "";
    }
    else {
        UpdateImageUrl('courseCanvass', 'images/' + selectedCourse);
    }
}

function UpdateCourseImageOffset() {
    document.getElementById("courseCanvass").style.backgroundPosition = document.getElementById("courseImageXOffset").value + "px " + document.getElementById("courseImageYOffset").value + "px";
}

