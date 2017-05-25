public class LinkedQueue<E> {

    private int size;
    public QueueNode<E> head;
    public QueueNode<E> tail;

    public int size() {
        return this.size;
    }

    private void setSize(int size) {
        this.size = size;
    }

    public void enqueue(E element) {
        if (this.head == null) {
            this.head = new QueueNode<>();
            this.head.value = element;

            this.tail = this.head;
        } else {
            QueueNode<E> newTail = new QueueNode<>();
            newTail.value = element;

            this.tail.nextNode = newTail;
            this.tail = newTail;
        }

        this.setSize(this.size() + 1);
    }

    public E dequeue() {
        if (this.size() == 0) {
            throw new IllegalArgumentException("Queue is empty!");
        }

        E value = this.head.value;

        this.head = this.head.nextNode;
        this.setSize(this.size() - 1);

        return value;
    }

    public E[] toArray() {
        E[] toReturn = (E[]) new Object[this.size()];

        QueueNode<E> tempHead = this.head;

        int index = 0;
        while (tempHead != null) {
            toReturn[index++] = tempHead.value;
            tempHead = tempHead.nextNode;
        }

        return toReturn;
    }

    private class QueueNode<E> {
        private E value;

        private QueueNode<E> nextNode;

        public E getValue() {
            return this.value;
        }

        private void setValue(E value) {
            this.value = value;
        }

        public QueueNode<E> getNextNode() {
            return this.nextNode;
        }

        public void setNextNode(QueueNode<E> nextNode) {
            this.nextNode = nextNode;
        }

    }
}