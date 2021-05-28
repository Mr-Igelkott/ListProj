namespace List
{
    public class DNode
    {
        public int Value { get; set; }
        public DNode Next { get; set; }
        public DNode Previous { get; set; }

        public DNode(int value)
        {
            Value = value;
            Next = null;
            Previous = null;
        }

        public DNode(int value, DNode prevNode)
        {
            this.Value = value;
            this.Previous = prevNode;
            prevNode.Next = this;
        }
    }
}
