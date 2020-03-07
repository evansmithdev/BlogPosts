from datetime import datetime, timedelta

# write the "main" function
def main():
    user = input("User: ")
    GreetUserExtended(user, datetime.now() - timedelta(days=3) + timedelta(hours=4))

# write functions you need in main
def GreetUserExtended(name, lastLogin):
    print(f"Welcome, {name}!")
    print(f"Current: {datetime.now():%B %d, %Y @ %H:%M:%S}")
    print(f"Last Login: {lastLogin:%B %d, %Y @ %H:%M:%S}")

# how do we make it run?
if __name__ == "__main__":
    main()


# emphasize the order in which functions are defined