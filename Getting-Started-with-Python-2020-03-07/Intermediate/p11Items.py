import csv

def main():
    
    item =  input("Add an item: ")
    price = float(input("      Price: "))
    print(f"\t\t\t{item} @ {('$' + f'{price:.2f}'):>10}")


def LoadInventory(filePath):
    contents = {}
    with open(filePath, newline='') as csvFile:
        csvFile.seek(0)
        csvReader = csv.DictReader(csvFile)
        for row in csvReader:
            contents[row['id']] = row
    return contents

def ValidateItem(items, id):
    if id in items.keys():
        return (True, items[id])
    return (False, None)
    