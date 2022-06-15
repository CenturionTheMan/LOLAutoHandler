import os
import json

class ChampionClass:
    def __init__(self, name, uiPicture, banPhaseSmallPicture, banPhasePicture, banPhaseLargePicture, pickPhaseSmallPicture, pickPhasePicture, pickPhaseLargePicture):
        self.Name = name
        self.UIPicture = uiPicture
        self.BanPhasePicture = banPhasePicture
        self.BanPhaseSmallPicture = banPhaseSmallPicture
        self.BanPhaseLargePicture = banPhaseLargePicture
        self.PickPhaseSmallPicture = pickPhaseSmallPicture
        self.PickPhasePicture = pickPhasePicture
        self.PickPhaseLargePicture = pickPhaseLargePicture

def GetNames(championsFolder):
    names = []
    for filename in os.listdir(championsFolder):
        names.append(filename)
    return names

def GetUIFromFolder(championsFolder, name):
    image = os.path.join('\\Python', championsFolder, name, name+'.png')
    return image

def CreatePathFromPrefix(championsFolder, name, prefix):
    image = os.path.join('Python', championsFolder, name, name + prefix + '.png')
    return image

championsHolderFolder = 'ChampionsFolders'
champions = []

names = GetNames(championsHolderFolder)

for temp in names:
    uiPicture = GetUIFromFolder(championsHolderFolder,temp)
    banPhaseSmallPicture = CreatePathFromPrefix(championsHolderFolder, temp, 'BanSmall')
    banPhasePicture = CreatePathFromPrefix(championsHolderFolder, temp, 'Ban')
    banPhaseLargePicture = CreatePathFromPrefix(championsHolderFolder, temp, 'BanLarge')
    pickPhasePicture = CreatePathFromPrefix(championsHolderFolder, temp, 'Pick')
    pickPhaseSmallPicture = CreatePathFromPrefix(championsHolderFolder, temp, 'PickSmall')
    pickPhaseLargePicture = CreatePathFromPrefix(championsHolderFolder, temp, 'PickLarge')
    champions.append(ChampionClass(temp, uiPicture, banPhaseSmallPicture, banPhasePicture, banPhaseLargePicture, pickPhaseSmallPicture, pickPhasePicture, pickPhaseLargePicture))

jsonArr = []

for champion in champions:
    jsonStr = json.dumps(champion.__dict__)
    jsonArr.append(jsonStr)


with open("ChampionsData.json","w") as outfile:
    outfile.write("[")
    for str in jsonArr:
        if str == jsonArr[-1]:
            outfile.write(str + "]")
        else:
            outfile.write(str+",")