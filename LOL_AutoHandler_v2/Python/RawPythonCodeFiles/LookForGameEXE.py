import pygetwindow as gw


lolWindow = None
temp = gw.getWindowsWithTitle('League of Legends (TM) Client')
if temp.__len__() > 0:
    lolWindow = temp[0]

if lolWindow is not None:
    print("true")
else:
    print("false")

