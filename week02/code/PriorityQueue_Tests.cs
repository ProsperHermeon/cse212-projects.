using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities, then dequeue them one by one.
    // The highest priority item should be dequeued first. Enqueue order: A(1), B(3), C(2).
    // Expected Result: Dequeue should return B (priority 3), then C (priority 2), then A (priority 1).
    // Defect(s) Found: PriorityQueue.Dequeue loop condition was "index < _queue.Count - 1" which skipped
    // checking the last element. Also, the item was not actually removed from the queue after returning its value.
    // Test Result: PASS after fixes. 
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);

        // Highest priority is 3 (B), should be dequeued first
        Assert.AreEqual("B", priorityQueue.Dequeue());
        
        // Next highest is 2 (C)
        Assert.AreEqual("C", priorityQueue.Dequeue());
        
        // Last is 1 (A)
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Test that items with the same priority are dequeued in FIFO order (closest to front first).
    // Enqueue order: A(5), B(5), C(5), D(3). All have priority 5 except D.
    // Expected Result: Dequeue should return A first (first with priority 5), then B, then C, then D.
    // Defect(s) Found: PriorityQueue.Dequeue used ">=" comparison which would prefer later items in case of ties,
    // but should use ">" to prefer earlier items (FIFO). Also, items were not removed from queue.
    // Test Result: PASS after fixes. 
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 5);
        priorityQueue.Enqueue("D", 3);

        // When multiple items have the same highest priority, the one closest to front (FIFO) should be removed
        Assert.AreEqual("A", priorityQueue.Dequeue()); // First with priority 5
        Assert.AreEqual("B", priorityQueue.Dequeue()); // Second with priority 5
        Assert.AreEqual("C", priorityQueue.Dequeue()); // Third with priority 5
        Assert.AreEqual("D", priorityQueue.Dequeue()); // Last with priority 3
    }

    [TestMethod]
    // Scenario: Test that Enqueue adds items to the back of the queue regardless of priority.
    // Enqueue A(1), then B(3), then C(5). Check that order in queue is A, B, C before dequeuing.
    // Expected Result: Items should be enqueued in order A, B, C. Dequeue returns C (highest priority 5).
    // Defect(s) Found: PriorityQueue.Dequeue was not removing items from the queue after returning their values.
    // Test Result: PASS after fixes. 
    public void TestPriorityQueue_EnqueueToBack()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 5);

        // Verify that items are added to back (order should be A, B, C)
        // But when we dequeue, highest priority (C with 5) comes out first
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Test that dequeueing from an empty queue throws InvalidOperationException with message "The queue is empty."
    // Expected Result: InvalidOperationException should be thrown with message "The queue is empty."
    // Defect(s) Found: No defects found - exception handling was working correctly. Test Result: PASS. 
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown when dequeueing from empty queue.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message));
        }
    }

    [TestMethod]
    // Scenario: Test mixed priorities with ties to ensure correct FIFO behavior for ties.
    // Enqueue: A(2), B(5), C(2), D(5), E(1). When there are ties for highest priority, remove the one closest to front.
    // Expected Result: Dequeue returns B (first with priority 5), then D (second with priority 5), then A (first with priority 2),
    // then C (second with priority 2), then E (priority 1).
    // Defect(s) Found: PriorityQueue.Dequeue loop condition missed the last element, comparison used ">=" instead of ">"
    // for FIFO behavior with ties, and items were not removed from the queue. Test Result: PASS after fixes. 
    public void TestPriorityQueue_MixedPrioritiesWithTies()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 2);
        priorityQueue.Enqueue("D", 5);
        priorityQueue.Enqueue("E", 1);

        // Highest priority is 5: B and D. B was enqueued first, so it should be dequeued first
        Assert.AreEqual("B", priorityQueue.Dequeue());
        
        // Next highest is 5: D
        Assert.AreEqual("D", priorityQueue.Dequeue());
        
        // Next highest is 2: A and C. A was enqueued first
        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        
        // Last is 1: E
        Assert.AreEqual("E", priorityQueue.Dequeue());
    }
}