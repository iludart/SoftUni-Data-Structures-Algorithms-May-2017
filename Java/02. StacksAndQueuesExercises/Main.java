/**
 * Created by Guest Lector on 5/25/2017.
 */
public class Main {

    public static void main(String[] args) {

        LinkedQueue<Integer> linkedQueue = new LinkedQueue<>();
        linkedQueue.enqueue(1);
        linkedQueue.dequeue();

        System.out.println(linkedQueue.head);
        System.out.println(linkedQueue.tail);

    }
}
