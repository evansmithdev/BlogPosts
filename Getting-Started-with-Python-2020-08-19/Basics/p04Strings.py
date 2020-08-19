# single quotes / double quotes / escape character
name = 'Evan'
print(name)

name = "Evan"
print(name)

name = 'O\'Neal'
print(name)

name = "O'Neal"
print(name)


# new line
name = "Nashville School of Law\nAttn:Civil Suits Dept"
print(name)

# tab
name = "Nashville School of Law\n\tBusiness Law\n\tCivil Suits\n\tCriminal Law\n\tTax Law"
print(name)

# raw strings
name = r'C:\temp'
print(name)

# multi-line strings
name = """\
    Nashville School of Law
        B-Law
        C-Suits
        C-Law
        T-Law
"""
print(name)


# concatenation
name = "Penny. " * 3
print(name)

name = 'Penny' 'Lane'
print(name)

first = 'Penny'
last = 'Lane'
name = first + last
print(name)

# subscripts / substrings
print(name[0])
print(name[1])
print(name[2])
print(name[3])
print(name[4])

# negative indexes
print(name[-1])
print(name[-2])
print(name[-3])
print(name[-4])

# slicing
print(name[0:5])
print(name[5:9])

# range defaults
print(name[:5])
print(name[5:])

# 0123456789
# PennyLane


# index out of range
#print(name[50])

# strings are immutable
#name[0] = "J"
print(name)

name = "J" + name[1:]
print(name)

print(len(name))

# formatting
print(f"My name is {name}.")
print(f"I am {18} years old.")

from datetime import datetime
birthday = datetime(year=1990, month=1, day=27)
print(f"My birthday is {birthday:%B %d, %Y}")
