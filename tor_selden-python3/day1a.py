inp = [int(n) for n in list(open('1.txt').read()) if n.isdigit()]

print("Part 1: ", sum(a[0] for a in zip(inp, inp[1:] + [inp[0]]) if a[0] == a[1]))
print("Part 2: ", sum(a[0] for a in zip(inp, inp[len(inp)//2:] + inp[:len(inp)//2]) if a[0] == a[1]))
