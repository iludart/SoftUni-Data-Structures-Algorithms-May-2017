package main;

import java.util.*;

public class Node<T> {

    private T value;

    private Node<T> parent;
    private Set<Node<T>> children;

    public Node(T value) {
        this(value, null);
    }

    public Node(T value, Node<T> parent) {
        this.children = new LinkedHashSet<>();
        this.value = value;
        this.parent = parent;
    }

    public T getValue() {
        return this.value;
    }

    public Node<T> getParent() {
        return this.parent;
    }

    public void setParent(Node<T> parent) {
        this.parent = parent;
    }

    public Set<Node<T>> getChildren() {
        return this.children;
    }

    public void addChild(Node<T> child) {
        this.children.add(child);
    }

    @Override
    public String toString() {
        return this.value + "";
    }
}
