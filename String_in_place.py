def string_reverse(word):
    #Convert the word into an array 
    auxword = ""
    arrayword =[]
    
    for char in word:
        arrayword += char

    Index = 0
    lastIndex = len(arrayword) -1

    while lastIndex != 1 : #Mientras que el ultimo index del arreglo sea diferente de 0 se cumple la condicion
        arrayword[Index], arrayword[lastIndex] = arrayword[lastIndex], arrayword[Index] #se volvea los index del arreglo

        #print('control#2',arrayword[Index])
        Index =  Index + 1 # n(0) + 1
        lastIndex = lastIndex -1 # n(n-1)

    #Transform this array in to a word to got back
    for char in arrayword:
        auxword += char

    if auxword == word:
        print('Word Initial: ', word +"\n"
                'Word Transmform: ', auxword+"\n",'Result: Palindrome')
        return auxword
    else:
        print('Word Initial: ', word +"\n"
                'Word Transmform: ', auxword+"\n", 'Result: Not Palindrome')
        return auxword

if __name__ == '__main__':
    word = "happy"
    word2 = "radar"
    string_reverse(word2) 


