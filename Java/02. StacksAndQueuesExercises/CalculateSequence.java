import java.util.Arrays;
import java.util.Deque;
import java.util.LinkedList;
import java.util.Scanner;

public class CalculateSequence {

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);

        int n = in.nextInt();

        Deque<Integer> queue = new LinkedList<>();
        queue.addLast(n);

        int index = 0;
        int[] members = new int[50];

        while (true) {
            int element = queue.removeFirst();

            members[index] = element;
            index++;
            if (index == 50) {
                break;
            }

            queue.addLast(element + 1);
            queue.addLast(2 * element + 1);
            queue.addLast(element + 2);
        }

        System.out.println(Arrays.toString(members).replace("[", "").replace("]", ""));
    }
}
