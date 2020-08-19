# Recall print()
#print("I don't actually know how print works ...")
#print(" ... and I don't actually care.")

# write a function that we've already used
def Add(number1, number2):
    return number1 + number2

#print(Add(2,5))


# write a useful function (maybe a function to greet a user on our web site?)
def GreetUser(name):
    from datetime import datetime
    print(f"Welcome, {name}!\nIt is now {datetime.now():%B %d, %Y @ %H:%M:%S}")

#GreetUser("Evan")


# extend the function (report the last login date/time mayhap?)
def GreetUserExtended(name, lastLogin):
    GreetUser(name)
    print(f"Last Login: {lastLogin:%B %d, %Y @ %H:%M:%S}")

#from datetime import datetime, timedelta
#GreetUserExtended("Evan", datetime.now() - timedelta(days=3) + timedelta(hours=4))

# prompt
#userEnteredName = input("User: ")
#GreetUser(userEnteredName)
