import sys
from pathlib import Path
import pyautogui as pyautogui

topLeftX = sys.argv[1]
topLeftY = sys.argv[2]
windowSizeX = sys.argv[3]
windowSizeY = sys.argv[4]
keyInput = sys.argv[5]
target = sys.argv[6]

topLeftX = int(topLeftX)
topLeftY = int(topLeftY)
windowSizeX = int(windowSizeX)
windowSizeY = int(windowSizeY)
target = str(target)


def click(x, y):
    pyautogui.leftClick(x, y)


def ClickOnImage(foundedImage):
    if foundedImage is not None:
        point = pyautogui.center(foundedImage)
        click(point.x, point.y)


def FakeTextInput(text):
    for letter in text:
        pyautogui.keyDown(letter)


def DetectImage(imagePath, x, y, width, height):
    if Path(imagePath).is_file():
        return pyautogui.locateOnScreen(imagePath, confidence=0.8, region=(x, y, width, height))
    else:
        return None


target = target.replace('$', '')
image = DetectImage(target, topLeftX, topLeftY, windowSizeX, windowSizeY)
if image is not None:
    ClickOnImage(image)
    if "!" not in keyInput:
        FakeTextInput(keyInput)
    print("true")
else:
    print("false")

