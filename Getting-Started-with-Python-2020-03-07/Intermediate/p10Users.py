import csv

def LoadUsers(filePath):
    users = {}
    with open(filePath, newline='') as userCsv:
        userCsv.seek(0)
        userReader = csv.DictReader(userCsv)
        for userRow in userReader:
            users[userRow['id']] = userRow
    return users

def ValidateUser(users, id):
    if id in users.keys():
        return (True, users[id])
    return (False, None)
