import sys
import pyautogui as pyautogui

xCord = int(sys.argv[1])
yCord = int(sys.argv[2])
lettersCount = sys.argv[3]

lettersCount = int(lettersCount)


def click(x, y):
    pyautogui.leftClick(x, y)


def RemoveLetters(letterCount):
    for x in range(0, letterCount):
        pyautogui.keyDown('backspace')


click(xCord, yCord)
RemoveLetters(lettersCount)

