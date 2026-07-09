# Roblox Multi-Instance

A tiny standalone Windows tool that lets you run **more than one Roblox client at the same time**.

## Download

Grab the latest **`RobloxMultiInstance.exe`** from the
[**Releases**](https://github.com/VirageRoblox/RobloxMultiInstance/releases/latest) page.

It's a single file — no installer, no .NET download required. Just run it.

> Windows SmartScreen may warn "unknown publisher" because the app isn't code-signed.
> Click **More info → Run anyway**. (The source is right here if you'd rather build it yourself.)

## How to use

1. **Close every Roblox window first.**
2. Open `RobloxMultiInstance.exe` — it should show a green **"Multi-instance is ACTIVE"**.
   (If it's red, close all Roblox and click **Retry**, or use **Force-close all Roblox**.)
3. **Keep the window open**, then launch Roblox as many times as you like — each client stays open.
4. Closing the window turns multi-instance back off.

## How it works

Roblox uses a named system object to enforce a single client and to close the old
client when you launch a new game. This tool simply claims that name first (as the
"wrong" object type), which disables that behavior so multiple clients can coexist.
It only works if the tool is running **before** you launch Roblox.

If a future Roblox update changes the name, multi-instance will stop working — the
name is a single constant in [`App.xaml.cs`](App.xaml.cs).

## Build it yourself

```
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

## Disclaimer

Personal utility, provided as-is. Running multiple clients and/or alternate accounts
may violate Roblox's Terms of Service — **use at your own risk**. Not affiliated with
or endorsed by Roblox Corporation.
