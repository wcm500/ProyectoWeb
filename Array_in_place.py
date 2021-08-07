def reverse(num):
    index = 0
    lastIndex = len(num)-1

    while num[index] > num[lastIndex]:
        
        num[index], num[lastIndex] = num[lastIndex], num[index]
        #num[lastIndex] = num[index]

        lastIndex = lastIndex -1
        index = index +1
    
    
if __name__ == '__main__':
    num = [5,4,3,2,1]
    reverse(num)
    print(num)




