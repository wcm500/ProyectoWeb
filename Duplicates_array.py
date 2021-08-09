def duplicates(numArr):
    count =0
    numTemp = 0
    reptArray =[]
    while count <2:
        for numCurrent in numArr:
            numTemp = numCurrent
            count = 0
            for num in numArr:
                if num == numTemp:
                    count = count+1
                    if count >=2:
                        reptArray.append(numTemp)
                        count = 0
        break

    
    print(reptArray) 


def find_duplicates_efficent(nums):
    for num in nums:
        if nums[abs(num)] >= 0:
            nums[abs(num)] = -nums[abs(num)]
        else:
            print ("repetion found: %s " % str(abs(num)))



if __name__ == '__main__':
    num = [3,3,3,1,1,5]
    duplicates(num) 
    find_duplicates_efficent(num)