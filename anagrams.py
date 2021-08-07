def anagrams(word1, word2):
    word_array = []

    if len(word1) != len(word2):
        return print('Not dude from here')
    else:
         for char1 in word1:
            for char2 in word2:
                if char1 != char2:
                    char1 += word_array

    return word_array                

if __name__ == '__main__':
    word = "happy"
    word2 = "radar"
    anagrams(word2, word) 