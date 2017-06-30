import java.util.Iterator;
import java.util.LinkedList;

public class HashTable<TKey, TValue> implements Iterable<KeyValue<TKey, TValue>> {

    private static final int INITIAL_CAPACITY = 16;

    private static final double LOAD_FACTOR = 0.75;

    private int size;

    private int capacity;

    private int maxItems;

    private LinkedList<KeyValue<TKey, TValue>>[] hashtable;

    public HashTable() {
        this(INITIAL_CAPACITY);
    }

    @SuppressWarnings(value = "unchecked")
    public HashTable(int capacity) {
        this.hashtable = new LinkedList[capacity];
        this.maxItems = (int) (capacity * LOAD_FACTOR);
        this.capacity = capacity;
    }

    public void add(TKey key, TValue value) {
        this.checkForGrow();

        int index = Math.abs(key.hashCode()) % this.hashtable.length;
        if (this.hashtable[index] == null) {
            this.hashtable[index] = new LinkedList<>();
        }

        LinkedList<KeyValue<TKey, TValue>> linkedList = this.hashtable[index];
        for (KeyValue<TKey, TValue> kvp : linkedList) {
            if (kvp.getKey().equals(key)) {
                throw new IllegalArgumentException(String.format("Key %s already exists", key));
            }
        }

        KeyValue<TKey, TValue> newKVP = new KeyValue<>(key, value);
        linkedList.addLast(newKVP);
        this.setSize(this.size() + 1);
    }

    public int size() {
        return this.size;
    }

    public void setSize(int size) {
        this.size = size;
    }

    public int capacity() {
        return this.capacity;
    }

    public boolean addOrReplace(TKey key, TValue value) {
        // add -> true
        // replace -> false
        int index = this.getIndex(key);

        if (this.hashtable[index] != null) {
            for (KeyValue<TKey, TValue> kvp : this.hashtable[index]) {
                if (kvp.getKey().equals(key)) {
                    kvp.setValue(value);
                    return false;
                }
            }
        }

        this.add(key, value);
        return true;
    }

    public TValue get(TKey key) {
        int index = this.getIndex(key);

        if (this.hashtable[index] != null) {
            for (KeyValue<TKey, TValue> kvp : this.hashtable[index]) {
                if (kvp.getKey().equals(key)) {
                    return kvp.getValue();
                }
            }
        }

        throw new IllegalArgumentException("Key is not presented in the hashtable");
    }

    public KeyValue<TKey, TValue> find(TKey key) {
        int index = this.getIndex(key);

        if (this.hashtable[index] != null) {
            for (KeyValue<TKey, TValue> kvp : this.hashtable[index]) {
                if (kvp.getKey().equals(key)) {
                    return kvp;
                }
            }
        }

        return null;
    }

    public boolean containsKey(TKey key) {
        int index = Math.abs(key.hashCode()) % this.hashtable.length;

        if (this.hashtable[index] != null) {
            LinkedList<KeyValue<TKey, TValue>> linkedList = this.hashtable[index];
            for (KeyValue<TKey, TValue> kvp : linkedList) {
                if (kvp.getKey().equals(key)) {
                    return true;
                }
            }
        }

        return false;
    }

    public boolean remove(TKey key) {
        int index = this.getIndex(key);

        LinkedList<KeyValue<TKey, TValue>> linkedList = this.hashtable[index];
        if (linkedList != null) {
            for (KeyValue<TKey,TValue> kvp : linkedList) {
                if (kvp.getKey().equals(key)) {
                    linkedList.remove(kvp);
                    this.setSize(this.size() - 1);
                    return true;
                }
            }
        }

        return false;
    }

    @SuppressWarnings(value = "unchecked")
    public void clear() {
        this.setSize(0);
        this.capacity = INITIAL_CAPACITY;
        this.hashtable = new LinkedList[capacity];
    }

    public Iterable<TKey> keys() {
        throw new UnsupportedOperationException();
    }

    public Iterable<TValue> values() {
        throw new UnsupportedOperationException();
    }

    @Override
    public Iterator<KeyValue<TKey, TValue>> iterator() {
        LinkedList<KeyValue<TKey, TValue>> hashTableIterator = new LinkedList<>();

        for (LinkedList<KeyValue<TKey, TValue>> linkedList : this.hashtable) {
            if (linkedList != null) {
                for (KeyValue<TKey, TValue> kvp : linkedList) {
                    hashTableIterator.add(kvp);
                }
            }
        }

        return hashTableIterator.iterator();
    }

    private void checkForGrow() {
        if (this.size() >= this.maxItems) {
            this.grow();
        }
    }

    @SuppressWarnings(value = "unchecked")
    private void grow() {
        this.capacity *= 2;
        this.maxItems = (int) (this.capacity * LOAD_FACTOR);

        HashTable<TKey, TValue> newHashtable = new HashTable<>(capacity);

        for (KeyValue<TKey, TValue> kvp : this) {
            newHashtable.add(kvp.getKey(), kvp.getValue());
        }

        this.hashtable = newHashtable.hashtable;
    }

    private int getIndex(TKey key) {
        return Math.abs(key.hashCode()) % this.hashtable.length;
    }
}
