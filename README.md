# FallbackMechanism
A Fallback Mechanism for Dell Laptops with Battery Amnesia

## Genesis

**FallbackMechanism** is a Windows program to solve a very specific problem. Many refurbished Dell Laptops these days, including the Latitude 7490 which I bought recently, ship with a very specific battery defect: There is nothing wrong with the battery itself which gives a decent 4-5 hours of runtime; however, the controller or bios firmware reports the incorrect percentage to Windows which results in your laptop shutting down much sooner at 43-45%. The charge is over at this point, but Windows thinks there are still few hours left.

The official or recommended path here is to get a new battery installed, of course. But in case you don't have the budget for a new battery, `FallbackMechanism` is a software solution that will let you continue using an otherwise fine machine with no other issues except for this little "battery amnesia".

Once you start the program, it stays in the system tray and warns you with a notification when the battery discharges below the set percentage value (50% by default). Mind you, the battery only has about 7-10% charge at this point and it's time to either shut the machine down or plug it to a charger.

![tray-icon](https://raw.githubusercontent.com/prahladyeri/fallbackmechanism/main/screenshots/tray-icon.png)

![program](https://raw.githubusercontent.com/prahladyeri/fallbackmechanism/main/screenshots/program.png)

## Installation

Just download the latest release [from here](https://github.com/prahladyeri/FallbackMechanism/releases/latest) and copy it to a folder in your C:\ drive. It's highly recommended to set it to run at startup for regular use.

## Memory usage

On my Latitude 7490 with 16GB RAM, it consumes a mere 3MB of memory! FallbackMechanism is pure .NET code (C#/CLR) and built with Visual Studio, it thus follows the most efficient workflow.