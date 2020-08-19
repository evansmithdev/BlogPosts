import p10Users, p11Items, pathlib
from pathlib import Path
from datetime import datetime

def main():
    # user log in
    print()
    print("You are accessing the secure Code PaLOUsa 2020 Point-Of-Sale application.")
    print()

    userId = input("User:" )
    print()

    scriptPath = pathlib.Path(__file__).parent

    userPath = scriptPath / 'users.csv'
    users = p10Users.LoadUsers(userPath)
    validatedResult = p10Users.ValidateUser(users, userId)
    if validatedResult[0]:
        print(f"\tWelcome to Code PaLOUsa 2020, {validatedResult[1]['firstName']} {validatedResult[1]['lastName']}!")
    else:
        print("* ACCESS DENIED! *   "*3)
    print()

    # load inventory
    print("... loading inventory ...")
    itemPath = scriptPath / 'items.csv'
    items = p11Items.LoadInventory(itemPath)
    print("... done!")
    print()

    # check out (collect items and look up prices)
    print("Ready for checkout!")
    print(f"\t\t\t{'Description':<25} {' Price'}")
    cart = []
    
    scannedItem = PromptForItem(items)
    if scannedItem[0]: cart.append(scannedItem[1])
    
    while scannedItem[2] == True:
        scannedItem = PromptForItem(items)
        if scannedItem[0]: cart.append(scannedItem[1])

    # print receipt
    print("... printing receipt ...")
    print()
    print()

    print(f"{'Intro to Python Store':^50}")
    print(f"{'4013 Armory Oaks Dr':^50}")
    print(f"{'Nashville, TN 37204':^50}")
    print(f"{'(615) 256-3684':^50}")
    print()
    print(f"{f'{datetime.now():%B %d, %Y  %H:%M:%S}':^50}")
    print()
    print(f"{f'##':<4}{'ID':<8}{'Description':<25}{'Price':>9} Tax")
    for i in range(len(cart)):
        if cart[i]['taxed'].upper() == "TRUE": taxFlag = "T"
        else: taxFlag = ""
        print(f"{f'{i+1:02}':<4}{cart[i]['id']:<8}{cart[i]['description']:<25}${float(cart[i]['price']):>8.2f} {taxFlag:>2}")

    print()
    print()
    print("... done!")

# returns (bool: isValid?, dict: itemInfo, bool: continue?)
def PromptForItem(items):
    print()
    item = input("Item: ")
    
    if item == "": return (False, None, False)

    validatedItem = p11Items.ValidateItem(items, item)
    if validatedItem[0]:
        itemInfo = validatedItem[1]
        print(f"\t\t\t{itemInfo['description']:<25} ${float(itemInfo['price']):.2f}")
    else:
        print("     *invalid*")
    return (validatedItem[0], validatedItem[1], True)

if __name__ == "__main__":
    main()