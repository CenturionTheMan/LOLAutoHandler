import pygetwindow as gw

def GetClientSize():
    lolWindow = None
    temp = gw.getWindowsWithTitle('League of Legends')
    if temp.__len__() > 0:
        lolWindow = temp[0]
        if lolWindow.isMinimized:
            return "WindowMinimized"
    return lolWindow

window = GetClientSize()

if window is None:
    print("None")
elif window == "WindowMinimized":
    print("WindowMinimized")
else:
    print("x={0}endX y={1}endY width={2}endW height={3}endH".format(window.topleft.x,window.topleft.y,window.width,window.height))