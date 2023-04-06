# Process Monitor

This is a simple C# utility to monitor an app running on the OS and terminates the same after a certain time, which is specefied as arguments. 
The user can exit the programm anytime by pressing the `q` key on the keyboard. 

## OS
Unit test cases are currenly running on MacOS, albeit can be run on any OS by adjusting the process paths.

## Running the application
  - Clone and checkout the git repository
  - Navigate into the project folder and open the command line tool.
  - To run the `.exe` file, type in, for example `MonitorProcess Notes 5 1` from the CMD tool, where  'MonitorProcess' is the name of the application , 'Notes' is the process to be monitored , '5' is the maximum lifetime the process should be active and '1' is the frequency of monitoring 
  - The utility is now running and monitoring the specefied process.
  - To exit the program , press `Q` or `q`.

## Screenhots
![Alt text](/screenshots/utility.png?raw=true "Utility running")

## Feature Improvement
  - Improve test cases
  - Further code splitting for scalability

