/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
        ListNode sumNode = GetSumNode(l1, l2);

        int carry = 0;
        ListNode curr = sumNode;
        while (curr != null)
        {
            if (carry > 0)
            {
                curr.val += carry;
                carry = 0;
            }

            if (curr.val > 9)
            {
                carry = (int)(curr.val / 10);
                curr.val -= 10;
            }

            // hack
            if (curr.next == null && carry > 0)
            {
                curr.next = new ListNode(carry, null);
                carry = 0;
            }

            curr = curr.next;
        }
            
        return sumNode;
    }

    public ListNode GetSumNode(ListNode l1, ListNode l2)
    {
        if (l1.next == null || l2.next == null)
        {
            ListNode node = new ListNode(l1.val + l2.val, null);

            if (l1.next != null)
            {
                node.next = l1.next;
            }

            if (l2.next != null)
            {
                node.next = l2.next;
            }

            return node;
        }

        ListNode sumNode = GetSumNode(l1.next, l2.next);

        return new ListNode(l1.val + l2.val, sumNode);
    }
}