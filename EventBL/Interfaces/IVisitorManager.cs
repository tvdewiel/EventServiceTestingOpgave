
namespace EventBL.Interfaces
{
    public interface IVisitorManager
    {
        bool ExistsVisitor(int id);
        IReadOnlyList<Visitor> GetAllVisitors();
        Visitor GetVisitor(int id);
        Visitor RegisterVisitor(Visitor visitor);
        void SubscribeVisitor(Visitor visitor);
        void UnsubscribeVisitor(Visitor visitor);
        void UpdateVisitor(Visitor visitor);
    }
}