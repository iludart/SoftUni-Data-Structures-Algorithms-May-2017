import java.util.*;

/**
 * Created by Guest Lector on 5/25/2017.
 */
public class SequenceMN {

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);

        int start = in.nextInt();
        int end = in.nextInt();

        if (end < start) {
            System.out.println("(no solution)");
            return;
        }

        Deque<Item> queue = new LinkedList<>();
        Item startItem = new Item();
        startItem.value = start;

        queue.addLast(startItem);
        while (true) {
            Item element = queue.removeFirst();

            if (element.value == end) {
                while (element != null) {
                    System.out.println(element.value);
                    element = element.prevItem;
                }

                return;
            }

            queue.addLast(new Item(element.value + 1, element));
            // check if added item has value == end
            // if so recover path

            queue.addLast(new Item(element.value + 2, element));
            queue.addLast(new Item(element.value * 2, element));
        }


    }

    private static class Item {
        private int value;
        private Item prevItem;

        public Item() {
        }

        public Item(int value, Item prevItem) {
            this.value = value;
            this.prevItem = prevItem;
        }
    }
}
