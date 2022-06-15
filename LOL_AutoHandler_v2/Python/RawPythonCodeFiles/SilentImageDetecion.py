import sys
from pathlib import Path
import pyautogui as pyautogui

topLeftX = sys.argv[1]
topLeftY = sys.argv[2]
windowSizeX = sys.argv[3]
windowSizeY = sys.argv[4]
target = sys.argv[5]
conf = float(sys.argv[6])

topLeftX = int(topLeftX)
topLeftY = int(topLeftY)
windowSizeX = int(windowSizeX)
windowSizeY = int(windowSizeY)
target = str(target)


def DetectImage(imagePath, x, y, width, height, confi):
    if Path(imagePath).is_file():
        return pyautogui.locateOnScreen(imagePath, confidence=confi, region=(x, y, width, height))
    else:
        return None


target = target.replace('$', '')
image = DetectImage(target, topLeftX, topLeftY, windowSizeX, windowSizeY, conf)
if image is not None:
    print("true")
else:
    print("false")
