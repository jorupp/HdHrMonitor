# HdHrMonitor

A quick tool I threw together to watch the status page of my HD Homerun Prime and capture the stats it shows over time to try to debug some issues.

## inital setup - create DB

```sh
dotnet tool restore
dotnet ef database update
```

## Running it

- Create a launch profile passing the IP of the HD Homerun Prime.
- Run the tool (ctrl-F5 if you don't want to debug it), and leave it running until the tuning/quality issue reproduces.
- Query the `Data` table in the DB to explore the data.  I expect I'll add some queries here once I find some interesting ones for debugging.
