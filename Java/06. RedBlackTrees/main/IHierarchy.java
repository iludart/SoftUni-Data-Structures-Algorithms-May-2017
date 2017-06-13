package main;

public interface IHierarchy<T> extends Iterable<T> {

    int getCount();

    void add(T element, T child);

    void remove(T element);

    Iterable<T> getChildren(T element);
    
    T getParent(T element);

    boolean contains(T element);

    Iterable<T> getCommonElements(IHierarchy<T> other);
}
