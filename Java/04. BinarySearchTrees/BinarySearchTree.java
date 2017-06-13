import java.util.Deque;
import java.util.LinkedList;
import java.util.function.Consumer;

public class BinarySearchTree<T extends Comparable<T>> {
    private Node root;
    private int nodesCount;

    public BinarySearchTree() {
    }

    private BinarySearchTree(Node root) {
        this.preOrderCopy(root);
    }

    private void preOrderCopy(Node node) {
        if (node == null) {
            return;
        }

        this.insert(node.value);
        this.preOrderCopy(node.left);
        this.preOrderCopy(node.right);
    }

    public Node getRoot() {
        return this.root;
    }

    public int getNodesCount() {
        return this.nodesCount;
    }

    public void insert(T value) {
        this.nodesCount++;

        if (this.root == null) {
            this.root = new Node(value);
            return;
        }

        Node parent = null;
        Node child = this.root;

        while (child != null) {
            if (value.compareTo(child.value) > 0) {
                parent = child;
                parent.childrenCount++;
                child = child.right;
            } else if (value.compareTo(child.value) < 0) {
                parent = child;
                parent.childrenCount++;
                child = child.left;
            } else {
                this.nodesCount--;
                return;
            }
        }

        Node newNode = new Node(value);
        if (parent.value.compareTo(value) > 0) {
            parent.left = newNode;
        } else {
            parent.right = newNode;
        }
    }

    public void insert1(T value) {
        this.nodesCount++;

        if (this.root == null) {
            this.root = new Node(value);
        } else {
            this.insertRecursive(null, this.root, value);
        }
    }

    private void insertRecursive(Node parent, Node node, T value) {
        if (node == null) {
            Node newNode = new Node(value);

            if (parent.value.compareTo(value) > 0) {
                parent.left = newNode;
            } else {
                parent.right = newNode;
            }

            return;
        }

        if (node.value.compareTo(value) > 0) {
            node.childrenCount++;
            insertRecursive(node, node.left, value);
        } else if (node.value.compareTo(value) < 0) {
            node.childrenCount++;
            insertRecursive(node, node.right, value);
        } else {
            this.nodesCount--;
            return;
        }
    }

    public boolean contains(T value) {
        Node current = this.root;
        while (current != null) {
            if (value.compareTo(current.value) < 0) {
                current = current.left;
            } else if (value.compareTo(current.value) > 0) {
                current = current.right;
            } else {
                break;
            }
        }

        return current != null;
    }

    public BinarySearchTree<T> search(T item) {
        Node current = this.root;
        while (current != null) {
            if (item.compareTo(current.value) < 0) {
                current = current.left;
            } else if (item.compareTo(current.value) > 0) {
                current = current.right;
            } else {
                break;
            }
        }

        return new BinarySearchTree<>(current);
    }

    public void eachInOrder(Consumer<T> consumer) {
        this.eachInOrder(this.root, consumer);
    }

    private void eachInOrder(Node node, Consumer<T> consumer) {
        if (node == null) {
            return;
        }

        this.eachInOrder(node.left, consumer);
        consumer.accept(node.value);
        this.eachInOrder(node.right, consumer);
    }

    public Iterable<T> range(T from, T to) {
        Deque<T> queue = new LinkedList<>();
        this.range(this.root, queue, from, to);
        return queue;
    }

    private void range(Node node, Deque<T> queue, T startRange, T endRange) {
        if (node == null) {
            return;
        }

        int compareStart = startRange.compareTo(node.value);
        int compareEnd = endRange.compareTo(node.value);
        if (compareStart < 0) {
            this.range(node.left, queue, startRange, endRange);
        }
        if (compareStart <= 0 && compareEnd >= 0) {
            queue.addLast(node.value);
        }
        if (compareEnd > 0) {
            this.range(node.right, queue, startRange, endRange);
        }
    }

    private T minValue(Node root) {
        T minv = root.value;
        while (root.left != null) {
            minv = root.left.value;
            root = root.left;
        }

        return minv;
    }

    public void deleteMin() {
        if (this.root == null) {
            throw new IllegalArgumentException("Tree is empty!");
        }

        Node min = this.root;
        Node parent = null;

        while (min.left != null) {
            parent = min;
            parent.childrenCount--;
            min = min.left;
        }

        if (parent == null) {
            this.root = this.root.right;
        } else {
            parent.left = min.right;
        }

        this.nodesCount--;
    }

    public void deleteMax() {
        if (this.root == null) {
            throw new IllegalArgumentException("Tree is empty!");
        }

        this.nodesCount--;

        // REMOVE MAX
        Node parent = null;
        Node child = this.root;
        while (child.right != null) {
            parent = child;
            parent.childrenCount--;
            child = child.right;
        }


        if (parent == null) {
            this.root = this.root.left;
        } else {
            parent.right = child.left;
        }
    }

    public void deleteMax1() {
        if (this.root != null) {
            this.root = deleteRecursive(this.root);
        } else {
            throw new IllegalArgumentException("Tree is empty!");
        }
    }

    private Node deleteRecursive(Node node) {
        if (node.right == null) {
            return node.left;
        }

        node.right = deleteRecursive(node.right);

        return node;
    }

    public T ceil(T element) {
        return ceil(this.root, element);
    }

    private T ceil(Node node, T element) {
        if (node == null) {
            return null;
        }

        if (node.value.compareTo(element) == 0) {
            return node.value;
        } else if (node.value.compareTo(element) < 0) {
            return ceil(node.getRight(), element);
        }

        T ceil = ceil(node.left, element);
        if (ceil == null) {
            return node.value;
        }

        return ceil;
    }

    public T floor(T element) {
        throw new UnsupportedOperationException();
    }

    public void delete(T key) {
        throw new UnsupportedOperationException();
    }

    public int rank(T item) {
        return this.getRankOfT(this.root, item);
    }

    private int getRankOfT(Node node, T item) {
        if (node == null) {
            return 0;
        }

        if (node.value.compareTo(item) > 0) {
            return getRankOfT(node.left, item);
        } else if (node.value.compareTo(item) < 0) {
            return 1 + this.getChildrenCount(node.left) + getRankOfT(node.getRight(), item);
        }

        return this.getChildrenCount(node.left);
    }


    private int getChildrenCount(Node node) {
        if (node == null) {
            return 0;
        }

        return node.childrenCount;
    }

    public T select(int n) {
        throw new UnsupportedOperationException();
    }

    class Node {
        private T value;
        private Node left;
        private Node right;

        private int childrenCount;

        public Node(T value) {
            this.value = value;
            this.childrenCount = 1;
        }

        public T getValue() {
            return this.value;
        }

        public void setValue(T value) {
            this.value = value;
        }

        public Node getLeft() {
            return this.left;
        }

        public void setLeft(Node left) {
            this.left = left;
        }

        public Node getRight() {
            return this.right;
        }

        public void setRight(Node right) {
            this.right = right;
        }

        @Override
        public String toString() {
            return this.value + "";
        }
    }
}

