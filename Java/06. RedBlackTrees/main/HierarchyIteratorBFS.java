package main;

import java.util.Deque;
import java.util.Iterator;
import java.util.LinkedList;

public class HierarchyIteratorBFS<T> implements Iterator<T> {

    private Deque<Node<T>> queue;

    public HierarchyIteratorBFS(Node<T> rootNode) {
        this.queue = new LinkedList<>();

        this.queue.addLast(rootNode);
    }

    @Override
    public boolean hasNext() {
        return !this.queue.isEmpty();
    }

    @Override
    public T next() {
        Node<T> head = queue.removeFirst();
        for (Node<T> child : head.getChildren()) {
            this.queue.addLast(child);
        }

        return head.getValue();
    }
}
