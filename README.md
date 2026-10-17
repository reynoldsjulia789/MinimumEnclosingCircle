# MinimumEnclosingCircle

**Premise:**

You are attempting to place a wireless transmitter to cover a group of locations that can each be represented by an x and y coordinate. You need to find the best location to put the transmitter. This can be done by finding the minimum enclosing circle where the center gives a transmitter position that minimizes the distance to the farthest location and the radius is the minimum range needed.

This problem assumes an open, flat area with uniform signal reach. Real transmitter placements would also need to account for buildings, terrain, and available installation sites.

## How to Run
1) Place .txt files containing coordinates into the directory `.\Graphs`
     * Coordinates for each point must be on a separate line
     * Coordinates for each point must be in the following format:
         * floatx, floaty
2) Use your preferred run method below:

**Command Line:**
1) Navigate to the folder containing the `.csproj` file (currently, it is in the inner "BrokenKeypad" folder)
2) Run the following command with your desired configuration: `dotnet run`

**Visual Studio:**
1) Navigate to the folder containing the solution file (`.slnx`)
2) Open the file in Visual Studio
3) Locate the play button
    * Alter the run settings
        1) Click the dropdown next to the play button and click the menu item that says "BrokenKeypad Debug Properties"
        2) Click the `Browse...` button next to the "Working directory" section and choose the directory with the `.csproj` file in it
            * Without it, the graph files containing the coordinates will not be able to be found
        3) Exit out of the Launch Profiles popup. It should save automatically.
4) Click the play button
