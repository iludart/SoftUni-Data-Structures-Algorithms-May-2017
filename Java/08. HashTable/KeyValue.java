import java.util.Objects;

public class KeyValue<TKey, TValue> {

    private TKey key;
    private TValue value;

    public KeyValue(TKey key, TValue value) {
        this.setKey(key);
        this.setValue(value);
    }

    public TKey getKey() {
        return this.key;
    }

    public void setKey(TKey key) {
        this.key = key;
    }

    public TValue getValue() {
        return this.value;
    }

    public void setValue(TValue value) {
        this.value = value;
    }

    @Override
    public int hashCode() {
        return this.combineHashCodes(this.getKey().hashCode(), this.getValue().hashCode());
    }

    @Override
    public boolean equals(Object obj) {
        KeyValue<TKey, TValue> element = (KeyValue<TKey, TValue>) obj;
        return Objects.equals(this.getKey(), element.getKey()) && Objects.equals(this.getValue(), element.getValue());
    }

    @Override
    public String toString() {
        return String.format("%s -> %s", this.getKey(), this.getValue());
    }

    private int combineHashCodes(int h1, int h2) {
        return ((h1 << 5) + h1) ^ h2;
    }
}