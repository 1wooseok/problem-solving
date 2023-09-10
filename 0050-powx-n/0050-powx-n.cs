public class Solution {
    public double MyPow(double x, int n)
    {
        double pow = calcPowRecursive(x, n);
        return n > 0 ? pow : 1.0 / pow;
    }    

    private double calcPowRecursive(double x, int n)
    {
        if (n == 0)
        {
            return 1;
        }

        double half = calcPowRecursive(x, n / 2);
        double result = half * half;

        return n % 2 == 0 ? result : result * x;
    }
}