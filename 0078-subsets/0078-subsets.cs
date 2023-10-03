public class Solution
{
    public IList<IList<int>> Subsets(int[] nums)
    {
        IList<IList<int>> subsets = new List<IList<int>>();

        getSubsetRecursive(subsets, nums, new List<int>(), 0);

        return subsets;
    }

    private void getSubsetRecursive(IList<IList<int>> subsets, int[] nodes, List<int> prevPath, int currNodeIndex)
    {
        subsets.Add(prevPath);

        for (int i = currNodeIndex; i < nodes.Length; ++i)
        {
            List<int> currNode = new List<int> { nodes[i] };
            List<int> nextPath = prevPath.Concat(currNode).ToList();

            getSubsetRecursive(subsets, nodes, nextPath, i + 1);
        }
    }
}