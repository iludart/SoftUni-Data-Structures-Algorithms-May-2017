package main;

import java.util.*;

public class Hierarchy<T> implements IHierarchy<T> {

    private Node<T> root;
    private Map<T, Node<T>> nodesByValue;

    public Hierarchy(T element){
        this.nodesByValue = new LinkedHashMap<>();

        this.root = new Node<>(element);
        this.nodesByValue.put(element, root);
    }

    public void add(T parent, T child){
        if (!this.nodesByValue.containsKey(parent)) {
            throw new IllegalArgumentException("Parent does not exist in hierarchy");
        }

        if (this.nodesByValue.containsKey(child)) {
            throw new IllegalArgumentException("Child already exists");
        }

        Node<T> parentNode = this.nodesByValue.get(parent);
        Node<T> childNode=  new Node<>(child, parentNode);
        parentNode.addChild(childNode);
        this.nodesByValue.put(child, childNode);
    }

    public int getCount() {
        return this.nodesByValue.size();
    }

    public void remove(T element){
        if (!this.nodesByValue.containsKey(element)) {
            throw new IllegalArgumentException("Element is not presented in Hierarchy");
        }

        Node<T> toBeRemoved = this.nodesByValue.get(element);
        Node<T> parent = toBeRemoved.getParent();
        if (parent == null) {
            throw new IllegalStateException("Cannot delete root!");
        }

        parent.getChildren().remove(toBeRemoved);

        Set<Node<T>> children = toBeRemoved.getChildren();
        for (Node<T> child : children) {
            parent.addChild(child);
            child.setParent(parent);
        }

        this.nodesByValue.remove(element);
    }

    public boolean contains(T element){
        return this.nodesByValue.containsKey(element);
    }

    public T getParent(T element){
        if (!this.contains(element)) {
            throw new IllegalArgumentException("Element is not presented in Tree");
        }

        Node<T> parent = this.nodesByValue.get(element).getParent();
        if (parent == null) {
            return null;
        }

        return parent.getValue();
    }

    public Iterable<T> getChildren(T element){
        if (!this.contains(element)) {
            throw new IllegalArgumentException("Element is not presented in Tree");
        }

        Node<T> node = this.nodesByValue.get(element);
        List<T> children = new ArrayList<>();
        for (Node<T> childNode : node.getChildren()) {
            children.add(childNode.getValue());
        }

        return children;
    }

    public Iterable<T> getCommonElements(IHierarchy<T> other){
        Collection<Node<T>> nodes = this.nodesByValue.values();
        List<T> commonElements = new ArrayList<>();
        for (Node<T> node : nodes) {
            if (other.contains(node.getValue())) {
                commonElements.add(node.getValue());
            }
        }

        return commonElements;
    }

    @Override
    public Iterator<T> iterator() {
        return new HierarchyIteratorBFS<>(this.root);
    }
}
