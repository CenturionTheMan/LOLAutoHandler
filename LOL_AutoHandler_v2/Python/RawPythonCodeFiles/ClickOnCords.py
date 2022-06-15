import sys
import pyautogui


cordX = int(sys.argv[1])
cordY = int(sys.argv[2])

pyautogui.click(cordX, cordY)
