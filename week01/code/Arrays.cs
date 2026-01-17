public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN FOR MultiplesOf:
        // Step 1: Create a new array of doubles with size equal to 'length'
        // Step 2: Use a loop to fill the array with multiples
        //         - The first element should be number * 1 (which is just the number itself)
        //         - The second element should be number * 2
        //         - The third element should be number * 3
        //         - Continue this pattern until we have 'length' elements
        // Step 3: For each index i (from 0 to length-1), set array[i] = number * (i + 1)
        //         This gives us: array[0] = number*1, array[1] = number*2, etc.
        // Step 4: Return the completed array

        // Create a new array with the specified length
        double[] result = new double[length];
        
        // Fill the array with multiples of the number
        for (int i = 0; i < length; i++)
        {
            // Each element is the number multiplied by (i + 1)
            // This gives us: number*1, number*2, number*3, ..., number*length
            result[i] = number * (i + 1);
        }
        
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // PLAN FOR RotateListRight:
        // Rotating right by 'amount' means moving the last 'amount' elements to the front.
        // Example: {1,2,3,4,5,6,7,8,9} rotated right by 5 becomes {5,6,7,8,9,1,2,3,4}
        // This means the last 5 elements (5,6,7,8,9) move to the front, and the first 4 (1,2,3,4) follow.
        //
        // Step 1: Calculate the split point - the index where we divide the list
        //         - The last 'amount' elements start at index (data.Count - amount)
        //         - The first (data.Count - amount) elements are from index 0 to (data.Count - amount - 1)
        // Step 2: Extract the last 'amount' elements using GetRange
        //         - GetRange(data.Count - amount, amount) gives us the elements to move to front
        // Step 3: Extract the first (data.Count - amount) elements using GetRange
        //         - GetRange(0, data.Count - amount) gives us the elements that will follow
        // Step 4: Clear the original list
        // Step 5: Add the last 'amount' elements first (they become the new front)
        // Step 6: Add the first (data.Count - amount) elements after (they become the new back)

        // Calculate the split point - where the list divides
        int splitIndex = data.Count - amount;
        
        // Extract the last 'amount' elements (these will move to the front)
        List<int> lastElements = data.GetRange(splitIndex, amount);
        
        // Extract the first (data.Count - amount) elements (these will follow)
        List<int> firstElements = data.GetRange(0, splitIndex);
        
        // Clear the original list
        data.Clear();
        
        // Add the last elements first (they become the new front of the list)
        data.AddRange(lastElements);
        
        // Add the first elements after (they become the new back of the list)
        data.AddRange(firstElements);
    }
}
