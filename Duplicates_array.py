def duplicates(numArr):

    numTemp = 0

    for num in numArr:
        #numero anterior es igual que el posterior ?
        numTemp += num# se guarda el numero 
        for numF in numArr:
            if numF == numTemp:
                return True
    
 

if __name__ == '__main__':
    num = [1,2,3,4,4,5]
    duplicates(num) 